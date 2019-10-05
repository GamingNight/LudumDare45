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

    void Start() {
        Sprite coinSprite = GetComponent<SpriteRenderer>().sprite;
        if (color == CoinColor.RED) {
            coinSprite = spriteRed;
        } else if (color == CoinColor.BLUE) {
            coinSprite = spriteBlue;
        } else if (color == CoinColor.YELLOW) {
            coinSprite = spriteYellow;
        } else if (color == CoinColor.GREEN) {
            coinSprite = spriteGreen;
        }
    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("coin.OnTriggerEnter2D");
        if (col.gameObject.tag == "Player") {
            Destroy (gameObject);
        }
    }

}
