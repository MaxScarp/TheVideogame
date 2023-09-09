using UnityEngine;

public class GridObjectVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void UpdateMaterial(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
