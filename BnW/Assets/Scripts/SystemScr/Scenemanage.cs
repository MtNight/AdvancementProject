using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanage : MonoBehaviour {

    private bool move;
    private GameObject UIM;
    private GameObject cam;
    private GameObject music;
    private AudioSource AS;
    public AudioClip StartSFX;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        move = false;
        UIM = GameObject.Find("UImanager");
        cam = GameObject.Find("Main Camera");
        music = GameObject.Find("MUSICmanager");
        AS = GetComponent<AudioSource>();
        StartCoroutine("LtoM");
    }
    public void MtoS1()
    {
        if (move == false)
        {
            move = true;
            StartCoroutine(MtoS1IE());
        }
    }
    public void S1toS2()
    {
        UIM.GetComponent<UImanage>().scenenum = 1;
        SceneManager.LoadScene("StageScene");
    }
    public void S2toS3()
    {
        UIM.GetComponent<UImanage>().scenenum = 1;
        SceneManager.LoadScene("StageScene");
    }
    public void GtoM()
    {
        cam.transform.position = new Vector3(-6, 0, -10);
        UIM.GetComponent<UImanage>().scenenum = 0;
        music.GetComponent<Musicmanage>().BGMStop(music.GetComponent<Musicmanage>().playch, false, 0);
        music.GetComponent<Musicmanage>().BGMPlay(1, 00, true);
        UIM.GetComponent<UImanage>().ResetLife(0);
        SceneManager.LoadScene("MainScene");
    }

    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator MtoS1IE()
    {
        while (true)
        {
            AS.clip = StartSFX;
            AS.volume = GameObject.Find("MUSICmanager").GetComponent<Musicmanage>().SFXvol;
            AS.PlayOneShot(StartSFX);
            music.GetComponent<Musicmanage>().BGMStop(1, true, 3.0f);
            yield return new WaitForSeconds(3.0f);
            music.GetComponent<Musicmanage>().BGMPlay(2, 11, true);
            cam.transform.position = new Vector3(0, 0, -10);
            UIM.GetComponent<UImanage>().scenenum = 1;
            UIM.GetComponent<UImanage>().ResetLife(3);
            if (GameObject.Find("Player") != null)
            {
                GameObject.Find("Player").gameObject.SetActive(true);
            }
            move = false;
            SceneManager.LoadScene("StageScene");
            yield break;
        }
    }

    IEnumerator LtoM()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("MainScene");
            UIM.GetComponent<UImanage>().scenenum = 0;
            cam.transform.position = new Vector3(-6, 0, -10);
            yield break;
        }
    }
}
