using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject mains;
    [SerializeField] private GameObject inGame;
    [SerializeField] private GameObject bagPanel;
    [SerializeField] private GameObject message;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject sceneChange;

    bool isPlay;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();
                if (instance == null) instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) { Destroy(gameObject); return; }
    }

    public void ResetUpgradesAndGold()
    {
        var gm = GameManager.Instance;
        if (gm.player == null) gm.NewUserSetting();
        var ups = FindObjectsByType<PlayerUpgrades>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var u in ups) u.ResetLevels();
        gm.player.playerGold = 0;

        var wallet = GoldWallet.Get();
        if (wallet != null) wallet.SetGold(0);

        var click = FindAnyObjectByType<ClickController>();
        if (click != null)
        {
            bool en = gm.player.playerAutoAtt > 0f;
            click.SetAutoAttackEnabled(en);
            click.SetAutoAttackRate(en ? gm.player.playerAutoAtt : 0f);
        }

        var buttons = FindObjectsByType<UpgradeButton>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var b in buttons) b.SendMessage("RefreshUI", SendMessageOptions.DontRequireReceiver);

        GoldViewText();
    }

    public void GoldViewText()
    {
        goldText.text = $"<color=yellow>Gold</color> <align=left>{GameManager.instance.player.playerGold}";
    }

    public void SaveData()
    {
        if (!isPlay)
        {
            GameManager.instance.SaveData(GameManager.instance.player);

            message.SetActive(true);
            StartCoroutine(WaitForAnimationEnd(message, "MainMessage", "Complete Save Data"));
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
    
    IEnumerator WaitForAnimationEnd(GameObject gameobj, string animatorName, string UGUItext = null)
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
