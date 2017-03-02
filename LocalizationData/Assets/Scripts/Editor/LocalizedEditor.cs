using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizedEditor : EditorWindow 
{

    public LocalizaitonData localizationData;

    [MenuItem("Window/Localized Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizedEditor)).Show(); 
    }

    private void OnGUI()
    {
        if (localizationData != null)
        {
            SerializedObject serializedobject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedobject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedobject.ApplyModifiedProperties();

            if (GUILayout.Button("Save Data"))
            {
                SaveGameData();
            }

        }

        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        if (GUILayout.Button("Creat Data"))
        {
            CreatNewData();     
        }
    }



    public void LoadGameData()
    {
        string filePath = EditorUtility.OpenFilePanel("Open Localization Data File", Application.streamingAssetsPath, "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            localizationData = JsonUtility.FromJson<LocalizaitonData>(dataAsJson);
        }
    }


    public void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save Localization Data File", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);

            File.WriteAllText(filePath, dataAsJson);

        }

    }

    private void CreatNewData()
    {
        localizationData = new LocalizaitonData();
    }

}
