

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressureplate : MonoBehaviour
{
    public Vector3 originalPos;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("pp pressed");
    }
}
    private void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("pp not pressed");
    }
}
}
