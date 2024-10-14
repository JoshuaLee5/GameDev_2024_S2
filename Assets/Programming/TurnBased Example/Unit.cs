using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBased
{
    public class Unit : MonoBehaviour
    {
        public Sprite unitIcon;
        public string unitName;
        public string unitDescription;
        public string unitAction;
        public int unitLevel;
        public int damage;
        public int maxHealth;
        public float currentHealth;

        //when running TakeDamage pass a damage value in for calculations
        public bool TakeDamage(int damage)
        { 
            currentHealth -= damage;
            //if that kill us
            if (currentHealth <= 0)
            {
                //say that kill us
                return true;
            }
            else
            {
                //else say it didn't kill us
                return false;
            }
        }
        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
    
}
