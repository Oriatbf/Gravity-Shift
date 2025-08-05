using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PrefabGridGenerator : EditorWindow
{
    [Header("Grid Settings")]
    public int width = 5;           // 가로길이
    public int height = 5;          // 높이
    public int depth = 10;          // Z축 깊이
    public GameObject prefab;       // 생성할 프리팹
    public int holeCount = 0;       // 제거할 프리팹 개수
    public float gap = 1.0f;        // 프리팹 간의 거리
    
    [Header("Generation Settings")]
    public Transform parentObject;  // 부모 오브젝트 (선택사항)
    public string generatedObjectName = "GeneratedGrid";
    
    private Vector2 scrollPosition;
    
    [MenuItem("Tools/Prefab Grid Generator")]
    public static void ShowWindow()
    {
        GetWindow<PrefabGridGenerator>("Prefab Grid Generator");
    }
    
    void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        GUILayout.Label("Prefab Grid Generator", EditorStyles.boldLabel);
        GUILayout.Space(10);
        
        // Grid Settings
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        width = EditorGUILayout.IntField("가로길이 (Width)", width);
        height = EditorGUILayout.IntField("높이 (Height)", height);
        depth = EditorGUILayout.IntField("Z축 깊이 (Depth)", depth);
        
        GUILayout.Space(5);
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        
        GUILayout.Space(5);
        gap = EditorGUILayout.FloatField("Gap (간격)", gap);
        holeCount = EditorGUILayout.IntField("Hole Count (구멍 개수)", holeCount);
        
        GUILayout.Space(10);
        
        // Generation Settings
        GUILayout.Label("Generation Settings", EditorStyles.boldLabel);
        parentObject = (Transform)EditorGUILayout.ObjectField("부모 오브젝트 (선택사항)", parentObject, typeof(Transform), true);
        generatedObjectName = EditorGUILayout.TextField("생성된 오브젝트 이름", generatedObjectName);
        
        GUILayout.Space(10);
        
        // Validation
        bool canGenerate = prefab != null && width > 0 && height > 0 && depth > 0;
        
        if (!canGenerate)
        {
            EditorGUILayout.HelpBox("프리팹을 설정하고 모든 크기 값이 0보다 커야 합니다.", MessageType.Warning);
        }
        
        if (holeCount >= (width * height * depth))
        {
            EditorGUILayout.HelpBox("Hole Count는 총 프리팹 개수보다 작아야 합니다.", MessageType.Warning);
            canGenerate = false;
        }
        
        // Info
        if (canGenerate)
        {
            int totalPrefabs = width * height * depth;
            int finalPrefabCount = totalPrefabs - holeCount;
            EditorGUILayout.HelpBox($"총 {totalPrefabs}개의 프리팹이 생성되고, {holeCount}개가 제거되어 최종 {finalPrefabCount}개가 남습니다.", MessageType.Info);
        }
        
        GUILayout.Space(10);
        
        // Buttons
        GUI.enabled = canGenerate;
        if (GUILayout.Button("프리팹 그리드 생성", GUILayout.Height(30)))
        {
            GeneratePrefabGrid();
        }
        GUI.enabled = true;
        
        if (GUILayout.Button("기존 그리드 삭제", GUILayout.Height(25)))
        {
            ClearExistingGrid();
        }
        
        EditorGUILayout.EndScrollView();
    }
    
    void GeneratePrefabGrid()
    {
        if (prefab == null) return;
        
        // 기존 그리드 삭제
        ClearExistingGrid();
        
        // 부모 오브젝트 생성
        GameObject gridParent = new GameObject(generatedObjectName);
        if (parentObject != null)
        {
            gridParent.transform.SetParent(parentObject);
        }
        
        // Undo 등록
        Undo.RegisterCreatedObjectUndo(gridParent, "Generate Prefab Grid");
        
        List<Vector3> positions = new List<Vector3>();
        List<GameObject> createdObjects = new List<GameObject>();
        
        // 모든 위치 계산
        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3 position = new Vector3(x * gap, y * gap, z * gap);
                    positions.Add(position);
                }
            }
        }
        
        // 프리팹 생성
        for (int i = 0; i < positions.Count; i++)
        {
            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            instance.transform.position = positions[i];
            instance.transform.SetParent(gridParent.transform);
            instance.name = $"{prefab.name}_{i:000}";
            
            createdObjects.Add(instance);
            Undo.RegisterCreatedObjectUndo(instance, "Generate Prefab Grid");
        }
        
        // 홀 생성 (랜덤하게 프리팹 제거)
        if (holeCount > 0)
        {
            CreateHoles(createdObjects);
        }
        
        // 생성된 그리드를 선택
        Selection.activeGameObject = gridParent;
        
        Debug.Log($"프리팹 그리드 생성 완료! 총 {createdObjects.Count - holeCount}개의 프리팹이 생성되었습니다.");
    }
    
    void CreateHoles(List<GameObject> objects)
    {
        List<int> indicesToRemove = new List<int>();
        
        // 랜덤하게 제거할 인덱스 선택
        while (indicesToRemove.Count < holeCount && indicesToRemove.Count < objects.Count)
        {
            int randomIndex = Random.Range(0, objects.Count);
            if (!indicesToRemove.Contains(randomIndex))
            {
                indicesToRemove.Add(randomIndex);
            }
        }
        
        // 인덱스를 내림차순으로 정렬 (뒤에서부터 제거하기 위해)
        indicesToRemove.Sort((a, b) => b.CompareTo(a));
        
        // 선택된 프리팹들 제거
        foreach (int index in indicesToRemove)
        {
            if (index < objects.Count && objects[index] != null)
            {
                Undo.DestroyObjectImmediate(objects[index]);
            }
        }
    }
    
    void ClearExistingGrid()
    {
        GameObject existingGrid = GameObject.Find(generatedObjectName);
        if (existingGrid != null)
        {
            if (EditorUtility.DisplayDialog("기존 그리드 삭제", 
                $"'{generatedObjectName}' 오브젝트가 이미 존재합니다. 삭제하시겠습니까?", 
                "예", "아니오"))
            {
                Undo.DestroyObjectImmediate(existingGrid);
            }
        }
    }
    
    void OnValidate()
    {
        width = Mathf.Max(1, width);
        height = Mathf.Max(1, height);
        depth = Mathf.Max(1, depth);
        holeCount = Mathf.Max(0, holeCount);
        gap = Mathf.Max(0.1f, gap);
    }
}