using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombscr : MonoBehaviour {

    Vector3 tmp;
    private AudioSource AS;
    public AudioClip BombSFX;

    void Start () {
        tmp = new Vector3(0, 0, 1);
        AS = GetComponent<AudioSource>();
        AS.clip = BombSFX;
        AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
        AS.PlayOneShot(BombSFX);
    }
	
	void Update () {
        tmp = transform.localScale;
        tmp.x += 100 * Time.deltaTime;
        tmp.y += 100 * Time.deltaTime;
        transform.localScale = tmp;
        if (tmp.x >= 100)
        {
            Destroy(gameObject);
        }
    }
}
