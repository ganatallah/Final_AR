using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColour : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] targetRenderers;
    private Material[] instancedMaterials;

    private void Awake()
    {
        instancedMaterials = new Material[targetRenderers.Length];
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            instancedMaterials[i] = targetRenderers[i].material;
        }
    }

    
    // <param name="color">New color tint</param>
    private void ChangeColor(Color color)
    {
        foreach (var mat in instancedMaterials)
        {
            mat.SetColor("_BaseColor", color); 
        }
    }

    public void OnColorPickerChange(Color newColor)
    {
        ChangeColor(newColor);
    }

    
    public void ChangeToRandomColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.6f, 1f);
        Debug.Log(randomColor);
        ChangeColor(randomColor);
    }
}
