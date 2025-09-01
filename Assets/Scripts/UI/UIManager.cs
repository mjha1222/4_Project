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

    [Header("Message & Change")]
    [SerializeField]
    private GameObject message;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private GameObject sceneChange;


    private bool isPlay;

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
        SoundManager.instance.PlayBGM(SoundManager.bgm.InGame);
        GameManager.instance.NewUserSetting();
        mains.SetActive(false);
        inGame.SetActive(true);
        sceneChange.SetActive(true);
        StartCoroutine(WaitForAnimationEnd(sceneChange, "completeRotation"));
    }

    public void SaveData()
    {
        if (!isPlay)
        {
            GameManager.instance.SaveData();

            message.SetActive(true);
            StartCoroutine(WaitForAnimationEnd(message,"MainMessage", "Complete Save Data"));
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
        if (!isPlay)
        {
            message.SetActive(true);
            StartCoroutine(WaitForAnimationEnd(message,"EnoughMessage", "Not enough gold"));
        }
    }

    public void GoldCheck(int needGold)
    {
        if (GameManager.instance.player.playerGold < needGold)
        {
            //È®ÀÎ¿ë
            GoldWarringMessage();
        }
    }

    IEnumerator WaitForAnimationEnd(GameObject gameobj,string animatorName, string UGUItext = null)
    {
        TextMeshProUGUI textMeshProugui = message.GetComponent<TextMeshProUGUI>();
        Animator animator = gameobj.GetComponent<Animator>();

        animator.Play(animatorName);
        textMeshProugui.text = UGUItext;
        isPlay = true;

        yield return null;

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        message.SetActive(false);
        sceneChange.SetActive(false);
        isPlay = false;
    }
}
