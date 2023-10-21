using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

namespace Boss
{
    public class BossData : Singleton<BossData>
    {
        public Dictionary<string, BossStatus> Status = new Dictionary<string, BossStatus>();
        public List<Dictionary<string, object>> BossDataCsv;
        // Start is called before the first frame update
        public void Read()
        {
            BossDataCsv = CSVReader.Read("BossData");
            for (int index = 0; index < BossDataCsv.Count; index++)
            {
                string name = BossDataCsv[index]["name"].ToString();
                int hp = Convert.ToInt32(BossDataCsv[index]["hp"]);
                int speed = Convert.ToInt32(BossDataCsv[index]["speed"]);
                int damage = Convert.ToInt32(BossDataCsv[index]["damage"]);
                
                Status.Add(name, new BossStatus(name, hp, speed, damage));
            }
        }
    }
}
