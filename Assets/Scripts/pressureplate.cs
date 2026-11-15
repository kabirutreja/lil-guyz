

using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressureplate : MonoBehaviour
{
    //public Vector3 originalPos;
    private Animator anim;
    public Animator animDoor;
    private int objectsOnPlate = 0;
    private HashSet<Collider2D> activeColliders = new HashSet<Collider2D>();
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.onPlayerRespawn += OnPlayerRespawn;
        GameManager.Instance.onRestartLevel += OnPlayerRespawn;
    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onPlayerRespawn -= OnPlayerRespawn;
            GameManager.Instance.onRestartLevel -= OnPlayerRespawn;
        }
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
    {
        activeColliders.Add(other);
        UpdatePlate();
    }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
    {
        activeColliders.Remove(other);
        UpdatePlate();
       
        
    }
    }
    private void Update()
    {
        if (activeColliders.Count > 0)
        {
            int beforeCount = activeColliders.Count;
            activeColliders.RemoveWhere(c => c == null || !c.enabled || !c.gameObject.activeInHierarchy);
            if (activeColliders.Count != beforeCount)
            {
                UpdatePlate();
            }
        }
    }
    private void UpdatePlate()
    {
        anim.SetBool("Pressed",activeColliders.Count >0);
        animDoor.SetBool("Pressed",activeColliders.Count > 0);
    }

      private void OnPlayerRespawn()
    {
        activeColliders.Clear();
        anim.SetBool("Pressed", false);

        
    }
}
