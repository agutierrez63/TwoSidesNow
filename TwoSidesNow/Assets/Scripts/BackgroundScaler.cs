using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public GameObject background;

    // Reference to the ProCamera2D component
    private ProCamera2D proCamera;

    // Initial scale of the background object
    private Vector3 initialScale;

    // Adjust this factor to fine-tune the background scale
    public float scaleMultiplier = 1f;

    void Start()
    {
        // Get the ProCamera2D component attached to the camera
        proCamera = GetComponent<ProCamera2D>();

        // Record the initial scale of the background
        initialScale = background.transform.localScale;
    }

    void Update()
    {
        // Ensure we have both the camera component and background assigned
        if (proCamera != null && background != null)
        {
            // Get the current orthographic size of the camera
            float orthographicSize = proCamera.GameCamera.orthographicSize;

            // Calculate the scale factor based on the orthographic size
            float scaleFactor = orthographicSize * scaleMultiplier;

            // Apply the scale factor to the initial scale of the background
            background.transform.localScale = initialScale * scaleFactor / 10;
        }
    }
}
