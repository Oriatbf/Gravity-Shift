using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class MapSpawnController : Singleton<MapSpawnController>
{
    [SerializeField] private List<CaveDotweenTest> maps;
    [SerializeField] private Transform spawnTrans;

    [Button]
    public void SpawnMap()
    {
        
        Debug.Log(maps[0]);
        Debug.Log(spawnTrans);
        var _spawn =  Instantiate(maps[0],spawnTrans.position,Quaternion.identity,transform);
        spawnTrans = _spawn.spawnTrans;
    }
}
