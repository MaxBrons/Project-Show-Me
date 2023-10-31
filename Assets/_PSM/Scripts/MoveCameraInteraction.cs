using ProjectShowMe.Input;
using UnityEngine;

public class MoveCameraInteraction : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Transform _defaultLocation;
    [SerializeField] private Transform _targetLocation;
    [SerializeField] private CameraTransitionHandler _handler;

    public void OnClick()
    {
        if (!_handler)
            return;

        _handler.StartTransition(_targetLocation, _defaultLocation);
    }

    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {

    }
}
