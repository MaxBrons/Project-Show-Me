using UnityEngine;

public class PositionGUI : MonoBehaviour
{
#if UNITY_EDITOR
    private readonly float _maxLength = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _maxLength)) {
            Vector3 endPoint = Vector3.Distance(transform.position, hit.point) < _maxLength ? hit.point : transform.position + transform.forward * _maxLength;

            Gizmos.DrawLine(transform.position, endPoint);
            Gizmos.color = new Color(0.8f, 0.8f, 0.1f);
            Gizmos.DrawWireSphere(endPoint, 0.05f);
            return;
        }

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _maxLength);
        Gizmos.color = new Color(0.8f, 0.8f, 0.1f);
        Gizmos.DrawWireSphere(transform.position + transform.forward * _maxLength, 0.05f);
    }
#else
    private void Start()
    {
        Destroy(this);
    }
#endif
}
