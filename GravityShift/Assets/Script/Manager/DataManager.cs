using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    public List<float> stageTimes = new List<float>();
    public int curStage = 1;
    public float sfxVolume = 0.5f,bgmVolume = 0.5f;
}
public class DataManager : SingletonDontDestroyOnLoad<DataManager>
{
    string path;
    public GameData Data;
    private string fileName = "GameData.json";


    protected override void Awake()
    {
        base.Awake();
        path = Path.Combine(Application.persistentDataPath, fileName);
        JsonLoad();
    }

    private void Start()
    {
        StickyKeysBlocker.Enable();
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
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            Data = JsonUtility.FromJson<GameData>(loadJson);
        }
    }

    public void JsonSave()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(path, json);
    }

    public void AddStageTime(float value)
    {
        Data.stageTimes.Add(value);
        JsonSave();
    }

    private void OnApplicationQuit() => JsonSave();
    private void OnApplicationFocus(bool focus) => JsonSave();
}
