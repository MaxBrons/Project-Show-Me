using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectShowMe.Input
{
    // Inplement this interface to receive the input events when the pointer interacts
    // with the parent transform.
    public interface IInputReceiver
    {
        public void OnClick();
        public void OnHoverEnter();
        public void OnHoverExit();

    }

    /// <summary>
    /// A class to propagate interaction events to the <see cref="IInputReceiver"/> classes.
    /// </summary>
    public class Pointer : MonoBehaviour
    {
        private InputActionsCore _inputActions = null;
        private Transform _currentInputReceiverObject = null;
        private Vector2 _mousePosition;
        private List<IInputReceiver> _currentInputReceivers = new();

        // Setup all the input events.
        void Awake()
        {
            _inputActions = new InputActionsCore();
            AddListener(_inputActions.UI.Point, OnPoint);
            AddListener(_inputActions.UI.Click, OnClick);
            AddListener(_inputActions.UI.Cancel, OnCancel);
            _inputActions.Enable();
        }

        // Raycast towards the mouse position to interact with the hit IInputReceiver classes.
        private void FixedUpdate()
        {
            // Fire the raycast and check if it hit something.
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(_mousePosition), out RaycastHit hitInfo);
            if (!hit) {
                OnHoverExit();
                return;
            }

            // If we stop hovering over the current transform, switch to the other transform.
            if (hitInfo.transform != _currentInputReceiverObject) {
                OnHoverExit();
                _currentInputReceiverObject = hitInfo.transform;
                _currentInputReceivers = _currentInputReceiverObject.GetComponents<IInputReceiver>().Where(x => (x as MonoBehaviour).enabled).ToList();
                OnHoverEnter();
            }
        }

        private void OnDestroy()
        {
            RemoveListener(_inputActions.UI.Point, OnPoint);
            RemoveListener(_inputActions.UI.Click, OnClick);
            RemoveListener(_inputActions.UI.Cancel, OnCancel);
        }

        private void OnEnable()
        {
            _inputActions?.Enable();
        }

        private void OnDisable()
        {
            _inputActions?.Disable();
        }

        // If the cancel button is pressed, just exit the hover.
        public void OnCancel(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnHoverExit();
            _currentInputReceiverObject = null;
            _currentInputReceivers = new();
        }

        // If the click button is pressed, propagate the click event to the input receiver.
        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            if (!_currentInputReceiverObject)
                return;

            if (_currentInputReceivers.Count < 1)
                return;

            foreach (var inputReceiver in _currentInputReceivers) {
                inputReceiver.OnClick();
            }
        }

        // If the mouse moves, store the current mouse position.
        public void OnPoint(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            _mousePosition = context.ReadValue<Vector2>();
        }

        // Propagate the hover enter event to the input receivers on the current input receiver transform.
        private void OnHoverEnter()
        {
            if (!_currentInputReceiverObject)
                return;

            if (_currentInputReceivers.Count < 1)
                return;

            foreach (var inputReceiver in _currentInputReceivers) {
                inputReceiver.OnHoverEnter();
            }
        }

        // Propagate the hover exit event to the input receivers on the current input receiver transform.
        private void OnHoverExit()
        {
            if (!_currentInputReceiverObject)
                return;

            if (_currentInputReceivers.Count < 1)
                return;

            foreach (var inputReceiver in _currentInputReceivers) {
                inputReceiver.OnHoverExit();
            }
            _currentInputReceiverObject = null;
        }

        // Bind the input event to all the action events.
        private void AddListener(InputAction action, Action<InputAction.CallbackContext> listener)
        {
            action.started += listener;
            action.performed += listener;
            action.canceled += listener;
        }

        // Unbind the input event to all the action events.
        private void RemoveListener(InputAction action, Action<InputAction.CallbackContext> listener)
        {
            action.started -= listener;
            action.performed -= listener;
            action.canceled -= listener;
        }
    }
}
