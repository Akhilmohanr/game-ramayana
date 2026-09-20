using UnityEngine;

public class ParallaxCamera2D : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layer;

        [Range(0f, 1f)]
        public float speed = 0.5f;

        [HideInInspector]
        public Vector3 startPosition;
    }

    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = transform.position;

        // Store starting position of each layer
        foreach (ParallaxLayer parallaxLayer in layers)
        {
            if (parallaxLayer.layer != null)
                parallaxLayer.startPosition = parallaxLayer.layer.position;
        }
    }

    void LateUpdate()
    {
        Vector3 cameraMovement = transform.position - lastCameraPosition;

        foreach (ParallaxLayer parallaxLayer in layers)
        {
            if (parallaxLayer.layer == null)
                continue;

            // Move the layer based on its individual speed
            parallaxLayer.layer.position += cameraMovement * parallaxLayer.speed;
        }

        lastCameraPosition = transform.position;
    }
}