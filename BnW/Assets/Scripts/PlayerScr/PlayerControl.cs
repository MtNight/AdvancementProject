using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool free;
    private bool spawn;
    public bool invin;

    private float speed;
    private Vector3 dir;
    private Vector3 move;

    private float xmin, xmax, ymin, ymax;

    private Vector3 tmp;
    private Camera cam;

    private AudioSource AS;
    public AudioClip ItemSFX;

    void Start()
    {
        free = false;
        spawn = false;
        invin = true;

        speed = 5.0f;
        dir = Vector3.zero;
        move = dir;

        xmin = -2.8f;
        xmax = -xmin;
        ymin = -3.8f;
        ymax = -ymin;

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        AS = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (free == false)
        {
            transform.position += Vector3.up * speed * Time.deltaTime * 0.5f;
            if (cam.WorldToViewportPoint(transform.position).x < 0 || cam.WorldToViewportPoint(transform.position).x > 1 || cam.WorldToViewportPoint(transform.position).y < 0 || cam.WorldToViewportPoint(transform.position).y > 1)
            {
            }
            else if (spawn == false)
            {
                spawn = true;
                StartCoroutine("Spawn");
                StartCoroutine("Invincible");
            }
        } 
        else
        {
            dir = Vector3.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                dir += Vector3.up;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                dir += Vector3.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dir += Vector3.left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                dir += Vector3.right;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                dir *= 0.5f;
            }

            move = dir * speed;
            transform.position += move * Time.deltaTime;
            tmp = transform.position;
            if (transform.position.x < xmin)
            {
                tmp.x = xmin;
            }
            if (transform.position.x > xmax)
            {
                tmp.x = xmax;
            }
            if (transform.position.y < ymin)
            {
                tmp.y = ymin;
            }
            if (transform.position.y > ymax)
            {
                tmp.y = ymax;
            }
            transform.position = tmp;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            free = true;
            yield break;
        }
    }
    IEnumerator Invincible()
    {
        int i = 0;
        while (true)
        {
            if (i % 2 == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.2f);
            i++;
            if (i > 10)
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                invin = false;
                yield break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            if (other.gameObject.name == "WUitem(Clone)")
            {
                Destroy(other.gameObject);
                if (transform.GetChild(0).GetComponent<PlayerBattle>().slevel < 5)
                {
                    transform.parent.GetComponent<PlayerSystem>().score += 10;
                    transform.GetChild(0).GetComponent<PlayerBattle>().slevel += 1;
                }
                else
                {
                    transform.parent.GetComponent<PlayerSystem>().score += 10;
                }
                AS.clip = ItemSFX;
                AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                AS.PlayOneShot(ItemSFX);
            }
            else if (other.gameObject.name == "BBitem(Clone)")
            {
                Destroy(other.gameObject);
                if (transform.GetChild(0).GetComponent<PlayerBattle>().boomnum < 5)
                {
                    transform.GetChild(0).GetComponent<PlayerBattle>().boomnum += 1;
                }
                else
                {
                    transform.parent.GetComponent<PlayerBattle>().Bombuse();
                }
                AS.clip = ItemSFX;
                AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                AS.Play();
            }
        }
    }
}
