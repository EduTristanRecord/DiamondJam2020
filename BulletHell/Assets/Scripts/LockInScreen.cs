using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LockInScreen : MonoBehaviour
{
    private SpriteRenderer targetSprite;
    private Vector2 rightTop;
    private Vector2 leftBottom;

    private void Awake()
    {
        targetSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        leftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.position.z));
    }

    private void LateUpdate()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = Mathf.Clamp(targetPos.x, leftBottom.x + targetSprite.bounds.extents.x, rightTop.x - targetSprite.bounds.extents.x);
        targetPos.y = Mathf.Clamp(targetPos.y, leftBottom.y + targetSprite.bounds.extents.y, rightTop.y - targetSprite.bounds.extents.y);
        transform.position = targetPos;
    }
}
