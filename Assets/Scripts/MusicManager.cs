using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioClip deathClip;
    public AudioClip movementClip;
    private AudioSource audioSource;

    // Adjust this delay as needed
    public float movementDelay = 0.1f;
    private bool isMovementAudioPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get or add an AudioSource component to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        // Example: Play movement audio clip if the player is moving (replace this condition with your actual movement check)
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!isMovementAudioPlaying)
            {
                StartCoroutine(PlayMovementAudioWithDelay());
            }
        }
    }

    private IEnumerator PlayMovementAudioWithDelay()
    {
        // Set the flag to prevent multiple coroutine instances
        isMovementAudioPlaying = true;

        // Wait for the specified delay
        yield return new WaitForSeconds(movementDelay);

        // Play the movement audio clip
        PlayAudioClip(movementClip);

        // Reset the flag after playing the audio clip
        isMovementAudioPlaying = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is "MainMenu" or "GameOver" and play the appropriate audio clip
        if (scene.name == "MainMenu")
        {
            PlayAudioClip(musicClip);
        }
        else if (scene.name == "GameOver")
        {
            PlayAudioClip(deathClip);
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        // Set the audio clip
        audioSource.clip = clip;

        // Play the audio clip
        audioSource.Play();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


