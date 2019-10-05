using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance() {

        return instance;
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
}
