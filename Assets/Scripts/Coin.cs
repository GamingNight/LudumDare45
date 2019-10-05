using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public enum CoinColor {
        RED, BLUE, YELLOW, GREEN
    }

    public CoinColor color = CoinColor.RED;

    void Start() {

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
