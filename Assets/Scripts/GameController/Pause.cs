using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool p;
    GameObject filter;

    private Sprite[] numbers;
    private Sprite[] status;
    string path;

    private GameObject nums;

    public GameObject buttonPause;

    SpawnControl spawn;
    public bool isCountDown = false;



    private void Start()
    {
        this.path = "Pause/";
        this.p = false;
        this.filter = GameObject.Find("Filter");
        this.buttonPause = GameObject.Find("ButtonPause");
        this.nums = GameObject.Find("Nums");
        this.spawn = GameObject.Find("Spawner").GetComponent<SpawnControl>();

        this.filter.SetActive(false);
        this.numbers = Resources.LoadAll<Sprite>(this.path + "Nums/");
        this.status = Resources.LoadAll<Sprite>(this.path + "Actions/");

        this.nums.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void pause()
    {

        this.p = !this.p;

        if (this.p)
        {
            Time.timeScale = 0;
            this.filter.SetActive(true);
            this.buttonPause.GetComponent<Image>().sprite = (this.status[0].name == "play") ? this.status[0] : this.status[1];
            gameObject.GetComponent<GameController>().pauseMusic();
        }
        else
        {
            this.spawn.setBoolSpawn(false);
            this.spawn.stopAllNahgs();
            Time.timeScale = 1;

            this.isCountDown = true;
            StartCoroutine(this.countdown());

        }
    }

    IEnumerator countdown()
    {
        //
        this.buttonPause.GetComponent<Button>().interactable = false;
        int i = 3;
        gameObject.GetComponent<GameController>().playMusic();
        while (i >= 0)
        {
            this.nums.GetComponent<SpriteRenderer>().sprite = this.numbers[i];
            i--;
            yield return new WaitForSeconds(1);

        }

        this.nums.GetComponent<SpriteRenderer>().sprite = null;
        this.filter.SetActive(false);
        this.buttonPause.GetComponent<Image>().sprite = (this.status[0].name == "pause") ? this.status[0] : this.status[1];


        this.spawn.playAllNahgs();
        this.isCountDown = false;

        if (!this.isCountDown)
        {
            yield return new WaitForSeconds(this.spawn.SpawnRate);
            this.buttonPause.GetComponent<Button>().interactable = true;
            this.spawn.setBoolSpawn(true);
            this.spawn.StartCoroutine(this.spawn.Spawn());
            Debug.Log("Ejecutado");
        }

        yield return null;
    }


}