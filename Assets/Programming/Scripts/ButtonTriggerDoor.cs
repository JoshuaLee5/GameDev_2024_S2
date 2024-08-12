using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ButtonTriggerDoor : MonoBehaviour
{
    [SerializeField] bool _canInteract = false;
    [SerializeField] Animator _anim;
    [SerializeField] bool _isOpen = false;
    //[SerializeField] float _timer = 7f;
    [SerializeField] GameObject _Button;
    [SerializeField] Animator _Door;

    // Start is called before the first frame update
    void Start()
    {
        _canInteract = false;
        _anim = GetComponent<Animator>();
        _isOpen = false;
        _anim.SetBool("Open", _isOpen);
        //_timer = 7.5f;
    }

    // Update is called once per frame
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

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
               if (other.tag == "PLAYER")
               {
                    Debug.Log("Door is open");
                    //_anim.SetBool("Open", !true);
                    _isOpen = !_isOpen;
                    _anim.SetBool("Open", _isOpen);
                    _anim.Play("Open");

               }
        }
    }
    //When button is pressed
        //Open Door
        //Start Timer = 3f
        //Show Timer
        //Timer End
        //Door Close

}
