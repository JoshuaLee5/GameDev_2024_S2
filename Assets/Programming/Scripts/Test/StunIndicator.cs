using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StunIndicator : MonoBehaviour
{
    [SerializeField] Text cooldowntext;
    [SerializeField] Image displayimageStun;
    [SerializeField] Image displayimageUsedStun;
    [SerializeField] private float MaxStun;
    [SerializeField] private float MinStun;



    private float timerValue;








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    void timer()
    {

    }


}
