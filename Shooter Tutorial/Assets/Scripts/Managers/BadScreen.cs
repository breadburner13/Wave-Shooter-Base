using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BadScreen : MonoBehaviour
{
    public string sceneToLoad;
    void OnTriggerEnter2D(Collider2D checktouch)
    {
        if(checktouch.CompareTag("Player"))
        {
            Debug.Log("don't touch that mouse");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
