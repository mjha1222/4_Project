using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    private string filePath = Application.dataPath + "/Save/userdata.json";

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();

                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    public void SaveData()
    {
        player.GetWeaponData(WeaponManager.Instance.weapons);

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
        //player = new Player(1, 1,0, 1, 0);

        //test¿ë
        player = new Player(1, 1, 100, 1, 0);
    }
}

