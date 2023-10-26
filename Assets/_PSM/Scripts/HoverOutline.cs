using ProjectShowMe.Input;
using UnityEngine;

public class HoverOutline : MonoBehaviour, IInputReceiver
{
    public void OnClick()
    {
    }

    public void OnHoverEnter()
    {
        print($"On Hover Enter: {transform.name}");
    }

    public void OnHoverExit()
    {
        print($"On Hover Exit: {transform.name}");
    }
}
