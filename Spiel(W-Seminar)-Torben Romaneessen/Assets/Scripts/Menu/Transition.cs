using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1.15f;

    public static Transition instance;



    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    //public IEnumerator Transitions()
    //{ 
    //        transition.SetTrigger("Start");
    //        Debug.Log("is working 1");
    //        yield return new WaitForSecondsRealtime(1.15f);
    //        Debug.Log("is working 2");
    //}
}
