using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    private PlayerStat stat;
    public TextMeshProUGUI lvl_value_ui;
    public TextMeshProUGUI exp_value_ui;

    private int current_lvl;
    private int current_lvl_exp;
    void Start()
    {
        stat = GetComponent<PlayerStat>();
        stat.Exp = stat.playerData.exp;
        CalculateLevel();

    }

    public void CalculateLevel()
    {
        // 5500 -> lv3 + 33%
        // 2000 -> 3500 -> 5000 = 2000 + 1500 * n
        if (stat.Exp > 0)
        {
            current_lvl = (stat.Exp - 2000) / 1500;
            // Tính toán tiến độ lên cấp dưới dạng phần trăm
            float expInCurrentLevel = stat.Exp - current_lvl * 1000;
            float expToNextLevel = 2000 + (1500 * (current_lvl + 1));
            current_lvl_exp = (int)((expInCurrentLevel / expToNextLevel) * 100f);

        }
        else
        {
            current_lvl = 0;
            current_lvl_exp = 0;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Cập nhật UI với thông tin cấp độ và tiến độ lên cấp
        lvl_value_ui.text = "Lv " + current_lvl.ToString();
        exp_value_ui.text = current_lvl_exp.ToString() + "%";
    }
}
