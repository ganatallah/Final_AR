using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Default: rotate around Y axis
    public float rotationSpeed = 30f;         // Degrees per second
    public bool rotateOnlyWhenVisible = true; // Optional: only rotate when marker is tracked

    private Renderer[] renderers;

    void Start()
    {
        if (rotateOnlyWhenVisible)
        {
            // Cache all renderers to check visibility (useful for marker tracking)
            renderers = GetComponentsInChildren<Renderer>();
        }
    }

    void Update()
    {
        if (rotateOnlyWhenVisible && !IsVisible())
            return;

        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private bool IsVisible()
    {
        foreach (var rend in renderers)
        {
            if (rend.isVisible)
                return true;
        }
        return false;
    }
}
