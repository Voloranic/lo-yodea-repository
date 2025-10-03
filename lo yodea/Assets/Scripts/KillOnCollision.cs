using UnityEngine;

public class KillOnCollision : MonoBehaviour
{
    [SerializeField] bool isTrigger = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTrigger) return;

        if (collision.gameObject.TryGetComponent(out Player_M player))
        {
            player.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigger) return;

        if (collision.TryGetComponent(out Player_M player))
        {
            player.Die();
        }
    }
}
