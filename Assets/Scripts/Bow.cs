using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBow : MonoBehaviour
{
    [Header("References")]
    public GameObject arrowPrefab;
    public Transform firePoint;
    public Camera cam;

    [Header("Bow Settings")]
    public float arrowSpeed = 15f;
    public float fireCooldown = 0.4f;

    private float cooldownTimer;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public void OnShoot(InputValue value)
    {
        if (!value.isPressed)
            return;

        Shoot();
    }

    void Shoot()
    {
        if (cooldownTimer > 0f)
            return;

        if (arrowPrefab == null || firePoint == null)
            return;

        cooldownTimer = fireCooldown;

        // Get mouse position in world space
        Vector3 mousePosition = cam.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

        mousePosition.z = 0f;

        // Direction from bow to mouse
        Vector2 direction = (
            mousePosition - firePoint.position
        ).normalized;

        // Create arrow
        GameObject arrow = Instantiate(
            arrowPrefab,
            firePoint.position,
            Quaternion.identity
        );

        // Give arrow velocity
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        if (arrowRb != null)
        {
            arrowRb.linearVelocity = direction * arrowSpeed;
        }

        // Rotate arrow to point in direction of travel
        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        arrow.transform.rotation = Quaternion.Euler(
            0f,
            0f,
            angle
        );
    }
}