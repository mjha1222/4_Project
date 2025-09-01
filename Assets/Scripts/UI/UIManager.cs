using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("GameObject")]
    [SerializeField]
    private GameObject mains;
    [SerializeField]
    private GameObject inGame;

    [Header("Message")]
    [SerializeField]
    private GameObject message;
    [SerializeField]
    private TextMeshProUGUI goldText;

    private bool clickSavebutton;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();

                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
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
            GameManager.instance.SaveData();

            message.SetActive(true);
            StartCoroutine(WaitForAnimationEnd("MainMessage", "Complete Save Data"));
        }
    }

    public void LoadData()
    {
        GameManager.instance.player = GameManager.instance.LoadData();
        mains.SetActive(false);
        inGame.SetActive(true);
    }

    public void DeleteData()
    {
        GameManager.instance.DeleteAllData();
    }


    public void GoldViewText()
    {
        goldText.text = $"<color=yellow>Gold</color> <align=left>{GameManager.instance.player.playerGold}";
    }

    private void GoldWarringMessage()
    {
        message.SetActive(true);
        StartCoroutine(WaitForAnimationEnd("EnoughMessage", "Not enough gold"));
    }

    public void GoldCheck(int needGold)
    {
        if (GameManager.instance.player.playerGold < needGold)
        {
            GoldWarringMessage();
        }
    }

    IEnumerator WaitForAnimationEnd(string animatorName,string UGUItext)
    {
        TextMeshProUGUI textMeshProugui = message.GetComponent<TextMeshProUGUI>();
        textMeshProugui.text = UGUItext;
        clickSavebutton = true;

        Animator animator = message.GetComponent<Animator>();
        animator.Play(animatorName);

        yield return null;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        float animationLength = stateInfo.length;
        yield return new WaitForSeconds(animationLength);

        message.SetActive(false);
        clickSavebutton = false;
    }
}
