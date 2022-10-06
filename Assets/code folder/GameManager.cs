using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Object Cheese;
    bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void StartGame()
    {
        if (!isStart)
        {
            isStart = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
