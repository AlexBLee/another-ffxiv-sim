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
    [SerializeField] private VariantDrawer _variantContainer;
    [SerializeField] private ActionDrawer _actionContainer;

    private BossTimeline _bossTimeline;

    private BossMechanic _currentBossMechanic;
    private Drawer _currentDrawer;
    private List<BossMechanic> _bossMechanics = new();
    private List<Drawer> _drawers = new();

    private void Start()
    {
        _bossTimeline = _bossTimelineRenderer.BossTimeline;
    }

    public void CreateNewBossTimeline()
    {
        _bossTimeline = ScriptableObject.CreateInstance<BossTimeline>();
        _bossTimelineRenderer.SetBossTimeline(_bossTimeline);
    }

    public void SetCurrentSelectedDrawer(Drawer drawer)
    {
        _currentDrawer = drawer;
    }

    public void AddMechanicDrawer()
    {
        var drawer = Instantiate(_mechanicDrawer, _scrollViewContainer.transform);
        _currentBossMechanic = _bossTimeline.AddNewMechanic();

        drawer.SetBossMechanic(_currentBossMechanic);
        drawer.SetToggleGroup(_toggleGroup);

        _drawers.Add(drawer);
        _bossMechanics.Add(_currentBossMechanic);
    }

    public void AddVariantDrawer()
    {
        if (_currentDrawer is not MechanicDrawer)
        {
            return;
        }

        var variantDrawer = Instantiate(_variantContainer, _scrollViewContainer.transform);
        variantDrawer.SetToggleGroup(_toggleGroup);

        var insertIndex = GetNextDrawerSpot(_currentDrawer, typeof(VariantDrawer));

        _drawers.Insert(insertIndex, variantDrawer);
        variantDrawer.transform.SetSiblingIndex(insertIndex);
        variantDrawer.SetMechanicVariantList(_currentBossMechanic.AddMechanicVariant());
    }

    public void AddActionDrawer()
    {
        if (_currentDrawer is not VariantDrawer drawer)
        {
            return;
        }

        var actionDrawer = Instantiate(_actionContainer, _scrollViewContainer.transform);
        actionDrawer.SetToggleGroup(_toggleGroup);

        var insertIndex = GetNextDrawerSpot(_currentDrawer, typeof(ActionDrawer));

        _drawers.Insert(insertIndex, actionDrawer);
        actionDrawer.transform.SetSiblingIndex(insertIndex);
        actionDrawer.SetBossAction(drawer.MechanicVariantList.AddBossAction());
    }

    private int GetNextDrawerSpot(Drawer drawer, Type type)
    {
        int variantIndex = drawer.transform.GetSiblingIndex();
        int insertIndex = variantIndex + 1;

        for (int i = insertIndex; i < _drawers.Count; i++)
        {
            var childDrawer = _drawers[i];

            if (type.IsInstanceOfType(childDrawer))
            {
                insertIndex = i + 1;
            }
            else
            {
                break;
            }
        }

        return insertIndex;
    }
}
