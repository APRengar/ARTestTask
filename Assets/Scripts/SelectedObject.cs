using UnityEngine;

public class SelectedObject : MonoBehaviour
{
    public Material myMaterial;
    [SerializeField] Color myDefaultColor;

    private void Start()
    {
        //Setup default color on start
        myMaterial.SetColor("baseColorFactor", myDefaultColor);
    }
}
