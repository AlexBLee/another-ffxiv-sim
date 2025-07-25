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

    public BossTimeline BossTimeline => _bossTimeline;

    private void Start()
    {
        _bossTimeline = _bossTimelineRenderer.BossTimeline;
    }

    public void CreateNewBossTimeline()
    {
        _bossTimeline = ScriptableObject.CreateInstance<BossTimeline>();
        _bossTimelineRenderer.SetBossTimeline(_bossTimeline);
        ResetDrawers();
    }

    public void LoadBossTimeline(BossTimeline bossTimeline)
    {
        _bossTimeline = bossTimeline;
        _bossTimelineRenderer.SetBossTimeline(_bossTimeline);

        ResetDrawers();

        foreach (var mechanic in _bossTimeline.Mechanics)
        {
            AddMechanicDrawer(mechanic);

            foreach (var mechanicVariantList in mechanic.MechanicVariants)
            {
                AddVariantDrawer(mechanicVariantList);

                foreach (var action in mechanicVariantList.BossActions)
                {
                    AddActionDrawer(action);
                }
            }
        }
    }

    public void SetCurrentSelectedDrawer(Drawer drawer)
    {
        _currentDrawer = drawer;
    }

    public void AddMechanicDrawer(BossMechanic mechanic = null)
    {
        var drawer = Instantiate(_mechanicDrawer, _scrollViewContainer.transform);
        _currentBossMechanic = mechanic ?? _bossTimeline.AddNewMechanic();

        drawer.SetBossMechanic(_currentBossMechanic);
        drawer.SetToggleGroup(_toggleGroup);

        _drawers.Add(drawer);
        _bossMechanics.Add(_currentBossMechanic);
        _currentDrawer = drawer;
    }

    public void AddVariantDrawer(MechanicVariantList variantList = null)
    {
        if (_currentDrawer is not MechanicDrawer && variantList == null)
        {
            return;
        }

        var variantDrawer = Instantiate(_variantContainer, _scrollViewContainer.transform);
        variantDrawer.SetToggleGroup(_toggleGroup);

        var insertIndex = GetNextDrawerSpot(_currentDrawer, typeof(VariantDrawer));
        _drawers.Insert(insertIndex, variantDrawer);
        variantDrawer.transform.SetSiblingIndex(insertIndex);

        MechanicVariantList mechanicVariantList = variantList ?? _currentBossMechanic.AddMechanicVariant();
        variantDrawer.SetMechanicVariantList(mechanicVariantList);
        _currentDrawer = variantDrawer;
    }

    public void AddActionDrawer(BossAction existingBossAction = null)
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

        BossAction bossAction = existingBossAction ?? drawer.MechanicVariantList.AddBossAction();
        actionDrawer.SetBossAction(bossAction, existingBossAction != null);
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

    private void ResetDrawers()
    {
        foreach (var drawer in _drawers)
        {
            Destroy(drawer.gameObject);
        }

        _drawers.Clear();
        _currentDrawer = null;
    }

    public void ResetActionDrawerHandles()
    {
        foreach (var drawer in _drawers)
        {
            if (drawer is ActionDrawer actionDrawer)
            {
                actionDrawer.ResetHandles();
            }
        }
    }
}
