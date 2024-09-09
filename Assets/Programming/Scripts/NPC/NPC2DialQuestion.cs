using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2DialQuestion : MonoBehaviour, IInteractable
{
    [SerializeField] string[] _lines;
    [SerializeField] string _name;
    [SerializeField] Sprite _face;

    public void Interaction()
    {
        DialogueManager.instance.OnActive(_lines, _name, _face);
    }
}
