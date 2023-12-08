using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMp : MonoBehaviour
{
    [SerializeField] private Slider manabar;
    private PlayerStat stat;
    [SerializeField] private TextMeshProUGUI mana_value_ui;
    void Start()
    {
        stat = GetComponent<PlayerStat>();
        manabar.maxValue = stat.playerData.mana;
        stat.Mana = stat.playerData.mana;
    }

    // Update is called once per frame
    void Update()
    {
        mana_value_ui.text = stat.Mana == stat.playerData.mana ? stat.Mana + "/" + manabar.maxValue : stat.Mana.ToString("F1") + "/" + manabar.maxValue;
        manabar.value = stat.Mana;
        if(stat.Mana < stat.playerData.mana)
        {
            stat.Mana += Time.deltaTime * 5;
        }
        else
        {
            stat.Mana = stat.playerData.mana;
        }

    }
}
