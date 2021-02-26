using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text text;
    
    public int currentLevel = 0;
    
    private int attacks = 0;

    private const string FILE_LEAST_ATTACKS = "/Logs/leastAttacks.txt";
    private const string FILE_ALL_ATTACKS = "/Logs/allAttacks.csv";
    string FILE_PATH_LEAST_ATTACKS;
    string FILE_PATH_ALL_ATTACKS;
    
    private const string PREF_KEY_LEAST_ATTACKS = "LeastAttacksKey";

    public int Attacks
    {
        get { return attacks; }
        set
        {
            attacks = value;
            if (!File.Exists(FILE_PATH_ALL_ATTACKS))
            {
                File.Create(FILE_PATH_ALL_ATTACKS);
            }
            string fileContents = File.ReadAllText(FILE_PATH_ALL_ATTACKS);
            fileContents += attacks + ",";
            File.WriteAllText(FILE_PATH_ALL_ATTACKS, fileContents);
        }

    }
    
    private int leastAttacks = -1;

    public int LeastAttacks
    {
        get
        {
            if (leastAttacks < 0)
            {
                //leastAttacks = PlayerPrefs.GetInt(PREF_KEY_LEAST_ATTACKS, 100);
                if (File.Exists(FILE_PATH_LEAST_ATTACKS))
                {
                    string fileContents = File.ReadAllText(FILE_PATH_LEAST_ATTACKS);
                    leastAttacks = Int32.Parse(fileContents);
                }
                else
                {
                    leastAttacks = 100;
                }
            }
            return leastAttacks;
        }
        set
        {
            leastAttacks = value;
            //PlayerPrefs.SetInt(PREF_KEY_LEAST_ATTACKS, leastAttacks);
            File.WriteAllText(FILE_PATH_LEAST_ATTACKS, LeastAttacks + "");
        }
    }
    
    private void Awake()
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
        //PlayerPrefs.DeleteAll();
        FILE_PATH_LEAST_ATTACKS = Application.dataPath + FILE_LEAST_ATTACKS;
        FILE_PATH_ALL_ATTACKS = Application.dataPath + FILE_ALL_ATTACKS;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Level: " + currentLevel +
                    "\nAttacks: " + attacks +
                    "\nLeast Attacks after Level Three: " + LeastAttacks;
    }
}
