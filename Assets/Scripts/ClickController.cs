using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour
{
    public Dummy dummy;
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
            
            if(dummy != null)
            {
                dummy.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("Not Founded Dummy");
            }
            
        }
    }
}
