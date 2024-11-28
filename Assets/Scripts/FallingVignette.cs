using UnityEngine;

public class FallingVignette : MonoBehaviour
{
    public Transform playerTransform;
    
    public float maxFallSpeed = 20f;

    [Tooltip("Minimum aperture size when falling at max speed.")]
    public float minApertureSize = 0.5f;

    [Tooltip("Material property name for the aperture size.")]
    private static readonly int ApertureSizeProperty = Shader.PropertyToID("_ApertureSize");

    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock propertyBlock;
    private float lastPlayerY;

    void Start()
    {
        if (playerTransform == null)
        {
            if (Camera.main != null)
            {
                playerTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogError("VerticalSpeedVignette: No playerTransform assigned and no main camera found.");
                enabled = false;
                return;
            }
        }

        lastPlayerY = playerTransform.position.y;

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("VerticalSpeedVignette requires a MeshRenderer component.");
            enabled = false;
            return;
        }

        propertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        float currentY = playerTransform.position.y;
        float verticalSpeed = (currentY - lastPlayerY) / Time.deltaTime;
        lastPlayerY = currentY;

        if (verticalSpeed < 0f)
        {
            float fallSpeed = -verticalSpeed; 
            float apertureSize = ComputeApertureSize(fallSpeed);
            UpdateVignette(apertureSize);
        }
        else
        {
            UpdateVignette(1f);
        }
    }

    float ComputeApertureSize(float fallSpeed)
    {
        fallSpeed = Mathf.Clamp(fallSpeed, 0f, maxFallSpeed);
        float t = fallSpeed / maxFallSpeed;
        float apertureSize = Mathf.Lerp(1f, minApertureSize, t);
        return apertureSize;
    }

    void UpdateVignette(float apertureSize)
    {
        meshRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat(ApertureSizeProperty, apertureSize);
        meshRenderer.SetPropertyBlock(propertyBlock);
    }
}
