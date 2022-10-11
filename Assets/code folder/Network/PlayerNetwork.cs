using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerNetwork : NetworkBehaviour
{
    public string NetworkID;
    public string PlayerName;
    private readonly NetworkVariable<Vector3> _netPos = new NetworkVariable<Vector3>(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _netRot = new NetworkVariable<Quaternion>(writePerm: NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn() //When Player Spawned by network
    {
        if (IsOwner)
        {
            PlayerName = GameManager.Instance.PlayerName;
            //Change Main camera focus on Player Character
            Camera camera = Camera.main;
            camera.GetComponent<CameraManager>().target = this.transform;
            camera.GetComponent<CameraManager>().ChangeCamera(Cameras.PlayerCamera);
        }
        if (IsServer)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
            if (gos.Length > 0)
            {
                GameObject objectgenerator = GameObject.FindGameObjectWithTag("ObjectGenerator");
                objectgenerator.GetComponent<ObjectGenerator>().SpawnCheeseRandomPos();
                objectgenerator.GetComponent<ObjectGenerator>().SpawnCatRandomPos();
            }
        }
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
