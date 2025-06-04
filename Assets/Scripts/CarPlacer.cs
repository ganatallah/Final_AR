using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class CarPlacer : MonoBehaviour
{
    public GameObject[] carPrefabs; // BMW M4 [0], BMW i8 [1]
    private int selectedIndex = 0;

    public TextMeshProUGUI toggleButtonText;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject currentCarInstance = null; // Tracks last spawned car

    void Awake()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        UpdateButtonLabel();
    }

    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began)
            return;

        Touch touch = Input.GetTouch(0);
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // Destroy previous car if it exists
            if (currentCarInstance != null)
                Destroy(currentCarInstance);

            // Instantiate the selected car
            currentCarInstance = Instantiate(carPrefabs[selectedIndex], hitPose.position, hitPose.rotation);
        }
    }

    public void ToggleCarSelection()
    {
        selectedIndex = (selectedIndex + 1) % carPrefabs.Length;
        Debug.Log("Switched to: " + carPrefabs[selectedIndex].name);
        UpdateButtonLabel();

        // Find all car objects
        GameObject[] allCars = GameObject.FindGameObjectsWithTag("Car");

        // Get position/rotation from the first car (if any)
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;
        bool foundOne = false;

        foreach (GameObject car in allCars)
        {
            if (!foundOne)
            {
                spawnPos = car.transform.position;
                spawnRot = car.transform.rotation;
                foundOne = true;
            }

            Destroy(car);
        }

        if (!foundOne)
        {
            Debug.LogWarning("No existing cars found to replace.");
            return;
        }

        // Spawn the selected car at the position of the first destroyed one
        currentCarInstance = Instantiate(carPrefabs[selectedIndex], spawnPos, spawnRot);
    }

    private void UpdateButtonLabel()
    {
        if (toggleButtonText != null)
        {
            toggleButtonText.text = "Selected: " + carPrefabs[selectedIndex].name;
        }
    }
}

