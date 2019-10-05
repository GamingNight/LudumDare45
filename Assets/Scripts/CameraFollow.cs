using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = new Vector3 (0f,2f,-10f);

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, 0.1f);
    }

}