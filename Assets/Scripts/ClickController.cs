using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour
{
    [SerializeField] private Dummy dummy;
    public bool isPaused = false;

    // 자동공격 기능 구매시 true로 전환
    [SerializeField] private bool autoAttackEnable = false;
    // 초당 공격횟수
    [SerializeField] private float attackPerSecond = 0f;

    private Coroutine autoAttackCoroutine;

    void OnEnable()
    {
        RestartAutoAttack();
    }

    private void OnDisable()
    {
        StopAutoAttack();
    }

    void Start()
    {
        //자동공격 테스트
        GetComponent<ClickController>().SetAutoAttackEnabled(true);
        GetComponent<ClickController>().SetAutoAttackRate(2f);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            //타켓이 있는 UI를 클릭할때는 반응하지않게 함
            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if(dummy != null)
            {
                int damage = GameManager.Instance.player.playerAtt;
                dummy.TakeDamage(damage);
                Debug.Log($"damage: {damage}");

                SoundManager.instance.PlayEffect(SoundManager.effect.click);
                dummy.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("Not Founded Dummy");
            }

            
        }
    }

    //장비사면 자동공격 활성화하게 장비담당에서 쓸수있게 만듬
    public void SetAutoAttackEnabled(bool enabled)
    {
        autoAttackEnable = enabled;
        RestartAutoAttack();
    }

    //초당 공격 횟수 설정
    public void SetAutoAttackRate(float rate)
    {
        attackPerSecond = Mathf.Max(0f, rate);
        RestartAutoAttack();
    }

    private void RestartAutoAttack()
    {
        StopAutoAttack();
        if(autoAttackEnable && attackPerSecond > 0f)
        {
            autoAttackCoroutine = StartCoroutine(AutoAttackLoop());
        }
    }

    private void StopAutoAttack()
    {
        if(autoAttackCoroutine != null)
        {
            StopCoroutine(autoAttackCoroutine);
            autoAttackCoroutine = null;
        }
    }

    private IEnumerator AutoAttackLoop()
    {
        var wait = new WaitForSeconds(1f / attackPerSecond);
        while(autoAttackEnable && attackPerSecond > 0f)
        {
            if(!isPaused && dummy != null)
            {
                int damage = GameManager.Instance.player.playerAtt;
                dummy.TakeDamage(damage);
                Debug.Log($"damage: {damage}");
            }
            yield return wait;
        }
        
    }
}
