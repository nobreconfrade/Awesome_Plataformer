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
            collision.gameObject.SetActive(false);
            audio_manager.Play();
            AddToCoinScore();
        }
    }
    void AddToCoinScore()
    {
        string[] score_text;
        int score;
        score_text = coin_text_score.text.Split('x');
        score = int.Parse(score_text[1]);
        score++;
        coin_text_score.text = 'x' + score.ToString();
    }
}
