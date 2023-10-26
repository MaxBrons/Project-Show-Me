using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectShowMe.Input
{
    public interface IInputReceiver
    {
        public void OnClick();
        public void OnHoverEnter();
        public void OnHoverExit();

    }

    public class Pointer : MonoBehaviour
    {
        private InputActionsCore _inputActions = null;
        private Transform _currentInputReceiver = null;
        private Vector2 _mousePosition;

        void Awake()
        {
            _inputActions = new InputActionsCore();
            AddListener(_inputActions.UI.Point, OnPoint);
            AddListener(_inputActions.UI.Click, OnClick);
            AddListener(_inputActions.UI.Cancel, OnCancel);
            _inputActions.Enable();
        }

        private void LateUpdate()
        {
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(_mousePosition), out RaycastHit hitInfo);
            if (!hit) {
                OnHoverExit();
                return;
            }

            if (hitInfo.transform != _currentInputReceiver) {
                OnHoverExit();
                _currentInputReceiver = hitInfo.transform;
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

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnHoverExit();
            _currentInputReceiver = null;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (!_currentInputReceiver)
                return;

            IInputReceiver[] inputReceivers = _currentInputReceiver.GetComponents<IInputReceiver>();
            if (inputReceivers == null)
                return;

            foreach (var inputReceiver in inputReceivers) {
                inputReceiver.OnClick();
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            _mousePosition = context.ReadValue<Vector2>();
        }

        private void OnHoverEnter()
        {
            if (!_currentInputReceiver)
                return;

            IInputReceiver[] inputReceivers = _currentInputReceiver.GetComponents<IInputReceiver>();
            if (inputReceivers == null)
                return;

            foreach (var inputReceiver in inputReceivers) {
                inputReceiver.OnHoverEnter();
            }
        }

        private void OnHoverExit()
        {
            if (!_currentInputReceiver)
                return;

            IInputReceiver[] inputReceivers = _currentInputReceiver.GetComponents<IInputReceiver>();
            if (inputReceivers == null)
                return;

            foreach (var inputReceiver in inputReceivers) {
                inputReceiver.OnHoverExit();
            }
            _currentInputReceiver = null;
        }

        private void AddListener(InputAction action, Action<InputAction.CallbackContext> listener)
        {
            action.started += listener;
            action.performed += listener;
            action.canceled += listener;
        }

        private void RemoveListener(InputAction action, Action<InputAction.CallbackContext> listener)
        {
            action.started -= listener;
            action.performed -= listener;
            action.canceled -= listener;
        }
    }
}
