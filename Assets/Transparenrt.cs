using UnityEngine;

public class PlayerTransparency : MonoBehaviour
{
    public Camera mainCamera;
    public float transparencyDistance = 5.0f;
    private Renderer playerRenderer;
    private Color[] originalColors;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();

        if (playerRenderer == null)
        {
            Debug.LogError("Renderer component not found on the player object.");
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not assigned.");
        }

        // Store the original colors of all materials
        originalColors = new Color[playerRenderer.materials.Length];
        for (int i = 0; i < playerRenderer.materials.Length; i++)
        {
            originalColors[i] = playerRenderer.materials[i].color;
        }
    }

    void FixedUpdate()
    {
        if (mainCamera == null || playerRenderer == null)
        {
            return;
        }

        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Loop through all materials and adjust their transparency
        for (int i = 0; i < playerRenderer.materials.Length; i++)
        {
            Color color = originalColors[i];

            if (distance < transparencyDistance)
            {
                // Set the material to transparent mode
                SetMaterialRenderingMode(playerRenderer.materials[i], true);
                // Calculate the alpha value based on distance
                color.a = Mathf.Lerp(0.0f, originalColors[i].a, distance / transparencyDistance);
            }
            else
            {
                // Set the material to opaque mode
                SetMaterialRenderingMode(playerRenderer.materials[i], false);
                color.a = originalColors[i].a;
            }

            playerRenderer.materials[i].color = color;
        }
    }

    void SetMaterialRenderingMode(Material material, bool isTransparent)
    {
        if (isTransparent)
        {
            material.SetFloat("_Mode", 2);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
        else
        {
            material.SetFloat("_Mode", 0);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }
    }
}
