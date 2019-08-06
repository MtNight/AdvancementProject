using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet3 : MonoBehaviour {

    
    public int demage;
    public float speed;
    public Vector3 dir;
    private bool down;
    private float scale;

    private Camera cam;
    GameObject stage;
    private AudioSource AS;
    public AudioClip EBullet3SFX;

    void Start()
    {
        demage = 50;
        down = false;
        speed = 2;
        scale = 2;
        stage = GameObject.FindWithTag("Stage");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        AS = GetComponent<AudioSource>();
        AS.clip = EBullet3SFX;
        AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol * 0.5f;
        AS.PlayOneShot(EBullet3SFX);
    }

    void Update()
    {
        if (down == false)
        {
            speed -= 2 * Time.deltaTime;
            if (speed < 0)
            {
                down = true;
                dir = (Vector3.down * 10 + dir).normalized;
                StartCoroutine(Attack());
            }
        }
        else
        {
            speed += 1f * Time.deltaTime;
            scale -= 0.25f * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1) * scale;
        }
        transform.position += speed * dir * Time.deltaTime;
        if (cam.transform.position == new Vector3(0, 0, -10))
        {
            if (cam.WorldToViewportPoint(transform.position).x < -0.1f || cam.WorldToViewportPoint(transform.position).x > 1.1f || cam.WorldToViewportPoint(transform.position).y < -0.1f || cam.WorldToViewportPoint(transform.position).y > 1.1f || scale < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bomb")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Vector3 atdir = new Vector3(1, -dir.normalized.x / dir.normalized.y, 0);
            stage.GetComponent<Stage1Scr>().Create_bullet(1, transform.position + new Vector3(0, 0, 1), Quaternion.Euler(0, 0, 0), 1f, 1f, atdir.normalized);
            yield return new WaitForSeconds(0.125f);
            atdir = -atdir;
            stage.GetComponent<Stage1Scr>().Create_bullet(1, transform.position + new Vector3(0, 0, 1), Quaternion.Euler(0, 0, 0), 1f, 1f, atdir.normalized);
            yield return new WaitForSeconds(0.125f);
        }
    }
}
