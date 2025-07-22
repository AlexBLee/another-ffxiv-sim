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

    public BossAction AddBossAction()
    {
        var bossAction = new BossAction();
        BossActions.Add(bossAction);

        return bossAction;
    }
}

[System.Serializable]
public class BossMechanic
{
    public string Name;
    public float Time;
    public List<MechanicVariantList> MechanicVariants;

    public MechanicVariantList AddMechanicVariant()
    {
        MechanicVariants ??= new List<MechanicVariantList>();

        var mechanicVariant = new MechanicVariantList();
        MechanicVariants.Add(mechanicVariant);

        return mechanicVariant;
    }

    public List<BossAction> GetRandomBossActionList()
    {
        return MechanicVariants[Random.Range(0, MechanicVariants.Count)].BossActions;
    }
}

[System.Serializable]
public class BossAction
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

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BossTimeline", order = 1)]
public class BossTimeline : ScriptableObject
{
    public List<BossMechanic> Mechanics = new();

    public BossMechanic AddNewMechanic()
    {
        var mechanic = new BossMechanic();
        Mechanics.Add(mechanic);

        return mechanic;
    }
}
