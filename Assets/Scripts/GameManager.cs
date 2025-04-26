using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject start;
    public Vector3 StartPosition;
    public GameObject Player;
    public GameObject EventManager;
    public bool GameStart = false;
    public float PlayTime = 0;
    //player state
    public bool IsDead=false;
    //canvas
    public GameObject MainMenu;
    public GameObject MaxScore;
    public GameObject DeadMenu;
    public TextMeshPro TimeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainMenu.SetActive(true);
        if (@PlayerPrefs.GetInt("MaxScore")!=null)
        {
            MaxScore.GetComponent<TextMeshPro>().text = PlayerPrefs.GetInt("MaxScore").ToString();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameStart)
        {
            PlayTime += Time.deltaTime;
            TimeText.text = (Mathf.Abs(PlayTime).ToString());
        }
        DeadFonk();
    }

    public void GameStartFonk()
    {
        IsDead = false;
        MainMenu.SetActive(false);
        DeadMenu.SetActive(false);
        TimeText.gameObject.SetActive(true);
        start.transform.position = StartPosition;
        Player.transform.position = StartPosition;
        EventManager.SetActive(true);
        //EventManager.GetComponent<EventManager>().Reset();
        GameStart = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            IsDead = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("oyuncu oldu");
            IsDead = true;
        }
    }
    public void DeadFonk()
    {
        if (IsDead)
        {
            if (PlayerPrefs.GetInt("MaxScore") != null)
            {
                if (PlayerPrefs.GetInt("MaxScore") < Convert.ToInt32(TimeText.GetComponent<TextMeshPro>()))
                {
                    PlayerPrefs.SetInt("MaxScore", Mathf.Abs((Convert.ToInt32(TimeText.GetComponent<TextMeshPro>()))));
                }
            }
            PlayerPrefs.Save();
            MainMenu.SetActive(false);
            DeadMenu.SetActive(true);
            TimeText.gameObject.SetActive(false);
            EventManager.gameObject.SetActive(false);
        }
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
