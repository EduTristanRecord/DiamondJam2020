using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternA : MonoBehaviour
{
    [SerializeField] private Transform targetDirection;

    [SerializeField] private float resetDelay = 2.5f;
    [SerializeField] private float intensity = 3f;
    [SerializeField] private float speed = 4f;

    [SerializeField] private bool update = true;

    private Vector3 direction;
    private Vector3 startPosition;
    private Vector3 nonSinusoidalPosition;

    private void Start()
    {
        startPosition = transform.position;
        nonSinusoidalPosition = transform.position;

        direction = (targetDirection.position - startPosition).normalized;

        StartCoroutine(PeriodicReset());
    }

    private void Update()
    {
        if (update) direction = (targetDirection.position - startPosition).normalized;

        nonSinusoidalPosition += direction * speed * Time.deltaTime;
        transform.position = nonSinusoidalPosition + Mathf.Sin((nonSinusoidalPosition - startPosition).magnitude) * Vector3.Cross(direction, Vector3.back) * intensity;
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
                nonSinusoidalPosition = startPosition;

                timer -= resetDelay;
            }

            yield return null;
        }
    }
}
