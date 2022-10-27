using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar_Script : MonoBehaviour
{
    private Image HealthBar;
    public float CurrentHealth;
    private float MaxHealth = 100.0f;
    PlayerStatus Player;

    private void Start()
    {
        HealthBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerStatus>();
    }

    private void Update()
    {
        CurrentHealth = Player.iHealth;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;   
    }
}
