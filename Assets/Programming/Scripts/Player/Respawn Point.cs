using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("GameDev/Player/Respawn Point")]
    
    
    public class RespawnPoint : MonoBehaviour
    {
        GameObject spawnPoint;
        //[SerializeField] float currentHealth;

        private void Update()
        {
            if(CompareTag("Respawn Point"))
            {
                //currentHealth = 0;
                transform.position = spawnPoint.transform.position;
            }

        }


    }

}


