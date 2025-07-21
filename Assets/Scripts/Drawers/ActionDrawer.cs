using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;

public class ActionDrawer : Drawer
{
    private enum HitboxType
    {
        Line,
        Circle,
    }

    [SerializeField] private TMP_Dropdown _hitboxDropdown;
    [SerializeField] private TMP_Dropdown _targetDropdown;
    [SerializeField] private TMP_InputField _timeInputField;
    [SerializeField] private TMP_InputField _castTimeInputField;

    [SerializeField] private Vector3InputField _locationInputField;
    [SerializeField] private Vector3InputField _rotationInputField;
    [SerializeField] private Vector3InputField _scaleInputField;

    [SerializeField] private SerializedDictionary<HitboxType, Hitbox> _hitboxCache;

    private BossAction _bossAction;
    private Hitbox _currentHitbox;

    private void OnEnable()
    {
        InitializeDropdown(_hitboxDropdown, typeof(HitboxType));
        _hitboxDropdown.onValueChanged.AddListener(OnShapeDropdownValueChanged);

        InitializeDropdown(_targetDropdown, typeof(TargetBehaviour));
        _targetDropdown.onValueChanged.AddListener(OnTargetBehaviourDropdownValueChanged);

        _timeInputField.onEndEdit.AddListener(OnTimeInputValueChanged);
        _castTimeInputField.onEndEdit.AddListener(OnCastTimeInputValueChanged);

        _locationInputField.OnValueChangedCallback += OnLocationChanged;
        _rotationInputField.OnValueChangedCallback += OnRotationChanged;
        _scaleInputField.OnValueChangedCallback += OnScaleChanged;
    }

    private void OnDisable()
    {
        _hitboxDropdown.onValueChanged.RemoveListener(OnShapeDropdownValueChanged);
        _hitboxDropdown.onValueChanged.RemoveListener(OnTargetBehaviourDropdownValueChanged);

        _timeInputField.onEndEdit.RemoveListener(OnTimeInputValueChanged);
        _castTimeInputField.onEndEdit.RemoveListener(OnCastTimeInputValueChanged);

        _locationInputField.OnValueChangedCallback -= OnLocationChanged;
        _rotationInputField.OnValueChangedCallback -= OnRotationChanged;
        _scaleInputField.OnValueChangedCallback -= OnScaleChanged;
    }

    public void SetBossAction(BossAction bossAction)
    {
        _bossAction = bossAction;

        // Setting one by default - in case the user never changes the option.
        _bossAction.Hitbox = _hitboxCache[HitboxType.Line];

        _currentHitbox = Instantiate(_hitboxCache[HitboxType.Line]);
    }

    private void InitializeDropdown(TMP_Dropdown dropdown, Type enumType)
    {
        var options = Enum.GetNames(enumType).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    private void OnShapeDropdownValueChanged(int shapeIndex)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Hitbox = _hitboxCache[(HitboxType)shapeIndex];

        var nextHitbox = Instantiate(_hitboxCache[(HitboxType)shapeIndex]);
        Destroy(_currentHitbox.gameObject);
        _currentHitbox = nextHitbox;
    }

    private void OnTargetBehaviourDropdownValueChanged(int targetBehaviourIndex)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.TargetBehaviour = (TargetBehaviour)targetBehaviourIndex;
    }

    private void OnTimeInputValueChanged(string arg0)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Time = float.Parse(arg0);
    }

    private void OnCastTimeInputValueChanged(string arg0)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.CastTime = float.Parse(arg0);
    }

    private void OnLocationChanged(Vector3 vector)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Location = vector;
    }

    private void OnRotationChanged(Vector3 vector)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Rotation = vector;
    }

    private void OnScaleChanged(Vector3 vector)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Scale = vector;
    }

}
