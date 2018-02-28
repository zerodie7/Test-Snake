using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    public AudioSource audioSource;

    void Awake() {
        DontDestroyOnLoad (gameObject);
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }

    void OnLevelWasLoaded(int level) {
        AudioClip thisLevelMusic = levelMusicChangeArray[level];

        if (thisLevelMusic) { // Some musci attached
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

	public void stopMusic()
	{
		audioSource.Stop();
	}

    public void SetVolume (float volume) {
        audioSource.volume = volume;
    }

}

