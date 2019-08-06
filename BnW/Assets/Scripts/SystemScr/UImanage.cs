using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanage : MonoBehaviour {

    public int scenenum;
    private int score;
    private bool option;
    public bool pause;
    public bool go;
    public bool ingame;
    float vol;

    public Text load;    //loading
    public Text main1;   //title
    public Text main2;   //start
    public Text main3;   //option
    public Text main4;   //exit
    public Text game1;   //score
    public Text game2;   //bomb
    public Text game3;   //popup
    public Text pause1;   //pausetxt
    public Text pause2;   //continue
    public Text pause3;   //quit
    public Text go1;   //score
    public Text go2;   //count
    public Image go3;   //bg
    public Text op1;   //bgmtxt
    public Slider op2;   //bgmscr
    public Text op3;   //sfxtxt
    public Slider op4;   //sfxscr
    public Text op5;   //back
    public Text op6;   //keyexplation


    private GameObject player;
    private GameObject lifeicon;
    private GameObject[] lifes;
    private GameObject music;
    private GameObject cam;

    private AudioSource AS;
    public AudioClip PauseSFX;
    public AudioClip UnpauseSFX;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start () {
        scenenum = -1; //load=-1 / main=0 / stage1=1 ...
        score = 0;
        option = false;
        pause = false;
        go = false;
        ingame = true;

        Screen.SetResolution(600, 800, false);

        load.gameObject.SetActive(true);
        main1.gameObject.SetActive(false);
        main2.gameObject.SetActive(false);
        main3.gameObject.SetActive(false);
        main4.gameObject.SetActive(false);
        go1.gameObject.SetActive(false);
        go2.gameObject.SetActive(false);
        go3.gameObject.SetActive(false);
        game1.gameObject.SetActive(false);
        game2.gameObject.SetActive(false);
        game3.gameObject.SetActive(false);
        pause1.gameObject.SetActive(false);
        pause2.gameObject.SetActive(false);
        pause3.gameObject.SetActive(false);
        op1.gameObject.SetActive(false);
        op2.gameObject.SetActive(false);
        op3.gameObject.SetActive(false);
        op4.gameObject.SetActive(false);
        op5.gameObject.SetActive(false);
        op6.gameObject.SetActive(false);

        op2.maxValue = 1;
        op2.minValue = 0;
        op2.value = 0.3f;
        op4.maxValue = 1;
        op4.minValue = 0;
        op4.value = 0.8f;

        lifeicon = Resources.Load("Life") as GameObject;
        lifes = new GameObject[5];
        Vector3 tmp = new Vector3(33.8f, 604f, 0f);
        for (int i = 0; i < 5; i++)
        {
            lifes[i] = Instantiate(lifeicon);
            lifes[i].transform.position = tmp;
            lifes[i].transform.localScale = new Vector3(0.317875f, 0.317875f, 0.317875f);//640x480기준
            lifes[i].transform.SetParent(transform.GetChild(0).transform.GetChild(3));
            tmp.x += 40;
            lifes[i].gameObject.SetActive(false);
        }
        music = GameObject.Find("MUSICmanager");
        cam = GameObject.Find("Main Camera");
        AS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        vol = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().BGMvol;
        op2.value = vol;
        vol = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
        op4.value = vol;


        if (scenenum == -1)     //load
        {
            load.gameObject.SetActive(true);
            main1.gameObject.SetActive(false);
            main2.gameObject.SetActive(false);
            main3.gameObject.SetActive(false);
            main4.gameObject.SetActive(false);
            go1.gameObject.SetActive(false);
            go2.gameObject.SetActive(false);
            go3.gameObject.SetActive(false);
            game1.gameObject.SetActive(false);
            game2.gameObject.SetActive(false);
            game3.gameObject.SetActive(false);
            pause1.gameObject.SetActive(false);
            pause2.gameObject.SetActive(false);
            pause3.gameObject.SetActive(false);
            op1.gameObject.SetActive(false);
            op2.gameObject.SetActive(false);
            op3.gameObject.SetActive(false);
            op4.gameObject.SetActive(false);
            op5.gameObject.SetActive(false);
            op6.gameObject.SetActive(false);
        }
        else if (scenenum == 0)     //main
        {
            load.gameObject.SetActive(false);
            main1.gameObject.SetActive(!option);
            main2.gameObject.SetActive(!option);
            main3.gameObject.SetActive(!option);
            main4.gameObject.SetActive(!option);
            go1.gameObject.SetActive(false);
            go2.gameObject.SetActive(false);
            go3.gameObject.SetActive(false);
            game1.gameObject.SetActive(false);
            game2.gameObject.SetActive(false);
            game3.gameObject.SetActive(false);
            pause1.gameObject.SetActive(false);
            pause2.gameObject.SetActive(false);
            pause3.gameObject.SetActive(false);
            op1.gameObject.SetActive(option);
            op2.gameObject.SetActive(option);
            op3.gameObject.SetActive(option);
            op4.gameObject.SetActive(option);
            op5.gameObject.SetActive(option);
            op6.gameObject.SetActive(option);
        }
        else if (scenenum == 1)     //game
        {
            //pause
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pause == false)
                {
                    GtoO();
                }
                else
                {
                    OtoG();
                }
            }

            //score
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            if (player != null)
            {
                if (score < player.GetComponent<PlayerSystem>().score)
                {
                    score += 1;
                }
                else if (score > player.GetComponent<PlayerSystem>().score)
                {
                    score = player.GetComponent<PlayerSystem>().score;
                }
                game1.text = score.ToString("D" + 10.ToString());
                if (GameObject.FindWithTag("Player") != null)
                {
                    game2.text = "Bomb " + GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<PlayerBattle>().boomnum + "/5";
                }
            }


            load.gameObject.SetActive(false);
            main1.gameObject.SetActive(false);
            main2.gameObject.SetActive(false);
            main3.gameObject.SetActive(false);
            main4.gameObject.SetActive(false);
            go1.gameObject.SetActive(!ingame);
            go2.gameObject.SetActive(!ingame);
            if (!ingame)
            {
                go2.text = "Continue?\n" + transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<GameoverScr>().continuecount;
            }
            go3.gameObject.SetActive(go);
            game1.gameObject.SetActive(ingame);
            game2.gameObject.SetActive(ingame);
            game3.gameObject.SetActive(ingame && !pause);
            pause1.gameObject.SetActive(pause);
            pause2.gameObject.SetActive(pause);
            pause3.gameObject.SetActive(pause);
            op1.gameObject.SetActive(pause);
            op2.gameObject.SetActive(pause);
            op3.gameObject.SetActive(pause);
            op4.gameObject.SetActive(pause);
            op5.gameObject.SetActive(false);
            op6.gameObject.SetActive(pause);
        }
    }
    public void ResetLife(int lifenum)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < lifenum)
            {
                lifes[i].gameObject.SetActive(true);
            }
            else
            {
                lifes[i].gameObject.SetActive(false);
            }
        }
    }

    public void MtoO()
    {
        cam.transform.position = new Vector3(-6, -8, -10);
        option = true;
    }
    public void OtoM()
    {
        cam.transform.position = new Vector3(-6, 0, -10);
        option = false;
    }
    public void BGMVolChange(float ch)
    {
        op2.value = ch;
        GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().BGMvol = op2.value;
    }
    public void SFXVolChange(float ch)
    {
        op4.value = ch;
        GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol = op4.value;
    }

    public void GOSOn(int score)
    {
        go1.text = "Your Score:\n" + score.ToString("D" + 10.ToString());
        go2.text = "Continue?\n" + transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<GameoverScr>().continuecount;
        go3.gameObject.SetActive(true);
        go3.GetComponent<GameoverScr>().GOS(score);
    }
    public void GOtoM()
    {
        ingame = true;
        GOSOff();
        Time.timeScale = 1;
        go = false;
        GameObject.Find("SCENEmanager").GetComponent<Scenemanage>().GtoM();
    }
    public void GOSOff()
    {
        go3.gameObject.SetActive(false);
        go3.GetComponent<GameoverScr>().GOE();
    }

    public void GtoO()
    {
        AS.clip = PauseSFX;
        AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
        AS.PlayOneShot(PauseSFX);
        cam.transform.position = new Vector3(-6, -8, -10);
        Time.timeScale = 0;
        pause = true;
        music.GetComponent<Musicmanage>().BGMPause(music.GetComponent<Musicmanage>().playch);
    }
    public void OtoG()
    {
        AS.clip = UnpauseSFX;
        AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
        AS.PlayOneShot(UnpauseSFX);
        cam.transform.position = new Vector3(0, 0, -10);
        Time.timeScale = 1;
        pause = false;
        if (music.GetComponent<Musicmanage>().np == 11)
        {
            music.GetComponent<Musicmanage>().BGMPlay(music.GetComponent<Musicmanage>().playch, 11, true);
        }
    }


    public void PopupCon(string text, float time)
    {
        StartCoroutine(PUC(text, time));
    }
    IEnumerator PUC(string text, float time)
    {
        while (true)
        {
            game3.text = text;
            StartCoroutine(AppearEff(game3, 0.1f));
            yield return new WaitForSeconds(time + 2.0f);
            StartCoroutine(DisappearEff(game3, 0.1f));
            yield break;
        }
    }
    IEnumerator AppearEff(Text obj, float time)
    {
        float x, y, delta = 0.1f;
        byte aa, ap = 0;
        aa = 1;
        x = 101;
        y = 0;
        obj.transform.localScale = new Vector3(x, y, 1);
        obj.GetComponent<Text>().color = new Color32(255, 255, 255, aa);
        while (true)
        {
            x = x - delta;
            y = y + delta / 100.0f;
            if (aa >= 255 || aa < ap)
            {
                aa = 255;
            }
            else
            {
                ap = aa;
                aa += (byte)(delta * 2.55f);
            }
            obj.transform.localScale = new Vector3(x, y, 1);
            obj.GetComponent<Text>().color = new Color32(255, 255, 255, aa);
            if (x <= 2)
            {
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                yield break;
            }
            delta += 0.1f;
            yield return new WaitForSeconds(Time.deltaTime / 4);
        }
    }
    IEnumerator DisappearEff(Text obj, float time)
    {
        float x, y, delta = 0.2f;
        byte aa, ap = 255;
        aa = 254;
        x = 1;
        y = 1;
        obj.transform.localScale = new Vector3(x, y, 1);
        obj.GetComponent<Text>().color = new Color32(255, 255, 255, aa);
        while (true)
        {
            x = x + delta;
            y = y - delta / 100.0f;
            if (aa <= 0 || aa > ap)
            {
                aa = 0;
            }
            else
            {
                ap = aa;
                aa -= (byte)(delta * 2.55f);
            }
            obj.transform.localScale = new Vector3(x, y, 1);
            obj.GetComponent<Text>().color = new Color32(255, 255, 255, aa);
            if (x >= 100)
            {
                obj.transform.localScale = new Vector3(101, 0, 1);
                obj.GetComponent<Text>().color = new Color32(255, 255, 255, 0);
                yield break;
            }
            delta += 0.2f;
            yield return new WaitForSeconds(Time.deltaTime / 4);
        }
    }
}
