using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maxHealth = 100f;
    PlayerMovement player;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        currentHealth = player.health;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
