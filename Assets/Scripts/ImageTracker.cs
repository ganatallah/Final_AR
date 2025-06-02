using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;
    List<GameObject> ARObjects = new List<GameObject>();

    void Awake()
    {
        Debug.Log("Starting");
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Added Images
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("Image added: " + trackedImage.referenceImage.name);

            foreach (var arPrefab in ArPrefabs)
            {
                Debug.Log("Checking prefab: " + arPrefab.name);

                if (trackedImage.referenceImage.name == arPrefab.name)
                {
                    Debug.Log("Match found! Instantiating prefab: " + arPrefab.name);
                    var newPrefab = Instantiate(arPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    newPrefab.transform.parent = trackedImage.transform;
                    ARObjects.Add(newPrefab);
                    break;
                }
            }
        }

        // Updated Images
        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log("Image updated: " + trackedImage.referenceImage.name + " | Tracking state: " + trackedImage.trackingState);

            foreach (var obj in ARObjects)
            {
                // Compare reference image name with spawned object name
                if (obj.name.Contains(trackedImage.referenceImage.name))
                {
                    bool isTracking = trackedImage.trackingState == TrackingState.Tracking;
                    Debug.Log("Updating object " + obj.name + " | Set active: " + isTracking);
                    obj.SetActive(isTracking);
                }
            }
        }

        // Removed Images
        foreach (var trackedImage in eventArgs.removed)
        {
            Debug.Log("Image removed: " + trackedImage.referenceImage.name);
        }
    }
}
