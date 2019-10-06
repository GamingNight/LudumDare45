using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSaveCollision : MonoBehaviour
{
	private Animator animator;

	    void Start() {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("ON", false);
    }

    void OnTriggerEnter2D(Collider2D col) {

        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerDeathManager>().QuickSave(transform.position);
            animator.SetBool("ON", true);
        }
    }
}
