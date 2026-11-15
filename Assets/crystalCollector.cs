using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crystalCollector : MonoBehaviour
{
    public GameObject crystal;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Crystal"))
        {
            Destroy(crystal);
            SceneManager.LoadScene("endScreen");
        }
    }
    
}
