using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverScr : MonoBehaviour {

    public bool go;
    public float alp;
    public int continuecount;

	void Start ()
    {
        go = false;
        alp = 0;
        continuecount = 15;
        GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }
	
	void Update () {
        if (go == true && alp < 255)
        {
            alp += 2.5f;
            GetComponent<Image>().color = new Color32(255, 255, 255, (byte)alp);
        }
	}

    public void GOS(int score)
    {
        go = true;
        StartCoroutine(ReCount());
    }
    public void GOE()
    {
        go = false;
        alp = 0;
        continuecount = 15;
        GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    IEnumerator ReCount()
    {
        while (true)
        {
            go = true;
            if (continuecount == 15)
            {
                yield return new WaitForSecondsRealtime(5.0f);
            }
            else
            {
                yield return new WaitForSecondsRealtime(1.0f);
            }
            go = true;
            continuecount -= 1;
            if (continuecount == 0)
            {
                yield break;
            }
        }
    }
}
