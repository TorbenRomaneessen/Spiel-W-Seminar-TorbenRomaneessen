using UnityEngine;

public class OldGuy : MonoBehaviour
{
    private Animator _animator;
    public GameObject Barrier;
    public static OldGuy Instance;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Update()
    {
        TalkAnimation();
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TalkAnimation()
    {
        if(Dialog.Instance.IsTalking == false)
        {
            _animator.SetBool("isTalking", true);
        }

        else
        {
            _animator.SetBool("isTalking", false);
        }
    }
}
