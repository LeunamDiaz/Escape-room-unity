using UnityEngine;

public class ClueInteract : MonoBehaviour
{
    [SerializeField] private GameObject cluePanel;
    private bool playerInRange;

    private void Start()
    {
        if (cluePanel != null) cluePanel.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (cluePanel != null && cluePanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Open();
        }
    }

    private void Open()
    {
        if (cluePanel == null) return;
        if (cluePanel.activeSelf) return;

        cluePanel.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Close()
    {
        if (cluePanel == null) return;
        cluePanel.SetActive(false);
        //Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}
