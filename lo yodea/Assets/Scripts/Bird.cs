using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    RaycastHit2D huntRay;
    RaycastHit2D drawRay;

    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform target;
    private int groundDistance;
    [SerializeField] private int height;
    LayerMask groundLayer;
    [SerializeField] float flySpeed;
    [SerializeField] float maxSpeed;
    private LineRenderer lineRenderer;
    [SerializeField] int speedMultiplyer;

    Vector2 direction;
    char kDive = 'h'; // hunt(h), fly(f), dive(d)
    int face;
    float scale;
    float angle;
    float radians;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("ground") | LayerMask.GetMask("floor");
        print(groundLayer.value);
        //line renderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.01f;

    }

    // Update is called once per frame
    void Update()
    {
        Hunt();
    }
    private void FixedUpdate()
    {

        forces();
    }
    private void forces()
    {
       /*if (transform.position.y < target.transform.position.y)
        {
            huntRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.up, height, groundLayer);
            if (huntRay.collider != null)
            {
               HitGround();
            }

        }
       */

        
        if (kDive == 'd' && transform.position.y - 1 > target.transform.position.y)
        {
            dive();
            rb.AddForce(direction * flySpeed * speedMultiplyer, ForceMode2D.Force);
            print("d");
        }
        else if (kDive == 'f')
        {
            rb.AddForce(flySpeed * speedMultiplyer * Vector2.up, ForceMode2D.Force);
            Fly();
            print("f");

        }
        else if (kDive == 'h')
        {
            rb.AddForce(flySpeed * face * transform.right, ForceMode2D.Force);
            print("h");

        }
    }
    private void Hunt()
    {
        if (kDive == 'h')
        {
            if (transform.rotation.z != 0) transform.rotation = Quaternion.Euler(0, 0, 0);

            //check if player in pos
            direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            huntRay = Physics2D.Raycast(transform.position, direction, 100, targetLayer);
            drawRay = Physics2D.Raycast(transform.position, direction, 100);

            Debug.DrawRay(transform.position, direction * 100, UnityEngine.Color.aliceBlue);

            if (huntRay.collider != null)//ray hit player
            {

                kDive = 'd';
                FreezeForce();
                rb.constraints = RigidbodyConstraints2D.None;
                print("dive!");
            }
            //follow player -4.5<->-6
            // scale = transform.localScale.x;
            if (transform.position.x + 4.7 < target.position.x && face != 1)
            {
                face = 1;
                angle = -60f;
                radians = angle * Mathf.Deg2Rad;

                
                //  if (scale < 0) scale *= -1;
            }
            else if (transform.position.x - 4.7 > target.position.x && face != -1)
            {
                face = -1;
                angle = -120f;
                radians = angle * Mathf.Deg2Rad;
                // if (scale > 0) scale *= -1;

            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, drawRay.point);


        }
        
    }
    private void dive()
    {
        if (transform.rotation.z != -30 && face == 1) transform.rotation = Quaternion.Euler(0, 0, -30);
        else if(transform.rotation.z !=30 && face == -1) Quaternion.Euler(0, 0, 30);

        // rotate towards player
        //fly towards the player, stop only when reaching target/ground
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Compare the layer of the thing we hit to our masks
        if (((1 << collision.gameObject.layer) & groundLayer) != 0 ||
            ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            HitGround();
        }
    }
    private void HitGround()
    {
        print("fly!");
        kDive = 'f';
        FreezeForce();
    }

    private void Fly()
    {
        //draw ray, rotate and fly until reach desired height
        if (transform.rotation.z != 90) transform.rotation = Quaternion.Euler(0, 0, 90);

        huntRay = Physics2D.Raycast(transform.position, -Vector2.up, height, groundLayer);
        if (huntRay.collider != null)
        {

        }
        else
        {
            print("hunt!");
            kDive = 'h';
            FreezeForce();
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }



    }
    private void FreezeForce()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }

}
