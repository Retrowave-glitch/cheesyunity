using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Hosting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
//using static System.Net.Mime.MediaTypeNames;

public class MainMenu : MonoBehaviour
{
    public void HostGame()
    {
        PlayGame();
    }
    public void JoinGame()
    {
        PlayGame();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene (aka the main game)
    }
    public void QuitGame()
    {
        Application.Quit(); //for the actual game
        UnityEditor.EditorApplication.isPlaying = false; //for the unity player
    }
}
