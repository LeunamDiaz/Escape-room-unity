using UnityEngine;

public class TurnstileGate : MonoBehaviour
{
    public int gateId = 0;

    [SerializeField] private Collider2D blockCollider;
    [SerializeField] private Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioSource openSound;

    private bool opened;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;
        if (!other.CompareTag("Player")) return;

        if (GameProgress.CurrentProgress >= gateId)
            Open();
    }

    public void Open()
    {
        opened = true;

        if (animator != null)
            animator.SetTrigger("Open");

        if (blockCollider != null)
            blockCollider.enabled = false;

        if (openSound != null)
            openSound.Play();
    }
}
