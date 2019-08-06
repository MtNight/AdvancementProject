using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmove : MonoBehaviour {

    public float scrollSpeed = 0.1f;
    private Material thisMaterial;

    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 newOffset = thisMaterial.mainTextureOffset;
        newOffset.Set(0, newOffset.y + (scrollSpeed * Time.deltaTime));
        thisMaterial.mainTextureOffset = newOffset;
    }

    public void SUF(float upspeed, float time)
    {
        scrollSpeed += upspeed * 0.5f;
        StartCoroutine(SpeedUP(upspeed, time));
    }
    IEnumerator SpeedUP(float upspeed, float time)
    {
        while (true)
        {
            if (upspeed - scrollSpeed > 0)
            {
                if (scrollSpeed <= upspeed)
                {
                    scrollSpeed += Time.deltaTime * (1 / (4 * time));
                }
                else
                {
                    scrollSpeed = upspeed;
                    yield break;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            else
            {
                yield break;
            }
        }
    }
}
