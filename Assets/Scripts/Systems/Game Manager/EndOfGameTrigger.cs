using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameTrigger : MonoBehaviour
{
    [Header("Auto-walk settings")]
    [SerializeField] private float moveDistance = 3f;   // unități Unity
    [SerializeField] private float moveSpeed = 2f;      // unități/secundă

    [Header("Scene transition")]
    [SerializeField] private string gameOverSceneName = "GameOver";
    [SerializeField] private float loadSceneDelay = 2f;

    [Header("Animator")]
    [Tooltip("Numele state-ului din Animator (dacă vrei să forțezi play direct).")]
    [SerializeField] private string runRightStateName = "Lila_Run_Right";

    [Header("Animator Parameters")]
    [SerializeField] private bool setAnimatorParameters = true;
    [SerializeField] private string isMovingParam = "isMoving";
    [SerializeField] private string moveXParam = "moveX";
    [SerializeField] private string moveYParam = "moveY";

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(AutoWalkAndEnd(other.gameObject));
    }

    private IEnumerator AutoWalkAndEnd(GameObject player)
    {
        var movement = player.GetComponent<TopDownPlayer>();
        if (movement != null)
            movement.enabled = false;

        var animator = player.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            if (setAnimatorParameters)
            {
                animator.SetBool(isMovingParam, true);
                animator.SetFloat(moveXParam, 1f);
                animator.SetFloat(moveYParam, 0f);
            }

            if (!string.IsNullOrEmpty(runRightStateName))
                animator.Play(runRightStateName, 0, 0f);
        }

        var rb = player.GetComponent<Rigidbody2D>();

        Vector3 start = player.transform.position;
        Vector3 target = start + Vector3.right * moveDistance;

        while (Vector3.Distance(player.transform.position, target) > 0.01f)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector3 newPos = Vector3.MoveTowards(player.transform.position, target, step);

            if (rb != null)
                rb.MovePosition(newPos);
            else
                player.transform.position = newPos;

            yield return null;
        }

        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(gameOverSceneName);
    }
}
