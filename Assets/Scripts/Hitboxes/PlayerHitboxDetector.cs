using System;
using UnityEngine;

public class PlayerHitboxDetector : MonoBehaviour
{
    private bool _playerInHitbox = false;

    public bool PlayerInHitbox => _playerInHitbox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Instance.Player.gameObject)
        {
            _playerInHitbox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Instance.Player.gameObject)
        {
            _playerInHitbox = false;
        }
    }
}
