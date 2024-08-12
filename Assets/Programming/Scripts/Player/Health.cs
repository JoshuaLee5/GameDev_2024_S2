using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [AddComponentMenu("GameDev/Player/Health")]
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth, currentHealth, regenValue;
        [SerializeField] Image displayImage;
        [SerializeField] Gradient gradientHealth;
        [SerializeField] Transform spawnPoint;
        private float timerValue;
        private bool canHeal = true;

        public void DamagePlayer(float damageValue)
        {
            timerValue = 0;
            canHeal = false;
            currentHealth -= damageValue;
            UpdateUI();
        }

        public void HealPlayer(float HealValue)
        {
            currentHealth -= HealValue;
            UpdateUI();
        }

        void UpdateUI() 
        {
            displayImage.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
            displayImage.color = gradientHealth.Evaluate(displayImage.fillAmount);
        }

        void Respawn()
        {
            if (currentHealth <= 0)
            {
                GetComponent<CharacterController>().enabled = false;

                this.transform.position = spawnPoint.position;
                this.transform.rotation = spawnPoint.rotation;
                currentHealth = maxHealth;
                UpdateUI();
                GetComponent<CharacterController>().enabled = true;
            }           

        }

        void HealOverTime()
        {
            if (canHeal)
            {
                if (currentHealth < maxHealth && currentHealth > 0)
                {
                    //current health to increase by a value over time
                    currentHealth += regenValue * Time.deltaTime;
                    UpdateUI();
                }
            }
            
        }

        void timer()
        {
            if (!canHeal) 
            {
                timerValue += Time.deltaTime;
                if (timerValue >= 1.5f)
                {
                    //allow healing
                    canHeal = true;
                    //reset timer
                    timerValue = 0;

                }
            }
        }

        #region Unity Event Functions

        private void Start () 
        {
            currentHealth = maxHealth;
            displayImage.fillAmount = 1;
        }

        private void Update () 
        {
            //displayImage.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
            //displayImage.color = gradientHealth.Evaluate(displayImage.fillAmount);

            HealOverTime();
            Respawn();
            timer();
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Damage"))
            {
                //Do a thing!!
                DamagePlayer(10);
                Debug.Log("Take Damage");
            }

            //if (collision.gameObject.CompareTag("Safe Zone"))
            //{
            //    //Do a thing!!
            //    DamagePlayer(10);
            //    Debug.Log("Take Damage");
            //}

        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            regenValue *= 2;

        }
        private void OnTriggerExit(Collider other)
        {
            regenValue /= 2;

        }
    }

}
