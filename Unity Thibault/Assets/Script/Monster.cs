using System;
using System.Threading;
using UnityEngine;

namespace Assets.Script
{
    public class Monster : MonoBehaviour
    {
        public GameObject prefabMonster;
        public GameObject[] monsters;
        
        void Start()
        {
            monsters = new GameObject[1];
            GameObject monster = Instantiate(prefabMonster, new Vector2(0, 0), Quaternion.identity);
            monster.transform.parent = transform;
            monster.name = "Sullivan";
            monsters[0] = monster;
        }

        void Update()
        {
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(1000);
                Debug.Log(i);
                //monsters[0].transform.position = new Vector3(i, 0, 0);
            }
        }
    }
}
