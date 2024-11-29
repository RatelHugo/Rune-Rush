using System.Collections;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    private float tempSpeed;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * GameManager.Instance.speedPlayer * Time.deltaTime);


        if (GameManager.Instance.isDead)
        {
            Death();
        }
        if (GameManager.Instance.isValidationRune)
        {
            DestroyObstacle();
        }
    }

    private void DestroyObstacle()
    {
        tempSpeed = GameManager.Instance.speedPlayer;
        GameManager.Instance.speedPlayer = 0;
        GameManager.Instance.isDestroy = true;
        Time.timeScale = 1f;
        StartCoroutine(PlayAnimationAndWait());
        GameManager.Instance.isValidationRune = false;
    }

    IEnumerator PlayAnimationAndWait()
    {
        anim.SetBool("isAttackAnim", true);
        yield return new WaitForSeconds(0.75f);
        anim.SetBool("isAttackAnim", false);
        GameManager.Instance.speedPlayer = tempSpeed;
    }

    private void Death()
    {
        GameManager.Instance.canvas.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.speedPlayer = 0;
        StartCoroutine(WaitDeathAnim());
    }

    IEnumerator WaitDeathAnim()
    {
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.isDeadEnd = true;
    }
}
