using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Drawer : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(OnToggleSelected);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleSelected);
    }

    private void OnToggleSelected(bool value)
    {
        if (value)
        {
            UIManager.Instance.EditorUI.OnNewButtonsClicked(this);
            UIManager.Instance.EditorUI.BossTimelineEditor.SetCurrentSelectedDrawer(this);
        }
    }

    public void SetToggleGroup(ToggleGroup toggleGroup)
    {
        _toggle.group = toggleGroup;
    }
}
