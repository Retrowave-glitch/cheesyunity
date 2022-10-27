using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float iHealth = 100.0f;
    public int iMaxHealth = 100;
    public float fMoveSpeed = 1.0f;
    public int iStamina = 100;
    private Effects effect = new Effects();
    private void Start()
    {
        effect.clear();
    }
    public class Effects
    {
        //Effects.... reference terraria debuffs
        public Effect Stun = new Effect(); //Can not move
        public Effect Hated = new Effect(); //Make enemy avoid you
        public Effect OnFire = new Effect(); //Health decreasing
        public Effect OverHealth= new Effect(); // Increase Health with Duration. taking damage less duration
        public Effect Wet = new Effect(); //Slow movement
        public Effect OutStamina = new Effect(); //Happens when out of Stamina, can not regen stamina and can not sprint
        public void clear()
        {
            Stun.clear();
            Hated.clear();
            OnFire.clear();
            OverHealth.clear();
            Wet.clear();
            OutStamina.clear();
        }
        public void DecreaseEffects()
        {
            if (!isEffected()) return;
            Stun.DecreaseEffect();
            Hated.DecreaseEffect();
            OnFire.DecreaseEffect();
            OverHealth.DecreaseEffect();
            Wet.DecreaseEffect();
            OutStamina.DecreaseEffect();
        }
        public bool isEffected()
        {
            return (
                Stun.isEffected() ||
                Hated.isEffected()||
                OnFire.isEffected()||
                OverHealth.isEffected()||
                Wet.isEffected()||
                OutStamina.isEffected()
                );//check one of effects are on
        }
    }
    public class Effect
    {
        private int Duration=0;
        public void add(int Duration_)
        {
            Duration += Duration_;
        }
        public void set(int Duration_)
        {
            Duration = Duration_;
        }
        public void clear()
        {
            this.Duration = 0;
        }
        public void DecreaseEffect()
        {
            if (!isEffected()) return;
            this.Duration -= 1;
        }
        public bool isEffected() { return (this.Duration > 0); }
    }
}
