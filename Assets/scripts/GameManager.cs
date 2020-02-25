using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region unity_functions
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }
    #endregion

    #region scene_transitions
    public void WinGame()
    {
        SceneManager.LoadScene("Gates");
    }
    public void LoseGame()
    {
        SceneManager.LoadScene("YouDead");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("donaldo_trump");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    #endregion
}
