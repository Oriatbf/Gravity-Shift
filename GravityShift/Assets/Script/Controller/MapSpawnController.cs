using System;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class MapSpawnController : Singleton<MapSpawnController>
{
    [SerializeField] private List<CaveDotweenTest> maps;
    [SerializeField] private Transform spawnTrans;
    [SerializeField] private bool singleMap = false;
    private int curMapIndex = 0;
    private bool isLastMap = false;
    private MapMoving mapMoving;
    private void Awake()
    {
        mapMoving = GetComponent<MapMoving>();
    }
    
    public void StopMapMoving() => mapMoving.StopMoving();

    [Button]
    public void SpawnMap()
    {
        CaveDotweenTest curMap = null;
        if (singleMap) curMap = Instantiate(maps[0],spawnTrans.position,maps[curMapIndex].transform.rotation,transform);
        else
        {
            if (curMapIndex >= maps.Count) return;
            curMap = Instantiate(maps[curMapIndex],spawnTrans.position,maps[curMapIndex].transform.rotation,transform);
            curMapIndex+=1;
            if (curMapIndex >= maps.Count) isLastMap = true;

        }
        spawnTrans = curMap?.spawnTrans;
    }

    private void Update()
    {
        if (isLastMap)
        {
            if(spawnTrans.position.z<=2.5f)
            {
                Debug.Log("맵 끝");
                isLastMap=false;
                SettingController.Inst.EndingUI(true);
            }
        }
    }
}
