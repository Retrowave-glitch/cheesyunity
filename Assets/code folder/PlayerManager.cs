using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private string playerID;
    private bool bLocalPlayer = false;
    public void setPlayerID(string _playerID) {
        playerID = _playerID;
    }
    public void isLocalPlayer(bool _bLocalPlayer)
    {
        bLocalPlayer = _bLocalPlayer;
    }
}
