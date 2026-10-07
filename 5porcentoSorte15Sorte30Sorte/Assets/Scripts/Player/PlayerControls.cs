using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerControls : MonoBehaviour
{
    [Header("Move Controls")]
    [SerializeField] private float moveSpeed;

    [Header("Jump Controls")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpTimeDuration;
    [SerializeField] private Transform sensorGround;
    [SerializeField] private Vector3 sensorSize;
    [SerializeField] private LayerMask Ground;

    private bool onGround;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float currentJumpTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(sensorGround.position, sensorSize);
    }
    
    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        OnMove();
        OnJump();
    }

    void Move()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f) * moveSpeed;

        if(direction.x > 0)
        {
            sr.flipX = false;
        }
        else if (direction.x < 0)
        {
            sr.flipX= true;
        }
    }

    void OnMove()
    {
        rb.linearVelocity = new Vector2(direction.x, rb.linearVelocityY);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && Grounded() == true)
        {
            currentJumpTime = jumpTimeDuration;
        }
        else if (Input.GetButton("Jump"))
        {
            currentJumpTime -= Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            currentJumpTime = 0;
        }
    }

    void OnJump()
    {
        if (currentJumpTime > 0)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    bool Grounded()
    {
        return Physics2D.OverlapBox(sensorGround.position, sensorSize, 0, Ground);
    }

    private void OnTriggerEnter(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            Destroy (collision.gameObject, 0.2f);
        }
    }

    public int MoveValue()
    {   
        return (int)direction.x;
    }

    public int Jumping()
    {
        return (int)direction.y;
    }

    public int Falling()
    {
        return(int)direction.y;
    }
}
