using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public GameObject pauseCanvas;
    private bool paused;
    private List<AudioSource> pausedAudioSources;
    void Start() {
        paused = false;
        pausedAudioSources = new List<AudioSource>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                PauseGame();
                paused = true;
            } else {
                ResumeGame();
                paused = false;
            }
        }
    }

    private void PauseGame() {

        Time.timeScale = 0;
        pausedAudioSources.Clear();
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audioSources) {
            if (a.isPlaying) {
                a.Pause();
                pausedAudioSources.Add(a);
            }
        }
        GetComponent<PauseUINavigation>().ResetCursor();
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        foreach (AudioSource a in pausedAudioSources) {
            a.UnPause();
        }
        pauseCanvas.SetActive(false);
    }

    public bool IsPaused() {
        return paused;
    }
}
