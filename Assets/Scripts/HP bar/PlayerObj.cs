using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    // ScriptableObject�� ���� FloatVariable
    public FloatVariable floatHP;

    // ScriptableObject�� ���� GameEvent
    public GameEvent gameEvtHP;

    public void GetHit(float dmg)
    {
        // ���ظ� ������ dmg ��ŭ runtimeValue ���� ����      
        floatHP.runtimeValue -= dmg;

        // �̺�Ʈ �߻� ��Ŵ
        gameEvtHP.Raise();
    }

    public void Heal(float heal)
    {
        // ġ���� ������ heal ��ŭ runtimeValue ����
        floatHP.runtimeValue += heal;

        // �̺�Ʈ �߻� ��Ŵ
        gameEvtHP.Raise();
    }
}
