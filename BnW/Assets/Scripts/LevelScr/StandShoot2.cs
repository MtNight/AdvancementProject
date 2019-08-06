using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandShoot2 : MonoBehaviour {

    private bool death;
    public bool spawn;
    private int hp;
    private bool att;

    public Vector3 dir;
    
    GameObject stage;
    private Camera cam;
    private Vector3 rect;
    private AudioSource AS;
    public AudioClip EDeathSFX;
    public AudioClip EBullet2SFX;

    void Start()
    {
        death = false;
        spawn = false;
        hp = 1000;
        att = false;

        if (gameObject.name == "shooter2-2"|| gameObject.name == "shooter2-2(Clone)")
        {
            dir = Vector3.right + Vector3.down;
        }
        else
        {
            dir = Vector3.right;
        }
        
        stage = GameObject.FindWithTag("Stage");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rect = cam.WorldToViewportPoint(transform.position);
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
                if (gameObject.name == "shooter2-1(Clone)" || gameObject.name == "shooter2-2(Clone)" || gameObject.name == "shooter2-3(Clone)")
                {
                    StartCoroutine(Attack(0.5f));
                }
                else
                {
                    StartCoroutine(Attack(1));
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

    IEnumerator Attack(float time)
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                stage.GetComponent<Stage1Scr>().Create_bullet(2, transform.position + new Vector3(0, 0, -15), Quaternion.Euler(0, 0, 0), 1, 2.0f, dir.normalized);
                if (gameObject.name == "shooter2-3" || gameObject.name == "shooter2-3(Clone)")
                {
                    AS.clip = EBullet2SFX;
                    AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                    AS.PlayOneShot(EBullet2SFX);
                }
                if (dir == Vector3.right)
                {
                    dir = Vector3.down;
                }
                else if (dir == Vector3.down)
                {
                    dir = Vector3.left;
                }
                else if (dir == Vector3.left)
                {
                    dir = Vector3.up;
                }
                else if (dir == Vector3.up)
                {
                    dir = Vector3.right;
                }
                else if (dir.x == 1 && dir.y == 1)
                {
                    dir.y = -1;
                }
                else if (dir.x == 1 && dir.y == -1)
                {
                    dir.x = -1;
                }
                else if (dir.x == -1 && dir.y == -1)
                {
                    dir.y = 1;
                }
                else if (dir.x == -1 && dir.y == 1)
                {
                    dir.x = 1;
                }
            }
            if (dir.x != 0 && dir.y != 0)
            {
                dir = Vector3.right;
            }
            else
            {
                dir = Vector3.right + Vector3.down;
            }
            yield return new WaitForSeconds(time);
        }
    }
}
