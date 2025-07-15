using System;
using DG.Tweening;
using UnityEngine;

public class HitboxAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 _targetSize;
    [SerializeField] private float _duration;

    public float Duration => _duration;

    private void Start()
    {
        transform.DOScaleX(_targetSize.x, _duration);
    }

    public void AnimateOut()
    {
        transform.DOScaleX(0, _duration);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
