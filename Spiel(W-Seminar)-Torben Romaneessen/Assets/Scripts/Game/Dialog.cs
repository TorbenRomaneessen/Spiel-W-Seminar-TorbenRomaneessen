using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool isTalking = true;

    public static Dialog instance;
    public GameObject continueButton;
    public GameObject DialogBox;

    private void Start()
    {
        //if (OldGuy.instance.startDialog == true)
        //{
            StartCoroutine(Type());
        //}

        if (instance == null)
        {
            instance = this;
        }

        isTalking = true;
    }


    private void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if (index == sentences.Length - 1)
        {
            isTalking = false;
            Dialog.instance.isTalking = false;
        }

    }


    IEnumerator Type()
    {
        DialogBox.SetActive(true);
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
           
        }
        continueButton.SetActive(true);
    }


    public void NextSentence()
    {
        continueButton.SetActive(false);

        if(index == sentences.Length - 1)
        {
            DialogBox.SetActive(false);
            isTalking = false;
            OldGuy.instance.Barrier.SetActive(false);
            Debug.Log("ISTALKING = FALSE");
            Dialog.instance.isTalking = false;
        }

        if (index < sentences.Length - 1)
        {

            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            isTalking = true;
        }

        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            isTalking = true;
        }
    }
}
