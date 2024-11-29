using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private bool triggered = false;
    public GameObject explosionParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && triggered == false)
        {
            triggered = true;

            Debug.Log("Obstacle Triggered! Ralentir le temps et afficher la rune.");

            Time.timeScale = 0.1f;
            GameManager.Instance.TriggerRune();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.isValidationRune)
        {
            GameManager.Instance.isDead = true;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isDestroy)
        {
            Instantiate(explosionParticle, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(gameObject);
            GameManager.Instance.isDestroy = false;
        }
    }
}


