using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CoinCounter : MonoBehaviour
{

    public int numberOfCoins;
    public TextMeshProUGUI numberOfCoinsText;

    private void Awake()
    {
        numberOfCoins = 0;
        numberOfCoinsText.text = numberOfCoins.ToString();
    }

    // Update is called once per frame
   public void AddCoin()
    {
        numberOfCoins++;
        numberOfCoinsText.text = numberOfCoins.ToString();
    }
}
