using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region Unity_functions
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    #region Scene_transisitons
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    #endregion
}
