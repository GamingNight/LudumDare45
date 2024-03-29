﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartUINavigation : MonoBehaviour {

    public Text start;
    public Text quit;

    public Font openLight;
    public Font openBold;

    public GameObject gameContainer;
    public AudioSource menuSelectSound;

    private int currentCursorValue;
    private float prevVerticalvalue;

    private AudioSource menuHoverSound;

    void Start() {
        currentCursorValue = 0;
        menuHoverSound = GetComponent<AudioSource>();
    }

    void Update() {

        float v = Input.GetAxisRaw("Vertical");

        if (v != 0 && v != prevVerticalvalue) {
            int newCursorValue = currentCursorValue - (int)v;
            if (newCursorValue < 0) {
                newCursorValue = 1;
            } else if (newCursorValue > 1) {
                newCursorValue = 0;
            }
            FocusCursorOnvalue(newCursorValue);
        }
        prevVerticalvalue = v;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (currentCursorValue) {
                case 0:
                    StartWithNothing();
                    break;
                case 1:
                    Quit();
                    break;
                default:
                    break;
            }
        }
    }

    public void FocusCursorOnvalue(int value) {
        switch (value) {
            case 0:
                start.font = openBold;
                quit.font = openLight;
                break;
            case 1:
                start.font = openLight;
                quit.font = openBold;
                break;
            default:
                break;
        }
        if (value != currentCursorValue) {
            menuHoverSound.Play();
        }

        currentCursorValue = value;
    }

    public void StartWithNothing() {

        StartCoroutine(StartWithNothingCoroutine());
    }

    public void Quit() {
        menuSelectSound.Play();
        Application.Quit();
    }

    private IEnumerator StartWithNothingCoroutine() {

        menuSelectSound.Play();
        while (menuSelectSound.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
        gameContainer.SetActive(true);
    }
}
