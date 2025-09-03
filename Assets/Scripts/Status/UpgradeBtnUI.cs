using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeBtnUI : MonoBehaviour
{
    public UpgradeStats stats;
    public GoldWallet wallet;
    public Player player;

    public Button buyButton;
    public Text titleText;
    public Text levelText;
    public Text descText;
    public Text costText;

    public TMP_Text titleTMP;
    public TMP_Text levelTMP;
    public TMP_Text descTMP;
    public TMP_Text costTMPTxt;

    public Color affordableColor = Color.black;
    public Color unaffordableColor = new Color(0.9f, 0.2f, 0.2f);

    [Header("Hold Repeat")]
    public float holdRepeatInterval = 0.2f;

    bool holding;
    bool holdDidPurchase;
    float holdTimer;

    void OnEnable()
    {
        if (buyButton) buyButton.onClick.AddListener(OnClickBuy);
        if (wallet) wallet.onGoldChanged += OnGoldChanged;
        RefreshAll();
    }

    void OnDisable()
    {
        if (buyButton) buyButton.onClick.RemoveListener(OnClickBuy);
        if (wallet) wallet.onGoldChanged -= OnGoldChanged;
    }

    void Update()
    {
        RefreshInteractableOnly();
        HandleHold();
    }

    void OnClickBuy()
    {
        if (holdDidPurchase) { holdDidPurchase = false; return; } // 홀드 중 이미 구매했으면 릴리즈 클릭 무시
        AttemptBuy();
    }

    void OnGoldChanged(double _) => RefreshAll();

    void RefreshAll()
    {
        if (stats == null || stats.dataTable == null) { RefreshInteractableOnly(); return; }
        var data = stats.Current;
        if (data == null)
        {
            SetText(descText, descTMP, "");
            SetText(costText, costTMPTxt, "—");
            if (buyButton) buyButton.interactable = false;
            return;
        }

        SetText(titleText, titleTMP, BuildTitleString());
        SetText(descText, descTMP, BuildDescString(data));
        SetText(costText, costTMPTxt, $"{data.cost:0}");
        RefreshInteractableOnly();
    }

    string BuildTitleString()
    {
        string current = titleText ? titleText.text : (titleTMP ? titleTMP.text : "업그레이드 0");
        int i = current.Length - 1; while (i >= 0 && char.IsDigit(current[i])) i--;
        string prefix = current.Substring(0, i + 1).TrimEnd();
        if (string.IsNullOrEmpty(prefix)) prefix = "업그레이드";
        return $"{prefix} {stats.currentLevel}";
    }

    string BuildDescString(UpgradeLevelData d)
    {
        var kind = stats.dataTable.kind;
        switch (kind)
        {
            case UpgradeKind.Auto: return $"{d.autoDps:0.00} 회/초";
            case UpgradeKind.Crit: return $"치명타 데미지 +{(d.critMultiplier - 1f) * 100f:0.0}%";
            case UpgradeKind.Gold: return $"골드 획득량 +{(d.goldMultiplier - 1f) * 100f:0.0}%";
        }
        return "";
    }

    void RefreshInteractableOnly()
    {
        if (buyButton == null || wallet == null || stats == null || stats.dataTable == null) return;
        var data = stats.Current;
        bool canBuy = data != null && wallet.CanAfford(data.cost);
        buyButton.interactable = canBuy;
        SetColor(costText, costTMPTxt, canBuy ? affordableColor : unaffordableColor);
    }

    bool AttemptBuy()
    {
        if (stats != null && stats.TryBuy(player))
        {
            RefreshAll();
            return true;
        }
        return false;
    }

    void HandleHold()
    {
        if (buyButton == null || !buyButton.interactable) { holding = false; return; }

#if UNITY_EDITOR || UNITY_STANDALONE
        // 마우스
        if (Input.GetMouseButtonDown(0) && IsScreenPointOverButton(Input.mousePosition))
        {
            holding = true;
            holdTimer = 0f;
            holdDidPurchase = false;
        }
        if (holding && Input.GetMouseButton(0))
        {
            holdTimer += Time.unscaledDeltaTime;
            while (holdTimer >= holdRepeatInterval)
            {
                holdTimer -= holdRepeatInterval;
                if (!AttemptBuy()) break;
                holdDidPurchase = true;
                if (!buyButton.interactable) break;
            }
        }
        if (Input.GetMouseButtonUp(0)) holding = false;
#else
        if (Input.touchCount > 0)
        {
            var t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began && IsScreenPointOverButton(t.position))
            {
                holding = true;
                holdTimer = 0f;
                holdDidPurchase = false;
            }
            if (holding && (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary))
            {
                holdTimer += Time.unscaledDeltaTime;
                while (holdTimer >= holdRepeatInterval)
                {
                    holdTimer -= holdRepeatInterval;
                    if (!AttemptBuy()) break;
                    holdDidPurchase = true;
                    if (!buyButton.interactable) break;
                }
            }
            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) holding = false;
        }
        else
        {
            holding = false;
        }
#endif
    }

    bool IsScreenPointOverButton(Vector2 screenPos)
    {
        if (!buyButton) return false;
        var rt = buyButton.transform as RectTransform;
        if (!rt) return false;

        Canvas canvas = buyButton.GetComponentInParent<Canvas>();
        Camera cam = null;
        if (canvas && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera;

        return RectTransformUtility.RectangleContainsScreenPoint(rt, screenPos, cam);
    }

    static void SetText(Text legacy, TMP_Text tmp, string v) { if (legacy) legacy.text = v; if (tmp) tmp.text = v; }
    static void SetColor(Text legacy, TMP_Text tmp, Color c) { if (legacy) legacy.color = c; if (tmp) tmp.color = c; }
}
