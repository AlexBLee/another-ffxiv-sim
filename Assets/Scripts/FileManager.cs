using System;
using System.IO;
using SFB;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public void Save(string fileName, BossTimeline bossTimeline)
    {
        string json = JsonUtility.ToJson(bossTimeline, true);
        File.WriteAllText(Application.dataPath + $"/{fileName}.json", json);
    }

    public void LoadFileBrowser(Action<BossTimeline> onJsonLoaded)
    {
        StandaloneFileBrowser.OpenFilePanelAsync(
            "Open File",
            Application.dataPath,
            "json",
            false,
            (paths) => HandleFileSelected(paths, onJsonLoaded)
        );
    }

    private void HandleFileSelected(string[] paths, Action<BossTimeline> onJsonLoaded)
    {
        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            string json = File.ReadAllText(paths[0]);
            BossTimeline loadedData = ScriptableObject.CreateInstance<BossTimeline>();
            JsonUtility.FromJsonOverwrite(json, loadedData);

            onJsonLoaded?.Invoke(loadedData);
        }
        else
        {
            onJsonLoaded?.Invoke(null);
        }
    }
}
