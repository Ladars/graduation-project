using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward : MonoBehaviour
{
    public int expReward;
    Player player;


    public void gainExp(int exp)
    {
        player.exp = exp;
    }
}
