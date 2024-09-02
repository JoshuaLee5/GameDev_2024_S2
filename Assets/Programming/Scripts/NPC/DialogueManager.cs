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
    [SerializeField] Text _name; //
    //[SerializeField] GameObject _next;
    //[SerializeField] GameObject _exit; //Exit Button we turn on and off.
    [SerializeField] Text _input;
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

    void OnDeActive()
    {
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
        else if (currentIndex < dialogueLines.Length - 1)
        {
            currentIndex++;
            _input.text = "Bye";
        }
        else 
        {
            currentIndex = 0;
            _input.text = "Next";
        }
        _DialogueText.text = dialogueLines[currentIndex];
    }
}
