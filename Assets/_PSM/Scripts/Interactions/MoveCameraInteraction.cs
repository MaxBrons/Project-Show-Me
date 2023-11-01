using ProjectShowMe.Input;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraInteraction : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Transform _defaultLocation;
    [SerializeField] private Transform _targetLocation;
    [SerializeField] private CameraTransitionHandler _handler;
    [SerializeField] private List<MonoBehaviour> _componentsToDisableOnInteraction = new();
    [SerializeField] private List<MonoBehaviour> _componentsToEnableOnInteraction = new();
    [SerializeField] private List<Collider> _collidersToDisableOnInteraction = new();

    public void OnClick()
    {
        if (!_handler)
            return;

        _handler.OnReturnButtonPressed -= OnReturnButtonPressed;
        _handler.OnReturnButtonPressed += OnReturnButtonPressed;

        _handler.OnTransitionToTarget -= OnTransitionToTarget;
        _handler.OnTransitionToTarget += OnTransitionToTarget;

        _handler.StartTransition(_targetLocation, _defaultLocation);
    }

    private void OnTransitionToTarget(Transform target)
    {
        bool enabledState = _targetLocation.position == target.position;

        if (!enabledState)
            _handler.OnReturnButtonPressed -= OnReturnButtonPressed;

        _componentsToDisableOnInteraction.ForEach(x => x.enabled = !enabledState);
        _collidersToDisableOnInteraction.ForEach(x => x.enabled = !enabledState);
        _componentsToEnableOnInteraction.ForEach(x => x.enabled = enabledState);
    }

    private void OnReturnButtonPressed(Transform target)
    {
        if (_defaultLocation.position != target.position)
            return;

        _handler.OnReturnButtonPressed -= OnReturnButtonPressed;
        _componentsToDisableOnInteraction.ForEach(x => x.enabled = true);
        _collidersToDisableOnInteraction.ForEach(x => x.enabled = true);
        _componentsToEnableOnInteraction.ForEach(x => x.enabled = false);
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
