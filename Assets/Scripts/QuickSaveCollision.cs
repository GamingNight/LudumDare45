using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSaveCollision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col) {

        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerDeathManager>().QuickSave(transform.position);
        }
    }
}
