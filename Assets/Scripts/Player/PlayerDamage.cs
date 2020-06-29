using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private Text life_text;
    private int life_score_count;

    private bool can_damage;

    private void Awake()
    {
        life_text = GameObject.Find("Life text").GetComponent<Text>();
        life_score_count = 3;
        life_text.text = "x" + life_score_count;

        can_damage = true;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void ReceiveDamage()
    {
        if (can_damage)
        {
            life_score_count--;
            if (life_score_count >= 0)
            {
                life_text.text = "x" + life_score_count;
            }
            if (life_score_count == 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }
            can_damage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        can_damage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("World 1-1");
    }

}
