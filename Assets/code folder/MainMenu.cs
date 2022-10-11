using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Hosting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class MainMenu : MonoBehaviour
{
    private string PlayerName = "DefaultName";
    public InputField NameField;
    private void Start()
    {
       // NameField = GameObject.Find("NameField").GetComponent<InputField>();
    }
    public void HostGame()
    {
        GameManager.Instance.SetNetworkType(NetworkType.Host);
        PlayGame();
    }
    public void JoinGame()
    {
        GameManager.Instance.SetNetworkType(NetworkType.Client);
        GameManager.Instance.ipaddress = "127.0.0.1";
        GameManager.Instance.port = 7777;
        PlayGame();
    }
    private void PlayGame()
    {
        GameManager.Instance.PlayerName = PlayerName;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene (aka the main game)
    }
    public void QuitGame()
    {
        Application.Quit(); //for the actual game
        UnityEditor.EditorApplication.isPlaying = false; //for the unity player
    }
    public void InputName()
    {
        PlayerName = NameField.text;
        Debug.Log(PlayerName);
    }
}
