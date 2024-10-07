using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class TextAssetSave : MonoBehaviour
{

    public void CreateText(string fileName)
    {
        //path
        string path = Application.dataPath + $"/{fileName.text}.txt";
        Debug.Log(path);
        //create a file if the file doesnt exist
        if (!File.Exists(path))
        {
            //File.Create(path);
            File.WriteAllText(path, "New");
        }
        //content for the file
        string content = $"Log Date/Time: {System.DateTime.Now}";
        //put content into file
        File.WriteAllText(path, content);
        //File.AppendAllLines(path, content);

    }



}
