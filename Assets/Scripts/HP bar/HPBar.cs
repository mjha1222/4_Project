using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image fill;

    // ScriptableObject로 만든 FloatVariable
    public FloatVariable hp;

    // Start is called before the first frame update
    void Start()
    {
        // HPBar 의 초기 설정
        fill.fillAmount = hp.runtimeValue / hp.initValue;
    }

    // GameEventListener 에 연결되는 이벤트 함수
    public void HPChanged()
    {
        fill.fillAmount = hp.runtimeValue / hp.initValue;
    }
}
