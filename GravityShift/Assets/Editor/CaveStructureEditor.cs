using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CaveStructureEditor : EditorWindow
{
    // 파라미터 변수들
    private int x = 10;  // 가로 길이
    private int y = 5;   // 높이
    private int z = 10;  // 세로 길이
    private GameObject targetObject;
    private float size = 1f;
    private int holeCount = 5;
    
    // 생성된 오브젝트들을 관리하기 위한 리스트
    private List<GameObject> generatedObjects = new List<GameObject>();
    
    [MenuItem("Tools/Cave Structure Editor")]
    public static void ShowWindow()
    {
        GetWindow<CaveStructureEditor>("Cave Structure Editor");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Cave Structure Generator", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        
        // 크기 설정
        GUILayout.Label("Structure Size:", EditorStyles.label);
        x = EditorGUILayout.IntField("Width (X):", x);
        y = EditorGUILayout.IntField("Height (Y):", y);
        z = EditorGUILayout.IntField("Depth (Z):", z);
        
        EditorGUILayout.Space();
        
        // 오브젝트 및 크기 설정
        targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object:", targetObject, typeof(GameObject), false);
        size = EditorGUILayout.FloatField("Object Size (Gap):", size);
        
        EditorGUILayout.Space();
        
        // 홀 개수 설정
        holeCount = EditorGUILayout.IntField("Hole Count:", holeCount);
        
        EditorGUILayout.Space();
        
        // 생성 버튼
        if (GUILayout.Button("Generate Cave Structure"))
        {
            GenerateCaveStructure();
        }
        
        EditorGUILayout.Space();
        
        // 삭제 버튼
        if (GUILayout.Button("Clear Generated Objects"))
        {
            ClearGeneratedObjects();
        }
        
        EditorGUILayout.Space();
        
        // 정보 표시
        if (generatedObjects.Count > 0)
        {
            GUILayout.Label($"Generated Objects: {generatedObjects.Count}", EditorStyles.helpBox);
        }
    }
    
    private void GenerateCaveStructure()
    {
        // 유효성 검사
        if (targetObject == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a target object!", "OK");
            return;
        }
        
        if (x <= 0 || y <= 0 || z <= 0)
        {
            EditorUtility.DisplayDialog("Error", "All dimensions must be greater than 0!", "OK");
            return;
        }
        
        // 기존 오브젝트들 삭제
        ClearGeneratedObjects();
        
        // 부모 오브젝트 생성
        GameObject parentObject = new GameObject("Cave Structure");
        
        // Z축 방향으로 도넛처럼 뚫린 구조
        List<Vector3> wallPositions = new List<Vector3>();
        
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    // X, Y축 가장자리에만 벽 생성 (Z축은 모든 레이어에서 뚫림)
                    bool isEdge = (i == 0 || i == x - 1) || (j == 0 || j == y - 1);
                    
                    if (isEdge)
                    {
                        Vector3 position = new Vector3(i * size, j * size, k * size);
                        wallPositions.Add(position);
                    }
                }
            }
        }
        
        // 홀 생성 (랜덤으로 벽면 오브젝트 제거)
        List<Vector3> finalPositions = new List<Vector3>(wallPositions);
        int actualHoleCount = Mathf.Min(holeCount, wallPositions.Count);
        
        for (int i = 0; i < actualHoleCount; i++)
        {
            if (finalPositions.Count > 0)
            {
                int randomIndex = Random.Range(0, finalPositions.Count);
                finalPositions.RemoveAt(randomIndex);
            }
        }
        
        // 실제 오브젝트 생성
        foreach (Vector3 position in finalPositions)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(targetObject);
            instance.transform.position = position;
            instance.transform.SetParent(parentObject.transform);
            generatedObjects.Add(instance);
        }
        
        // 부모 오브젝트도 관리 리스트에 추가
        generatedObjects.Add(parentObject);
        
        // 씬 저장 알림
        EditorUtility.SetDirty(parentObject);
        
        Debug.Log($"Cave structure generated! Objects: {finalPositions.Count}, Holes: {actualHoleCount}");
    }
    
    private void ClearGeneratedObjects()
    {
        foreach (GameObject obj in generatedObjects)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }
        generatedObjects.Clear();
    }
    
    private void OnDestroy()
    {
        // 에디터 윈도우가 닫힐 때 정리
        // 실제로는 생성된 오브젝트들을 삭제하지 않습니다.
        // 필요시 주석 해제하여 자동 정리 가능
        // ClearGeneratedObjects();
    }
}