using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemySpeed = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemySpeed = 2 + GameManager.instance.currentLevel * 3;
        
        if (transform.position.x > 10)
        {
            transform.position = new Vector2(-10, 3);
        }
        transform.Translate(transform.right * enemySpeed * Time.deltaTime, Space.World);
        
        
        
    }
}
