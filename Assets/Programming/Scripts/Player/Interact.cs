using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

//Consider the Following
//namespace
//componet menu
//restriction

namespace Player
{
    public class Interact : MonoBehaviour
    {

        public GUIStyle crossHair;
        public LayerMask interactionLayer;
        public bool showToolTip = false;
        public string action, button, instruction;

        //void Start () 
        //{ 
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
        private void Update()
        {

            //create a ray (a Ray is ?? a beam, line that comes into contact with colliders)
            Ray interactRay;

            //This ray shoots forward from the center of the camera
            interactRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            //creats hit info (this holds the info for the stuff we interact with)
            RaycastHit hitInfo;

            //if our interaction button or key is pressed
            if (Physics.Raycast(interactRay, out hitInfo, 10, interactionLayer))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    showToolTip = true;
                    if (hitInfo.collider.CompareTag("RayDoor"))
                    {
                        //do thing
                        if (hitInfo.collider.GetComponent<RayDoor>())
                        {
                            hitInfo.collider.GetComponent<RayDoor>().Interaction();
                        }
                        //i want you to open
                    }
                    #region Long code
                    //if (hitInfo.collider.CompareTag("NPC"))
                    //{
                    //    if (hitInfo.collider.GetComponent<IMGUILG>())
                    //    {
                    //        hitInfo.collider.GetComponent<IMGUILG>().OnInteraction();
                    //    } 
                    //}
                    //if (hitInfo.collider.CompareTag("Chest"))
                    //{ 

                    //}

                    //if (hitInfo.collider.CompareTag("Item"))
                    //{

                    //}

                    //if (hitInfo.collider.CompareTag("Pet"))
                    //{

                    //}

                    //if (hitInfo.collider.CompareTag("Bed"))
                    //{

                    //}

                    //if (hitInfo.collider.CompareTag("Campfire"))
                    //{

                    //}

                    //if (hitInfo.collider.CompareTag("CraftingStation"))
                    //{

                    //}
                    #endregion

                    #region Short Code
                    if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interact))
                    { 
                        //if (hitInfo.collider.GetComponent<RayDoor>))
                        //{
                        //    hitInfo.collider.GetComponent<RayDoor>().Interaction();
                        //}

                        interact.Interaction();
                    }

                    #endregion
                }



            }
            else
            {
                showToolTip = false;
            }

            void OnGUI()
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        GUI.Box(new Rect(x, y, 1, 1), "");
                        GUI.Label(new Rect(x, y, 1, 1), x + ":" + y);

                        //x*Screen.width/16, y * Screen.height / 9, Screen.width / 16, Screen.height / 9
                        //x* Screen.width / 16, y* Screen.height / 9, Screen.width / 16, Screen.height / 9
                    }

                }


                //GUI.Box(new Rect(7.5f * Screen.width / 16, 4 * Screen.height / 9, 0.5f * (Screen.width / 16), 0.5f * (Screen.height)), "");
                GUI.Box(UIPos(7.75f, 4.25f, 0.5f, 0.5f), "", crossHair);
                if (showToolTip ) 
                {
                    GUI.Box(UIPos(6.5f, 3.75f, 3f, 0.5f), $"{action} {button} {instruction}");
                }
            }

            //Optional
            //if(Input.GetMouseButton(0))
            //{ 

            //}

        }
        Rect UIPos(float startX, float startY, float sizeX, float sizeY)
        {
            return new Rect(startX * (Screen.width / 16), startY * (Screen.height / 9), sizeX * (Screen.width / 16), sizeY * (Screen.height / 9));
        }

        //if the physics ray gets cast in a direction hits a object withing out distances and or layers
        //do the this
    }
}



