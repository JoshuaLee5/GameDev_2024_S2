using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputTriggerDoor : MonoBehaviour
{
    [SerializeField] bool _canInteract = false;
    [SerializeField] Animator _anim;
    [SerializeField] bool _isOpen = false;
    
    //Start is called before the first frame update
    void Start()
    {
        _canInteract = false;
        _anim = GetComponent<Animator>();
        _isOpen = false;
        _anim.SetBool("Open", _isOpen);
    }

    // Update is called once pre frame
    void Update()
    {
        if (_canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            { 
                _isOpen = !_isOpen;
                _anim.SetBool("Open", _isOpen);
            }
        }
    }

    //when in the trigger
    private void OnTriggerEnter(Collider other)
    {
        //we can interact
        _canInteract = true;
    }
}
