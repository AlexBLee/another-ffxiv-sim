using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class HitboxAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultTargetSize;
    [SerializeField] private float _duration;

    public float Duration => _duration;

    private Tweener _startTween;
    private Tweener _endTween;
    private Vector3 _targetScale;

    public void SetTargetScaleAndPlay(Vector3 targetScale)
    {
        _targetScale = targetScale == Vector3.zero
            ? _defaultTargetSize
            : targetScale;

        _startTween = transform.DOScale(new Vector3(_targetScale.x, transform.localScale.y, _targetScale.z), _duration);
    }

    public void AnimateOut()
    {
        _endTween = transform.DOScale(new Vector3(0, transform.localScale.y, 0), _duration);
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
