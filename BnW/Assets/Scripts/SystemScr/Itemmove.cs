using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemmove : MonoBehaviour
{
    private int wall;
    private float speed;
    private Vector3 dir;

    private float xmin, xmax, ymin, ymax;

    void Start()
    {
        wall = 0;
        transform.SetParent(GameObject.FindWithTag("Stage").transform);
        speed = 3.0f;
        dir = transform.parent.GetComponent<Stage1Scr>().itemdir.normalized;
        transform.parent.GetComponent<Stage1Scr>().ItemdirChange();

        xmin = -3.0f;
        xmax = -xmin;
        ymin = -4.0f;
        ymax = -ymin;
    }

    void Update()
    {
        transform.position += speed * dir * Time.deltaTime;

        if (xmin >= transform.position.x && wall != 1)
        {
            dir.x = -dir.x;
            wall = 1;
        }
        else if (xmax <= transform.position.x && wall != 2)
        {
            dir.x = -dir.x;
            wall = 2;
        }
        else if (ymin >= transform.position.y && wall != 3)
        {
            dir.y = -dir.y;
            wall = 3;
        }
        else if (ymax <= transform.position.y && wall != 4)
        {
            dir.y = -dir.y;
            wall = 4;
        }
    }
}
