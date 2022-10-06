using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
public class NetworkTrigger : MonoBehaviour
{

    private void Update()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            switch (GameManager.Instance.GetNetworkType())
            {
                case NetworkType.Host:
                    GameManager.Instance.UpdateGameState(GameState.WaitingPlayer);
                    NetworkManager.Singleton.StartHost();
                    break;
                case NetworkType.Server://doesnt made 
                    NetworkManager.Singleton.StartServer();
                    break;
                case NetworkType.Client:
                    var utp = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
                    utp.SetConnectionData(GameManager.Instance.ipaddress, (ushort)GameManager.Instance.port);
                    NetworkManager.Singleton.StartClient();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}