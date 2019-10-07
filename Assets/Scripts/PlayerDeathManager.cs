using UnityEngine;

public class PlayerDeathManager : MonoBehaviour {

    public GameObject coinContainer;
    public AudioSource deathSound;
    public AudioSource saveSound;

    private Vector3 lastQuickSavePosition;
    private QuickSaveCollision lastQuickSaveTriggerActivated;

    void Start() {
        lastQuickSavePosition = transform.position;
        lastQuickSaveTriggerActivated = null;
    }

    public void ResetSave() {
        lastQuickSavePosition = GetComponent<PlayerController>().GetInitPosition();
    }

    public void Die() {
        deathSound.Play();
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

    public void QuickSaveTriggerUpdate(QuickSaveCollision quickSaveTrigger) {
        if (lastQuickSaveTriggerActivated != null) {
            lastQuickSaveTriggerActivated.SetAnimation(false);
        }
        if (lastQuickSaveTriggerActivated != quickSaveTrigger) {
            saveSound.Play();
        }

        lastQuickSaveTriggerActivated = quickSaveTrigger;
        lastQuickSaveTriggerActivated.SetAnimation(true);
    }

}
