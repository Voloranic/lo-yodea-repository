using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    RaycastHit2D huntRay;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform target;
    private int groundDistance;
    [SerializeField] private int height;
    LayerMask groundLayer;
    [SerializeField] float flySpeed;
    [SerializeField] float maxSpeed;
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
        if (transform.position.y < target.transform.position.y)
        {
            huntRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.up, height, groundLayer);
            if (huntRay.collider != null)
            {
                kDive = 'f';
            }

        }
        if (kDive == 'd' && transform.position.y - 1 > target.transform.position.y)
        {
            dive();
            rb.AddForce(direction * flySpeed * 100, ForceMode2D.Force);
            FreezeForce();
            print("d");
        }
        else if (kDive == 'f')
        {
            kDive = 'f';
            Fly();
            FreezeForce();
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
            Debug.DrawRay(transform.position, direction * 100, Color.cyan);

            if (huntRay.collider != null)//ray hit player
            {
                kDive = 'd';
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
            
            // transform.localScale = new Vector3(scale,transform.localScale.y,1);


        }
        
    }
    private void dive()
    {
        if (transform.rotation.z != 30) transform.rotation = Quaternion.Euler(0, 0, -30);

        // rotate towards player
        //fly towards the player, stop only when reaching target/ground
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(targetLayer) || collision.IsTouchingLayers(groundLayer))
        {
            print("fly!");
            kDive = 'f';
            //Invoke("SetDive", 2);
        }
    }

    private void Fly()
    {
        //draw ray, rotate and fly until reach desired height
        if (transform.rotation.z != 90) transform.rotation = Quaternion.Euler(0, 0, 90);

        huntRay = Physics2D.Raycast(transform.position, -Vector2.up, height, groundLayer);
        if (huntRay.collider != null)
        {
            rb.AddForce(Vector2.up * flySpeed * 100 / 2, ForceMode2D.Force);

        }
        else
        {
            print("hunt!");
            FreezeForce();

            kDive = 'h';
        }



    }
    private void FreezeForce()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }

}
