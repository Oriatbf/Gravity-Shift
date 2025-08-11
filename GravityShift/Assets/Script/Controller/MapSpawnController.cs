using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class MapSpawnController : Singleton<MapSpawnController>
{
    [SerializeField] private List<CaveDotweenTest> maps;
    [SerializeField] private Transform spawnTrans;
    [SerializeField] private bool singleMap = false;
    private int curMapIndex = 0;

    [Button]
    public void SpawnMap()
    {
        CaveDotweenTest curMap = null;
        if (singleMap) curMap = Instantiate(maps[0],spawnTrans.position,maps[curMapIndex].transform.rotation,transform);
        else
        {
            if (curMapIndex >= maps.Count) return;
            curMap = Instantiate(maps[curMapIndex],spawnTrans.position,maps[curMapIndex].transform.rotation,transform);
            curMapIndex++;
        }
        spawnTrans = curMap?.spawnTrans;
    }
}
