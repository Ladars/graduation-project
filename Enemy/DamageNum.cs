using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNum : MonoBehaviour
{
    public TMP_Text damageText;
    public float lifeTimer;
    public float upSpeed;

    private void Start()
    {
        Destroy(gameObject, lifeTimer);
    }

    private void Update()
    {
        transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0); //Text move up in lifeTimer
    }

    public void showUIDamage(float _amount)
    {
        damageText.text = _amount.ToString();
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

}
