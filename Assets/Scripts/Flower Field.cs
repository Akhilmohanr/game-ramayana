using UnityEngine;

public class FlowerField : MonoBehaviour
{
    [Header("Flower")]
    [SerializeField] private GameObject flowerPrefab;

    [Header("Field Size")]
    [SerializeField] private float fieldWidth = 30f;
    [SerializeField] private float fieldDepth = 6f;

    [Header("Flower Count")]
    [SerializeField] private int flowerCount = 500;

    [Header("Flower Size")]
    [SerializeField] private float frontSize = 1.4f;
    [SerializeField] private float backSize = 0.35f;

    [Header("Size Variation")]
    [SerializeField] private float sizeVariation = 0.15f;

    [Header("Natural Movement")]
    [SerializeField] private float rotationVariation = 4f;

    [Header("Depth Distribution")]
    [SerializeField] private float frontDensity = 1.5f;

    private void Start()
    {
        GenerateFlowers();
    }

    private void GenerateFlowers()
    {
        for (int i = 0; i < flowerCount; i++)
        {
            // -----------------------------------------
            // POSITION
            // -----------------------------------------

            float x = Random.Range(
                -fieldWidth / 2f,
                fieldWidth / 2f
            );

            // Bias flowers toward the front
            float depth = Mathf.Pow(
                Random.value,
                frontDensity
            );

            float y = depth * fieldDepth;

            Vector3 position = transform.position +
                               new Vector3(x, y, 0f);

            // -----------------------------------------
            // CREATE FLOWER
            // -----------------------------------------

            GameObject flower = Instantiate(
                flowerPrefab,
                position,
                Quaternion.identity,
                transform
            );

            // -----------------------------------------
            // SIZE BASED ON DEPTH
            // -----------------------------------------

            // 0 = front
            // 1 = back

            float depthPercent = y / fieldDepth;

            float size = Mathf.Lerp(
                frontSize,
                backSize,
                depthPercent
            );

            // Small random variation
            size += Random.Range(
                -sizeVariation,
                sizeVariation
            );

            flower.transform.localScale =
                Vector3.one * size;

            // -----------------------------------------
            // RANDOM ROTATION
            // -----------------------------------------

            float rotation = Random.Range(
                -rotationVariation,
                rotationVariation
            );

            flower.transform.localRotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    rotation
                );

            // -----------------------------------------
            // SORTING
            // -----------------------------------------

            SpriteRenderer renderer =
                flower.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                // Front flowers render in front
                renderer.sortingOrder =
                    Mathf.RoundToInt(
                        (1f - depthPercent) * 100
                    );
            }

            // -----------------------------------------
            // INITIALIZE SWAY
            // -----------------------------------------

            FlowerSway sway =
                flower.GetComponent<FlowerSway>();

            if (sway != null)
            {
                sway.Initialize();
            }
        }
    }
}