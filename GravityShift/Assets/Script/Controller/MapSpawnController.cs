using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class MapSpawnController : MonoBehaviour
{
    [SerializeField] private List<CaveDotweenTest> maps;
    [SerializeField] private Transform spawnTrans;

    [Button]
    public void SpawnMap()
    {
        var _spawn =  Instantiate(maps[0],spawnTrans.position,Quaternion.identity,transform);
        spawnTrans = _spawn.spawnTrans;
    }
}
