using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Transform player;

    public Animator anim;

    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;
    public float waitTime = 2f;

    [Header("Chase")]
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;

    private Transform currentTarget;
    private bool chasing;

    private Player playerScript;

    private bool waiting;
    private float waitTimer;

    void Start()
    {
        playerScript = player.GetComponent<Player>();

        currentTarget = pointB;
    }

    void FixedUpdate()
    {
        if (playerScript == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        // Only detect the player if we are NOT already chasing
        if (!chasing)
        {
            if (distance <= detectionRange && !playerScript.isHidden)
            {
                chasing = true;
                anim.SetTrigger("Attack");
            }
        }

        if (chasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (waiting)
        {
            rb.linearVelocity = new Vector2(
                0,
                rb.linearVelocity.y
            );

            waitTimer -= Time.fixedDeltaTime;

            if (waitTimer <= 0)
            {
                waiting = false;

                if (currentTarget == pointA)
                    currentTarget = pointB;
                else
                    currentTarget = pointA;
            }

            return;
        }

        float direction = Mathf.Sign(
            currentTarget.position.x - transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direction * patrolSpeed,
            rb.linearVelocity.y
        );

        // Reached patrol point
        if (Mathf.Abs(
            transform.position.x - currentTarget.position.x
        ) < 0.1f)
        {
            rb.linearVelocity = new Vector2(
                0,
                rb.linearVelocity.y
            );

            waiting = true;
            waitTimer = waitTime;
        }
    }

    void ChasePlayer()
    {
        float direction = Mathf.Sign(
            player.position.x - transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direction * chaseSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );
    }
}