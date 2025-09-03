using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject
{

    // �߻� ��Ų �̺�Ʈ�� ������ listener ���
    private List<GameEventListener> listeners = new List<GameEventListener>();

    // �̺�Ʈ �߻�
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    // listener ���
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    // ��ϵ� listener ����
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
   
}
