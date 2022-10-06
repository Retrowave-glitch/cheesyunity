using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class ItemCollector : MonoBehaviour
{
    private int Cheese = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cheese"))
        {
            collision.GetComponent<NetworkObject>().Despawn(true);
            Destroy(collision.gameObject);
            Cheese++;
        }
    }
}
