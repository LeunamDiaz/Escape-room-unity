using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static int CurrentProgress { get; private set; } = 0;

    public static void SetProgress(int value)
    {
        CurrentProgress = Mathf.Max(CurrentProgress, value);
    }
}
