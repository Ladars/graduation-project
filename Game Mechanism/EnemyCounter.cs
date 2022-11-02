using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    public TMP_Text enemyLeft;
    private string enemyTotal;
    public GameObject SuccessUI;
    private bool isSuccess;
    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyTotal = enemies.Length.ToString();
    }
    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyLeft.text = "µ–»À£∫" + enemies.Length.ToString()+"/"+enemyTotal;
        gameWin();
    }
    private void gameWin()
    {
        if (enemies.Length <= 0)
        {
            Cursor.visible = true;
            if (!isSuccess)
            {
                SuccessUI.SetActive(true);
                Time.timeScale = 0;
                isSuccess = true;
            }
        }
    }
}
