using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform spawnedObjectPrefab;
    private Transform spawnedObjectTransform;
    private NetworkVariable<MyCustomData> CustomData = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            iGamemode = 1,
            isStart = true,
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    struct MyCustomData:INetworkSerializable
    {
        public int iGamemode;
        public bool isStart;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref iGamemode);
            serializer.SerializeValue(ref isStart);
        }
    }
    private readonly NetworkVariable<Vector3> _netPos = new NetworkVariable<Vector3>(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _netRot = new NetworkVariable<Quaternion>(writePerm: NetworkVariableWritePermission.Owner);
    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        CustomData.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
         {

         };
    }
    private void Update()
    {
        if (IsOwner)
        {
            _netPos.Value = transform.position;
            _netRot.Value = transform.rotation;
            if (Input.GetKeyDown(KeyCode.T))
            {
                spawnedObjectTransform = Instantiate(spawnedObjectPrefab);
                spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                spawnedObjectTransform.GetComponent<NetworkObject>().Despawn(true);
                Destroy(spawnedObjectTransform.gameObject);
            }
        }
        {
            transform.position = _netPos.Value;
            transform.rotation = _netRot.Value;
        }
    }
}
