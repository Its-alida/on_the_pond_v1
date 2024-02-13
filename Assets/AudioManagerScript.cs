using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Play the audio when the game starts
        PlayAudio();
    }

    void PlayAudio()
    {
        // Check if an audio clip is assigned
        if (audioSource.clip != null)
        {
            // Play the assigned audio clip
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No audio clip assigned to the AudioManager's AudioSource.");
        }
    }
}
