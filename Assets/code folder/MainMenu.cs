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
        GameManager.Instance.SetNetworkType(NetworkType.Host);
        PlayGame();
    }
    public void JoinGame()
    {
        GameManager.Instance.SetNetworkType(NetworkType.Client);
        PlayGame();
    }
    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene (aka the main game)
    }
    public void QuitGame()
    {
        Application.Quit(); //for the actual game
        UnityEditor.EditorApplication.isPlaying = false; //for the unity player
    }
}
