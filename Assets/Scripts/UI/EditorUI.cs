using UnityEngine;

public class EditorUI : MonoBehaviour
{
    [SerializeField] private BossTimelineEditor _bossTimelineEditor;

    public void CreateNewBossTimeline()
    {
        _bossTimelineEditor.CreateNewBossTimeline();
    }
}
