using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageGenerator : MonoBehaviour
{
    GameObject player;

    public GameObject afterImagePrefab;
    Sprite currentSprite;
    public float spawnDelay;

    public float fadeSpeed;
    public float startOpacity;

    float count;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        count += Time.unscaledDeltaTime;
        if (count >= spawnDelay)
        {
            Spawn();
            count = 0;
        }
    }

    void Spawn()
    {
        currentSprite = player.GetComponent<SpriteRenderer>().sprite;
        GameObject afterImage = Instantiate(afterImagePrefab, player.transform.position, Quaternion.identity);

        afterImage.GetComponent<SpriteRenderer>().transform.localScale = player.GetComponent<SpriteRenderer>().transform.localScale;
        afterImage.GetComponent<SpriteRenderer>().sprite = currentSprite;
        afterImage.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, startOpacity / 255f);
        StartCoroutine(LerpOpacity(afterImage, new Color(1f, 1f, 1f, startOpacity / 255f), new Color(1f, 1f, 1f, 0f)));

        Destroy(afterImage, 1);
    }

    IEnumerator LerpOpacity(GameObject target, Color startColor, Color endColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            target.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, elapsedTime / fadeSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.GetComponent<SpriteRenderer>().color = endColor;
    }
}
