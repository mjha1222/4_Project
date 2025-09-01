using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    private string filePath = Application.dataPath + "/Save/userdata.json";


    private void Awake()
    {
        NewUserSetting();
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SaveData()
    {
        string saveJson = JsonUtility.ToJson(player);

        File.WriteAllText(filePath, saveJson);
    }

    public Player LoadData()
    {
        if (!File.Exists(filePath))
            return null;

        string loadFile = File.ReadAllText(filePath);
        Player playerdata = JsonUtility.FromJson<Player>(loadFile);
        return playerdata;
    }

    public void DeleteAllData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public void NewUserSetting()
    {
        player = new Player(1, 1,0, 1, 0, 1, 1);
    }
}
