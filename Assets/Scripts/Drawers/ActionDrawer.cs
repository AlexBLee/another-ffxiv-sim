using System;
using System.Globalization;
using System.Linq;
using AYellowpaper.SerializedCollections;
using TMPro;
using TransformHandles;
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
    private Handle _currentHandle;

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

    private void OnHandleInteraction(Handle handle)
    {
        var hitboxTransform = _currentHitbox.transform;

        switch (_currentHandle.type)
        {
            case HandleType.Position:
                OnLocationChanged(hitboxTransform.localPosition);
                _locationInputField.SetValue(hitboxTransform.localPosition);
                break;
            case HandleType.Rotation:
                OnLocationChanged(hitboxTransform.localRotation.eulerAngles);
                _rotationInputField.SetValue(hitboxTransform.localRotation.eulerAngles);
                break;
            case HandleType.Scale:
                OnScaleChanged(hitboxTransform.localScale);
                _scaleInputField.SetValue(hitboxTransform.localScale);
                break;
        }
    }

    public void SetBossAction(BossAction bossAction, bool wasLoaded)
    {
        _bossAction = bossAction;

        _targetDropdown.value = (int)bossAction.TargetBehaviour;
        _timeInputField.text = bossAction.Time.ToString(CultureInfo.InvariantCulture);
        _castTimeInputField.text = bossAction.CastTime.ToString(CultureInfo.InvariantCulture);
        _locationInputField.SetValue(bossAction.Location);
        _rotationInputField.SetValue(bossAction.Rotation);
        _scaleInputField.SetValue(bossAction.Scale == Vector3.zero ? Vector3.one : bossAction.Scale);

        // Setting one by default - in case the user never changes the option.
        _bossAction.Hitbox = wasLoaded
            ? bossAction.Hitbox
            : _hitboxCache[HitboxType.Line];

        if (!wasLoaded)
        {
            TransformHandleManager.Instance.DestroyAllHandles();

            _currentHitbox = Instantiate(_hitboxCache[HitboxType.Line]);
            _currentHandle = TransformHandleManager.Instance.CreateHandle(_currentHitbox.transform);

            _currentHitbox.transform.localPosition = bossAction.Location;
            _currentHitbox.transform.localRotation = Quaternion.Euler(bossAction.Rotation);
            _currentHitbox.transform.localScale = bossAction.Scale == Vector3.zero ? Vector3.one : bossAction.Scale;

            _currentHandle.OnInteractionEvent += OnHandleInteraction;
        }
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

        _currentHandle.OnInteractionEvent -= OnHandleInteraction;
        TransformHandleManager.Instance.RemoveHandle(_currentHandle);

        Destroy(_currentHitbox.gameObject);

        var nextHitbox = Instantiate(_hitboxCache[(HitboxType)shapeIndex]);
        _currentHitbox = nextHitbox;
        _currentHandle = TransformHandleManager.Instance.CreateHandle(_currentHitbox.transform);
        _currentHandle.OnInteractionEvent += OnHandleInteraction;
    }

    private void OnTargetBehaviourDropdownValueChanged(int targetBehaviourIndex)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.TargetBehaviour = (TargetBehaviour)targetBehaviourIndex;
    }

    private void OnTimeInputValueChanged(string text)
    {
        if (_bossAction == null)
        {
            return;
        }

        if (!float.TryParse(text, out float time))
        {
            return;
        }

        _bossAction.Time = time;
    }

    private void OnCastTimeInputValueChanged(string text)
    {
        if (_bossAction == null)
        {
            return;
        }

        if (!float.TryParse(text, out float castTime))
        {
            return;
        }

        _bossAction.CastTime = castTime;
    }

    private void OnLocationChanged(Vector3 vector)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Location = vector;
        _currentHitbox.transform.localPosition = vector;
    }

    private void OnRotationChanged(Vector3 vector)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Rotation = vector;
        _currentHitbox.transform.localRotation = new Quaternion(vector.x, vector.y, vector.z, vector.z);
    }

    private void OnScaleChanged(Vector3 vector)
    {
        if (_bossAction == null)
        {
            return;
        }

        _bossAction.Scale = vector;
        _currentHitbox.transform.localScale = vector;
    }

    public void ResetAll()
    {
        _currentHandle.OnInteractionEvent -= OnHandleInteraction;
        TransformHandleManager.Instance.RemoveHandle(_currentHandle);

        Destroy(_currentHitbox.gameObject);
    }
}
