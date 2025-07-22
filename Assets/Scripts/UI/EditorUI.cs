using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour
{
    [SerializeField] private BossTimelineEditor _bossTimelineEditor;
    [SerializeField] private FileManager _fileManager;

    [SerializeField] private Button _newMechanicButton;
    [SerializeField] private Button _newVariantButton;
    [SerializeField] private Button _newActionButton;

    [SerializeField] private TMP_InputField _saveNameInputField;

    public BossTimelineEditor BossTimelineEditor => _bossTimelineEditor;

    public void CreateNewBossTimeline()
    {
        _bossTimelineEditor.CreateNewBossTimeline();
    }

    public void OnNewButtonsClicked(Drawer drawer)
    {
        switch (drawer)
        {
            case MechanicDrawer:
                _newVariantButton.gameObject.SetActive(true);
                _newActionButton.gameObject.SetActive(false);
                break;

            case VariantDrawer:
                _newVariantButton.gameObject.SetActive(true);
                _newActionButton.gameObject.SetActive(true);
                break;

            case ActionDrawer:
                // Do nothing..
                break;

            default:
                Debug.LogWarning("Unknown Drawer");
                break;
        }
    }

    public void Save()
    {
        _fileManager.Save(_saveNameInputField.text, _bossTimelineEditor.BossTimeline);
    }

    public void Load()
    {
        _fileManager.LoadFileBrowser((timeline) =>
        {
            _bossTimelineEditor.LoadBossTimeline(timeline);
        });
    }
}
