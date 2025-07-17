using System.Collections.Generic;
using UnityEngine;

public enum TargetBehaviour
{
    TargetsMap,
    TargetsPlayer,
    FollowsPlayer,
}

public enum TargetCount
{
    None,
    Single,
    Partners,
    LightParties,
    Stack
}

[System.Serializable]
public class MechanicVariantList
{
    public List<BossAction> BossActions = new List<BossAction>();
}

[System.Serializable]
public struct BossMechanic
{
    public string Name;
    public float Time;
    public List<MechanicVariantList> MechanicVariants;

    public List<BossAction> GetRandomBossActionList()
    {
        return MechanicVariants[Random.Range(0, MechanicVariants.Count)].BossActions;
    }
}

[System.Serializable]
public struct BossAction
{
    public Hitbox Hitbox;
    public TargetBehaviour TargetBehaviour;
    public TargetCount TargetCount;

    [Header("Times")]
    public float Time;
    public float CastTime;

    [Header("Positions")]
    public Vector3 Location;
    public Vector3 Rotation;
    public Vector3 Scale;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BossTimeline", order = 1)]
public class BossTimeline : ScriptableObject
{
    public List<BossMechanic> Mechanics;
}
