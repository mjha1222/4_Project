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
        //�׽�Ʈ�� �ε�� ������ �Ҷ� mainScene ����
        SceneManager.LoadScene("01");
        //SceneManager.LoadScene("MainScene");
    }
}
