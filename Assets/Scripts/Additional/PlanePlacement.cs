using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanePlacement : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private GameObject placedObject;

    void Awake()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        planeManager = FindObjectOfType<ARPlaneManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if (placedObject == null)
                    {
                        placedObject = Instantiate(prefabToSpawn, hitPose.position, hitPose.rotation);
                    }
                    else
                    {
                        placedObject.transform.position = hitPose.position;
                        placedObject.transform.rotation = hitPose.rotation;
                    }
                }
            }
        }
    }
}