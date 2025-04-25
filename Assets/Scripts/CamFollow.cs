using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 TargetDistance = new Vector3(0, 5f, -8f);
    public float followSpeed = 3f;
    private Vector3 desiredPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void LateUpdate()
    {
        _FollowTarget();
    }

    public void _FollowTarget()
    {
        if (Target == null) return;
        desiredPosition = Target.position + TargetDistance;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(Target);
    }
}
