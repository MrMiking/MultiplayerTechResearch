using UnityEngine;

public class Window : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject content;
    [SerializeField] private Animator animator;

    public void WindowFadeIn()
    {
        animator.Play("In");
    }

    public void WindowFadeOut()
    {
        animator.Play("Out");
    }

    public void SetWindowActiveFalse() => content.SetActive(false);

    public void SetWindowActiveTrue() => content.SetActive(true);
}
