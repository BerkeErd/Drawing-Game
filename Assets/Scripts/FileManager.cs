using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileManager : MonoBehaviour
{
    private string backgroundColorKey = "BackgroundColor";

    private void SaveLines()
    {
        string filePath = Application.persistentDataPath + "/" + "drawnLines" + ".txt";

        SaveFile newSaveFile = new SaveFile(CanvasDrawer.drawnLineRenderers);
        string jsonString = JsonUtility.ToJson(newSaveFile,true);
        File.WriteAllText(filePath, jsonString);
    }

    private void LoadLines()
    {
        string filePath = Application.persistentDataPath + "/" + "drawnLines" + ".txt";

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            SaveFile newSaveFile = JsonUtility.FromJson<SaveFile>(jsonString);
            newSaveFile.Draw();
        }
        else
        {
            Debug.Log("No savefile with that name");
        }
    }

    private void SaveStamps()
    {
        string filePath = Application.persistentDataPath + "/" + "stamps" + ".txt";

        SaveFile newSaveFile = new SaveFile(null,CanvasDrawer.stampObjects);
        string jsonString = JsonUtility.ToJson(newSaveFile,true);
        File.WriteAllText(filePath, jsonString);
    }

    private void LoadStamps()
    {
        string filePath = Application.persistentDataPath + "/" + "stamps" + ".txt";

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            SaveFile newSaveFile = JsonUtility.FromJson<SaveFile>(jsonString);
            newSaveFile.Draw();
        }
        else
        {
            Debug.Log("No savefile with that name");
        }
    }

    private void SaveSplashes()
    {
        string filePath = Application.persistentDataPath + "/" + "splashes" + ".txt";

        SaveFile newSaveFile = new SaveFile(null, null, CanvasDrawer.splashObjects);
        string jsonString = JsonUtility.ToJson(newSaveFile, true);
        File.WriteAllText(filePath, jsonString);
    }

    private void LoadSplashes()
    {
        string filePath = Application.persistentDataPath + "/" + "splashes" + ".txt";

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            SaveFile newSaveFile = JsonUtility.FromJson<SaveFile>(jsonString);
            newSaveFile.Draw();
        }
        else
        {
            Debug.Log("No savefile with that name");
        }
    }

    public void SaveCanvas()
    {
        SaveLines();
        SaveStamps();
        SaveSplashes();
        SaveBackgroundColor(ToolManager.Instance.CurrentBackgroundColor);
    }

    public void LoadCanvas()
    {
        LoadLines();
        LoadStamps();
        LoadSplashes();
        LoadBackgroundColor();
    }

    private void SaveBackgroundColor(Color backgroundColor)
    {
        PlayerPrefs.SetFloat(backgroundColorKey + "_R", backgroundColor.r);
        PlayerPrefs.SetFloat(backgroundColorKey + "_G", backgroundColor.g);
        PlayerPrefs.SetFloat(backgroundColorKey + "_B", backgroundColor.b);
        PlayerPrefs.SetFloat(backgroundColorKey + "_A", backgroundColor.a);
        PlayerPrefs.Save();
    }

    private void LoadBackgroundColor()
    {
        if (PlayerPrefs.HasKey(backgroundColorKey + "_R"))
        {
            float r = PlayerPrefs.GetFloat(backgroundColorKey + "_R");
            float g = PlayerPrefs.GetFloat(backgroundColorKey + "_G");
            float b = PlayerPrefs.GetFloat(backgroundColorKey + "_B");
            float a = PlayerPrefs.GetFloat(backgroundColorKey + "_A");
            Color loadedColor = new Color(r, g, b, a);
            ToolManager.Instance.CurrentBackgroundColor = loadedColor;
            Camera.main.backgroundColor = loadedColor;
        }
        else
        {
            Camera.main.backgroundColor = ToolManager.Instance.CurrentBackgroundColor;
        }
    }
}