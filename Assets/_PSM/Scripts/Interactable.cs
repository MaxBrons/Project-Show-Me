using ProjectShowMe.Input;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInputReceiver
{
    [SerializeField] private UnityEvent OnClickEvent;
    [SerializeField] private UnityEvent OnHoverEnterEvent;
    [SerializeField] private UnityEvent OnHoverExitEvent;

    public void OnClick()
    {
        OnClickEvent?.Invoke();
    }

    public void OnHoverEnter()
    {
        OnHoverEnterEvent?.Invoke();
    }

    public void OnHoverExit()
    {
        OnHoverExitEvent?.Invoke();
    }
}
