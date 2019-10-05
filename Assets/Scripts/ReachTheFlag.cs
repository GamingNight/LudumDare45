using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTheFlag : MonoBehaviour
{

    public GameObject endUI;
    public GameObject gameContainer;

    void OnTriggerEnter2D(Collider2D col) {

        if(col.gameObject.tag == "Player") {
            gameContainer.SetActive(false);
            endUI.SetActive(true);
        }
    }
}
