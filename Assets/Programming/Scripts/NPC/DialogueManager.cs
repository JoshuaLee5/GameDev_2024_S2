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
    //[SerializeField] Button _noButton;
    #endregion

    #region Dialogue Variables
    //lines to read
    [SerializeField] string[] dialogueLines;
    //current line
    [SerializeField] int currentIndex = 0;
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
        //_noButton.SetActive (false);

        dialogueLines = lines;
        currentIndex = 0;
        _input.text = "Next";
        _displayPicture.sprite = dp;
        _name.text = name;
        //questionIndex = index;

        GameManager.Instance.ChangeState(GameManager.GameState.Menu);
        _DialogueText.text = dialogueLines[currentIndex];

    }

    void OnDeActive()
    {
        _DialogueBox.SetActive(false);
        //_noButton.SetActive(false);
        //Instantiate(,);
        _DialogueBox.SetActive(false);
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

    public void Input()
    {
        if (currentIndex < dialogueLines.Length - 2)
        {
            _input.text = "Next";
            currentIndex++;
        }
        else if (currentIndex < dialogueLines.Length - 3)
        { 
            
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
        _DialogueText.text = dialogueLines[currentIndex];
    }
    public void Skip()
    { 
        currentIndex = dialogueLines.Length - 1;
        _input.text = "Bye!";
        //if (_noButton.activeSelf == true)
        //{
        //    _noButton.SetActive(false);
        //}
        _DialogueText.text = dialogueLines[currentIndex];
    }

}
