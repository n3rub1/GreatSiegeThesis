using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsCannon : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private AudioClip[] cannonSounds;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject smoke;
    [SerializeField] private float fireInterval;
    [SerializeField] private float fireEffectInterval;
    [SerializeField] private float smokeDuration;
    [SerializeField] private Vector3 smokeMoveDirection = new Vector3(1f, 1f, 0f);
    [SerializeField] private float smokeMoveDistance;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        fire.SetActive(false);
        smoke.SetActive(false);
        StartCoroutine(FireLoop());
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval + Random.Range(1,5));
            FireCannon();
        }
    }

    private void FireCannon()
    {
        fire.SetActive(true);
        StartCoroutine(FlashFire());
        StartCoroutine(SmokeEffect());

        if (cannonSounds.Length > 0)
        {
            AudioClip clip = cannonSounds[Random.Range(0, cannonSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    IEnumerator FlashFire()
    {
        yield return new WaitForSeconds(fireEffectInterval);
        fire.SetActive(false);
    }

    IEnumerator SmokeEffect()
    {

        smoke.SetActive(true);
        SpriteRenderer spriteRenderer = smoke.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Vector3 startPos = smoke.transform.position;
        Vector3 targetPos = startPos + smokeMoveDirection.normalized * smokeMoveDistance;

        float elapsed = 0f;

        while (elapsed < smokeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / smokeDuration;

            smoke.transform.position = Vector3.Lerp(startPos, targetPos, t);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, t));

            yield return null;
        }

        spriteRenderer.color = originalColor;
        smoke.SetActive(false);
        smoke.transform.position = startPos;
    }

}
