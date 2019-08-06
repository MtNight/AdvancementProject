using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Scr : MonoBehaviour {
    
    private int enemynum;
    private GameObject bullet;
    private GameObject[] b1, b2, b3, b4, b5;
    private int index1, index2;
    public Vector3 itemdir;
    private GameObject music;
    private GameObject BG;
    private GameObject UIM;

    private bool hitplay;
    private AudioSource AS;
    public AudioClip EHitSFX;
    public AudioClip BoostSFX;

    void Start () {
        enemynum = 1;
        b1 = new GameObject[500];
        b2 = new GameObject[100];
        index1 = 0;
        index2 = 0;

        for (int i = 0; i < 500; i++)
        {
            if (i >= 100)
            {
                bullet = Resources.Load("EBullet1") as GameObject;
                b1[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
                b1[i].transform.parent = gameObject.transform;
            }
            else
            {
                bullet = Resources.Load("EBullet1") as GameObject;
                b1[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
                b1[i].transform.parent = gameObject.transform;
                bullet = Resources.Load("EBullet2") as GameObject;
                b2[i] = Instantiate(bullet, new Vector3(-10, -10, -10), Quaternion.Euler(0, 0, 0));
                b2[i].transform.parent = gameObject.transform;
            }
        }

        itemdir = new Vector3(1, -1, 0);

        music = GameObject.Find("MUSICmanager");
        BG = GameObject.Find("S1BG");
        UIM = GameObject.Find("UImanager");
        hitplay = false;
        AS = GetComponent<AudioSource>();
        StartCoroutine("Stage1");
	}
	
	void Update () {
		
	}

    public void Create_bullet(int type, Vector3 pos, Quaternion ang, float scale, float speed, Vector3 dir)
    {
        switch (type)
        {
            case 1:
                if (b1[index1].GetComponent<EBullet1>().act == false)
                {
                    b1[index1].GetComponent<EBullet1>().On(pos, ang, scale);
                    b1[index1].GetComponent<EBullet1>().speed = speed;
                    b1[index1].GetComponent<EBullet1>().dir = dir;
                    b1[index1].GetComponent<EBullet1>().transform.localScale *= scale;
                    index1++;
                }
                break;
            case 2:
                if (b2[index2].GetComponent<EBullet1>().act == false)
                {
                    b2[index2].GetComponent<EBullet1>().On(pos, ang, scale);
                    b2[index2].GetComponent<EBullet1>().speed = speed;
                    b2[index2].GetComponent<EBullet1>().dir = dir;
                    b1[index1].GetComponent<EBullet1>().transform.localScale *= scale;
                    index2++;
                }
                break;
        }
        if (index1 >= 500) { index1 = 0; }
        if (index2 >= 100) { index2 = 0; }
    }

    IEnumerator Stage1()
    {
        while (true)
        {
            UIM.GetComponent<UImanage>().PopupCon("Chapter 1:\nThe Begining of The End\nOST: Sakuzyo - AXION", 8.0f);
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(E1_1(8, 0.5f));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(E1_1_L1(8, 0.2f));
            StartCoroutine(E1_1_L1(8, 0.8f));
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(E1_2(8, 0.6f, 1));
            StartCoroutine(E1_2(8, 0.4f, -1));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(E1_1_v(4, 0.9f)); 
            StartCoroutine(EMBBullet(0.5f));
            yield return new WaitForSeconds(3.0f);
            StartCoroutine(E1_1_v(4, 0.1f));
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(E1_1_R(5, 0.5f));
            StartCoroutine(E1_1_L(5, 0.5f));
            StartCoroutine(E1_1(5, 0.5f));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(EMBBullet(0.25f));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(EMBBullet(0.75f));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(E1_2(7, 0.1f, 1));
            StartCoroutine(E1_2(7, 0.9f, -1));
            yield return new WaitForSeconds(3.0f);
            StartCoroutine(E1_1(8, 0.5f));
            yield return new WaitForSeconds(6.0f);
            StartCoroutine(E1_1_v(8, 0.5f));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(EMBBullet(0.2f));
            StartCoroutine(EMBBullet(0.8f));
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(E1_2(8, 0.3f, 1));
            StartCoroutine(E1_2(8, 0.7f, -1));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(EMBBullet(0.1f));
            StartCoroutine(EMBBullet(0.9f));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(E1_1_R(4, 0.5f));
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(E1_1_R(4, 0.3f));
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(E1_1_R(4, 0.1f));
            StartCoroutine(E1_1_L(4, 0.5f));
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(E1_1_L(4, 0.7f));
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(E1_1_L(4, 0.9f));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(EMBBullet(0.5f));
            yield return new WaitForSeconds(3.0f);
            StartCoroutine(EMBBullet(0.8f));
            StartCoroutine(EMBBullet(0.2f));
            yield return new WaitForSeconds(3.0f);
            StartCoroutine(EMBBullet(0.75f));
            StartCoroutine(EMBBullet(0.25f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.5f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.5f));
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(E1_1(4, 0.5f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(E1_2(3, 0.7f, 1));
            StartCoroutine(E1_2(3, 0.3f, -1));
            StartCoroutine(EMBBullet(0.9f));
            StartCoroutine(EMBBullet(0.1f));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(EMBBullet(0.9f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.675f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.325f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.1f));
            StartCoroutine(E1_1(1, 0.9f));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(EMBBullet(0.1f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.375f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.625f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EMBBullet(0.9f));
            StartCoroutine(E1_1(1, 0.1f));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(EMBBullet(0.6f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(EMBBullet(0.4f));
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(EMBBullet(0.2f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(EMBBullet(0.5f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(EMBBullet(0.8f));
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(EMBBullet(0.7f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(EMBBullet(0.3f));
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(EMBBullet(0.1f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(EMBBullet(0.325f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(E1_1(1, 0.5f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(EMBBullet(0.675f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(EMBBullet(0.9f));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(E1_1_R(1, 0.4f));
            StartCoroutine(E1_1_L(1, 0.6f));

            //boss
            yield return new WaitForSeconds(10.0f);
            if (music.GetComponent<Musicmanage>().np == 11)
            {
                music.GetComponent<Musicmanage>().BGMStop(2, true, 5.0f);
                yield return new WaitForSeconds(2.0f);
                BG.GetComponent<BGmove>().SUF(0.8f, 8);
                AS.clip = BoostSFX;
                AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
                AS.PlayOneShot(BoostSFX);
                UIM.GetComponent<UImanage>().PopupCon("CAUTION!\n Enemy Approaching!\nOST: SHIKI - Pure Ruby", 3.0f);
                music.GetComponent<Musicmanage>().BGMPlay(1, 12, true);
                yield return new WaitForSeconds(10.0f);
                StartCoroutine(EMB1_1(1));
                yield return new WaitForSeconds(120.0f);

            }
           
            yield break;
        }
    }

    IEnumerator E1_1(int num, float xpoint)   //random spawn
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_1") as GameObject;
            float x;
            for (int i = 0; i < num; i++)
            {
                if (xpoint == 0)
                {
                    x = Random.Range(-2.4f, 2.4f);
                }
                else
                {
                    x = -2.4f + 4.8f * xpoint;
                }
                GameObject enemy = Instantiate(spawn, new Vector3(x, 4.0f, 5), Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<Enemy1_1>().dir = Vector3.down;

                enemy.GetComponent<Enemy1_1>().num = enemynum;
                enemynum++;
                yield return new WaitForSeconds(0.5f);
            }
            yield break;
        }
    }
    IEnumerator E1_1_R(int num, float xpoint)  //random right-left
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_1") as GameObject;
            float x;
            for (int i = 0; i < num; i++)
            {
                if (xpoint == 0)
                {
                    x = Random.Range(-2.0f, 2.0f);
                }
                else
                {
                    x = -2.0f + 4.0f * xpoint;
                }
                GameObject enemy = Instantiate(spawn, new Vector3(4.0f + x / Mathf.Sqrt(2), 4.0f - x / Mathf.Sqrt(2), 5), Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<Enemy1_1>().dir = (Vector3.down + Vector3.left).normalized;

                enemy.GetComponent<Enemy1_1>().num = enemynum;
                enemynum++;
                yield return new WaitForSeconds(0.5f);
            }
            yield break;
        }
    }
    IEnumerator E1_1_L(int num, float xpoint)  //random left-right
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_1") as GameObject;
            float x;
            for (int i = 0; i < num; i++)
            {
                if (xpoint == 0)
                {
                    x = Random.Range(-2.0f, 2.0f);
                }
                else
                {
                    x = -2.0f + 4.0f * xpoint;
                }
                GameObject enemy = Instantiate(spawn, new Vector3(-4.0f + x / Mathf.Sqrt(2), 4.0f + x / Mathf.Sqrt(2), 5), Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<Enemy1_1>().dir = (Vector3.down + Vector3.right).normalized;

                enemy.GetComponent<Enemy1_1>().num = enemynum;
                enemynum++;
                yield return new WaitForSeconds(0.5f);
            }
            yield break;
        }
    }
    IEnumerator E1_1_L1(int num, float xpoint)    //random 1-line
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_1") as GameObject;
            float x;
            if (xpoint == 0)
            {
                x = Random.Range(-2.4f, 2.4f);
            }
            else
            {
                x = -2.4f + 4.8f * xpoint;
            }
            for (int i = 0; i < num; i++)
            {
                GameObject enemy = Instantiate(spawn, new Vector3(x, 4.0f, 5), Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<Enemy1_1>().dir = Vector3.down;

                enemy.GetComponent<Enemy1_1>().num = enemynum;
                enemynum++;
                yield return new WaitForSeconds(0.5f);
            }
            yield break;
        }
    }
    IEnumerator E1_1_L3(int num, float xpoint)    //random 1-line continuos 3times
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_1") as GameObject;
            float x;
            if (xpoint == 0)
            {
                x = Random.Range(-2.4f, 0);
            }
            else
            {
                x = -2.4f + 4.8f * xpoint;
            }
            GameObject enemy;
            for (int i = 0; i < num * 2; i++)
            {
                if (i < num)
                {
                    enemy = Instantiate(spawn, new Vector3(x, 4.0f, 5), Quaternion.Euler(0, 0, 0));
                    enemy.GetComponent<Enemy1_1>().dir = Vector3.down;
                    enemy.GetComponent<Enemy1_1>().num = enemynum;
                    enemynum++;
                };
                if (i > num - 3 && i < num*2 - 3)
                {
                    enemy = Instantiate(spawn, new Vector3(x + 0.8f, 4.0f, 5), Quaternion.Euler(0, 0, 0));
                    enemy.GetComponent<Enemy1_1>().dir = Vector3.down;
                    enemy.GetComponent<Enemy1_1>().num = enemynum;
                    enemynum++;
                }
                if (i > num*2 - 6)
                {
                    enemy = Instantiate(spawn, new Vector3(x + 1.6f, 4.0f, 5), Quaternion.Euler(0, 0, 0));
                    enemy.GetComponent<Enemy1_1>().dir = Vector3.down;
                    enemy.GetComponent<Enemy1_1>().num = enemynum;
                    enemynum++;
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield break;
        }
    }

    IEnumerator E1_1_v(int num, float xpoint)     //random V-shape 
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_1") as GameObject;
            float x;
            if (xpoint == 0)
            {
                x = Random.Range(-1.2f, 1.2f);
            }
            else
            {
                x = -1.2f + 2.4f * xpoint;
            }
            Vector3 pos = new Vector3(x, 4.0f, 5), plus = Vector3.zero;
            for (int i = 0; i < num; i++)
            {
                GameObject enemy = Instantiate(spawn, pos + plus, Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<Enemy1_1>().dir = Vector3.down;
                enemy.GetComponent<Enemy1_1>().num = enemynum;
                enemynum++;
                if (i > 0)
                {
                    enemy = Instantiate(spawn, pos - plus, Quaternion.Euler(0, 0, 0));
                    enemy.GetComponent<Enemy1_1>().dir = Vector3.down;
                    enemy.GetComponent<Enemy1_1>().num = enemynum;
                    enemynum++;
                }
                plus.x += 0.3f;
                yield return new WaitForSeconds(0.4f);
            }
            yield break;
        }
    }


    IEnumerator E1_2(int num, float xpoint, int direct)   //random 'direct' fade
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_2") as GameObject;
            float x;
            for (int i = 0; i < num; i++)
            {
                if (xpoint == 0)
                {
                    x = Random.Range(-2.4f, 2.4f);
                }
                else
                {
                    x = -2.4f + 4.8f * xpoint;
                }
                GameObject enemy = Instantiate(spawn, new Vector3(x, 4.0f, 5), Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<Enemy1_2>().dir = Vector3.down;
                enemy.GetComponent<Enemy1_2>().rl = direct;

                enemy.GetComponent<Enemy1_2>().num = enemynum;
                enemynum++;
                yield return new WaitForSeconds(0.5f);
            }
            yield break;
        }
    }
    IEnumerator E1_2_L1(int num, float xpoint, int direct)    //random 1-line 'direct' fade
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_2") as GameObject;
            float x;
            if (xpoint == 0)
            {
                x = Random.Range(-2.4f, 2.4f);
            }
            else
            {
                x = -2.4f + 4.8f * xpoint;
            }
            for (int i = 0; i < num; i++)
            {
                GameObject enemy = Instantiate(spawn, new Vector3(x, 4.0f, 5), Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<Enemy1_2>().dir = Vector3.down;
                enemy.GetComponent<Enemy1_2>().rl = direct;

                enemy.GetComponent<Enemy1_2>().num = enemynum;
                enemynum++;
                yield return new WaitForSeconds(0.5f);
            }
            yield break;
        }
    }
    IEnumerator EMBBullet(float xpoint)
    {
        while (true)
        {
            GameObject spawn = Resources.Load("EBullet3_1") as GameObject;
            float x;
            if (xpoint == 0)
            {
                x = Random.Range(-2.4f, 2.4f);
            }
            else
            {
                x = -2.4f + 4.8f * xpoint;
            }
            GameObject enemy = Instantiate(spawn, new Vector3(x, 4.0f, 5), Quaternion.Euler(0, 0, 0));
            enemy.GetComponent<EBullet3>().dir = new Vector3(0, -1, 0);
            
            yield return new WaitForSeconds(0.5f);
            yield break;
        }
    }
    IEnumerator EMB1_1(int num)   //random spawn
    {
        while (true)
        {
            GameObject spawn = Resources.Load("Enemy1_3") as GameObject;
            GameObject enemy = Instantiate(spawn, new Vector3(-5, 5, 21), Quaternion.Euler(0, 0, 0));

            enemy.GetComponent<Enemy1_MB1>().num = enemynum;
            enemynum++;
            yield return new WaitForSeconds(0.5f);
            yield break;
        }
    }

    public void ItemdirChange()
    {
        if (itemdir.x == itemdir.y)
        {
            itemdir.y = -itemdir.y;
        }
        else
        {
            itemdir.x = -itemdir.x;
        }
    }


    IEnumerator EndGame()
    {
        while (true)
        {
            StartCoroutine(EMBBullet(1.0f));
            StartCoroutine(E1_1(1, 0.0f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.9f));
            StartCoroutine(E1_1(1, 0.1f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.8f));
            StartCoroutine(E1_1(1, 0.2f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.7f));
            StartCoroutine(E1_1(1, 0.3f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.6f));
            StartCoroutine(E1_1(1, 0.4f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.5f));
            StartCoroutine(E1_1(1, 0.5f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.4f));
            StartCoroutine(E1_1(1, 0.6f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.3f));
            StartCoroutine(E1_1(1, 0.7f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.2f));
            StartCoroutine(E1_1(1, 0.8f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.1f));
            StartCoroutine(E1_1(1, 0.9f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(EMBBullet(0.0f));
            StartCoroutine(E1_1(1, 1.0f));
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void SCF()
    {
        StartCoroutine(StageClear());
    }
    IEnumerator StageClear()
    {
        while (true)
        {
            //BG.GetComponent<BGmove>().SUF(0.1f, 8);
            UIM.GetComponent<UImanage>().PopupCon("Boss Clear!\nBonus Score: 1000", 3.0f);
            GameObject.Find("Player").GetComponent<PlayerSystem>().score += 1000;
            yield return new WaitForSeconds(6.0f);
            UIM.GetComponent<UImanage>().PopupCon("There is no next stage.\nIf you wanna play next stage...", 5.0f);
            yield return new WaitForSeconds(8.0f);
            UIM.GetComponent<UImanage>().PopupCon("Send me message to make me create new level.\nSo enjoy rest time in infinity mode!", 8.0f);
            yield return new WaitForSeconds(10.0f);
            StartCoroutine(EndGame());
            yield break;
        }
    }

    public void EHitSoundFunc()
    {
        if (hitplay == false)
        {
            hitplay = true;
            AS.clip = EHitSFX;
            AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
            AS.PlayOneShot(EHitSFX);
            StartCoroutine(EHS());
        }
    }
    IEnumerator EHS()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.075f);
            //AS.Stop();
            hitplay = false;
            yield break;
        }
    }
}
