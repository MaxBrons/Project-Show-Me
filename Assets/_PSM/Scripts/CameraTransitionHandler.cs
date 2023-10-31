using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraTransitionHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxTransitionSpeed = 0.5f;
    [SerializeField] private Canvas _interactionUI;
    [SerializeField] private Button _backButton;

    private UnityAction _onBackButtonPressed;

    private Transform _targetLocation;
    private Transform _returnLocation;
    private IEnumerator _forwardTransition;
    private IEnumerator _returnTransition;

    private void Start()
    {
        _onBackButtonPressed += OnBackButtonPressed;
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
        _backButton.onClick.AddListener(_onBackButtonPressed);

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

            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, target.position, _maxTransitionSpeed);

            if (distance < targetDistanceThreshold)
                token.Cancel();
            yield return null;
        }
        _interactionUI.enabled = target == _targetLocation;
    }
}
