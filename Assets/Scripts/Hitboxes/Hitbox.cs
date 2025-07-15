using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private PlayerHitboxDetector[] _playerHitboxDetector;
    [SerializeField] private HitboxAnimator _hitboxAnimator;

    private GameObject _player;
    private float _snapShotTime;
    private float _animateOutTime;
    private float _currentTime;

    private bool _executed;

    public float SnapShotTime => _snapShotTime;

    public void SetSnapShotTime(float time)
    {
        if (_hitboxAnimator != null)
        {
            _animateOutTime = time - _hitboxAnimator.Duration;
        }

        _snapShotTime = time;
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    void Update()
    {
        if (_executed)
            return;

        _currentTime += Time.deltaTime;

        if (_player != null)
        {
            transform.localPosition = new Vector3(_player.transform.localPosition.x, 0, _player.transform.localPosition.z);
        }

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

    protected virtual void Execute()
    {
        CheckForHitboxHit();
        Destroy(gameObject);
    }

    protected void CheckForHitboxHit()
    {
        foreach (var hitboxDetector in _playerHitboxDetector)
        {
            if (hitboxDetector.PlayerInHitbox)
            {
                Debug.Log("Hit!");
            }
        }
    }
}
