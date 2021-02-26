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
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the bullet touches the enemy, the game enters the next level
        GameManager.instance.currentLevel++;
        
        SceneManager.LoadScene(GameManager.instance.currentLevel);
        
        // the game ends at level 3
        if (GameManager.instance.currentLevel == 3)
        {
            GameManager.instance.isGame = false;
        }
    }
}
