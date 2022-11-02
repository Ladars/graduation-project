using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    private void Start()
    {
        StartCoroutine(gameTutorial());
    }
    public void turorialConfirm()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    IEnumerator gameTutorial()
    {
        
        yield return new WaitForSeconds(1f);
        tutorialPanel.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}
