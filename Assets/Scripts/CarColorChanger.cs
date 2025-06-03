using UnityEngine;

public class CarColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] targetRenderers;
    private Material[] instancedMaterials;

    private void Awake()
    {
        // Create unique material instances to avoid affecting other cars
        instancedMaterials = new Material[targetRenderers.Length];
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            instancedMaterials[i] = targetRenderers[i].material;
        }
    }

    /// <summary>
    /// Call this to change the car's base color
    /// </summary>
    /// <param name="color">New color tint</param>
    private void ChangeColor(Color color)
    {
        foreach (var mat in instancedMaterials)
        {
            mat.SetColor("_BaseColor", color); // "_BaseColor" is the property used in URP/Lit
        }
    }

    public void OnColorPickerChange(Color newColor)
    {
        ChangeColor(newColor);
    }

    /// <summary>
    /// Call this to assign a completely random color to the car
    /// </summary>
    public void ChangeToRandomColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.6f, 1f);
        Debug.Log(randomColor);
        ChangeColor(randomColor);
    }
}
