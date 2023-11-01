using ProjectShowMe.Input;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used to propagate the <see cref="IInputReceiver"/> calls 
/// via UnityEvents when the <see cref="Pointer"/> interacts with the object.
/// </summary>
public class Interactable : MonoBehaviour, IInputReceiver
{
    public event Action OnClickEvent;
    public event Action OnHoverEnterEvent;
    public event Action OnHoverExitEvent;

    [SerializeField] private UnityEvent ClickEvent;
    [SerializeField] private UnityEvent HoverEnterEvent;
    [SerializeField] private UnityEvent HoverExitEvent;

    public void OnClick()
    {
        ClickEvent?.Invoke();
        OnClickEvent?.Invoke();
    }

    public void OnHoverEnter()
    {
        HoverEnterEvent?.Invoke();
        OnHoverEnterEvent?.Invoke();
    }

    public void OnHoverExit()
    {
        HoverExitEvent?.Invoke();
        OnHoverExitEvent?.Invoke();
    }
}
