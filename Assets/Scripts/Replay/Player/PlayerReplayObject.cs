using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplayObject : ReplayObject
{
    private Animator anim;
    private bool isDead = false;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.onGoalReached += OnTurnEnded;
    }
    private void OnDestroy()
    {
        GameManager.Instance.onGoalReached -= OnTurnEnded;
    }
    private void OnTurnEnded()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        anim.SetFloat("Speed", 0f);
        anim.SetTrigger("Dead");
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
            OnTurnEnded();

        }
        Vector3 scaler = this.transform.localScale;
        scaler.x = Mathf.Abs(scaler.x) * (playerData.facingRight ? 1f : -1f);
        this.transform.localScale = scaler;
    }
   
}
