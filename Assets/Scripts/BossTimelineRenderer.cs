using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class BossTimelineRenderer : MonoBehaviour
{
    private struct ScheduledAction
    {
        public BossAction Action;
        public float GlobalTime;
    }

    [SerializeField] private BossTimeline _bossTimeline;

    private GameObject _player;

    private Queue<ScheduledAction> _actionQueue = new();
    private bool _started = false;

    private void Start()
    {
        _player = FindFirstObjectByType<Player>().gameObject;
        InitializeTimeline();
    }

    private void InitializeTimeline()
    {
        foreach (var mechanic in _bossTimeline.Mechanics)
        {
            var mechanicActionList = mechanic.GetRandomBossActionList();

            foreach (var action in mechanicActionList)
            {
                _actionQueue.Enqueue(new ScheduledAction
                {
                    Action = action,
                    GlobalTime = mechanic.Time + action.Time
                });
            }
        }
    }

    public void StartTimer()
    {
        if (_started)
            return;

        _started = true;

        RunTimeline().Forget();
    }

    private async UniTaskVoid RunTimeline()
    {
        var startTime = Time.time;

        while (_actionQueue.Count > 0)
        {
            var scheduled = _actionQueue.Peek();
            float waitTime = scheduled.GlobalTime - (Time.time - startTime);

            if (waitTime > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

            _actionQueue.Dequeue();
            SpawnAction(scheduled.Action);
        }
    }

    private void SpawnAction(BossAction action)
    {
        var hitbox = Instantiate(action.Hitbox, action.Location, Quaternion.Euler(action.Rotation));
        hitbox.SetBossAction(action);
    }
}