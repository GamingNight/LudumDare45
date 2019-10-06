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
        foreach (Transform switchedchild in coinContainer.transform) {
            Coin switchedCoin = switchedchild.gameObject.GetComponent<Coin>();
            if (switchedCoin == null) {
                foreach (Transform child in switchedchild.transform) {
                resetPickUpCoin(child.gameObject.GetComponent<Coin>());
                }
            } else {
                resetPickUpCoin(switchedCoin);
            }
            CoinContainer switchedCoinContainer = switchedchild.gameObject.GetComponent<CoinContainer>();
            if (switchedCoinContainer != null) {
                resetPickUpCoinContainer(switchedCoinContainer);
            }
        }
    }

    public void QuickSave(Vector3 quickSavePosition) {
        lastQuickSavePosition = quickSavePosition;

        foreach (Transform switchedchild in coinContainer.transform) {
            Coin switchedCoin = switchedchild.gameObject.GetComponent<Coin>();
            if (switchedCoin == null) {
                foreach (Transform child in switchedchild.transform) {
                    QuickSaveCoin(child.gameObject.GetComponent<Coin>());
                }
            } else {
            	QuickSaveCoin(switchedCoin);
            }
            CoinContainer switchedCoinContainer = switchedchild.gameObject.GetComponent<CoinContainer>();
            if (switchedCoinContainer != null) {
                QuickSaveCoinContainer(switchedCoinContainer);
            }
        }

    }

    private void resetPickUpCoin(Coin coin) {
        if (coin.GetStatus() == Coin.CoinStatus.PICKED_UP_NOT_SAVED) {
            coin.resetPickUp();
        }
    }

    private void resetPickUpCoinContainer(CoinContainer coinContainer) {
        if (coinContainer.GetStatus() == CoinContainer.CoinContainerStatus.PICKED_UP_NOT_SAVED) {
            coinContainer.resetPickUp();
        }
    }

    private void QuickSaveCoin(Coin coin) {
        if (coin.GetStatus() == Coin.CoinStatus.PICKED_UP_NOT_SAVED) {
            coin.saveCoin();
        }
    }

    private void QuickSaveCoinContainer(CoinContainer coinContainer) {
        if (coinContainer.GetStatus() == CoinContainer.CoinContainerStatus.PICKED_UP_NOT_SAVED) {
            coinContainer.saveCoin();
        }
    }
}
