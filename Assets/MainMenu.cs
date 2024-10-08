using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        UserState.Reset();
        SceneManager.LoadScene("DayScene");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit!");
        // Application.Quit();
    }
}
