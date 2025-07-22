using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour
{
    [SerializeField] private BossTimelineEditor _bossTimelineEditor;
    [SerializeField] private FileManager _fileManager;

    [SerializeField] private Button _newMechanicButton;
    [SerializeField] private Button _newVariantButton;
    [SerializeField] private Button _newActionButton;

    [SerializeField] private TMP_InputField _saveNameInputField;

    private UnityAction _newMechanicAction;
    private UnityAction _newVariantAction;
    private UnityAction _newAction;

    public BossTimelineEditor BossTimelineEditor => _bossTimelineEditor;

    private void OnEnable()
    {
        _newMechanicAction = () => _bossTimelineEditor.AddMechanicDrawer();
        _newVariantAction = () => _bossTimelineEditor.AddVariantDrawer();
        _newAction = () => _bossTimelineEditor.AddActionDrawer();

        _newMechanicButton.onClick.AddListener(_newMechanicAction);
        _newVariantButton.onClick.AddListener(_newVariantAction);
        _newActionButton.onClick.AddListener(_newAction);
    }

    private void OnDisable()
    {
        _newMechanicButton.onClick.RemoveListener(_newMechanicAction);
        _newVariantButton.onClick.RemoveListener(_newVariantAction);
        _newActionButton.onClick.RemoveListener(_newAction);

        _newMechanicAction = null;
        _newVariantAction = null;
        _newAction = null;
    }

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
