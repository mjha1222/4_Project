using UnityEngine;

public class MessageController : MonoBehaviour
{
    private void OnEnable()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(0);
    }
}
