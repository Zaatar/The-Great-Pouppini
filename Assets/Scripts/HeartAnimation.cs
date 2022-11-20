using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    SpriteRenderer renderer;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation(float value = 0)
    {
        if(value/1.0f < 1.0f)
        {
            value += Time.deltaTime;
            renderer.color = Color.Lerp(Color.clear, Color.white, value/1.0f);
            transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
            yield return null;
            StartCoroutine(StartAnimation(value));
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EndAnimation());
        }
    }
    IEnumerator EndAnimation(float value = 0)
    {
        if (value / 1.0f < 1.0f)
        {
            value += Time.deltaTime;
            renderer.color = Color.Lerp(Color.white, Color.clear,  value / 1.0f);
            transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
            yield return null;
            StartCoroutine(EndAnimation(value));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

