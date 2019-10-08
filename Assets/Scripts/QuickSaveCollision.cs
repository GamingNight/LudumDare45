using UnityEngine;

public class QuickSaveCollision : MonoBehaviour {

    private Animator animator;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
        SetAnimation(false);
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerDeathManager>().QuickSave(transform.position);
            col.gameObject.GetComponent<PlayerDeathManager>().QuickSaveTriggerUpdate(this);
        }
    }

    public void SetAnimation(bool mybool) {

        animator.SetBool("ON", mybool);
    }
}
