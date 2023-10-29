using ProjectShowMe.Input;
using UnityEditor.UIElements;
using UnityEngine;

/// <summary>
/// This class is used to create the highlight effect on an object when 
/// the <see cref="Pointer"/> moves over the object.
/// </summary>
public class HoverOutline : MonoBehaviour, IInputReceiver
{
    [Range(0, 31)]
    [SerializeField] private int _highlightLayer = 6;
    [SerializeField] private GameObject[] _overideTargets;

    private int[] _defaultLayers;

    // Initialize the default layers array with either the overides or
    // the current gameobject.
    private void Start()
    {
        if (_overideTargets == null || _overideTargets.Length < 1)
            _overideTargets = new GameObject[] { gameObject };

        SetDefaultLayers();
        OnHoverExit();
    }

    public void OnClick()
    {
    }

    // Activate the hover effect.
    public void OnHoverEnter()
    {
        SwitchLayers(false);
    }

    // Deactivate the hover effect.
    public void OnHoverExit()
    {
        SwitchLayers(true);
    }

    // Initialize the default layers array with the layer of the overrides.
    private void SetDefaultLayers()
    {
        _defaultLayers = new int[_overideTargets.Length];

        for (int i = 0; i < _overideTargets.Length; i++) {
            _defaultLayers[i] = _overideTargets[i].layer;
        }
    }

    // Switch the layer of the overrides to the default layer or
    // the hover effect layer.
    private void SwitchLayers(bool setDefault)
    {
        for (int i = 0; i < _overideTargets.Length; i++) {
            _overideTargets[i].layer = setDefault ? _defaultLayers[i] : _highlightLayer;
        }
    }
}
