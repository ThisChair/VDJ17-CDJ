using UnityEngine;

public class AudioController : MonoBehaviour {

    private static AudioSource audioController;

    private void Start()
    {
        audioController = GetComponent<AudioSource>();
    }

    public static void SetAndPlayAudioClip(AudioClip clip, float volume = 0.5f) {

        audioController.clip = clip;
        audioController.volume = volume;
        audioController.Play();

    }

}
