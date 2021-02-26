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

    public bool isGame = true;
    private bool updated = false;
    
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

    private List<int> leastAttacks;
    
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
        if (isGame == true)
        {
            text.text = "Level: " + currentLevel +
                        "\nAttacks: " + attacks;
        }
        else
        {
            
            // the UpdateLeastScores should be called only once
            // I spent quite a lot of time to figure out why multiple least attacks were added to the file even though I used "break"
            // till I found out that the UpdateLeastScores function is placed in the Update, and is called each frame...
            if (!updated)
            {
                UpdateLeastScores();
                updated = true;
            }

            string leastAttackString = "Least Attacks\n\n";

            for (var i = 0; i < leastAttacks.Count; i++)
            {
                leastAttackString += leastAttacks[i] + "\n";
            }
            
            text.text = leastAttackString;
        }
    }

    void UpdateLeastScores()
    {
        if (leastAttacks == null)
        {
            leastAttacks = new List<int>();

            string fileContents = File.ReadAllText(FILE_PATH_LEAST_ATTACKS);

            string[] fileScores = fileContents.Split(',');

            for (var i = 0; i < fileScores.Length - 1; i++)
            {
                leastAttacks.Add(Int32.Parse(fileScores[i]));
            }
        }

        for (var i = 0; i < leastAttacks.Count; i++)
        {
            // the least attacks scores are updated
            
            if (attacks < leastAttacks[i])
            {
                leastAttacks.Insert(i, attacks);
                
                break;
            }
        }

        string saveLeastAttacksString = "";

        for (int i = 0; i < leastAttacks.Count; i++)
        {
            saveLeastAttacksString += leastAttacks[i] + ",";
        }
        
        File.WriteAllText(FILE_PATH_LEAST_ATTACKS, saveLeastAttacksString);
    }
}
