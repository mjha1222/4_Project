using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject message;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject sceneChange;

    public List<UpgradeButton> upgrades;

    PlayerUpgrades playerUpgrade;

    private bool isPlay;

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

    private void Start()
    {
        playerUpgrade = FindObjectOfType<PlayerUpgrades>();
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

    public void NewData()
    {
        SoundManager.instance.PlayBGM(SoundManager.bgm.InGame);
        GameManager.instance.NewUserSetting();
        playerUpgrade.ResetLevels();
        GameManager.instance.player.FinalStatusSet();

        sceneChange.SetActive(true);

        EnemyManager.Instance.Init();

        Image image = sceneChange.GetComponent<Image>();
        image.DOFillAmount(0, 2).SetEase(Ease.OutQuart);
    }

    public void SaveData()
    {
        if (!isPlay)
        {
            GameManager.instance.SaveData();

            message.SetActive(true);
            StartCoroutine(WaitForAnimationEnd(message, "Complete Save Data"));
        }
    }

    public void LoadData()
    {
        GameManager.instance.player = GameManager.instance.LoadData();
        
        StartCoroutine(WaitForLoadStatus());

        sceneChange.SetActive(true);

        EnemyManager.Instance.SetStageValue();
        EnemyManager.Instance.Init();



        Image image = sceneChange.GetComponent<Image>();
        image.DOFillAmount(0, 2).SetEase(Ease.OutQuart);
    }

    public void DeleteData()
    {
        GameManager.instance.DeleteAllData();
    }

    public void GoldWarringMessage()
    {
        if (!isPlay)
        {
            message.SetActive(true);
            StartCoroutine(WaitForAnimationEnd(message, "Not enough gold"));
        }
    }

    public void UpdateUpgradeUI()
    {
        foreach (UpgradeButton ui in upgrades)
        {
            ui.RefreshUI();
        }
    }


    IEnumerator WaitForAnimationEnd(GameObject gameobj, string UGUItext = null)
    {
        TextMeshProUGUI textMeshProugui = message.GetComponent<TextMeshProUGUI>();

        textMeshProugui.text = UGUItext;

        isPlay = true;

        Color randomcolor = Random.ColorHSV();
        textMeshProugui.color = new Color(0, 0, 0, 1);
        textMeshProugui.DOColor(randomcolor, 2f);
        textMeshProugui.DOFade(0, 2f);

        yield return new WaitForSeconds(2f);

        message.SetActive(false);
        sceneChange.SetActive(false);
        isPlay = false;
    }

    IEnumerator WaitForLoadStatus()
    {
        yield return new WaitForSeconds(0.1f);
        playerUpgrade.goldLevel = GameManager.instance.player.levelGold;
        playerUpgrade.critLevel = GameManager.instance.player.levelCri;
        playerUpgrade.autoLevel = GameManager.instance.player.levelAuto;
        playerUpgrade.ApplyAllToPlayer();
        
        GameManager.instance.player.SetWeaponData(WeaponManager.Instance.weapons);
        GameManager.instance.player.FinalStatusSet();

        UpdateUpgradeUI();

        WeaponManager.Instance.weaponUI.UpdateUI();
    }
}
