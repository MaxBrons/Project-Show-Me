using ProjectShowMe.Input;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraInteraction : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Transform _defaultLocation;
    [SerializeField] private Transform _targetLocation;
    [SerializeField] private CameraTransitionHandler _handler;
    [SerializeField] private List<MonoBehaviour> _componentsToDisableOnInteraction = new();
    [SerializeField] private List<Collider> _collidersToDisableOnInteraction = new();

    public void OnClick()
    {
        if (!_handler)
            return;

        _handler.OnReturnButtonPressed -= OnReturnButtonPressed;
        _handler.OnReturnButtonPressed += OnReturnButtonPressed;

        _handler.OnTransitionToTarget -= OnTransitionToTarget;
        _handler.OnTransitionToTarget += OnTransitionToTarget;

        _componentsToDisableOnInteraction.ForEach(x => x.enabled = false);

        _handler.StartTransition(_targetLocation, _defaultLocation);
    }

    private void OnTransitionToTarget(Transform target)
    {
        if (_targetLocation.position == target.position)
            return;

        _handler.OnReturnButtonPressed -= OnReturnButtonPressed;
        _componentsToDisableOnInteraction.ForEach(x => x.enabled = true);
        _collidersToDisableOnInteraction.ForEach(x => x.enabled = true);
    }

    private void OnReturnButtonPressed(Transform target)
    {
        if (_defaultLocation.position != target.position)
            return;

        _handler.OnReturnButtonPressed -= OnReturnButtonPressed;
        _componentsToDisableOnInteraction.ForEach(x => x.enabled = true);
        _collidersToDisableOnInteraction.ForEach(x => x.enabled = true);
    }

    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {

    }

    private void OnDestroy()
    {
        if (_handler != null)
            _handler.OnReturnButtonPressed -= OnReturnButtonPressed;
    }
}
