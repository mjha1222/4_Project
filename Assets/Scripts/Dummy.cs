using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private Animator anim;

    private const string DummyAnim = "Dummy";

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

    public void TakeDamage(int  damage)
    {
        anim.Play(DummyAnim, 0, 0f);
        Debug.Log("Dummy hit");
    }
}
