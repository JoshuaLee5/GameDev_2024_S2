using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUILG : MonoBehaviour
{
    //should be private/ serialise field
    public string[] linesOfDlg;
    public int LineIndex = 0;
    public string characterName = "";
    public bool showDlg = false;

    #region Can be used for canvas
    public void OnInteraction()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Menu);
        LineIndex = 0;
        showDlg = true;

    }

    public void next()
    {
        LineIndex++;
    }

    public void Exit()
    {
        LineIndex = 0;
        showDlg = false;
    }
    #endregion


    #region explain 
    Rect UIPos(float startX, float startY, float sizeX, float sizeY)
    {
        return new Rect(startX * (Screen.width / 16), startY * (Screen.height / 9), sizeX * (Screen.width / 16), sizeY * (Screen.height / 9));
    }

    private void OnGUI()
    {

        if (showDlg)
        {
            //GUI Box that covers bottom 3rd of screen that displays our current line of DLG
            //GUI.Box(UIPos(0,6,13,3), linesOfDlg[LineIndex]);

            // W/ characterName
            //GUI.Box(UIPos(0, 6, 13, 3), characterName + ": "+ linesOfDlg[LineIndex]);

            // W/O characterName
            GUI.Box(UIPos(0, 6, 16, 3), $"{characterName}: {linesOfDlg[LineIndex]}");

            //be able to move through the lines if its not the last line
            //are we not at the last line??
            if (LineIndex+1 < linesOfDlg.Length-1)
            {
                //go to next
                if (GUI.Button(UIPos(2, 0, 1, 1), "Next"))
                {
                    //LineIndex++;
                    //LineIndex += 1;
                    //LineIndex = LineIndex + 1;
                    next();
                }
            }
            //if it is the last line we need to end conversation
            else 
            {
                //bye
                if (GUI.Button(UIPos(4, 0, 2, 2), "Bye"))
                {
                    //LineIndex = 0;
                    //showDlg = false;
                    Exit();
                }
            }
        }
        #endregion
    }
}
