using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mains;
    [SerializeField]
    private GameObject inGame;

    [SerializeField]
    private GameObject message;

    private bool clickSavebutton;

    public void NewData()
    {
        GameManager.instance.NewUserSetting();
        mains.SetActive(false);
        inGame.SetActive(true);
    }

    public void SaveData()
    {
        if (!clickSavebutton)
        {
            Debug.Log("saveTest");
            GameManager.instance.SaveData();
            message.SetActive(true);
            StartCoroutine(WaitForAnimationEnd());
        }
    }

    public void LoadData()
    {
        GameManager.instance.player = GameManager.instance.LoadData();
    }

    public void DeleteData()
    {
        GameManager.instance.DeleteAllData();
    }

   
    IEnumerator WaitForAnimationEnd()
    {
        clickSavebutton = true;

        yield return null;

        Animator animator = message.GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        float animationLength = stateInfo.length;
        yield return new WaitForSeconds(animationLength);

        message.SetActive(false);
        clickSavebutton = false;
    }
}
