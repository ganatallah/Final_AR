using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class CarPlacer : MonoBehaviour
{
    public GameObject[] carPrefabs; // Assign BMW M4 [0], BMW i8 [1]
    private int selectedIndex = 0;

    public TextMeshProUGUI toggleButtonText; // Optional: to show current car name

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

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
            Instantiate(carPrefabs[selectedIndex], hitPose.position, hitPose.rotation);
        }
    }

    public void ToggleCarSelection()
    {
        selectedIndex = (selectedIndex + 1) % carPrefabs.Length;
        Debug.Log("Switched to: " + carPrefabs[selectedIndex].name);
        UpdateButtonLabel();
    }

    private void UpdateButtonLabel()
    {
        if (toggleButtonText != null)
        {
            toggleButtonText.text = "Selected: " + carPrefabs[selectedIndex].name;
        }
    }
}
