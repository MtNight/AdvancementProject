using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour {

    private bool destroy;

    private bool shootable;
    private bool boomable;
    public int slevel;
    private int index;

    public int boomnum;
    private GameObject boom;

    private GameObject sys;
    private SpriteRenderer sr;

    void Start ()
    {
        destroy = false;

        shootable = true;
        boomable = true;
        slevel = 1;
        index = 0;

        boomnum = 3;
        boom = Resources.Load("bomb") as GameObject;

        sys = transform.parent.transform.parent.gameObject;
        sr = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (shootable == true)
            {
                Shooting(slevel);
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (boomable == true && boomnum > 0)
            {
                Bombuse();
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))   //개발자용
        {
            slevel += 1;
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroy == false)
        {
            if (other.tag == "EBullet" || other.tag == "Enemy")
            {
                if (transform.parent.GetComponent<PlayerControl>().invin == false)
                {
                    if (other.tag == "EBullet")
                    {
                        if (other.gameObject.GetComponent<EBullet1>() != null)
                        {
                            other.gameObject.GetComponent<EBullet1>().Off();
                        }
                        else if (other.gameObject.GetComponent<EBullet2>() != null)
                        {
                            other.gameObject.GetComponent<EBullet2>().Off();
                        }
                        else
                        {
                            Destroy(other.gameObject);
                        }
                    }
                    else if (other.tag == "Enemy")
                    {
                    }
                    if (destroy == false)
                    {
                        destroy = true;
                        sys.GetComponent<PlayerSystem>().Dest();
                    }
                    for (int i = 0; i < (slevel - 1) / 2; i++)
                    {
                        GameObject item = Resources.Load("WUitem") as GameObject;
                        Instantiate(item, transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    void Shooting(int level)
    {
        StartCoroutine("ShootCool");
        Vector3 tmp = transform.position;
        tmp.y += 0.25f;
        tmp.z = 11;
        if (level > 0)
        {
            sys.GetComponent<PlayerSystem>().b1[index].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 1);
        }
        if (level > 1)
        {
            tmp.y -= 0.05f;
            tmp.x += 0.2f;
            sys.GetComponent<PlayerSystem>().b2[index].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.75f);
            tmp.x -= 0.4f;
            sys.GetComponent<PlayerSystem>().b2[index + 10].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.75f);
        }
        if (level > 2)
        {
            tmp.y -= 0.075f;
            tmp.x -= 0.2f;
            sys.GetComponent<PlayerSystem>().b3[index].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.5f);
            tmp.x += 0.8f;
            sys.GetComponent<PlayerSystem>().b3[index + 10].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.5f);
        }
        if (level > 3)
        {
            tmp.y += 0.05f;
            tmp.x -= 0.4f;
            sys.GetComponent<PlayerSystem>().b4[index].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.5f);
            sys.GetComponent<PlayerSystem>().b5[index].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.5f);
        }
        if (level > 4)
        {
            tmp.y += 0.05f;
            tmp.x -= 0.2f;
            sys.GetComponent<PlayerSystem>().b4[index + 10].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.5f);
            tmp.x += 0.4f;
            sys.GetComponent<PlayerSystem>().b5[index + 10].GetComponent<PlayerBullet>().On(tmp, Quaternion.Euler(0, 0, 0), 0.5f);
        }
        index++;
        if (index >= 10)
        {
            index = 0;
        }
    }
    public void Bombuse()
    {
        Instantiate(boom, transform.position, Quaternion.Euler(0, 0, 0));
        boomnum--;
        StartCoroutine("BoomCool");
    }

    IEnumerator ShootCool()
    {
        while (true)
        {
            if (shootable == true)
            {
                shootable = false;
                yield return new WaitForSeconds(0.05f);
                shootable = true;
            }
            yield break;
        }
    }
    IEnumerator BoomCool()
    {
        while (true)
        {
            boomable = false;
            yield return new WaitForSeconds(3.0f);
            boomable = true;
            yield break;
        }
    }
}
