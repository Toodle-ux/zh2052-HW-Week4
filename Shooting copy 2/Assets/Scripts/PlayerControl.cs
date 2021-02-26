using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb2D;
    
    public GameObject BulletPrefab;

    public float forceAmount = 5;

    public static PlayerControl instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // use A & D to move the circle left or right
        if (Input.GetKey(KeyCode.A))
        {
            rb2D.AddForce(Vector2.left * forceAmount);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb2D.AddForce(Vector2.right * forceAmount);
        }

        // press SPACE to shoot a bullet upward
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.Attacks++;
            Instantiate(BulletPrefab, transform.position, transform.rotation);
        }
    }
}
