using System;
using UnityEngine;

public class BossTimelineEditor : MonoBehaviour
{
    [SerializeField] private BossTimelineRenderer _bossTimelineRenderer;
    [SerializeField] private GameObject _scrollViewContainer;
    [SerializeField] private MechanicDrawer _mechanicDrawer;

    private BossTimeline _bossTimeline;

    private void Start()
    {
        _bossTimeline = _bossTimelineRenderer.BossTimeline;
    }

    public void CreateNewBossTimeline()
    {
        _bossTimeline = ScriptableObject.CreateInstance<BossTimeline>();
        _bossTimelineRenderer.SetBossTimeline(_bossTimeline);
    }

    public void AddMechanicDrawer()
    {
        var drawer = Instantiate(_mechanicDrawer, _scrollViewContainer.transform);
        var mechanic = _bossTimeline.AddNewMechanic();
        drawer.SetBossMechanic(mechanic);
    }
}
