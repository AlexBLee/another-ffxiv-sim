using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private PlayerHitboxDetector[] _playerHitboxDetector;
    [SerializeField] private HitboxAnimator _hitboxAnimator;
    [SerializeField] private Vector3 _defaultTargetScale;

    private Player _player;
    private TargetBehaviour _targetBehaviour;
    private BossAction _bossAction;

    private float _snapShotTime;
    private float _animateOutTime;
    private float _currentTime;

    private bool _executed;

    public float SnapShotTime => _snapShotTime;

    private void Start()
    {
        _player = GameManager.Instance.Player;

        if (UIManager.Instance.InEditorMode)
        {
            gameObject.SetActive(true);
            _hitboxAnimator.transform.localScale = _defaultTargetScale;
        }
    }

    public void SetSnapShotTime(float time)
    {
        if (_hitboxAnimator != null)
        {
            _animateOutTime = time - _hitboxAnimator.Duration;
        }

        _snapShotTime = time;
    }

    public void SetBossAction(BossAction bossAction)
    {
        _bossAction = bossAction;
        _targetBehaviour = _bossAction.TargetBehaviour;

        SetSnapShotTime(bossAction.CastTime);

        var targetScale = bossAction.Scale == Vector3.zero
            ? _defaultTargetScale
            : bossAction.Scale;

        if (_hitboxAnimator != null)
        {
            _hitboxAnimator.SetTargetScaleAndPlay(targetScale);
        }

        foreach (var hitboxDetector in _playerHitboxDetector)
        {
            hitboxDetector.transform.localScale = targetScale;
        }
    }

    void Update()
    {
        if (_executed || UIManager.Instance.InEditorMode)
            return;

        _currentTime += Time.deltaTime;

        DetermineBehaviour();

        if (_currentTime > _animateOutTime && _hitboxAnimator != null)
        {
            _hitboxAnimator.AnimateOut();
        }

        if (_currentTime > _snapShotTime)
        {
            Execute();
            _executed = true;
        }
    }

    private void DetermineBehaviour()
    {
        if (_targetBehaviour == TargetBehaviour.FollowsPlayer && _player != null)
        {
            transform.localPosition = new Vector3(_player.transform.localPosition.x, 0, _player.transform.localPosition.z);
            return;
        }

        if (_targetBehaviour == TargetBehaviour.TargetsPlayer)
        {
            transform.LookAt(_player.transform);
        }
    }

    protected virtual async void Execute()
    {
        if (_hitboxAnimator != null)
        {
            _hitboxAnimator.CleanupTweens();
        }

        CheckForHitboxHit();
        gameObject.SetActive(false);

        await UniTask.WaitForSeconds(1);
        Destroy(gameObject);
    }

    protected void CheckForHitboxHit()
    {
        foreach (var hitboxDetector in _playerHitboxDetector)
        {
            if (hitboxDetector.PlayerInHitbox)
            {
                UIManager.Instance.GameplayUI.ShowHitText();
            }
        }
    }
}
