using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    //public TextMeshProUGUI textDisplay;
    public Text textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool isTalking = true;


    public static Dialog instance;
    public GameObject continueButton;
    public GameObject DialogBox;


    private void Start()
    {
        StartCoroutine(Type());

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

        if(PauseMenu.instance.finishText)
        {
            textDisplay.text = sentences[index];
            PauseMenu.instance.finishText = false;
        }

        if (index == sentences.Length - 1 && textDisplay.text == sentences[index])
        {
            DialogBox.SetActive(false);
            instance.isTalking = false;
            OldGuy.instance.Barrier.SetActive(false);
        }
    }


    IEnumerator Type()
    {
        //FindObjectOfType<AudioManager>().Play("DialogSound");
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
        FindObjectOfType<AudioManager>().Play("ClickSound");
        continueButton.SetActive(false);

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
            OldGuy.instance.Barrier.SetActive(false);
        }
    }


    public void CloseDialog()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        DialogBox.SetActive(false);
        OldGuy.instance.Barrier.SetActive(false);
        instance.isTalking = false;
    }
}
