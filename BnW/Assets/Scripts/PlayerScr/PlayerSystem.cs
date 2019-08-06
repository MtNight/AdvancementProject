using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour {

    private int life;
    public int score;

    private GameObject flight;
    private GameObject bullet;
    public GameObject[] b1, b2, b3, b4, b5;

    private GameObject UIM;
    private GameObject music;
    //private GameObject scene;

    private AudioSource AS;
    public AudioClip BoostSFX;
    public AudioClip DeathSFX;

    void Start ()
    {
        UIM = GameObject.Find("UImanager");
        music = GameObject.Find("MUSICmanager");
        //scene = GameObject.Find("SCENEmanager");

        life = 3;
        score = 0;

        bullet = Resources.Load("PBullet") as GameObject;
        b1 = new GameObject[10];
        b2 = new GameObject[20];
        b3 = new GameObject[20];
        b4 = new GameObject[20];
        b5 = new GameObject[20];
        for (int i = 0; i < 20; i++)
        {
            b2[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
            b3[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
            b4[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
            b5[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
            b2[i].transform.SetParent(gameObject.transform);
            b3[i].transform.SetParent(gameObject.transform);
            b4[i].transform.SetParent(gameObject.transform);
            b5[i].transform.SetParent(gameObject.transform);

            b2[i].GetComponent<PlayerBullet>().speed = 18.0f;
            b3[i].GetComponent<PlayerBullet>().speed = 16.0f;
            b4[i].GetComponent<PlayerBullet>().speed = 15.0f;
            b5[i].GetComponent<PlayerBullet>().speed = 15.0f;
            b2[i].GetComponent<PlayerBullet>().dir = Vector3.up;
            b3[i].GetComponent<PlayerBullet>().dir = Vector3.up;
            b4[i].GetComponent<PlayerBullet>().dir = (Vector3.up + Vector3.left).normalized;
            b5[i].GetComponent<PlayerBullet>().dir = (Vector3.up + Vector3.right).normalized;
            if (i < 10)
            {
                b1[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
                b1[i].transform.SetParent(gameObject.transform);

                b1[i].GetComponent<PlayerBullet>().speed = 20.0f;
                b1[i].GetComponent<PlayerBullet>().dir = Vector3.up;
                b4[i].GetComponent<PlayerBullet>().dir *= 0.8f;
                b5[i].GetComponent<PlayerBullet>().dir *= 0.8f;
            }
        }
        AS = GetComponent<AudioSource>();
        StartCoroutine("SurviveScore");
    }
	
	void Update () {
	}

    public void Dest()
    {
        life -= 1;
        UIM.GetComponent<UImanage>().ResetLife(life);
        AS.clip = DeathSFX;
        AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
        AS.PlayOneShot(DeathSFX);

        if (life == 0)
        {
            life = -1;
            StartCoroutine(GameOver());
        }
        else
        {
            StartCoroutine("Respawn");
        }
    }

    IEnumerator Respawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            AS.clip = BoostSFX;
            AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
            AS.PlayOneShot(BoostSFX);
            flight = Resources.Load("SDL4-78") as GameObject;
            GameObject tmp = Instantiate(flight, new Vector3(0, -5, 10), Quaternion.Euler(0, 0, 0));
            tmp.transform.SetParent(gameObject.transform);
            yield break; ;
        }
    }

    IEnumerator SurviveScore()
    {
        while (true)
        {
            if (GameObject.Find("SDL4-78") != null || GameObject.Find("SDL4-78(Clone)") != null)
            {
                score += 1;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator GameOver()
    {
        while (true)
        {
            music.GetComponent<Musicmanage>().BGMStop(2, false, 0);
            music.GetComponent<Musicmanage>().BGMStop(1, false, 0);
            yield return new WaitForSeconds(0.5f);
            UIM.GetComponent<UImanage>().go = true;
            UIM.GetComponent<UImanage>().GOSOn(score);
            music.GetComponent<Musicmanage>().BGMPlay(2, -1, false);
            yield return new WaitForSeconds(5.0f);
            UIM.GetComponent<UImanage>().ingame = false;
            Time.timeScale = 0;
            yield return new WaitForSeconds(25.0f);
            music.GetComponent<Musicmanage>().BGMStop(2, false, 0);
            yield return new WaitForSeconds(0.5f);
            UIM.GetComponent<UImanage>().GOtoM();
            yield break;
            
        }
    }
}
