using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_1 : MonoBehaviour {

    private bool death;
    private bool spawn;
    public int num;
    private int hp;

    private float speed;
    public Vector3 dir;

    GameObject player;
    GameObject stage;
    private Camera cam;
    private Vector3 rect;
    private AudioSource AS;
    public AudioClip EDeathSFX;
    public AudioClip EBullet1SFX;

    void Start () {
        death = false;
        spawn = false;
        hp = 20;

        speed = 2.0f;
        transform.Rotate(new Vector3(0, 0, 0 - Mathf.Asin(-dir.x) * Mathf.Rad2Deg));

        player = GameObject.FindWithTag("Player");
        stage = GameObject.FindWithTag("Stage");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rect = cam.WorldToViewportPoint(transform.position);
        AS = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        transform.position += speed * dir * Time.deltaTime;
        rect = cam.WorldToViewportPoint(transform.position);
        if (cam.transform.position == new Vector3(0, 0, -10))
        {
            if (rect.x < 0 || rect.x > 1 || rect.y < 0 || rect.y > 1)
            {
                spawn = false;
            }
            else if (spawn == false)
            {
                spawn = true;
                StartCoroutine(Attack());
            }
        }
    }

    private void OnBecameInvisible()
    {
        if (cam.transform.position == new Vector3(0, 0, -10))
        {
            Destroy(gameObject);
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
                AS.clip = EDeathSFX;
                AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                AS.PlayOneShot(EDeathSFX);
                GameObject.Find("Player").GetComponent<PlayerSystem>().score += 20;
                Itemspawn();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            if (player != null)
            {
                Vector3 atv = player.transform.position - transform.position;
                if (stage != null /*&& Mathf.Acos((atv.x * dir.x + atv.y * dir.y) / (Mathf.Sqrt(atv.x * atv.x + atv.y * atv.y) * Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y))) * Mathf.Rad2Deg < 45*/)
                {
                    AS.clip = EBullet1SFX;
                    AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                    AS.PlayOneShot(EBullet1SFX);
                    Vector3 pos = dir;
                    pos.z += 1;
                    float tmp = pos.x;
                    pos.x = -pos.y;
                    pos.y = tmp;
                    pos *= 0.2f;
                    Vector3 ans = atv - pos;
                    ans.z = 0;
                    stage.GetComponent<Stage1Scr>().Create_bullet(1, transform.position + pos, Quaternion.Euler(0, 0, 0), 1, speed + 1.5f, ans.normalized);

                    pos.x = -pos.x;
                    pos.y = -pos.y;
                    ans = atv - pos;
                    ans.z = 0;
                    //ans = atv - pos;
                    //ztmp = ans.z;
                    //ans.z = 0;
                    //ans = ans.normalized;
                    //ans.z = ztmp;
                    stage.GetComponent<Stage1Scr>().Create_bullet(1, transform.position + pos, Quaternion.Euler(0, 0, 0), 1, speed + 1.5f, ans.normalized);
                    yield break;
                }
            }
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
}
