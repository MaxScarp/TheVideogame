using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    private GridObject gridObject;

    /// <summary>
    /// Initialize the GridDebugObject.
    /// </summary>
    /// <param name="gridObject">GridObject to which the GridDebugObject will refer to.</param>
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        text.text = gridObject.ToString();
    }
}
