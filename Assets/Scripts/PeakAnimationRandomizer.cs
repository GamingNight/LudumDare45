using UnityEngine;

public class PeakAnimationRandomizer : MonoBehaviour {

    void Start() {
        GetComponent<Animator>().speed = Random.Range(0.9f, 1.1f);
    }
}
