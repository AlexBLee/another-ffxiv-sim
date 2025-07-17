using System;
using DG.Tweening;
using UnityEngine;

public class HitboxAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 _targetSize;
    [SerializeField] private float _duration;

    public float Duration => _duration;

    private Tweener _startTween;
    private Tweener _endTween;

    private void Start()
    {
        _startTween = transform.DOScaleX(_targetSize.x, _duration);
    }

    public void AnimateOut()
    {
        _endTween = transform.DOScaleX(0, _duration);
    }

    public void CleanupTweens()
    {
        if (_startTween != null && _startTween.IsActive())
        {
            _startTween.Kill();
        }

        if (_endTween != null && _endTween.IsActive())
        {
            _endTween.Kill();
        }
    }
}
