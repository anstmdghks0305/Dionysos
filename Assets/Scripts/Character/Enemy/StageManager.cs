using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Jung
{
    public class StageManager : MonoBehaviour
    {
        public List<int> SpawnList = new List<int>();
        // Start is called before the first frame update
        void Start()
        {
            for (int i =0;i<this.transform.childCount;i++)
            {
                for(int j = 0; j<this.transform.GetChild(i).transform.childCount;j++)
                {
                    SpawnList.AddRange(this.transform.GetChild(i).transform.GetChild(j).GetComponent<EnemySpawn>().enemies.Distinct<int>().ToList<int>());
                    SpawnList = SpawnList.Distinct<int>().ToList<int>();
                    Debug.Log(SpawnList.Count);
                }
            }
            EnemyDataInputer.EnemyDataInput(SpawnList);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}