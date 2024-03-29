﻿using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance() {

        return instance;
    }

    private int blackCoinCount;
    private int redCoinCount;
    private int blueCoinCount;
    private int yellowCoinCount;
    private int deathCount;
    private float timer;

    public GameObject platformContainer;
    public GameObject peakContainer;
    public GameObject mobilePeakContainer;
    public GameObject coinContainer;
    public GameObject player;
    public AudioSource musicSource;

    public bool debugActivateCoins = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        Cursor.visible = false;
        ResetCounters();
    }

    private void ResetCounters() {
        blackCoinCount = 0;
        redCoinCount = 0;
        blueCoinCount = 0;
        yellowCoinCount = 0;
        deathCount = 0;
        timer = 0;

        if (debugActivateCoins) {
            blackCoinCount++;
            redCoinCount++;
            blueCoinCount++;
            yellowCoinCount++;
        }
    }

    public void AddOneDeath() {
        deathCount++;
    }

    public void AddToTimer(float t) {
        timer += t;
    }

    public void AddCoinValue(Coin.CoinColor color, int val) {

        switch (color) {
            case Coin.CoinColor.BLACK:
                blackCoinCount += val;
                break;
            case Coin.CoinColor.RED:
                redCoinCount += val;
                break;
            case Coin.CoinColor.BLUE:
                blueCoinCount += val;
                break;
            case Coin.CoinColor.YELLOW:
                yellowCoinCount += val;
                break;
            default:
                break;
        }
    }

    private void UpdateLevelActivation() {

        SetPlatformActivation(blackCoinCount > 0);
        SetPeakActivation(redCoinCount > 0);
        SetMobilePeakActivation(blueCoinCount > 0);
    }

    void Update() {
        UpdateLevelActivation();
    }

    private void SetPlatformActivation(bool value) {

        foreach (Transform platform in platformContainer.transform) {
            platform.gameObject.SetActive(value);
        }
    }

    private void SetPeakActivation(bool value) {

        foreach (Transform peak in peakContainer.transform) {
            peak.gameObject.SetActive(value);
        }
    }

    private void SetMobilePeakActivation(bool value) {

        foreach (Transform mPeak in mobilePeakContainer.transform) {
            mPeak.gameObject.SetActive(value);
        }
    }

    public void ResetGame() {
        UpdateLevelActivation();
        player.GetComponent<PlayerController>().ResetPosition();
        player.GetComponent<PlayerDeathManager>().ResetSave();
        foreach (Transform child in coinContainer.transform) {
            Coin switchedCoin = child.GetComponent<Coin>();
            bool isContainer = switchedCoin == null;
            if (isContainer) {
                foreach (Transform subChild in child.transform) {
                    subChild.GetComponent<Coin>().Deactivate();
                }
            } else {
                child.GetComponent<Coin>().Deactivate();
                child.GetComponent<Coin>().Activate();
            }
        }
        ResetCounters();
        musicSource.Stop();
    }

    public int GetCoinCount(Coin.CoinColor color) {

        int value = 0;
        switch (color) {
            case Coin.CoinColor.BLACK:
                value = blackCoinCount;
                break;
            case Coin.CoinColor.RED:
                value = redCoinCount;
                break;
            case Coin.CoinColor.BLUE:
                value = blueCoinCount;
                break;
            case Coin.CoinColor.YELLOW:
                value = yellowCoinCount;
                break;
            default:
                break;
        }
        return value;
    }

    public int GetDeathCount() {
        return deathCount;
    }

    public float GetTimer() {
        return timer;
    }
}
