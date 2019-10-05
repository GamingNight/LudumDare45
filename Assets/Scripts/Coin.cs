using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public enum CoinColor {
        RED, BLUE, YELLOW, GREEN
    }

    public Sprite spriteRed;
    public Sprite spriteBlue;
    public Sprite spriteYello;
    public Sprite spriteGreen;

    public CoinColor color = CoinColor.RED;

    void Start() {
        //GetComponent<SpriteRenderer>().sprint = spriteRed;

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
