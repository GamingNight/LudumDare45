using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public int color = 0;

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
