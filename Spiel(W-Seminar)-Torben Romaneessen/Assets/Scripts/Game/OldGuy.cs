using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGuy : MonoBehaviour
{
    public Animator animator;
    public GameObject Barrier;
    public static OldGuy instance;
    public bool startDialog;



    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Update()
    {
        TalkAnimation();
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TalkAnimation()
    {
        if(Dialog.instance.isTalking == false)
        {
            animator.SetBool("isTalking", true);
            Debug.Log("Functining");
        }

        else
        {
            animator.SetBool("isTalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            startDialog = true;
        }
    }
}
