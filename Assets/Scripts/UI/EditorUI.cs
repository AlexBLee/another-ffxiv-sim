using UnityEngine;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour
{
    [SerializeField] private BossTimelineEditor _bossTimelineEditor;

    [SerializeField] private Button _newMechanicButton;
    [SerializeField] private Button _newVariantButton;
    [SerializeField] private Button _newActionButton;

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
}
