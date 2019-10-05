using UnityEngine;

public class MobilePeakTrajectory : MonoBehaviour {

    public Vector2 minPosition;
    public Vector2 maxPosition;
    [RangeAttribute(0, 1)]
    public float startPositionPercentage = 0;
    public float trajectoryDuration = 2;
    public bool startFromMinToStart = true;
    public float timeBetweenTrajectories = 1;


    private float currentPosition;
    private bool fromMinToStart;
    private float waitTime;

    void Start() {
        Init();
    }

    void OnEnable() {
        Init();
    }

    void Init() {
        currentPosition = startPositionPercentage;
        fromMinToStart = startFromMinToStart;
        waitTime = -1;
    }

    void Update() {

        if (waitTime >= 0) {
            waitTime += Time.deltaTime;
            if (waitTime >= timeBetweenTrajectories) {
                waitTime = -1;
            }
        } else {
            currentPosition += fromMinToStart ? Time.deltaTime : -Time.deltaTime;
            currentPosition = Mathf.Max(0, Mathf.Min(trajectoryDuration, currentPosition));
            float normPosition = currentPosition / trajectoryDuration;
            transform.localPosition = Vector2.Lerp(minPosition, maxPosition, normPosition);
            if (normPosition >= 1 || normPosition <= 0) {
                waitTime = 0;
                fromMinToStart = !fromMinToStart;
            }
        }
    }
}
