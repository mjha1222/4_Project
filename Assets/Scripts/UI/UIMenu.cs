using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{

    public void OptionMenu()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }


    public void Exit()
    {
        //테스트용 로드씬 실제로 할때 mainScene 수정
        Time.timeScale = 1f;
        SoundManager.instance.PlayBGM(SoundManager.bgm.Main);
        SceneManager.LoadScene("01");
        //SceneManager.LoadScene("MainScene");
    }
}
