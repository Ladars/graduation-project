using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int exp;
    public int itemAmout;

    public List<Quest> questList = new List<Quest>();
    //public Dictionary<string, Quest> questDict = new Dictionary<string, Quest>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void gainExp()
    {
       
    }

}
