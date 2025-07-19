using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class BossTimelineRenderer : MonoBehaviour
{
    [SerializeField] private BossTimeline _bossTimeline;

    public struct ScheduledAction
    {
        public string Name;
        public BossAction Action;
        public float GlobalTime;
        public float ActualTime;
    }

    private Queue<ScheduledAction> _actionQueue = new();
    private bool _started = false;

    private void Start()
    {
        InitializeTimeline();
    }

    private void InitializeTimeline()
    {
        float totalTime = 0;

        foreach (var mechanic in _bossTimeline.Mechanics)
        {
            var mechanicActionList = mechanic.GetRandomBossActionList();

            _actionQueue.Enqueue(new ScheduledAction()
            {
                Name = mechanic.Name,
                GlobalTime = totalTime + mechanic.Time,
                ActualTime = mechanic.Time
            });

            totalTime += mechanic.Time;

            Debug.Log("Mechanic Time: " + mechanic.Time + " - Total Time: " + totalTime);

            foreach (var action in mechanicActionList)
            {
                _actionQueue.Enqueue(new ScheduledAction
                {
                    Action = action,
                    GlobalTime = totalTime + action.Time,
                    ActualTime = action.Time
                });

                totalTime += action.Time;

                Debug.Log("Action Time: " + action.Time + " - Total Time: " + totalTime);
            }
        }
    }

    public void StartTimer()
    {
        if (_started)
            return;

        UIManager.Instance.HideHitText();
        UIManager.Instance.SetStartButtonInteractiveState(false);

        if (_actionQueue.Count == 0)
        {
            InitializeTimeline();
        }

        _started = true;

        RunTimeline().Forget();
    }

    private async UniTaskVoid RunTimeline()
    {
        var startTime = Time.time;
        ScheduledAction lastScheduledAction = new();

        while (_actionQueue.Count > 0)
        {
            var scheduled = _actionQueue.Peek();
            float waitTime = scheduled.GlobalTime - (Time.time - startTime);

            UIManager.Instance.ShowProgressBar(scheduled);

            if (waitTime > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

            lastScheduledAction = _actionQueue.Dequeue();
            SpawnAction(scheduled.Action);
        }

        await UniTask.WaitForSeconds(lastScheduledAction.Action.CastTime);
        UIManager.Instance.SetStartButtonInteractiveState(true);
        _started = false;
    }

    private void SpawnAction(BossAction action)
    {
        if (action.Hitbox == null)
        {
            return;
        }

        var hitbox = Instantiate(action.Hitbox, action.Location, Quaternion.Euler(action.Rotation));
        hitbox.SetBossAction(action);
    }
}