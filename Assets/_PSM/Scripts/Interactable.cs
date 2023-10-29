using ProjectShowMe.Input;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used to propagate the <see cref="IInputReceiver"/> calls 
/// via UnityEvents when the <see cref="Pointer"/> interacts with the object.
/// </summary>
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
