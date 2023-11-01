using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CameraTransitionHandler : MonoBehaviour
{
    public event Action<Transform> OnTransitionToTarget;
    public event Action<Transform> OnReturnButtonPressed;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxTransitionSpeed = 0.5f;
    [SerializeField] private float _maxRotationSpeed = 2.5f;
    [SerializeField] private Canvas _interactionUI;
    [SerializeField] private Button _backButton;

    private Transform _targetLocation;
    private Transform _returnLocation;
    private IEnumerator _forwardTransition;
    private IEnumerator _returnTransition;

    private void Start()
    {
        _interactionUI.enabled = false;
    }

    public void StartTransition(Transform targetLocation, Transform returnLocation)
    {
        if (!_camera || !targetLocation || !returnLocation)
            return;

        StopAllCoroutines();
        _targetLocation = targetLocation;
        _returnLocation = returnLocation;

        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(OnBackButtonPressed);

        OnTransitionToTarget?.Invoke(targetLocation);

        _forwardTransition = MoveCamera(targetLocation);
        StartCoroutine(_forwardTransition);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private void OnBackButtonPressed()
    {
        _interactionUI.enabled = false;

        OnReturnButtonPressed?.Invoke(_returnLocation);

        _returnTransition = MoveCamera(_returnLocation);
        StartCoroutine(_returnTransition);
    }

    private IEnumerator MoveCamera(Transform target)
    {
        float startDistance = Vector3.Distance(_camera.transform.position, target.position);
        const float targetDistanceThreshold = 0.1f;
        const int timerThreshold = 5000;
        CancellationTokenSource token = new CancellationTokenSource(timerThreshold);

        while (!token.IsCancellationRequested) {
            float distance = Vector3.Distance(_camera.transform.position, target.position);
            float angle = Mathf.Abs(Quaternion.Angle(_camera.transform.rotation, target.rotation));

            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, target.position, _maxTransitionSpeed);
            _camera.transform.rotation = Quaternion.RotateTowards(_camera.transform.rotation, target.rotation, _maxRotationSpeed);

            if (distance < targetDistanceThreshold && angle < targetDistanceThreshold)
                token.Cancel();
            yield return null;
        }

        _interactionUI.enabled = target == _targetLocation;
    }
}
