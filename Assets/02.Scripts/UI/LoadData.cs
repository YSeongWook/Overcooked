using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadData : Singleton<LoadData>
{
    public OptionData optionData;

    protected override void Awake()
    {
        base.Awake();
        LoadOptionDataFromJson();
        UIManager.Instance.JsonUILoad();
        UIManager.Instance.SetResolution();
    }

    private void Start()
    {      
        
    }

    [ContextMenu("To Json Data")]
    public void SaveOptionDataToJson()
    {
        optionData.saveWindowMode = UIManager.Instance.windowScreen;
        optionData.saveResolutionNum = UIManager.Instance.resolutionArrNum;
        optionData.saveBgmVolume = SoundManager.Instance.volumeBGM;
        optionData.saveEffectVolume = SoundManager.Instance.volumeEffect;
        string jsonData = JsonUtility.ToJson(optionData, true);
        //string path = Path.Combine(Application.dataPath, "optionData.json");    -> 유니티 
        string path = GetFilePath();
        File.WriteAllText(path, jsonData);
        Debug.Log("저장");
    }

    [ContextMenu("From Json Data")]
    public void LoadOptionDataFromJson()
    {
        //string path = Path.Combine(Application.dataPath, "optionData.json");      -> 유니티 
        string path = GetFilePath();
        if (File.Exists(path))
        {           
            Debug.Log("불러오기 성공");
            string jsonData = File.ReadAllText(path);
            optionData = JsonUtility.FromJson<OptionData>(jsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");       
            optionData.saveWindowMode = true;
            optionData.saveResolutionNum = 2;
            optionData.saveBgmVolume = 0.2f;
            optionData.saveEffectVolume = 0.2f;
            UIManager.Instance.JsonUILoad();
            UIManager.Instance.SetResolution();
        }      
    }

    private string GetFilePath()
    {
        string fileName = "optionData.json";
        if (Application.platform == RuntimePlatform.Android)
        {
            return Path.Combine(Application.persistentDataPath, fileName);
        }
        else // PC나 다른 플랫폼의 경우
        {
            return Path.Combine(Application.dataPath, fileName);
        }
    }

    private void OnApplicationQuit()
    {
        SaveOptionDataToJson();
    }

    [System.Serializable]
    public class OptionData
    {
        public bool saveWindowMode;
        public int saveResolutionNum;
        public float saveBgmVolume;
        public float saveEffectVolume;
    }
}
