using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTimelineEditor : MonoBehaviour
{
    [SerializeField] private BossTimelineRenderer _bossTimelineRenderer;
    [SerializeField] private GameObject _scrollViewContainer;
    [SerializeField] private ToggleGroup _toggleGroup;

    [SerializeField] private MechanicDrawer _mechanicDrawer;
    [SerializeField] private Drawer _variantContainer;
    [SerializeField] private Drawer _actionContainer;

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

        drawer.SetBossMechanic(_currentBossMechanic);
        drawer.SetToggleGroup(_toggleGroup);

        _bossMechanics.Add(_currentBossMechanic);

        _currentBossMechanic = _bossTimeline.AddNewMechanic();
    }

    public void AddVariantDrawer()
    {
        var variantDrawer = Instantiate(_variantContainer, _scrollViewContainer.transform);
        variantDrawer.SetToggleGroup(_toggleGroup);

        _currentBossMechanic.AddMechanicVariant();
    }

    public void AddActionDrawer()
    {
        var actionDrawer = Instantiate(_actionContainer, _scrollViewContainer.transform);
        actionDrawer.SetToggleGroup(_toggleGroup);

        _currentBossMechanic.MechanicVariants[0].AddBossAction();
    }

    private void OnToggleSelected(bool obj)
    {
    }
}
