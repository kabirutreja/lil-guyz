using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplayObject : ReplayObject
{
    private Animator anim;
    private Collider2D coll;

    private bool isDead = false;
    private float oglife = 10f;
    private float elapsedTime = 0f;
    private Timer timer;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider2D>();
    }
    private void Start()
    {
        GameManager.Instance.onGoalReached += OnTurnEnded;
    }
    private void OnDestroy()
    {
        GameManager.Instance.onGoalReached -= OnTurnEnded;
    }
    public void SetupLifespan(float lifespan, Timer objTimer)
    {
        this.oglife = lifespan;
        this.timer = objTimer;
        this.elapsedTime = 0f;
        this.isDead = false;
        if (coll!=null)
        {
            coll.enabled = true;
        }
        
        
    }
    private void Update()
    {
        if (isDead || timer == null || !timer.isStarted)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= oglife)
        {
            DieEarly();
        }
    }
    private void DieEarly()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        anim.SetFloat("Speed", 0f);
        anim.SetTrigger("Dead");

        if (coll!=null)
        {
            coll.enabled = false;
        }
        
    }
    private void OnTurnEnded()
    {
        DieEarly();
    }
    public override void SetDataForFrame(ReplayData data)
    {
        if (isDead)
        {
            return;
        }
        PlayerReplayData playerData = (PlayerReplayData) data;
        this.transform.position = playerData.position;
        anim.SetFloat("Speed", playerData.movement.magnitude);
        if (playerData.deathThisFrame)
        {
            DieEarly();

        }
        Vector3 scaler = this.transform.localScale;
        scaler.x = Mathf.Abs(scaler.x) * (playerData.facingRight ? 1f : -1f);
        this.transform.localScale = scaler;
    }
   
}
