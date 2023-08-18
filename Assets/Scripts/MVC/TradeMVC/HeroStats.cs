using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    [SerializeField] private TMP_Text _attack;
    [SerializeField] private TMP_Text _defence;
    [SerializeField] private TMP_Text _power;
    [SerializeField] private TMP_Text _knowledge;

    public void SetStatsForHero(int attack,  int defence, int power, int knowledge)
    {
        Debug.Log("TradeWork");
        _attack.text = attack.ToString();
        _defence.text = defence.ToString();
        _power.text = power.ToString();
        _knowledge.text = knowledge.ToString();
    }
}