using UnityEngine;

/// <summary>
/// Persistent singleton that holds all shared SFX clips and music tracks, and exposes
/// central Play / music control methods. Assign the AudioClip fields in the Inspector on
/// the Menu scene's AudioManager GameObject.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("SFX Clips")]
    public AudioClip clipHit;       // Box hit  — hit_7
    public AudioClip clipJump;      // Trampoline bounce — jump_4
    public AudioClip clipCoin;      // Fish / Fruit collected — coin_7
    public AudioClip clipDamage;    // Player takes damage — beep_2
    public AudioClip clipExplosion; // Box destroyed (fire release) — explosion_7

    [Header("Music Clips")]
    public AudioClip clipMainMenu;  // main_menu/call_to_adventure_loop
    public AudioClip clipGame;      // game/status_quo_loop
    public AudioClip clipGameOver;  // game_over/crisis_loop
    public AudioClip clipVictory;   // victory/victory_loop

    [Header("Music Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 0.35f;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy only this component — the GameObject may host other persistent
            // components (GameManager, MainMenuController) that must stay alive.
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
    }

    /// <summary>Plays the given SFX clip as a one-shot so overlapping sounds work correctly.</summary>
    public void Play(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    /// <summary>Starts looping the main menu music track. Ignored if already playing.</summary>
    public void PlayMainMenuMusic()
    {
        PlayMusic(clipMainMenu);
    }

    /// <summary>Starts looping the in-game ambient music track. Ignored if already playing.</summary>
    public void PlayGameMusic()
    {
        PlayMusic(clipGame);
    }

    /// <summary>Plays the game over music track once (no loop).</summary>
    public void PlayGameOverMusic()
    {
        if (clipGameOver == null) return;
        musicSource.loop = false;
        musicSource.volume = musicVolume;
        musicSource.clip = clipGameOver;
        musicSource.Play();
    }

    /// <summary>Stops the current music track immediately.</summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ------------------------------------------------------------------ //
    // Internal helpers
    // ------------------------------------------------------------------ //

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.clip = clip;
        musicSource.Play();
    }
}
