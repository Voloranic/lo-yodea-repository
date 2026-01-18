using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    RaycastHit2D huntRay;
    [SerializeField] LayerMask targetLayer;
    [SerializeField]Transform target;
    private int groundDistance;
    [SerializeField] private int height;
    LayerMask groundLayer;
    [SerializeField] float flySpeed;
    Vector2 direction;
    char kDive; // hunt(h), fly(f), dive(d)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("ground") | LayerMask.GetMask("ground");

    }

    // Update is called once per frame
    void Update()
    {
        Hunt();
    }
    private void Hunt()
    {
       if(kDive == 'h')
        {
            if (transform.rotation.z != 0) transform.Rotate(0, 0, 0);

            float angle = -60f;
            float radians = angle * Mathf.Deg2Rad;

            direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            //check if player in pos
            huntRay = Physics2D.Raycast(transform.position, direction, 100, targetLayer);
            Debug.DrawRay(transform.position, direction * huntRay.distance, Color.red);
            if (huntRay.collider != null)//ray hit player
            {
                kDive = 'd';
            }
           
        }
        else if (kDive == 'd')
        {
            dive();
        }
        else if (kDive == 'f')
        {
            Fly();
        }
    }
    private void dive()
    {
        if (transform.rotation.z != 30) transform.Rotate(0, 0, 30);
        // rotate towards player
        //fly towards the player, stop only when reaching target/ground
        rb.AddForce(transform.forward * flySpeed, ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.callbackLayers == targetLayer || collision.callbackLayers == groundLayer)
        {
            kDive = 'f';
        }
    }

    private void Fly()
    {
        //draw ray, rotate and fly until reach desired height
        if (transform.rotation.z != 90) transform.Rotate(0, 0, 90);

        huntRay = Physics2D.Raycast(transform.position,-Vector2.up, height, targetLayer);
        if (huntRay.collider != null)
        {
            rb.AddForce(Vector2.up* flySpeed / 1.5f, ForceMode2D.Force);

        }
        else
        {
            kDive = 'h';
        }
       

    }
}
