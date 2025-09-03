using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    // ��ũ��Ʈ�� Ȱ��ȭ �Ǹ� GameEvent �� ����մϴ�.
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    // ��ũ��Ʈ�� ��Ȱ��ȭ �Ǹ� GameEvent ���� �����մϴ�.
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    // ����Ƽ �����Ϳ��� UnityEvent�� ����� �Լ��� ȣ���մϴ�.
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
