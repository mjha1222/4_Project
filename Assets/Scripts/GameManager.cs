using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private string filePath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


}
