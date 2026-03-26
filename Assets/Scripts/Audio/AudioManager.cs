using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        if (musicSource != null)
        {
            musicSource.loop = true;
            if (!musicSource.isPlaying)
                musicSource.Play();
        }
    }

    public void SetMusicVolume(float v)
    {
        if (musicSource != null) musicSource.volume = v;
    }

    public void PauseMusic() => musicSource?.Pause();
    public void ResumeMusic() => musicSource?.UnPause();
}
