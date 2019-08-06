using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_MB1 : MonoBehaviour
{

    private bool death;
    private bool spawn;
    private bool invin;
    public int minion;
    public int num;
    private int hp;

    private int mode;
    private float yd;
    private int move;
    private float speed;
    public Vector3 dir;

    GameObject stage;
    GameObject Bullet;
    GameObject Muzzle;
    private Camera cam;
    private Vector3 rect;
    private AudioSource AS;
    public AudioClip EBDemageSFX;
    public AudioClip EBDeathSFX;

    void Start()
    {
        death = false;
        spawn = false;
        invin = true;
        minion = 5;
        hp = 15000;

        mode = 0;
        move = 1;
        dir = Vector3.zero;
        speed = 3.0f;

        stage = GameObject.FindWithTag("Stage");
        Bullet = Resources.Load("EBullet3_1") as GameObject;
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rect = cam.WorldToViewportPoint(transform.position);
        AS = GetComponent<AudioSource>();
    }

    void Update()
    {
        rect = cam.WorldToViewportPoint(transform.position);
        if (cam.transform.position == new Vector3(0, 0, -10))
        {
            if (rect.x < 0.1f || rect.x > 0.9f || rect.y < 0.1f || rect.y > 0.9f)
            {
                spawn = false;
                dir = (new Vector3(0, 2, transform.position.z) - transform.position).normalized;
                speed -= 0.001f;
            }
            else if (spawn == false)
            {
                spawn = true;
                StartCoroutine(Move());
            }
            else
            {
                speed -= 0.0001f;
            }
            transform.position += speed * dir * Time.deltaTime;
        }

        if (minion > 0)
        {
            mode = 1;
        }
        else if (minion == 0)
        {
            AS.clip = EBDemageSFX;
            AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol * 2.0f;
            AS.PlayOneShot(EBDemageSFX);
            mode = 2;
            minion -= 1;
            StartCoroutine(Fix());
            invin = false;
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawn == true && invin == false)
        {
            if (other.tag == "PBullet")
            {
                if (death == false)
                {
                    stage.GetComponent<Stage1Scr>().EHitSoundFunc();
                    hp -= 10;
                    other.gameObject.GetComponent<PlayerBullet>().Off();
                }
            }
            else if (other.tag == "Bomb")
            {
                hp -= 20;
            }
            else if (other.tag == "Player")
            {
                hp -= 20;
            }
        }

        if (hp <= 0)
        {
            if (death == false)
            {
                death = true;
                AS.clip = EBDeathSFX;
                AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol * 2.0f;
                AS.PlayOneShot(EBDeathSFX);
                GameObject.Find("Player").GetComponent<PlayerSystem>().score += 10;
                Itemspawn();
                StartCoroutine(Deathdelay());
            }
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (death == false)
            {
                GameObject i = Instantiate(Bullet, transform.position + new Vector3(0.4f, 1, -24), Quaternion.Euler(0, 0, 0));
                i.GetComponent<EBullet3>().dir = new Vector3(1, 1, 0);
                yield return new WaitForSeconds(2.0f);
            }
            if (death == false)
            {
                GameObject i = Instantiate(Bullet, transform.position + new Vector3(-0.4f, 1, -24), Quaternion.Euler(0, 0, 0));
                i.GetComponent<EBullet3>().dir = new Vector3(-1, 1, 0);
                yield return new WaitForSeconds(2.0f);
            }
            else
            {
                yield break;
            }
        }
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (mode == 0)
            {
                speed = 1f + Random.Range(0, 0.5f);
            }
            else if (mode == 1)
            {
                speed = 2.0f + Random.Range(0, 0.5f);
            }
            else if (mode == 2)
            {
                speed = 3.0f + Random.Range(0, 0.5f);
            }

            if (death == false)
            {
                if (move == 1)
                {
                    dir = (new Vector3(3, 3 + yd, transform.position.z) - transform.position).normalized;
                    yd = -1 + Random.Range(-1, 1);
                    move = 0;
                }
                else if (move == -1)
                {
                    dir = (new Vector3(-3, 3 + yd, transform.position.z) - transform.position).normalized;
                    yd = -1 + Random.Range(-1, 1);
                    move = 0;
                }
                else
                {
                    if (transform.position.x < 0)
                    {
                        move = 1;
                    }
                    else if (transform.position.x >= 0)
                    {
                        move = -1;
                    }
                }
            }
            else
            {
                speed = 0.5f;
            }
            if (yd < -2) { yd = -3; }
            if (yd > 2) { yd = 3; }
            yield return new WaitForSeconds(2.0f);
        }
    }
    IEnumerator Fix()
    {
        while (true)
        {
            Muzzle = Resources.Load("shooter2-1") as GameObject;
            GameObject i = Instantiate(Muzzle, transform.position + new Vector3(0, 0.7f, -4), Quaternion.Euler(0, 0, 0));
            i.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(0.8f);
            Muzzle = Resources.Load("shooter2-2") as GameObject;
            i = Instantiate(Muzzle, transform.position + new Vector3(0, 0.85f, -4), Quaternion.Euler(0, 0, 0));
            i.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(0.8f);
            Muzzle = Resources.Load("shooter2-3") as GameObject;
            i = Instantiate(Muzzle, transform.position + new Vector3(0, 1, -4), Quaternion.Euler(0, 0, 0));
            i.transform.parent = gameObject.transform;
            yield break;
        } 
    }
    private void Itemspawn()
    {
        if (num % 50 == 0)
        {
            GameObject item = Resources.Load("WUitem") as GameObject;
            Instantiate(item, transform.position, Quaternion.Euler(0, 0, 0));
        }
        else if (num % 55 == 0)
        {
            GameObject item = Resources.Load("BBitem") as GameObject;
            Instantiate(item, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    IEnumerator Deathdelay()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            yield return new WaitForSeconds(3.0f);
            GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage1Scr>().SCF();
            Destroy(gameObject);
        }
    }
}
