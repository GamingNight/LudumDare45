using UnityEngine;

public class PlayerDeathManager : MonoBehaviour {

    private Vector3 lastQuickSavePosition;
    public GameObject coinContainer;

    void Start() {
        lastQuickSavePosition = transform.position;
    }

    public void ResetSave() {
        lastQuickSavePosition = GetComponent<PlayerController>().GetInitPosition();
    }

    public void Die() {
        GetComponent<PlayerController>().ResetVelocity();
        transform.position = lastQuickSavePosition;
        foreach (Transform child in coinContainer.transform) {
            Coin coin = child.gameObject.GetComponent<Coin>();
            if (coin.GetStatus() == Coin.CoinStatus.PICKED_UP_NOT_SAVED) {
                coin.resetPickUp();
            }
        }
    }

    public void QuickSave(Vector3 quickSavePosition) {
        lastQuickSavePosition = quickSavePosition;
        foreach (Transform child in coinContainer.transform) {
            Coin coin = child.gameObject.GetComponent<Coin>();
            if (coin.GetStatus() == Coin.CoinStatus.PICKED_UP_NOT_SAVED) {
                coin.saveCoin();
            }
        }
    }
}
