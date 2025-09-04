using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject mains;
    [SerializeField] private GameObject inGame;
    [SerializeField] private GameObject bagPanel;
    [SerializeField] private GameObject message;
    [SerializeField] private TMPro.TextMeshProUGUI goldText;
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
}
