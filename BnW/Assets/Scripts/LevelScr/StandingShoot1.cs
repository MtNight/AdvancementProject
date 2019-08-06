using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingShoot1 : MonoBehaviour
{
    private bool death;
    private bool spawn;
    private int hp;
    private bool att;

    public Vector3 lookdir;
    public float dir;
    public float deg;

    GameObject player;
    GameObject stage;
    private Camera cam;
    private Vector3 rect;
    private AudioSource AS;
    public AudioClip EDeathSFX;
    public AudioClip EBullet1SFX;

    void Start()
    {
        death = false;
        spawn = false;
        hp = 1000;
        att = false;

        player = GameObject.FindWithTag("Player");
        stage = GameObject.FindWithTag("Stage");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rect = cam.WorldToViewportPoint(transform.position);

        lookdir = player.transform.position - transform.position;
        dir = -90.0f;
        AS = GetComponent<AudioSource>();
    }

    void Update()
    {
        rect = cam.WorldToViewportPoint(transform.position);
        if (rect.x < 0 || rect.x > 1 || rect.y < 0 || rect.y > 1)
        {
            spawn = false;
        }
        else if (spawn == false)
        {
            spawn = true;
            if (att == false)
            {
                att = true;
                StartCoroutine(Attack());
            }
        }
        else
        {   //in , spawn true

            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            if (player != null)
            {
                lookdir = player.transform.position - transform.position;
                deg = GetAngle(Vector3.zero, lookdir);
                if (deg >= dir + 180 || deg <= dir - 180)
                {
                    deg = -deg;
                }

                if (deg > dir && Mathf.Abs(deg - dir) > 1)
                {
                    transform.Rotate(new Vector3(0, 0, 1));
                    dir += 1f;
                }
                else if (deg < dir && Mathf.Abs(deg - dir) > 1)
                {
                    transform.Rotate(new Vector3(0, 0, -1));//시계방향
                    dir -= 1f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawn == true)
        {
            if (other.tag == "PBullet")
            {
                stage.GetComponent<Stage1Scr>().EHitSoundFunc();
                hp -= 10;
                other.gameObject.GetComponent<PlayerBullet>().Off();
            }
            else if (other.tag == "Bomb")
            {
                hp -= 20;
            }
        }

        if (hp <= 0)
        {
            if (death == false)
            {
                death = true;
                AS.clip = EDeathSFX;
                AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                AS.PlayOneShot(EDeathSFX);
                GameObject.Find("Player").GetComponent<PlayerSystem>().score += 10;
                transform.parent.GetComponent<Enemy1_MB1>().minion -= 1;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            if (player != null)
            {
                if (stage != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (dir < 90 && dir > -90)
                        {
                            stage.GetComponent<Stage1Scr>().Create_bullet(1, transform.position + new Vector3(0, 0, -15), Quaternion.Euler(0, 0, 0), 0.8f, 2f, new Vector3(1, Mathf.Tan(dir * Mathf.Deg2Rad), 0).normalized);
                        }
                        else
                        {
                            stage.GetComponent<Stage1Scr>().Create_bullet(1, transform.position + new Vector3(0, 0, -15), Quaternion.Euler(0, 0, 0), 0.8f, 2f, new Vector3(-1, -Mathf.Tan(dir * Mathf.Deg2Rad), 0).normalized);
                        }
                        AS.clip = EBullet1SFX;
                        AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                        AS.PlayOneShot(EBullet1SFX);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }
    }
    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
