using UnityEngine;

public class TrapCollision : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {

        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerDeathManager>().Die();
        }
    }
}
