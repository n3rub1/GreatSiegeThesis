using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBounce : MonoBehaviour
{
    private Quaternion originalRotation;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        originalRotation = transform.localRotation;
    }

    public void TimerRotateWithSpeed(float angle, float duration, float speed)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeTimer(angle, duration, speed));
    }

    private IEnumerator ShakeTimer(float angle, float duration, float speed)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float z = Mathf.Sin(elapsed * speed) * angle;
            transform.localRotation =
                originalRotation * Quaternion.Euler(0f, 0f, z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = originalRotation;
    }

}
