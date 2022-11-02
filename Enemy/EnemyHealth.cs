using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth :EnemyAI
{
    public Image hpImage;//"Red" Health bar Image
    public Image hpEffectImage;//"White Effect" Health bar Image
    [SerializeField] private float hurtSpeed = 0.005f;
    private protected override void Update()
    {
        base.Update();
        hpImage.fillAmount = health/maxHealth;  //������ѪЧ��  ��ɫ��Ѫ����ʾ��ǰ����ֵ���Ե�ǰ����ֵ���������ֵ�ı�����ʾ

        if (hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
}
