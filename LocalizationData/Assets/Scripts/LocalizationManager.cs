using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour {

    //实现单例
    public static LocalizationManager instance;

    private string missingtext = "can't find the text";

    private bool isReady = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    public Dictionary<string, string> LocalizedText;

    public void LoadLocalizedText(string filename)
    {
        LocalizedText = new Dictionary<string ,string>();
        string filepath = Path.Combine(Application.streamingAssetsPath, filename);

        if (File.Exists(filepath))
        {
            string dataAsJson = File.ReadAllText(filepath);

            LocalizaitonData loadedData = JsonUtility.FromJson<LocalizaitonData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                LocalizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data Loaded,directory contains : " + LocalizedText.Count + "entries");
        }
        else
        {
            Debug.LogError("Can't find the file!!!");
        }

        isReady = true;

    }

    public string GetLocalizedTextFromKey(string key)
    {
        string result = missingtext;

        if (LocalizedText.ContainsKey(key))
        {
            result = LocalizedText[key];
        }
        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
