using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinCounter coinCounter;

    // Update is called once per frame
    void Update()
    {
        coinCounter.AddCoin();
        Destroy(gameObject);
    }
}
