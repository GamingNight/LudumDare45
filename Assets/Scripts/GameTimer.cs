using UnityEngine;

public class GameTimer : MonoBehaviour {
    private bool update;

    void OnEnable() {
        update = true;
    }

    void OnDisable() {
        update = false;
    }

    void Update() {
        if (update) {
            GameManager.Instance().AddToTimer(Time.deltaTime);
        }
    }
}
