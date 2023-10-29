using ProjectShowMe.Input;
using UnityEditor.UIElements;
using UnityEngine;

/// <summary>
/// This class is used to create the scaling effect on an object when 
/// the <see cref="Pointer"/> moves over the object.
/// </summary>
public class HoverSizeIncrease : MonoBehaviour, IInputReceiver
{
    [SerializeField] private float _scale = 1.1f;
    [SerializeField] private Transform[] _overideTargets;

    private Vector3[] _defaultScales;

    // Initialize the default scales array with either the overides or
    // the current gameobject.
    private void Start()
    {
        if (_overideTargets == null || _overideTargets.Length < 1)
            _overideTargets = new Transform[] { transform };
       
        SetDefaultScales();
        OnHoverExit();
    }


    public void OnClick()
    {
    }

    // Activate the scaling effect.
    public void OnHoverEnter()
    {
        SwitchScales(false);
    }

    // Deactivate the scaling effect.
    public void OnHoverExit()
    {
        SwitchScales(true);
    }

    // Initialize the default scales array with the local scale of the overrides.
    private void SetDefaultScales()
    {
        _defaultScales = new Vector3[_overideTargets.Length];

        for (int i = 0; i < _overideTargets.Length; i++) {
            _defaultScales[i] = _overideTargets[i].transform.localScale;
        }
    }

    // Switch the scale of the overrides to the default scale or
    // the scaling effect scale.
    private void SwitchScales(bool setDefault)
    {
        for (int i = 0; i < _overideTargets.Length; i++) {
            _overideTargets[i].localScale = setDefault ? _defaultScales[i] : _defaultScales[i] * _scale;
        }
    }
}
