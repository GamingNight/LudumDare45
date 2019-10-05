using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{

    private Vector3 lastQuickSavePosition;

    public void Die() {
        GetComponent<PlayerController>().ResetVelocity();
        transform.position = lastQuickSavePosition;
    }

    public void SetQuickSavePosition(Vector3 quickSavePosition) {
        lastQuickSavePosition = quickSavePosition;
    }
}
