using UnityEngine;
using TMPro;

public class PuzzleQuestionInteract : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text feedbackText;

    [Header("Answer")]
    [SerializeField] private string correctAnswer = "DAVINCI";
    [SerializeField] private int progressOnSolve = 1;

    private bool playerInRange;
    private bool solved;

    private void Start()
    {
        if (questionPanel != null) questionPanel.SetActive(false);
        if (feedbackText != null) feedbackText.text = "";
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (questionPanel != null && questionPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Open();
        }
    }

    private void Open()
    {
        if (questionPanel == null) return;
        if (questionPanel.activeSelf) return; // safety

        questionPanel.SetActive(true);

        if (feedbackText != null)
            feedbackText.text = solved ? "Already solved!" : "";

        if (inputField != null)
        {
            inputField.text = "";
            inputField.ActivateInputField();
        }

       // Time.timeScale = 0f;
    }

    public void Close()
    {
        if (questionPanel == null) return;
        questionPanel.SetActive(false);
       // Time.timeScale = 1f;
    }

    public void Submit()
    {
        if (solved) return;

        string answer = inputField != null ? inputField.text : "";
        answer = answer.ToUpper().Replace(" ", "");

        if (answer == correctAnswer)
        {
            solved = true;
            if (feedbackText != null) feedbackText.text = "Correct!";

            GameProgress.SetProgress(progressOnSolve);

            Invoke(nameof(Close), 0.5f);
        }
        else
        {
            if (feedbackText != null) feedbackText.text = "Wrong. Try again!";
            if (inputField != null) inputField.ActivateInputField();
        }
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
