using UnityEngine;

//this is our family for the scripts.
namespace Player
{
    [AddComponentMenu("GameDev/Player/First Person Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        #region Variables
        //the direction we are moving
        [SerializeField] Vector3 _moveDirection;
        //the reference to the CharacterController
        [SerializeField] CharacterController _characterController;
        //walk, crouch, sprint, jump, gravity
        [SerializeField] float _movementSpeed, _walk = 5,_run = 10,_crouch = 2.5f,_jump = 8, _gravity = 20;
        #endregion

        #region Function
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }
        private void Update() 
        {
            //ture
            if (GameManager.Instance.gameState == GameManager.GameState.Playing)
            {
                //speed change
                //_movementSpeed, walk, run, crouch, jump
                //Left Shift and Left Control

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _movementSpeed = _run;
                }

                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    _movementSpeed = _crouch;
                }
                else
                {
                    _movementSpeed = _walk;                
                }

                //if(Input.GetKeyDown("C"))
                //{
                //    _movementSpeed = _crouch;
                //}
                //if (Input.GetKeyDown("Space"))
                //{ 
                //}



                //if(Input.GetKeyDown("Left Shift"))
                //{
                //    _movementSpeed = _run;
                //}

                //moving the character
                //if our refernce to the character controller has a value aka we connected it yay!!! woop
                if (_characterController != null)
                {
                    //check of we are on the ground so we can move coz thats how people work
                    if (_characterController.isGrounded)
                    {
                        //what is our direction? set the move direction based off Inputs
                        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                        //make sure that the direction forward is according to the players forward and not the world north
                        _moveDirection = transform.TransformDirection(_moveDirection);
                        //apply speed to the movement Direction
                        _moveDirection *= _movementSpeed;

                        //_moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movementSpeed);
                        
                        //if we jump
                        if (Input.GetButton("Jump"))
                        {
                            //move up
                            _moveDirection.y = _jump;
                        }
                    }
                    //apply gravity ti direction.
                    _moveDirection.y -= _gravity * Time.deltaTime;
                    // APPLY movement
                    _characterController.Move( _moveDirection * Time.deltaTime);
                }
                else
                {
                    Debug.LogWarning("!!!Missing Character Controller Connection for the player");
                }
            }
        }
        #endregion
    }

}