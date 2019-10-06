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
            bool isContainer = coin == null;
            if (!isContainer) {
                coin.ResetPickUp();
            } else {
                foreach (Transform subChild in child.transform) {
                    subChild.GetComponent<Coin>().ResetPickUp();
                }
            }
        }
    }

    public void QuickSave(Vector3 quickSavePosition) {
        lastQuickSavePosition = quickSavePosition;

        foreach (Transform child in coinContainer.transform) {
            Coin coin = child.gameObject.GetComponent<Coin>();
            bool isContainer = coin == null;
            if (!isContainer) {
                coin.SaveCoin();
            } else {
                foreach (Transform subChild in child.transform) {
                    subChild.gameObject.GetComponent<Coin>().SaveCoin();
                }
            }
        }

    }
}
