using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicmanage : MonoBehaviour {

    private AudioSource c1;
    private AudioSource c2;
    public int playch;
    public int np;
    public float BGMvol;
    public float SFXvol;
    public AudioClip BGMD;
    public AudioClip BGM0;
    public AudioClip BGM1_1;
    public AudioClip BGM1_2;
    public AudioClip BGM2;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start () {
        c1 = transform.GetChild(0).GetComponent<AudioSource>();
        c2 = transform.GetChild(1).GetComponent<AudioSource>();
        playch = 1;
        BGMvol = 0.3f;
        SFXvol = 0.8f;

        BGMPlay(1, 00, true);
    }
	
	void Update () {
        if (playch == 1)
        {
            c1.volume = BGMvol;
        }
        else if (playch == 2)
        {
            c2.volume = BGMvol;
        }
    }
    public void BGMPlay(int channel, int mnum, bool loop)
    {
        AudioClip music = BGMnumber(mnum);
        np = mnum;
        AudioSource c;
        if (channel == 1)
        {
            c = c1;
            playch = 1;
        }
        else
        {
            c = c2;
            playch = 2;
        }
        c.volume = BGMvol;
        c.clip = music;
        c.loop = loop;
        c.Play();
    }

    public void BGMStop(int channel, bool fade, float time)
    {
        AudioSource c;
        if (channel == 1)
        {
            c = c1;
            playch = 2;
        }
        else
        {
            c = c2;
            playch = 1;
        }

        if (fade == true)
        {
            StartCoroutine(VolDown(c, time));
        }
        else
        {
            c.Stop();
        }
    }
    public void BGMPause(int channel)
    {
        AudioSource c;
        if (channel == 1)
        {
            c = c1;
            playch = 1;
        }
        else
        {
            c = c2;
            playch = 2;
        }
        c.volume = BGMvol;
        c.Pause();
    }

    private AudioClip BGMnumber(int mnum)
    {
        switch (mnum)
        {
            case -1: return BGMD;
            case 00: return BGM0;
            case 11: return BGM1_1;
            case 12: return BGM1_2;
            default: return null;
        }
    }

    IEnumerator VolDown(AudioSource c, float time)
    {
        while (true)
        {
            if (c.volume > 0)
            {
                c.volume -= 0.1f * (1 / (4 * time)); ;
            }
            else
            {
                c.Stop();
                c.volume = BGMvol;
                yield break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
