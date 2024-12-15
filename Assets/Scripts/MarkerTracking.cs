using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerTracking : MonoBehaviour
{
    [SerializeField] private ObjectManager objectsManager;
    [SerializeField] private ARTrackedImageManager imageManager;

    [SerializeField] private GameObject chairPrefab, cactusPrefab, mixerPrefab, robotPrefab;
    [SerializeField] private Vector3 offset;

    private Dictionary<string, GameObject> markerToPrefabMap;

    private GameObject currentObject;

     void Awake()
    {
        // Initialize the dictionary
        markerToPrefabMap = new Dictionary<string, GameObject>
        {
            { "chair", chairPrefab },
            { "cactus", cactusPrefab },
            { "mixer", mixerPrefab },
            { "robot", robotPrefab }
        };
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Loop through the added images
        foreach (var addedImage in eventArgs.added)
        {
            UpdateTrackedImage(addedImage);
        }

        // Update existing tracked images
        foreach (var updatedImage in eventArgs.updated)
        {
            UpdateTrackedImage(updatedImage);
        }

        // Remove objects for lost tracked images
        foreach (var removedImage in eventArgs.removed)
        {
            if (currentObject != null)
            {
                Destroy(currentObject);
                currentObject = null;
            }
        }
    }
    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        // Get the name of the tracked marker
        string markerName = trackedImage.referenceImage.name;

        // Check if the marker exists in the dictionary
        if (markerToPrefabMap.TryGetValue(markerName, out GameObject prefabToSpawn))
        {
            // Destroy the previously instantiated object, if any
            if (currentObject != null)
            {
                Destroy(currentObject);
            }

            // Instantiate the new object at the tracked marker's position and rotation
            currentObject = Instantiate(prefabToSpawn, trackedImage.transform.position + offset, trackedImage.transform.rotation);

            // Optional: Parent the object to the tracked marker so it moves with it
            currentObject.transform.SetParent(trackedImage.transform);
            objectsManager.ChangeObjectByImage(currentObject);
        }
    }
}

