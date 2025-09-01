using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Option()
    {
        
    }

    public void Exit()
    {
        //테스트용 로드씬 실제로 할때 mainScene 수정
        SceneManager.LoadScene("01");
        //SceneManager.LoadScene("MainScene");
    }
}
