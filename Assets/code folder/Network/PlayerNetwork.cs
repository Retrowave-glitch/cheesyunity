using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerNetwork : NetworkBehaviour
{
    public PlayerManager PlayerManagerpointer;
    private readonly NetworkVariable<Vector3> _netPos = new NetworkVariable<Vector3>(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _netRot = new NetworkVariable<Quaternion>(writePerm: NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            PlayerManagerpointer.isLocalPlayer(true);
            Camera camera = Camera.main;
            camera.GetComponent<CameraManager>().target = this.transform;
            camera.GetComponent<CameraManager>().ChangeCamera(Cameras.PlayerCamera);
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
