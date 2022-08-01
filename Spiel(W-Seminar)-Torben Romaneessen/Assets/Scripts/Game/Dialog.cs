using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private Text _textDisplay;
    [SerializeField]
    private string[] _sentences;
    private int _index;
    private const float TypingSpeed = 0.04f;
    public bool IsTalking = true;

    public static Dialog Instance;
    public GameObject ContinueButton;
    public GameObject DialogBox;


    private void Start()
    {
        StartCoroutine(Type());

        if (Instance == null)
        {
            Instance = this;
        }

        IsTalking = true;
    }


    private void Update()
    {
        if (_textDisplay.text == _sentences[_index])
        {
            ContinueButton.SetActive(true);
        }

        if (PauseMenu.Instance.FinishText)
        {
            _textDisplay.text = _sentences[_index];
            PauseMenu.Instance.FinishText = false;
        }

        if (_index == _sentences.Length - 1 && _textDisplay.text == _sentences[_index])
        {
            DialogBox.SetActive(false);
            Instance.IsTalking = false;
            OldGuy.Instance.Barrier.SetActive(false);
        }
    }

    IEnumerator Type()
    {
        DialogBox.SetActive(true);
        foreach (char letter in _sentences[_index].ToCharArray())
        {
            _textDisplay.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
           
        }

        ContinueButton.SetActive(true);
    }


    public void NextSentence()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        ContinueButton.SetActive(false);

        if (_index < _sentences.Length - 1)
        {
            _index++;
            _textDisplay.text = "";
            StartCoroutine(Type());
            IsTalking = true;
        }

        else
        {
            _textDisplay.text = "";
            ContinueButton.SetActive(false);
            OldGuy.Instance.Barrier.SetActive(false);
        }
    }

    public void CloseDialog()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        DialogBox.SetActive(false);
        OldGuy.Instance.Barrier.SetActive(false);
        Instance.IsTalking = false;
    }
}
