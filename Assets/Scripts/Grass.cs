using UnityEngine;

public class Grass : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            player.isHidden = true;
            Debug.Log("PLAYER IS HIDDEN");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            player.isHidden = false;
            Debug.Log("PLAYER IS VISIBLE");
        }
    }
}