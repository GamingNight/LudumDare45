﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndUINavigation : MonoBehaviour {

    public Text blackCoins;
    public Text redCoins;
    public Text blueCoins;
    public Text yellowCoins;
    public Text deathCount;
    public Text timeCount;
    public Text restart;
    public Text restartWith;
    public Text quit;
    public Font lightFont;
    public Font boldFont;

    public GameObject gameContainer;
    public AudioSource menuSelectSound;

    private int currentCursorValue;
    private float prevVerticalvalue;
    private AudioSource menuHoverSound;

    void Start() {
        currentCursorValue = 0;
        menuHoverSound = GetComponent<AudioSource>();
    }

    void OnEnable() {
        blackCoins.text = "Black: " + GameManager.Instance().GetCoinCount(Coin.CoinColor.BLACK) + "/20";
        redCoins.text = "Red: " + GameManager.Instance().GetCoinCount(Coin.CoinColor.RED) + "/20";
        blueCoins.text = "Blue: " + GameManager.Instance().GetCoinCount(Coin.CoinColor.BLUE) + "/20";
        yellowCoins.text = "Yellow: " + GameManager.Instance().GetCoinCount(Coin.CoinColor.YELLOW) + "/20";
        deathCount.text = "Deaths: " + GameManager.Instance().GetDeathCount();
        float timer = GameManager.Instance().GetTimer();
        float minutes = (int)timer / 60;
        float seconds = (int) timer % 60;
        int milliseconds = (int)((timer - (minutes * 60) - seconds) * 100);
        timeCount.text = "Time: " + minutes + ":" + seconds + "." + milliseconds;
    }

    void Update() {
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0 && v != prevVerticalvalue) {
            int newCursorValue = currentCursorValue - (int)v;
            if (newCursorValue < 0) {
                newCursorValue = 2;
            } else if (newCursorValue > 2) {
                newCursorValue = 0;
            }
            FocusCursorOnvalue(newCursorValue);
        }

        prevVerticalvalue = v;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (currentCursorValue) {
                case 0:
                    RestartWithNothing();
                    break;
                case 1:
                    RestartWithSomething();
                    break;
                case 2:
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
                restart.font = boldFont;
                restartWith.font = lightFont;
                quit.font = lightFont;
                break;
            case 1:
                restart.font = lightFont;
                restartWith.font = boldFont;
                quit.font = lightFont;
                break;
            case 2:
                restart.font = lightFont;
                restartWith.font = lightFont;
                quit.font = boldFont;
                break;
            default:
                break;
        }
        if (currentCursorValue != value) {
            menuHoverSound.Play();
        }
        currentCursorValue = value;
    }

    public void RestartWithNothing() {

        StartCoroutine(RestartWithNothingCoroutine());
    }

    private IEnumerator RestartWithNothingCoroutine() {

        menuSelectSound.Play();
        while (menuSelectSound.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
        GameManager.Instance().ResetGame();
        gameContainer.SetActive(true);
    }

    public void RestartWithSomething()
    {
        StartCoroutine(RestartWithSomethingCoroutine());
    }

    private IEnumerator RestartWithSomethingCoroutine() {

        menuSelectSound.Play();
        while (menuSelectSound.isPlaying) {
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
        GameManager.Instance().player.transform.position = new Vector3(2.86f, 18.30f, 0);
        gameContainer.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }
}
