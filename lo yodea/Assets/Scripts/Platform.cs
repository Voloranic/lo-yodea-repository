using UnityEngine;

public class Platform : MonoBehaviour
{
    BoxCollider2D Bcollider;
    [SerializeField] private float activeDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bcollider = this.gameObject.GetComponentInParent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Bcollider.enabled = true;
            Invoke("disableCollider", activeDelay);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Bcollider.enabled = true;
            Invoke("disableCollider", activeDelay);
        }
    }

    private void disableCollider()
    {
        Bcollider.enabled = false;
    }
}
