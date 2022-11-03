using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinCounter coinCounter;

    private void Awake()
    {
        GameObject coinCounterObj = GameObject.FindWithTag("CoinCounter");
        coinCounter = coinCounterObj.GetComponent<CoinCounter>();
    }
    // Update is called once per frame
    void Update()
    {
        coinCounter.AddCoin();
        Destroy(gameObject);
    }
}
