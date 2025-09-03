using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    // ScriptableObject로 만든 FloatVariable
    public FloatVariable floatHP;

    // ScriptableObject로 만든 GameEvent
    public GameEvent gameEvtHP;

    public void GetHit(float dmg)
    {
        // 피해를 받으면 dmg 만큼 runtimeValue 에서 차감      
        floatHP.runtimeValue -= dmg;

        // 이벤트 발생 시킴
        gameEvtHP.Raise();
    }

    public void Heal(float heal)
    {
        // 치유를 받으면 heal 만큼 runtimeValue 증가
        floatHP.runtimeValue += heal;

        // 이벤트 발생 시킴
        gameEvtHP.Raise();
    }
}
