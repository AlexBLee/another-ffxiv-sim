using System;
using System.Collections.Generic;
using UnityEngine;

public class BossTimelineEditor : MonoBehaviour
{
    [SerializeField] private BossTimelineRenderer _bossTimelineRenderer;
    [SerializeField] private GameObject _scrollViewContainer;
    [SerializeField] private MechanicDrawer _mechanicDrawer;
    [SerializeField] private GameObject _variantContainer;
    [SerializeField] private GameObject _actionContainer;

    private BossTimeline _bossTimeline;

    private BossMechanic _currentBossMechanic;
    private List<BossMechanic> _bossMechanics = new();

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

        _currentBossMechanic = _bossTimeline.AddNewMechanic();
        drawer.SetBossMechanic(_currentBossMechanic);

        _bossMechanics.Add(_currentBossMechanic);
    }

    public void AddVariantDrawer()
    {
        var variantDrawer = Instantiate(_variantContainer, _scrollViewContainer.transform);
        _currentBossMechanic.AddMechanicVariant();
    }

    public void AddActionDrawer()
    {
        var actionDrawer = Instantiate(_actionContainer, _scrollViewContainer.transform);

    }
}
