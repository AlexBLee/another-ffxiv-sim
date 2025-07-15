using UnityEngine;

public class StarburstHitbox : Hitbox
{
    public Hitbox HitboxPrefab;

    protected override void Execute()
    {
        var hitbox = Instantiate(HitboxPrefab, transform.position, Quaternion.identity);
        hitbox.SetSnapShotTime(SnapShotTime);
        Destroy(gameObject);
    }
}
