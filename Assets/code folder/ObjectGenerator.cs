using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

public class ObjectGenerator : NetworkBehaviour
{
    public GameObject CheesePrefab;
    public GameObject CatPrefab;
    public void SpawnCheese(Vector3 _Pos)
    {
        GameObject Cheese = Instantiate(CheesePrefab,
                                           _Pos,
                                            Quaternion.identity);
        var networkObj = Cheese.GetComponent<NetworkObject>();
        networkObj.Spawn(true);
    }
    public void SpawnCheeseRandomPos()
    {
        Vector3 _Pos = new Vector3(5.0f, 5.0f, 0.0f);
        GameObject Cheese = Instantiate(CheesePrefab,
                                           _Pos,
                                            Quaternion.identity);
        var networkObj = Cheese.GetComponent<NetworkObject>();
        networkObj.Spawn(true);
    }
    public void SpawnCatRandomPos()
    {
        Vector3 _Pos = new Vector3(5.0f, 5.0f, 0.0f);
        GameObject Cat = Instantiate(CatPrefab,
                                           _Pos,
                                            Quaternion.identity);
        var networkObj = Cat.GetComponent<NetworkObject>();
        networkObj.Spawn(true);
    }
}