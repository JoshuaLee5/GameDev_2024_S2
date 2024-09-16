using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    #region Singleton
    //there can be only one!
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //if the instance ref
        else if (instance != null && instance != this)
        {
            //Destory the imposter
            Destroy(this);
        }
    }
    #endregion

    #region Dialogue UI Variables
    [SerializeField] GameObject _DialogueBox;
    [SerializeField] Text _DialogueText;
    [SerializeField] Image _displayPicture;
    [SerializeField] Text _name; //The 
    //[SerializeField] GameObject _next;
    //[SerializeField] GameObject _exit; //Exit Button we turn on and off.
    [SerializeField] Text _input;
    [SerializeField] GameObject _noButton;
    #endregion

    #region Dialogue Variables
    //lines to read
    [SerializeField] string[] dialogueLines;
    //current line
    [SerializeField] int currentIndex = 0;
    [SerializeField] int questionIndex = -1;
    #region Approval
    [SerializeField] ApprovalDialogueLines _approvalDialogueLines;
    [SerializeField] DialogueApproval _currentApproval;
    #endregion

    #endregion



    public void OnActive(string[] lines, string name, Sprite dp)
    {
        dialogueLines = lines;
        currentIndex = 0;
        _input.text = "Next";
        _displayPicture.sprite = dp;
        _name.text = name;



        _DialogueBox.SetActive(true);
        GameManager.Instance.ChangeState(GameManager.GameState.Menu);
        _DialogueText.text = dialogueLines[currentIndex];
    }

    public void OnActive(string[] lines, string name, Sprite dp, int index)
    { 
        _DialogueBox.SetActive (true);
        _noButton.SetActive(false);

        dialogueLines = lines;
        currentIndex = 0;
        _input.text = "Next";
        if (lines.Length <= 1)
        {
            _input.text = "Bye!";
        }
        
        _displayPicture.sprite = dp;
        _name.text = name;
        questionIndex = index;

        GameManager.Instance.ChangeState(GameManager.GameState.Menu);
        _DialogueText.text = dialogueLines[currentIndex];

    }


    public void OnActive(ApprovalDialogueLines lines, string name, Sprite dp, int index, DialogueApproval approval)
    {
        _DialogueBox.SetActive(true);
        _noButton.SetActive(false);

        _approvalDialogueLines = lines;
        currentIndex = 0;
        _input.text = "Next";
        if (lines.neutralLines.Length <= 1)
        {
            _input.text = "Bye!";
        }

        _displayPicture.sprite = dp;
        _name.text = name;
        questionIndex = index;

        GameManager.Instance.ChangeState(GameManager.GameState.Menu);
        //We need a switch Statement Function to select the correct shiz
        //that will go here
        ChangeApproval();
        _DialogueText.text = dialogueLines[currentIndex];
    }
    
    void OnDeActive()
    {
        _DialogueBox.SetActive(false);
        _noButton.SetActive(false);
        _currentApproval = null;
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

    void ChangeApproval()
    {
        if(_currentApproval == null)
        {
            return;
        }
        
        switch (_currentApproval.approvalValve)
        {
            case -1:
                dialogueLines = _approvalDialogueLines.dislikeLines;
                break;
            case 0:
                dialogueLines = _approvalDialogueLines.neutralLines;
                break;
            case 1:
                dialogueLines = _approvalDialogueLines.likedLines;
                break;

            default:
                Debug.Log("Approval is Broken");
                break;
        }
    }

    public void Input()
    {
        if (currentIndex < dialogueLines.Length - 2 && !(currentIndex == questionIndex - 2 || currentIndex == questionIndex -1))
        {
            _input.text = "Next";
            if (_noButton.activeSelf == true)
            {
                _noButton.SetActive(false);
            }
            currentIndex++;
        }
        else if (currentIndex < dialogueLines.Length - 2 && currentIndex == questionIndex - 2)
        {
            _input.text = "yes";
            _noButton.SetActive(true);
            currentIndex++;
        }
        else if (currentIndex == questionIndex - 1)
        {
            _input.text = "next";
            if (_noButton.activeSelf == true)
            { 
                _noButton.SetActive (false);
            }
            currentIndex++;
            if (_currentApproval != null)
            {
                if (_currentApproval.approvalValve < 1)
                {
                    _currentApproval.approvalValve++;
                }
            }
        }
        else if (currentIndex < dialogueLines.Length - 1)
        {
            currentIndex++;
            _input.text = "Bye";
        }
        else
        {
            currentIndex = 0;
            _input.text = "Next";
            OnDeActive();
        }
        ChangeApproval();
        _DialogueText.text = dialogueLines[currentIndex];
    }
    public void Skip()
    {
        if (_currentApproval.approvalValve >= 0)
        {
            _currentApproval.approvalValve--;
        }
        currentIndex = dialogueLines.Length - 1;
        _input.text = "Bye!";
        if (_noButton.activeSelf == true)
        {
            _noButton.SetActive(false);
        }
        ChangeApproval();
        _DialogueText.text = dialogueLines[currentIndex];
    }

}


[System.Serializable]
public struct ApprovalDialogueLines
{
    public string[] dislikeLines;
    public string[] neutralLines;
    public string[] likedLines;
}