using UnityEngine;

public class SmoothFollowCS : MonoBehaviour
{
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public Transform target;

    void LateUpdate()
    {
        if (target)
        {
            var wantedRotationAngle = target.eulerAngles.y;
            var currentRotationAngle = transform.eulerAngles.y;
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
            var targetPosition = target.position - currentRotation * Vector3.forward * distance;
            targetPosition.y = height; 
            var currentHeight = transform.position.y;
            var wantedHeight = target.position.y + height;
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            transform.position = new Vector3(targetPosition.x, currentHeight, targetPosition.z);
            transform.LookAt(target);
        }
    }
}