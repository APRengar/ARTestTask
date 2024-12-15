using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectManipulator : MonoBehaviour
{
    private Vector2 initialTouchPosition; // For single finger drag
    private Vector2 initialTouch1, initialTouch2; // For pinch and rotate
    private float initialPinchDistance; // For scaling
    private Quaternion initialRotation; // For rotation
    private Transform selectedObject; // The object being manipulated

    [SerializeField] private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Update()
    {
        if (Input.touchCount == 1) // Drag
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Start drag
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    initialTouchPosition = touch.position;
                    selectedObject = hits[0].trackable.gameObject.transform;
                }
            }
            else if (touch.phase == TouchPhase.Moved && selectedObject != null)
            {
                // Dragging the object
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    selectedObject.position = hitPose.position;
                }
            }
        }
        else if (Input.touchCount == 2) // Pinch and Rotate
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                // Start pinch or rotate
                initialTouch1 = touch1.position;
                initialTouch2 = touch2.position;
                initialPinchDistance = Vector2.Distance(initialTouch1, initialTouch2);
                if (selectedObject != null) initialRotation = selectedObject.rotation;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // Pinch to scale
                float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float scaleChange = currentPinchDistance / initialPinchDistance;

                if (selectedObject != null)
                {
                    selectedObject.localScale *= scaleChange;
                    initialPinchDistance = currentPinchDistance; // Update for smooth scaling
                }

                // Rotate
                Vector2 currentTouch1 = touch1.position;
                Vector2 currentTouch2 = touch2.position;

                float initialAngle = Mathf.Atan2(initialTouch2.y - initialTouch1.y, initialTouch2.x - initialTouch1.x) * Mathf.Rad2Deg;
                float currentAngle = Mathf.Atan2(currentTouch2.y - currentTouch1.y, currentTouch2.x - currentTouch1.x) * Mathf.Rad2Deg;

                float angleDifference = currentAngle - initialAngle;

                if (selectedObject != null)
                {
                    selectedObject.rotation = initialRotation * Quaternion.Euler(0, -angleDifference, 0);
                }
            }
        }

        // Reset when fingers are lifted
        if (Input.touchCount == 0)
        {
            selectedObject = null;
        }
    }
}