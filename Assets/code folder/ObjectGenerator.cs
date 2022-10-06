using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

public class ObjectGenerator : NetworkBehaviour
{
    public GameObject CheesePrefab;
    public GameObject CatPrefab;
    private void Awake()
    {
    }

    private void GameManager_OnGameStateChanged(GameState obj)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        if (!IsHost) return;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                SpawnCheese(new Vector3(x, y, 0));
            }
        }
    }
    public void SpawnCheese(Vector3 _Pos)
    {
        if (!IsHost) return;
        GameObject Cheese = Instantiate(CheesePrefab,
                                           _Pos,
                                            Quaternion.identity);
        Cheese.GetComponent<NetworkObject>().Spawn(true);
    }
}