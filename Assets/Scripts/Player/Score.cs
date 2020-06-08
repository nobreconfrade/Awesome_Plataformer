using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text coin_text_score;
    private AudioSource audio_manager;
    private int score_count = 0;

    private void Awake()
    {
        audio_manager = GetComponent<AudioSource>();
    }

    private void Start()
    {
        coin_text_score = GameObject.Find("Coin text").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tags.COIN)
        {
            Debug.Log("BAM");
            collision.gameObject.SetActive(false);
            score_count++;
            audio_manager.Play();
            coin_text_score.text = 'x' + score_count.ToString();
        }
    }

}
