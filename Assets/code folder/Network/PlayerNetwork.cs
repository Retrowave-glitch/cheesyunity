using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityEngine.UI;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    public String PlayerName;
    private readonly NetworkVariable<Vector3> _netPos = new NetworkVariable<Vector3>(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _netRot = new NetworkVariable<Quaternion>(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Unity.Collections.FixedString64Bytes> _netName = new NetworkVariable<Unity.Collections.FixedString64Bytes>(writePerm: NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn() //When Player Spawned by network
    {
        if (IsOwner)
        {
            PlayerName = GameManager.Instance.PlayerName;
            //Change Main camera focus on Player Character
            Camera camera = Camera.main;
            camera.GetComponent<CameraManager>().target = this.transform;
            camera.GetComponent<CameraManager>().ChangeCamera(Cameras.PlayerCamera);
            SetPlayerNameServer(PlayerName);
        }
        if (IsServer)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
            //if one more player login
            if (gos.Length > 0)
            {
                GameObject objectgenerator = GameObject.FindGameObjectWithTag("ObjectGenerator");
               // objectgenerator.GetComponent<ObjectGenerator>().SpawnCheeseRandomPos();
                objectgenerator.GetComponent<ObjectGenerator>().SpawnCatRandomPos();
            }
        }
    }
    private void SetPlayerNameServer(string playerName)
    {
        _netName.Value = playerName;
    }
    private void Start()
    {

        _netName.OnValueChanged += OnChangePlayerName;
        PlayerName = _netName.Value.ToString();
    }
    private void OnChangePlayerName(FixedString64Bytes previousValue, FixedString64Bytes newValue)
    {
        PlayerName = _netName.Value.ToString();
    }

    private void Update()
    {
        if (IsOwner)
        {
            _netPos.Value = transform.position;
            _netRot.Value = transform.rotation;
        }
        {
            transform.position = _netPos.Value;
            transform.rotation = _netRot.Value;
        }
    }
}
