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
            
            if(EnemyManager.Instance.nowEnemy != null)
            {
                EnemyManager.Instance.nowEnemy.TakeDamage(5);
            }
            else
            {
                Debug.LogWarning("Not Founded Dummy");
            }
            
        }
    }
}
