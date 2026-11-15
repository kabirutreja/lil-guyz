using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void endscene()
    {
        SceneManager.LoadScene("endScreen");
    }
    public void startscene()
    {
        SceneManager.LoadScene("Start");
    }

    public void game()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
