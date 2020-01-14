using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [Header("Camera Move")]
    [SerializeField] private float CameraSpeed; // vitesse de déplacement de la caméra
    [SerializeField] private AnimationCurve AnimCurveCamMove; // Animation Curve déplacement caméra
    [SerializeField] private List<Transform> LiCamPos; // liste de position de la caméra

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.B))
            StartCoroutine(MoveCamera(0));
    }

    public IEnumerator ShakeCamera(float duration, float magnitude)  // shake camera coroutine
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public IEnumerator MoveCamera(int idPos)   //déplacement Camera
    {
        float distance = Vector3.Distance(transform.position, LiCamPos[idPos].position);
        float duration =  distance / CameraSpeed;

        Vector3 startPos = transform.position;
        float startTime = Time.time;
        while(Time.time < startTime + duration)
        {
            transform.position = Vector3.Slerp(startPos, LiCamPos[idPos].position, AnimCurveCamMove.Evaluate((Time.time - startTime) / duration));
            yield return null;
        }
        transform.position = LiCamPos[idPos].position;
    }
}
