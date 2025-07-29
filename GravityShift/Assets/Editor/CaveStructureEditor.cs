using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CaveStructureEditor : EditorWindow
{
    // 파라미터 변수들
    private int x = 5;  // 가로 길이
    private int y = 5;   // 높이
    private int z = 20;  // 세로 길이
    private GameObject targetObject;
    private float size = 1f;
    private int holeCount = 84;
    
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
    
    // 랜덤 90도 회전을 생성하는 함수
    private Quaternion GetRandomRotation()
    {
        // 각 축에서 0, 90, 180, 270도 중 랜덤 선택
        float xRotation = Random.Range(0, 4) * 90f;
        float yRotation = Random.Range(0, 4) * 90f;
        float zRotation = Random.Range(0, 4) * 90f;
        
        return Quaternion.Euler(xRotation, yRotation, zRotation);
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
        
        // Z축 레이어별로 벽 위치 생성
        Dictionary<int, List<Vector3>> layerPositions = new Dictionary<int, List<Vector3>>();
        
        for (int k = 0; k < z; k++)
        {
            layerPositions[k] = new List<Vector3>();
            
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    // X, Y축 가장자리에만 벽 생성
                    bool isEdge = (i == 0 || i == x - 1) || (j == 0 || j == y - 1);
                    
                    if (isEdge)
                    {
                        Vector3 position = new Vector3(i * size, j * size, k * size);
                        layerPositions[k].Add(position);
                    }
                }
            }
        }
        
        // 연결성을 유지하며 홀 생성
        List<Vector3> finalPositions = CreateHolesWithConnectivity(layerPositions);
        
        // 실제 오브젝트 생성 (랜덤 회전 적용)
        foreach (Vector3 position in finalPositions)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(targetObject);
            instance.transform.position = position;
            instance.transform.rotation = GetRandomRotation(); // 랜덤 90도 회전 적용
            instance.transform.SetParent(parentObject.transform);
            generatedObjects.Add(instance);
        }
        
        // 부모 오브젝트도 관리 리스트에 추가
        generatedObjects.Add(parentObject);
        
        // 씬 저장 알림
        EditorUtility.SetDirty(parentObject);
        
        int totalWalls = 0;
        foreach (var layer in layerPositions.Values)
            totalWalls += layer.Count;
        
        Debug.Log($"Cave structure generated! Objects: {finalPositions.Count}/{totalWalls}, Holes: {totalWalls - finalPositions.Count}");
    }
    
    // 연결성을 유지하며 홀을 생성하는 함수
    private List<Vector3> CreateHolesWithConnectivity(Dictionary<int, List<Vector3>> layerPositions)
    {
        List<Vector3> result = new List<Vector3>();
        Dictionary<int, List<Vector3>> remainingPositions = new Dictionary<int, List<Vector3>>();
        
        // 각 레이어의 위치들을 복사
        foreach (var kvp in layerPositions)
        {
            remainingPositions[kvp.Key] = new List<Vector3>(kvp.Value);
        }
        
        // 각 레이어에서 최소 1개의 연결점 보장
        List<Vector3> connectionPoints = new List<Vector3>();
        
        for (int k = 0; k < z; k++)
        {
            if (remainingPositions[k].Count > 0)
            {
                // 현재 레이어에서 랜덤하게 연결점 선택
                Vector3 connectionPoint = remainingPositions[k][Random.Range(0, remainingPositions[k].Count)];
                connectionPoints.Add(connectionPoint);
                
                // 다음 레이어와의 연결성 확인 및 보장
                if (k < z - 1 && remainingPositions[k + 1].Count > 0)
                {
                    // 현재 연결점과 가장 가까운 다음 레이어의 점을 찾아서 보존
                    Vector3 closestNextPoint = FindClosestPoint(connectionPoint, remainingPositions[k + 1]);
                    if (!connectionPoints.Contains(closestNextPoint))
                    {
                        connectionPoints.Add(closestNextPoint);
                    }
                }
            }
        }
        
        // 연결점들을 결과에 추가 (삭제되지 않도록)
        foreach (Vector3 point in connectionPoints)
        {
            result.Add(point);
            // 해당 레이어에서 연결점 제거 (중복 방지)
            int layerIndex = Mathf.RoundToInt(point.z / size);
            remainingPositions[layerIndex].Remove(point);
        }
        
        // 나머지 블럭들에서 홀 생성
        List<Vector3> allRemainingPositions = new List<Vector3>();
        foreach (var layer in remainingPositions.Values)
        {
            allRemainingPositions.AddRange(layer);
        }
        
        // 홀 개수 계산 (연결점을 제외한 나머지에서)
        int remainingHoleCount = Mathf.Min(holeCount, allRemainingPositions.Count);
        
        // 랜덤하게 홀 생성
        for (int i = 0; i < remainingHoleCount; i++)
        {
            if (allRemainingPositions.Count > 0)
            {
                int randomIndex = Random.Range(0, allRemainingPositions.Count);
                allRemainingPositions.RemoveAt(randomIndex);
            }
        }
        
        // 남은 블럭들을 결과에 추가
        result.AddRange(allRemainingPositions);
        
        return result;
    }
    
    // 가장 가까운 점을 찾는 함수
    private Vector3 FindClosestPoint(Vector3 target, List<Vector3> points)
    {
        Vector3 closest = points[0];
        float minDistance = Vector3.Distance(target, closest);
        
        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(target, point);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = point;
            }
        }
        
        return closest;
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