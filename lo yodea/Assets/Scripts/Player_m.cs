using Unity.VisualScripting;
using UnityEngine;

public class Player_M : MonoBehaviour
{
    [SerializeField] private int scaredMultiplyer;
    [SerializeField] private float speed;
    [SerializeField] Transform groundPoint;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;
    Rigidbody2D rb;
    private float scared;
    private float horizontal;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask floorLayer;
    [SerializeField] float gravityMultiplyer;
    bool jump;
    bool grounded;
    private float groundDistance;
    LayerMask combineMask;
    [SerializeField] float stopForce;
    [SerializeField] GameObject deathScreen;
    private bool dead;
    RaycastHit2D floorRay;
    RaycastHit2D groundRay;
    [SerializeField] float dashForce;

    int face;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        combineMask = groundLayer | floorLayer;

    }

    private void FixedUpdate()
    {
        checkIfScared();
        addForce();
    }

    void Update()
    {
        if (!dead)
        {
            movement();
            attacks();
            direction();
        }
        checkGround();

    }
    private void attacks()
    {
        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(new Vector2(dashForce * face, 0), ForceMode2D.Impulse);
            print("dash!");
        }
    }
    private void direction()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            face = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            face = -1;
        }
    }
    private void checkGround()
    {
        groundRay = Physics2D.Raycast(groundPoint.position, -Vector2.up, 100, combineMask);
        //draw ray
        Debug.DrawRay(groundPoint.position, -Vector2.up * groundRay.distance, Color.red);
        groundDistance = groundRay.distance;
        if (groundRay.collider != null && groundDistance <= 0.5f)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

    }

    private void movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyDown(KeyCode.Space);

        if (grounded)
        {
            if (jump)
            {
                rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            }
        }
        else
        {
            rb.AddForce(-Vector2.up * gravityMultiplyer, ForceMode2D.Force);
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-Vector2.up * gravityMultiplyer / 1.3f, ForceMode2D.Force);
            }
        }
        if (horizontal == 0 && rb.linearVelocityX != 0)
        {
            rb.AddForce(new Vector2(-rb.linearVelocityX * stopForce, 0), ForceMode2D.Force);
        }

    
    }
    private void addForce()
    {
        if (!dead)
        {
            rb.AddForce(new Vector2(horizontal * speed, 0), ForceMode2D.Force);
        }
    }

    private void checkIfScared()
    {
        floorRay = Physics2D.Raycast(groundPoint.position, -Vector2.up, 1, groundLayer);
        //draw ray
        Debug.DrawRay(groundPoint.position, -Vector2.up * floorRay.distance, Color.cyan);
        if (floorRay.collider != null)
        {
            if (scared > 0)
            {
                scared -= Time.deltaTime * scaredMultiplyer;
            }
        }
        else
        {
            scared += Time.deltaTime * scaredMultiplyer * (1 + (int)groundDistance / 10);
        }

        if (scared >= 100)
        {
            Die();
        }
    }
    public void Die()
    {
        dead = true;
        GameCanvas.instance.EnableDeathScreen();
    }
}
