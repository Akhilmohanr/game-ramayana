using UnityEngine;

public class FlowerSway : MonoBehaviour
{
    [Header("Sway")]
    [SerializeField] private float swayAmount = 8f;
    [SerializeField] private float swaySpeed = 2f;

    [Header("Random Variation")]
    [SerializeField] private float speedVariation = 0.3f;
    [SerializeField] private float amountVariation = 0.2f;

    private float offset;
    private float startRotation;
    private float individualSpeed;
    private float individualAmount;

    public void Initialize()
    {
        startRotation = transform.localEulerAngles.z;

        offset = Random.Range(0f, 100f);

        individualSpeed =
            swaySpeed * Random.Range(
                1f - speedVariation,
                1f + speedVariation
            );

        individualAmount =
            swayAmount * Random.Range(
                1f - amountVariation,
                1f + amountVariation
            );
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        // Smooth left-right movement
        float sway =
            Mathf.Sin(Time.time * individualSpeed + offset)
            * individualAmount;

        transform.localRotation =
            Quaternion.Euler(
                0f,
                0f,
                startRotation + sway
            );
    }
}