using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CoinCounter : MonoBehaviour
{

    public int numberOfCoins=0;
    public TextMeshProUGUI numberOfCoinsText;

    // Start is called before the first frame update
    void Start()
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
