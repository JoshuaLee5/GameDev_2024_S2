using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [AddComponentMenu("GameDev/Player/Misc/PressT2takeDamage")]
    public class PressT2takeDamage : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Space key was pressed.");
            }

            if (Input.GetKeyUp(KeyCode.T))
            {
                Debug.Log("Space key was released.");
            }
        }
    }
}




