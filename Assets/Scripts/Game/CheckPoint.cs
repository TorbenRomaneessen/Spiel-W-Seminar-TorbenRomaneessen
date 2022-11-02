using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector3 ReachedPoint;
    [SerializeField]
    private Animator _flagAnimator;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            ReachedPoint = this.transform.position;
        }

        if (col.gameObject.CompareTag("Character"))
        {
            _flagAnimator.SetTrigger("NewCheckPoint");
        }
    }
}