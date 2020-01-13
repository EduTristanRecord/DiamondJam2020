﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternA : MonoBehaviour
{
    [SerializeField] private float resetDelay = 2.5f;
    [SerializeField] private float speedY = 4f;
    [SerializeField] private float intensityX = 3f;

    private Vector3 startPosition;
    private float baseOffsetY;

    private void Start()
    {
        startPosition = transform.position;
        baseOffsetY = transform.position.y;

        StartCoroutine(PeriodicReset());
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Sin(transform.position.y - baseOffsetY) * intensityX, transform.position.y, transform.position.z) + Time.deltaTime * speedY * Vector3.down;
    }

    private IEnumerator PeriodicReset()
    {
        float timer = 0f;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer >= resetDelay)
            {
                transform.position = startPosition;
                timer -= resetDelay;
            }

            yield return null;
        }
    }
}
