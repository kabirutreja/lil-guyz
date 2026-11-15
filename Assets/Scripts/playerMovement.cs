
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //public static playerMovement Instance{get; private set;}
    [Header("forces")]
    public float movespeed;
    private Vector3 defaultSpawnPos;
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
    public DemoControl dc;
    
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
        GameManager.Instance.onRestartLevel -= Respawn;
    //GameManager.Instance.onPlayerRespawn -= Respawn;
    }
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
       // anim = GetComponent<Animator>();
       defaultSpawnPos = transform.position;
       GameManager.Instance.onRestartLevel += Respawn;
    //GameManager.Instance.onPlayerRespawn += Respawn;
    }
    public void ResetPlayer()
    {
    transform.position = defaultSpawnPos;
    rb.position = defaultSpawnPos;
    rb.velocity = Vector2.zero;
    moveVelocity = Vector2.zero;

    IsDead = false;
    deathhandled = false;
    deathThisFrame = false;
    coll.enabled = true;
    anim.Play("Idle",0,0f);
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
             //StartCoroutine(WaitRoutine());
             moveVelocity = Vector2.zero;
             
    }
    
    }
    
 
    
    private void FixedUpdate()
    {
      
      if(!IsDead && timer.isStarted)
        {
            
        
      ReplayData data = new PlayerReplayData(this.transform.position,rb.velocity,facingRight,deathThisFrame);
        recorder.RecordReplayFrame(data);
        deathThisFrame = false;
        }
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
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
    rb.position = defaultSpawnPos;
    rb.velocity = Vector2.zero;
    moveVelocity = Vector2.zero;
    IsDead = false;
    deathThisFrame = false;
    anim.ResetTrigger("Dead");
    anim.Play("Idle",0,0f);
    anim.Update(0f);
    deathhandled = false;
    coll.enabled = true;
    GameManager.Instance.PlayerRespawn();
    transform.position = defaultSpawnPos;
    GameManager.Instance.ChangeCameraTarget(this.gameObject);
    timer.ResetTimer();
    dc.RandomizeCharacter();
    }
}
