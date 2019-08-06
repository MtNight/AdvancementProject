using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet1 : MonoBehaviour {

    public bool act;
    public int demage;
    public float speed;
    public Vector3 dir;

    private Camera cam;

    void Start()
    {
        act = false;
        demage = 10;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (act == true)
        {
            transform.position += speed * dir * Time.deltaTime;
            if (cam.transform.position == new Vector3(0, 0, -10))
            {
                if (cam.WorldToViewportPoint(transform.position).x < 0 || cam.WorldToViewportPoint(transform.position).x > 1 || cam.WorldToViewportPoint(transform.position).y < 0 || cam.WorldToViewportPoint(transform.position).y > 1)
                {
                    Off();
                }
            }
        }
    }

    public void On(Vector3 pos, Quaternion ang, float scale)
    {
        act = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        transform.position = pos;
        transform.localRotation = ang;
        transform.Rotate(dir);
        transform.localScale = new Vector3(1, 1, 1) * scale;
    }

    public void Off()
    {
        act = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        transform.position = new Vector3(-10, -10, -10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bomb")
        {
            Off();
        }
    }
}
