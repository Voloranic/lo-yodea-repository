using UnityEngine;

public class Platform : MonoBehaviour
{
    BoxCollider2D Bcollider;
    [SerializeField] private float activeDelay;
    private LineRenderer lineRenderer;
    [SerializeField]Transform point;
    [SerializeField] LayerMask attackLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bcollider = this.gameObject.GetComponentInParent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.01f;
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, point.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & attackLayer) != 0)
        {
            Bcollider.enabled = true;
            Invoke("disableCollider", activeDelay);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & attackLayer) != 0)
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
