using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VInspector;

[Serializable]
public class GameData
{
    public int curStage = 0;
    public float sfxVolume = 0.5f,bgmVolume = 0.5f;
    public List<StageData> StageDatas = new List<StageData>();
}

[Serializable]
public class StageData
{
    public bool isClear = false;
    public float finalTime = 0;
    public int coinCount = 0;
}
public class DataManager : SingletonDontDestroyOnLoad<DataManager>
{
    string path;
    public GameData Data;
    private string fileName = "GameData.json";
    public bool isOpening = true;


    protected override void Awake()
    {
        base.Awake();
        path = Path.Combine(Application.persistentDataPath, fileName);
        JsonLoad();
    }

    private void Start()
    {
        StickyKeysBlocker.Enable();
        if(Data.StageDatas == null || Data.StageDatas.Count<6)InstanceStageData();
    }

    public void SetStage(int stage)
    {
        Data.curStage = stage;
    }


    private void JsonLoad()
    {
        if (!File.Exists(path))
        {
            Data = new GameData();
            InstanceStageData();
            
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            Data = JsonUtility.FromJson<GameData>(loadJson);
        }
    }

    private void InstanceStageData()
    {
        for (int i = 0; i < 6; i++)
        {
            Data.StageDatas.Add(new StageData());
        }
    }

    public void JsonSave()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(path, json);
    }

    public void InitStageData(float time,int coinCount,bool isClear)
    {
        var stage = Data.StageDatas[Data.curStage];
        if(!stage.isClear)
            stage.isClear = isClear;
        if(time>stage.finalTime)
            stage.finalTime = time;
        if(coinCount>stage.coinCount)
            stage.coinCount = coinCount;
        JsonSave();
    }

    [Button]
    public void Reset()
    {
        if (File.Exists(path))
        {
            File.Delete(path); // 파일 삭제
        }

        // 새 데이터로 초기화
        Data = new GameData();
        InstanceStageData();
        JsonSave();
    }

    private void OnApplicationQuit() => JsonSave();
    private void OnApplicationFocus(bool focus) => JsonSave();
}
