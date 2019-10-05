using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public enum CoinColor {
        RED, BLUE, YELLOW, GREEN
    }

    public Sprite spriteRed;
    public Sprite spriteBlue;
    public Sprite spriteYellow;
    public Sprite spriteGreen;

    public CoinColor color = CoinColor.RED;
    public bool activated = true;

    void Start() {
        Sprite coinSprite = GetComponent<SpriteRenderer>().sprite;
        switch (color) {
        case Coin.CoinColor.RED:
            coinSprite = spriteRed;
            break;
        case Coin.CoinColor.BLUE:
            coinSprite = spriteBlue;
            break;
        case Coin.CoinColor.YELLOW:
            coinSprite = spriteYellow;
            break;
        case Coin.CoinColor.GREEN:
            coinSprite = spriteGreen;
            break;
        default:
            break;
        }
    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log(" activated = false");
        if (!activated) 
            return;

        //Debug.Log(" activated = true");
        
        GameManager.Instance().UpdateCoinCount(color);
        if (col.gameObject.tag == "Player") {
            activated = false;
            // the destroy must be done later by the gamemanager
            Destroy (gameObject);
        }
    }

}
