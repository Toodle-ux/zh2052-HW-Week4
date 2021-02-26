using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("a");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.currentLevel++;
        if (GameManager.instance.currentLevel == 3)
        {
            if (GameManager.instance.Attacks < GameManager.instance.LeastAttacks)
            {
                GameManager.instance.LeastAttacks = GameManager.instance.Attacks;
            }
        }
        SceneManager.LoadScene(GameManager.instance.currentLevel);
    }
}
