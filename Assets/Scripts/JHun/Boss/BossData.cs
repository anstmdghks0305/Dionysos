using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

namespace Boss
{
    public class BossData : Singleton<BossData>
    {
        public List<Dictionary<string, object>> BossDataCsv;
        // Start is called before the first frame update
        public void Read()
        {
            BossDataCsv = CSVReader.Read("BossData");
            for (int index = 0; index < BossDataCsv.Count; index++)
            {
                string name = BossDataCsv[index]["Name"].ToString();
                int hp = Convert.ToInt32(BossDataCsv[index]["Hp"]);
                float speed = Convert.ToSingle(BossDataCsv[index]["Speed"]);
                int damage = Convert.ToInt32(BossDataCsv[index]["Damage"]);
                float attackSpeed = Convert.ToSingle(BossDataCsv[index]["AttackSpeed"]);
                float attackRange = Convert.ToSingle(BossDataCsv[index]["AttackRange"]);

                DataManager<BossStatus>._BossStatus.Add(name, new BossStatus(name, hp, speed, damage, attackSpeed, attackRange));
            }
        }
    }
}
