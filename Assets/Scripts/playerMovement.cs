
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //public static playerMovement Instance{get; private set;}
    [Header("forces")]
    public float movespeed;
    //public float jumpForce = 10f;

    private Rigidbody2D rb;
    public Animator anim;
    private bool facingRight = true;
    public Vector2 moveVelocity;
    public Timer timer;
    public bool IsDead = false;
    bool deathhandled = false;
    private Collider2D coll;
    private Recorder recorder;
    public bool deathThisFrame = false;
    
    // Start is called before the first frame update
    void Awake()
    {
       // if (Instance != null && Instance != this)
       // {
        //    Destroy(gameObject);
       //     return;
        //}
        //Instance = this;
        coll = GetComponent<CapsuleCollider2D>();
        recorder = GetComponent<Recorder>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void OnDestroy()
    {
       // if (Instance == this)
        //{
        //    Instance = null;
        //}
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
                //GameManager.Instance.GoalReached();
            }
        }
    }
    if (IsDead && !deathhandled)
    {
        deathhandled = true;
        rb.velocity = Vector3.zero;
             coll.enabled = false;
             StartCoroutine(WaitRoutine());
             moveVelocity = Vector2.zero;
             
    }
    
    }
     IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(1f);
        Respawn();
    }
    private void  LateUpdate()
    {
        
    }
    
    private void FixedUpdate()
    {
      rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
      ReplayData data = new PlayerReplayData(this.transform.position,rb.velocity,facingRight,deathThisFrame);
        recorder.RecordReplayFrame(data);
        deathThisFrame = false;
    }
    private void Flip()
    {
       facingRight = !facingRight;
        Vector3 scaler = rb.transform.localScale;
        scaler.x *= -1;
        rb.transform.localScale = scaler;
    }
    private void Respawn()
    {
        
    }
}
