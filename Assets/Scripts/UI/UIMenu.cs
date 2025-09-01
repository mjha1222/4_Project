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
        //테스트용 로드씬 실제로 할때 mainScene 수정
        Time.timeScale = 1f;

        SceneManager.LoadScene("01");
        //SceneManager.LoadScene("MainScene");
    }
}
