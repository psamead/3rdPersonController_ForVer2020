using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform targetTransform;  // The object that the camera will follow
    public Transform cameraPivot; // The object the camera uses to pivot (look up and down)
    private Transform cameraTransform; // The transfomr of the actual camera object in the scene
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero; 

    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpped = 2;
    public float cameraPivotSpped = 2;

    public float lookAngle;  // Camera looking up and down
    public float pivotAngle;  // Camera looking left and right
    public float minimunPivotAngle = -35;
    public float maximumPivotAngle = 35;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpped);
        pivotAngle = pivotAngle + (inputManager.cameraInputY * cameraPivotSpped);
        pivotAngle = Mathf.Clamp(pivotAngle, minimunPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollision()
    {
        float targetPosition = defaultPosition;
    }
}
