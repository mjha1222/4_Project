using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour
{
    public bool isPaused = false;
    

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Debug.Log("Attack!");
        }
    }
}
