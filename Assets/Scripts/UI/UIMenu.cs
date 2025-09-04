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
        //�׽�Ʈ�� �ε�� ������ �Ҷ� mainScene ����
        Time.timeScale = 1f;
        SoundManager.instance.PlayBGM(SoundManager.bgm.Main);
        SceneManager.LoadScene("01");
        //SceneManager.LoadScene("MainScene");
    }
}
