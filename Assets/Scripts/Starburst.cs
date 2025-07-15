using UnityEngine;

public class Starburst : Hitbox
{
    [SerializeField]
    private GameObject _offObjects;

    [SerializeField]
    private GameObject _onObjects;

    protected override void Execute()
    {
        CheckForHitboxHit();
        _offObjects.gameObject.SetActive(false);
        _onObjects.gameObject.SetActive(true);
    }
}
