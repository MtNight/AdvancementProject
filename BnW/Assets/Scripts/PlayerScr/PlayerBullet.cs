using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    private bool act;
    public int demage;
    public float speed;
    public Vector3 dir;
    private Vector3 rotation;

    private Camera cam;

    private AudioSource AS;
    public AudioClip ShootSFX;

    void Start () {
        act = false;
        demage = 10;
        if (transform.position.x < -3.75f || transform.position.x > 3.75f) { OutOff(); }
        if (transform.position.y < -4.75f || transform.position.y > 4.75f) { OutOff(); }
        rotation = new Vector3(0, 0, Random.Range(-1, 2));

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        AS = GetComponent<AudioSource>();
        AS.pitch = 0.5f;
    }
	
	void Update () {
        if (act == true)
        {
            transform.position += speed * dir * Time.deltaTime;
            transform.Rotate(rotation);
        }
        if (cam.WorldToViewportPoint(transform.position).x < 0 || cam.WorldToViewportPoint(transform.position).x > 1 || cam.WorldToViewportPoint(transform.position).y < 0 || cam.WorldToViewportPoint(transform.position).y > 1)
        {
            OutOff();
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
        if (scale == 1.0f && AS.isPlaying == false)
        {
            AS.clip = ShootSFX;
            AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol * 0.4f;
            AS.PlayOneShot(ShootSFX);
        }
    }

    public void Off()
    {
        GameObject.Find("Stage1").GetComponent<Stage1Scr>().EHitSoundFunc();
        act = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        transform.position = new Vector3(-10, -10, -10);
    }
    public void OutOff()
    {
        act = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        transform.position = new Vector3(-10, -10, -10);
    }
}
