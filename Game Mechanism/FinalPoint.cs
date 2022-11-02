using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPoint : MonoBehaviour
{
    private SceneTransition sceneTransition;
    public GameObject successUI;

    private void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneTransition.FinalPoint();
            //successUI.SetActive(true);
            //Time.timeScale = 0;
        }
    }
}
