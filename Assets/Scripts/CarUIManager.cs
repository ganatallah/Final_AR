using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CarUIManager : MonoBehaviour
{
    public GameObject car; // Root of the spawned car

    private List<MeshRenderer> carRenderers;

    [Header("UI Panels")]
    public GameObject colorPanel;
    public GameObject infoPanel;

    [Header("Audio")]
    public AudioSource engineAudioSource;
    public AudioSource infoAudioSource;

    [Header("Car Materials")]
    public Material redMat;
    public Material blueMat;
    public Material greenMat;

    void Start()
    {
        // Automatically get all MeshRenderers inside the car
        if (car != null)
        {
            carRenderers = car.GetComponentsInChildren<MeshRenderer>().ToList();
            Debug.Log("Found renderers: " + carRenderers.Count);
        }

        colorPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void ToggleColorPanel()
    {
        colorPanel.SetActive(!colorPanel.activeSelf);
    }

    public void ChangeColor(string color)
    {
        if (carRenderers == null || carRenderers.Count == 0) return;

        Material selectedMat = null;

        switch (color)
        {
            case "Red": selectedMat = redMat; break;
            case "Blue": selectedMat = blueMat; break;
            case "Green": selectedMat = greenMat; break;
        }

        if (selectedMat != null)
        {
            foreach (var renderer in carRenderers)
            {
                renderer.material = selectedMat;
            }
        }

        colorPanel.SetActive(false);
    }

    public void PlayEngineSound()
    {
        if (engineAudioSource && !engineAudioSource.isPlaying)
            engineAudioSource.Play();
    }

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        if (infoAudioSource) infoAudioSource.Play();
    }

    public void CloseInfo()
    {
        infoPanel.SetActive(false);
        if (infoAudioSource) infoAudioSource.Stop();
    }
}


