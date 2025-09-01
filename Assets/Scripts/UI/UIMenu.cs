using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField]
    private GameObject menu;

    [Header("SoundOption")]
    [SerializeField]
    private GameObject inMenu;

    public void Start()
    {
        inMenu.SetActive(false);
    }

    public void OptionMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Option()
    {
        inMenu.SetActive(!inMenu.activeSelf);
    }


    public void Exit()
    {
        //�׽�Ʈ�� �ε�� ������ �Ҷ� mainScene ����
        Time.timeScale = 1f;
        SoundManager.instance.PlayBGM(SoundManager.bgm.Main);
        SceneManager.LoadScene("01");
        //SceneManager.LoadScene("MainScene");
    }
}
