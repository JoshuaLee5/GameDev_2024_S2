using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI: MonoBehaviour
{
    public GameObject NPCDialogue;
    public Text Dialogue1;
    public Text Dialogue2;
    public Text Dialogue3;
    public Text Dialogue4;
    public Text Dialogue5;

    public Transform t_cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(t_cam.position, t_cam.forward, out hit, 3))
            {
                Debug.Log(hit.collider.gameObject.name);
            }

            if (Physics.Raycast(t_cam.position, t_cam.forward, out hit, 3))
            {
                if (hit.collider.gameObject.CompareTag("NPC 2"))
                {
                    NPCDialogue.SetActive(true);
                }
            }

        }
        if (Input.GetKey(KeyCode.R))
        {
            NPCDialogue.SetActive(false);
        }
    }



}
