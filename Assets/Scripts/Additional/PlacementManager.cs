using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    private GameObject currentlySelectedPrefab;

    private XROrigin sessionOrigin;
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeMAnager;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    private void Awake()
    {
        sessionOrigin = GetComponent<XROrigin>();
        raycastManager = GetComponent<ARRaycastManager>();
        planeMAnager = GetComponent<ARPlaneManager>();
    }

    public void SetNewObjectTospawn(GameObject newObject)
    {
        currentlySelectedPrefab  = newObject;
    }
    public bool isUITouched()
    {
        return EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() != null;
    }

    private void Update() 
    {
        if(currentlySelectedPrefab == null)
            return;
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //ShootRaycast
                //test on PC
                //bool collision = raycastManager.Raycast(Input.mousePosition, raycastHits, TrackableType.PlaneWithinPolygon);
                //For android touchscreen
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);

                //PlaceObject
                if (collision && isUITouched() == false)
                {
                    GameObject _object = Instantiate(currentlySelectedPrefab);
                    _object.transform.position = raycastHits[0].pose.position;
                    _object.transform.rotation = raycastHits[0].pose.rotation;
                }
                //Disable plane
                foreach (var plane in planeMAnager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                planeMAnager.enabled = false;
            }    
        }
    }
}
