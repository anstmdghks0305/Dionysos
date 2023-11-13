using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class PlayerDataRead : MonoBehaviour
{
    public List<Dictionary<string, object>> PlayerDataCsv;
    void Start()
    {
        PlayerDataCsv = CSVReader.Read("PlayerData");
        for (int index = 0; index < PlayerDataCsv.Count; index++)
        {
            int hp = Convert.ToInt32(PlayerDataCsv[index]["hp"]);
            int fever = Convert.ToInt32(PlayerDataCsv[index]["fever"]);
            int speed = Convert.ToInt32(PlayerDataCsv[index]["speed"]);
            float attackSpeed = Convert.ToSingle(PlayerDataCsv[index]["attackSpeed"]);
            float dashDistance = Convert.ToSingle(PlayerDataCsv[index]["dashDistance"]);
            int attackDamage = Convert.ToInt32(PlayerDataCsv[index]["attackDamage"]);
            int powerAttackDamage = Convert.ToInt32(PlayerDataCsv[index]["powerAttackDamage"]);
            int dashDamage = Convert.ToInt32(PlayerDataCsv[index]["dashDamage"]);
            int powerDashDamage = Convert.ToInt32(PlayerDataCsv[index]["powerDashDamage"]);
            float dashCoolTime = Convert.ToSingle(PlayerDataCsv[index]["dashCoolTime"]);
            int slashDamage = Convert.ToInt32(PlayerDataCsv[index]["slashDamage"]);
            int powerSlashDamage = Convert.ToInt32(PlayerDataCsv[index]["powerSlashDamage"]);
            float slashCoolTime = Convert.ToSingle(PlayerDataCsv[index]["slashCoolTime"]);
            int slashCount = Convert.ToInt32(PlayerDataCsv[index]["slashCount"]);
            int fireballDamage = Convert.ToInt32(PlayerDataCsv[index]["fireballDamage"]);
            float fireballCoolTime = Convert.ToSingle(PlayerDataCsv[index]["fireballCoolTime"]);
            int powerFireballDamage = Convert.ToInt32(PlayerDataCsv[index]["powerFireballDamage"]);
            float hurtTime = Convert.ToSingle(PlayerDataCsv[index]["hurtTime"]);
            float feverTime = Convert.ToSingle(PlayerDataCsv[index]["feverTime"]);
            int powerAttackRange = Convert.ToInt32(PlayerDataCsv[index]["powerAttackRange"]);

            GameManager.Instance.playerData = new PlayerData(hp, fever, speed, attackSpeed, dashDistance, attackDamage, powerAttackDamage, dashDamage, powerDashDamage
                , dashCoolTime, slashDamage, powerSlashDamage, slashCoolTime, slashCount, fireballDamage, powerFireballDamage, fireballCoolTime, hurtTime, feverTime, powerAttackRange);
        }
    }
}
