using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariable : ScriptableObject , ISerializationCallbackReceiver
{
    public float initValue;  // 초기 설정 값

    [System.NonSerialized]
    public float runtimeValue;

    // 디스크에 저장된 값을 역직렬화로 initValue 로 가져온 후   
    public void OnAfterDeserialize()
    {
        runtimeValue = initValue;
    }

    public void OnBeforeSerialize()
    {

    }
    
}
