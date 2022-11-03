using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;
    public Tilemap ground;
    public Tilemap obstacle;
    void CheckInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Awake()
    {
        CheckInstance();
    }
}
