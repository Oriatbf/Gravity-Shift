using System;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class MapSpawnController : Singleton<MapSpawnController>
{
    [SerializeField] private List<MapListSO> mapListSOs = new List<MapListSO>();
    [SerializeField]private MapListSO curListSO;
    [SerializeField]private Transform spawnTrans;
    private int curOrderIndex = 0;
    private bool isLastMap = false;
    private MapMoving mapMoving;
    private int randomMapIndex;
    private int infinityCount = 0;
    private void Awake()
    {
        mapMoving = GetComponent<MapMoving>();
    }

    private void Start()
    {
        int index = 0; 
        if(DataManager.Inst!=null)
            index = DataManager.Inst.Data.curStage;
        curListSO=mapListSOs[index];
        SpawnMap();
        
    }

    public void StopMapMoving() => mapMoving.StopMoving();

    [Button]
    public void SpawnMap()
    {
        MapEffection curMap = null;
        if (curListSO.isSingleMap) 
            curMap = Instantiate(curListSO.MapEffections[0],spawnTrans.position,curListSO.MapEffections[0].transform.rotation,transform);
        else if (curListSO.isInfinity)
        {
            isLastMap = false;
            randomMapIndex = UnityEngine.Random.Range(0, curListSO.MapEffections.Count);
            var map = curListSO.MapEffections[randomMapIndex];
            curMap = Instantiate(map, spawnTrans.position, map.transform.rotation, transform);
            infinityCount+=1;
            if (infinityCount >= 4)
            {
                mapMoving.SpeedUp();
                infinityCount = 0;
            }
        }
        else
        {
            if (curOrderIndex >= curListSO.MapEffections.Count) return;
            var map = curListSO.MapEffections[curOrderIndex];
            curMap = Instantiate(map,spawnTrans.position,map.transform.rotation,transform);
            curOrderIndex+=1;
            if (curOrderIndex >= curListSO.MapEffections.Count) isLastMap = true;

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
