using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariable : ScriptableObject , ISerializationCallbackReceiver
{
    public float initValue;  // �ʱ� ���� ��

    [System.NonSerialized]
    public float runtimeValue;

    // ��ũ�� ����� ���� ������ȭ�� initValue �� ������ ��   
    public void OnAfterDeserialize()
    {
        runtimeValue = initValue;
    }

    public void OnBeforeSerialize()
    {

    }
    
}
