

using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressureplate : MonoBehaviour
{
    //public Vector3 originalPos;
    private Animator anim;
    private int objectsOnPlate = 0;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.onPlayerRespawn += OnPlayerRespawn;
        GameManager.Instance.onRestartLevel += OnPlayerRespawn;
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        objectsOnPlate ++;
        anim.SetBool("Pressed", true);
    }
}
private void OnPlayerRespawn()
    {
        objectsOnPlate = 0;
        anim.SetBool("Pressed", false);

        
    }
    private void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        objectsOnPlate = Mathf.Max(0, objectsOnPlate - 1);
        if (objectsOnPlate == 0)
        {
            anim.SetBool("Pressed", false);
        }
       
        
    }
}
}
