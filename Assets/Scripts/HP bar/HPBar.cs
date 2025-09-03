using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image fill;

    // ScriptableObject�� ���� FloatVariable
    public FloatVariable hp;

    // Start is called before the first frame update
    void Start()
    {
        // HPBar �� �ʱ� ����
        fill.fillAmount = hp.runtimeValue / hp.initValue;
    }

    // GameEventListener �� ����Ǵ� �̺�Ʈ �Լ�
    public void HPChanged()
    {
        fill.fillAmount = hp.runtimeValue / hp.initValue;
    }
}
