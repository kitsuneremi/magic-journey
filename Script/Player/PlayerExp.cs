using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [SerializeField] private PlayerStat stat;
    public TextMeshProUGUI lvl_value_ui;
    public TextMeshProUGUI exp_value_ui;
    [SerializeField] private int exp_require_lvl_1;
    [SerializeField] private int exp_require_lvl_2;
    [SerializeField] private int exp_require_lvl_3;
    [SerializeField] private int exp_require_lvl_4;
    [SerializeField] private int exp_require_lvl_5;

    private int current_lvl;
    private int current_lvl_exp;
    void Start()
    {
        stat.Exp = stat.playerData.exp;
        CalculateLevel();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateLevel();
        UpdateUI();
    }

    private void CalculateLevel()
    {
        // Tính toán cấp độ dựa trên mức kinh nghiệm
        if (stat.Exp >= exp_require_lvl_5)
        {
            current_lvl = 5;
        }
        else if (stat.Exp >= exp_require_lvl_4)
        {
            current_lvl = 4;
        }
        else if (stat.Exp >= exp_require_lvl_3)
        {
            current_lvl = 3;
        }
        else if (stat.Exp >= exp_require_lvl_2)
        {
            current_lvl = 2;
        }
        else if (stat.Exp >= exp_require_lvl_1)
        {
            current_lvl = 1;
        }
        else
        {
            current_lvl = 0;
        }

        // Tính toán tiến độ lên cấp dưới dạng phần trăm
        float expInCurrentLevel = stat.Exp - GetExpRequiredForLevel(current_lvl);
        float expToNextLevel = GetExpRequiredForLevel(current_lvl + 1) - GetExpRequiredForLevel(current_lvl);
        current_lvl_exp = (int)((expInCurrentLevel / expToNextLevel) * 100f);
    }

    private void UpdateUI()
    {
        // Cập nhật UI với thông tin cấp độ và tiến độ lên cấp
        lvl_value_ui.text = "Lv " + current_lvl.ToString();
        exp_value_ui.text = current_lvl_exp.ToString() + "%";
    }

    private int GetExpRequiredForLevel(int level)
    {
        // Lấy số kinh nghiệm cần cho cấp độ cụ thể
        switch (level)
        {
            case 1: return exp_require_lvl_1;
            case 2: return exp_require_lvl_2;
            case 3: return exp_require_lvl_3;
            case 4: return exp_require_lvl_4;
            case 5: return exp_require_lvl_5;
            default: return 0;
        }
    }
}
