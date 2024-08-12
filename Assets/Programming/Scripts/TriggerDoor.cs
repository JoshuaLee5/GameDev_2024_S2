using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("Open", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        _anim.SetBool("Open", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _anim.SetBool("Open", false);
    }
}
