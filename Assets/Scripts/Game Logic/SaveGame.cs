using UnityEngine;
using SaveSystem;

public class SaveGame : MonoBehaviour
{
    public PlayerController Player;
    private FileSave _fileSave = new FileSave(FileFormat.Xml);

    public void Save() 
    {
        //The standard file path in unity
        Debug.Log("Your files are located here: " + Application.persistentDataPath);

        //Creates a new FileSave object with the file format XML.
        _fileSave.WriteToFile(Application.persistentDataPath + "/mySaveFile.xml", Player.PlayerStats);

        //Changes the file format to Binary file
        //_fileSave.fileFormat = FileFormat.Binary;

        //Writes a binary file
        //_fileSave.WriteToFile(Application.persistentDataPath + "/mySaveFile.bin", Player.PlayerStats);
    }

    public void LoadSaveFile()
    {
        //Changes the file format back to XML...
        _fileSave.fileFormat = FileFormat.Xml;

        Player.PlayerStats = _fileSave.ReadFromFile<PlayerStats>(Application.persistentDataPath + "/mySaveFile.xml");
    }
}