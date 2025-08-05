using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DonutPrefabGridGenerator : EditorWindow
{
    [Header("Square Donut Settings")]
    public int outerSize = 5;        // 도넛 외부 크기 (한 변의 길이)
    public int innerSize = 2;        // 도넛 내부 크기 (구멍 크기)
    public int depth = 5;            // 도넛의 두께 (Z축 방향)
    public GameObject prefab;        // 생성할 프리팹
    public int holeCount = 0;        // 제거할 프리팹 개수
    public float gap = 1.0f;         // 프리팹 간의 거리
    
    [Header("Rotation Settings")]
    public Vector3 frontFaceRotation = new Vector3(0, 0, 0);      // 앞면 회전 (Z=0)
    public Vector3 backFaceRotation = new Vector3(0, 180, 0);     // 뒷면 회전 (Z=depth-1)
    public Vector3 outerWallRotation = new Vector3(0, 0, 90);     // 외벽 회전
    public Vector3 innerWallRotation = new Vector3(0, 180, 90);   // 내벽 회전
    
    [Header("Generation Settings")]
    public Transform parentObject;   // 부모 오브젝트 (선택사항)
    public string generatedObjectName = "DonutGrid";
    
    private Vector2 scrollPosition;
    
    [MenuItem("Tools/Donut Prefab Grid Generator")]
    public static void ShowWindow()
    {
        GetWindow<DonutPrefabGridGenerator>("Donut Prefab Grid Generator");
    }
    
    void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        GUILayout.Label("Square Donut Prefab Grid Generator", EditorStyles.boldLabel);
        GUILayout.Space(10);
        
        // Square Donut Settings
        GUILayout.Label("Square Donut Settings", EditorStyles.boldLabel);
        outerSize = EditorGUILayout.IntField("외부 크기 (Outer Size)", outerSize);
        innerSize = EditorGUILayout.IntField("내부 크기 (Inner Size)", innerSize);
        depth = EditorGUILayout.IntField("두께 (Depth)", depth);
        
        GUILayout.Space(5);
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        gap = EditorGUILayout.FloatField("Gap (간격)", gap);
        holeCount = EditorGUILayout.IntField("Hole Count (구멍 개수)", holeCount);
        
        GUILayout.Space(10);
        
        // Rotation Settings
        GUILayout.Label("Rotation Settings", EditorStyles.boldLabel);
        frontFaceRotation = EditorGUILayout.Vector3Field("앞면 회전", frontFaceRotation);
        backFaceRotation = EditorGUILayout.Vector3Field("뒷면 회전", backFaceRotation);
        outerWallRotation = EditorGUILayout.Vector3Field("외벽 회전", outerWallRotation);
        innerWallRotation = EditorGUILayout.Vector3Field("내벽 회전", innerWallRotation);
        
        GUILayout.Space(10);
        
        // Generation Settings
        GUILayout.Label("Generation Settings", EditorStyles.boldLabel);
        parentObject = (Transform)EditorGUILayout.ObjectField("부모 오브젝트 (선택사항)", parentObject, typeof(Transform), true);
        generatedObjectName = EditorGUILayout.TextField("생성된 오브젝트 이름", generatedObjectName);
        
        GUILayout.Space(10);
        
        // Validation
        bool canGenerate = prefab != null && outerSize > 0 && depth > 0 && innerSize >= 0 && innerSize < outerSize;
        
        if (!canGenerate)
        {
            if (prefab == null)
                EditorGUILayout.HelpBox("프리팹을 설정해야 합니다.", MessageType.Warning);
            else if (outerSize <= 0 || depth <= 0)
                EditorGUILayout.HelpBox("외부 크기와 두께는 0보다 커야 합니다.", MessageType.Warning);
            else if (innerSize >= outerSize)
                EditorGUILayout.HelpBox("내부 크기는 외부 크기보다 작아야 합니다.", MessageType.Warning);
        }
        
        // HoleCount 검증
        if (canGenerate)
        {
            int estimatedCount = CalculateEstimatedPrefabCount();
            if (holeCount >= estimatedCount)
            {
                EditorGUILayout.HelpBox("Hole Count는 총 프리팹 개수보다 작아야 합니다.", MessageType.Warning);
                canGenerate = false;
            }
        }
        
        // Info
        if (canGenerate)
        {
            int estimatedCount = CalculateEstimatedPrefabCount();
            int finalPrefabCount = estimatedCount - holeCount;
            EditorGUILayout.HelpBox($"총 {estimatedCount}개의 프리팹이 생성되고, {holeCount}개가 제거되어 최종 {finalPrefabCount}개가 남습니다.", MessageType.Info);
        }
        
        GUILayout.Space(10);
        
        // Buttons
        GUI.enabled = canGenerate;
        if (GUILayout.Button("네모 도넛 그리드 생성", GUILayout.Height(30)))
        {
            GenerateSquareDonutGrid();
        }
        GUI.enabled = true;
        
        if (GUILayout.Button("기존 그리드 삭제", GUILayout.Height(25)))
        {
            ClearExistingGrid();
        }
        
        EditorGUILayout.EndScrollView();
    }
    
    int CalculateEstimatedPrefabCount()
    {
        int count = 0;
        int halfOuter = outerSize / 2;
        int halfInner = innerSize / 2;
        
        for (int z = 0; z < depth; z++)
        {
            for (int x = -halfOuter; x <= halfOuter; x++)
            {
                for (int y = -halfOuter; y <= halfOuter; y++)
                {
                    // 외부 사각형 안에 있고 내부 사각형(구멍) 밖에 있는지 확인
                    bool inOuterSquare = (Mathf.Abs(x) <= halfOuter && Mathf.Abs(y) <= halfOuter);
                    bool inInnerSquare = (Mathf.Abs(x) <= halfInner && Mathf.Abs(y) <= halfInner);
                    
                    if (inOuterSquare && !inInnerSquare)
                    {
                        // 앞면과 뒷면
                        if (z == 0 || z == depth - 1)
                        {
                            count++;
                        }
                        // 외벽과 내벽
                        else if (IsOnSquareEdge(x, y, halfOuter) || IsOnSquareEdge(x, y, halfInner))
                        {
                            count++;
                        }
                    }
                }
            }
        }
        
        return count;
    }
    
    bool IsOnSquareEdge(int x, int y, int halfSize)
    {
        return (Mathf.Abs(x) == halfSize || Mathf.Abs(y) == halfSize);
    }
    
    void GenerateSquareDonutGrid()
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
        Undo.RegisterCreatedObjectUndo(gridParent, "Generate Square Donut Grid");
        
        List<GameObject> createdObjects = new List<GameObject>();
        int objectIndex = 0;
        
        int halfOuter = outerSize / 2;
        int halfInner = innerSize / 2;
        
        // 네모 도넛 형태로 프리팹 생성
        for (int z = 0; z < depth; z++)
        {
            for (int x = -halfOuter; x <= halfOuter; x++)
            {
                for (int y = -halfOuter; y <= halfOuter; y++)
                {
                    Vector3 position = new Vector3(x * gap, y * gap, z * gap);
                    
                    // 외부 사각형 안에 있고 내부 사각형(구멍) 밖에 있는지 확인
                    bool inOuterSquare = (Mathf.Abs(x) <= halfOuter && Mathf.Abs(y) <= halfOuter);
                    bool inInnerSquare = (Mathf.Abs(x) <= halfInner && Mathf.Abs(y) <= halfInner);
                    
                    if (inOuterSquare && !inInnerSquare)
                    {
                        SquareDonutFaceType faceType = GetSquareFaceType(x, y, z, halfOuter, halfInner);
                        
                        // 해당 면에 프리팹이 필요한지 확인
                        if (ShouldPlaceSquarePrefab(faceType, x, y, z, halfOuter, halfInner))
                        {
                            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                            instance.transform.position = position;
                            instance.transform.SetParent(gridParent.transform);
                            
                            // 면에 따른 회전 적용
                            ApplySquareRotationForFace(instance, faceType, x, y);
                            
                            instance.name = $"{prefab.name}_{GetSquareFaceTypeName(faceType)}_{objectIndex:000}";
                            
                            createdObjects.Add(instance);
                            Undo.RegisterCreatedObjectUndo(instance, "Generate Square Donut Grid");
                            objectIndex++;
                        }
                    }
                }
            }
        }
        
        // 홀 생성 (랜덤하게 프리팹 제거)
        if (holeCount > 0)
        {
            //CreateHoles(createdObjects);
        }
        
        // 생성된 그리드를 선택
        Selection.activeGameObject = gridParent;
        
        Debug.Log($"네모 도넛 그리드 생성 완료! 총 {createdObjects.Count - holeCount}개의 프리팹이 생성되었습니다.");
    }
    
    enum SquareDonutFaceType
    {
        Front,      // 앞면 (Z=0)
        Back,       // 뒷면 (Z=depth-1)
        OuterWall,  // 외벽
        InnerWall   // 내벽
    }
    
    SquareDonutFaceType GetSquareFaceType(int x, int y, int z, int halfOuter, int halfInner)
    {
        // 앞면과 뒷면 판단
        if (z == 0) return SquareDonutFaceType.Front;
        if (z == depth - 1) return SquareDonutFaceType.Back;
        
        // 외벽과 내벽 판단
        bool onOuterEdge = (Mathf.Abs(x) == halfOuter || Mathf.Abs(y) == halfOuter);
        bool onInnerEdge = (Mathf.Abs(x) == halfInner || Mathf.Abs(y) == halfInner);
        
        if (onOuterEdge) return SquareDonutFaceType.OuterWall;
        if (onInnerEdge) return SquareDonutFaceType.InnerWall;
        
        return SquareDonutFaceType.OuterWall; // 기본값
    }
    
    bool ShouldPlaceSquarePrefab(SquareDonutFaceType faceType, int x, int y, int z, int halfOuter, int halfInner)
    {
        switch (faceType)
        {
            case SquareDonutFaceType.Front:
            case SquareDonutFaceType.Back:
                return true; // 앞면과 뒷면은 모든 도넛 영역에 배치
                
            case SquareDonutFaceType.OuterWall:
                // 외벽: 외부 사각형 가장자리에만 배치
                return (Mathf.Abs(x) == halfOuter || Mathf.Abs(y) == halfOuter);
                
            case SquareDonutFaceType.InnerWall:
                // 내벽: 내부 사각형 가장자리에만 배치
                return (Mathf.Abs(x) == halfInner || Mathf.Abs(y) == halfInner);
                
            default:
                return false;
        }
    }
    
    void ApplySquareRotationForFace(GameObject instance, SquareDonutFaceType faceType, int x, int y)
    {
        Vector3 rotation = Vector3.zero;
        
        switch (faceType)
        {
            case SquareDonutFaceType.Front:
                rotation = frontFaceRotation;
                break;
                
            case SquareDonutFaceType.Back:
                rotation = backFaceRotation;
                break;
                
            case SquareDonutFaceType.OuterWall:
                rotation = outerWallRotation;
                // 외벽의 경우 면 방향에 따른 추가 회전
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // 좌우 벽
                    rotation.y += (x > 0) ? 90 : -90;
                }
                else
                {
                    // 상하 벽
                    rotation.y += (y > 0) ? 0 : 180;
                }
                break;
                
            case SquareDonutFaceType.InnerWall:
                rotation = innerWallRotation;
                // 내벽의 경우 면 방향에 따른 추가 회전 (내부를 향하도록)
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // 좌우 벽
                    rotation.y += (x > 0) ? -90 : 90;
                }
                else
                {
                    // 상하 벽
                    rotation.y += (y > 0) ? 180 : 0;
                }
                break;
        }
        
        instance.transform.rotation = Quaternion.Euler(rotation);
    }
    
    string GetSquareFaceTypeName(SquareDonutFaceType faceType)
    {
        switch (faceType)
        {
            case SquareDonutFaceType.Front: return "Front";
            case SquareDonutFaceType.Back: return "Back";
            case SquareDonutFaceType.OuterWall: return "Outer";
            case SquareDonutFaceType.InnerWall: return "Inner";
            default: return "Unknown";
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
        outerSize = Mathf.Max(1, outerSize);
        innerSize = Mathf.Max(0, innerSize);
        depth = Mathf.Max(1, depth);
        holeCount = Mathf.Max(0, holeCount);
        gap = Mathf.Max(0.1f, gap);
        
        // 내부 크기가 외부 크기보다 크거나 같으면 조정
        if (innerSize >= outerSize)
        {
            innerSize = outerSize - 1;
        }
    }
}