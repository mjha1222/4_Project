using UnityEngine;

public class Dummy : MonoBehaviour
{
    private Animator anim;

    private const string HitTrigger = "Hit";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        anim.ResetTrigger(HitTrigger);
        anim.SetTrigger(HitTrigger);
    }
}
