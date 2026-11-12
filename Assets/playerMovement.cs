

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public static playerMovement Instance{get; private set;}
    [Header("forces")]
    public float movespeed;
    //public float jumpForce = 10f;

    public Rigidbody2D rb;
    public Animator anim;
    private bool facingRight = true;
    public Vector2 moveVelocity;
    public Timer timer;
    public bool IsDead = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
       // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        
        moveVelocity = moveInput.normalized * movespeed;
        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(moveInput.x < 0 && facingRight)
        {
            Flip();
        }
        anim.SetFloat("Speed", moveVelocity.magnitude);
        if (moveInput.magnitude > 0)
        {
           if(!timer.isStarted)
            {
                timer.isStarted = true;
            }
        }
    }
    }
    
    private void FixedUpdate()
    {
      rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void Flip()
    {
       facingRight = !facingRight;
        Vector3 scaler = rb.transform.localScale;
        scaler.x *= -1;
        rb.transform.localScale = scaler;
    }
}
