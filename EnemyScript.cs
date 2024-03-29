﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;

    public float speed = 1.0f;

    private float startTime;

    private float journeyLength;

    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));
        transform.Rotate(new Vector3(0, 0, -45) * Time.deltaTime);
    }
}
