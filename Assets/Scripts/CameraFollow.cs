using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = new Vector3(0f, 2f, -10f);
    public Vector4 leftRightDownUpBoundaries;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate() {
        Vector3 velocity = Vector3.zero;
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, dampTime);
        targetPosition = new Vector3(Mathf.Max(leftRightDownUpBoundaries.x, Mathf.Min(leftRightDownUpBoundaries.y, targetPosition.x)), Mathf.Max(leftRightDownUpBoundaries.z, Mathf.Min(leftRightDownUpBoundaries.w, targetPosition.y)), targetPosition.z);
        transform.position = targetPosition;
    }

}