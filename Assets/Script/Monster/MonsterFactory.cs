using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    public static MonsterFactory Instance { get; private set; }

    public  MonsterPrefab monsterPrefab;

    public GameObject SpawnPoint;

/*    public MonsterDB[] monsterDB;*/

    private void Start()
    {
        Instance = this;
    }
    public void Spawn(MonsterType monsterType)
    {
        MonsterPrefab mp = Instantiate(monsterPrefab, SpawnPoint.transform).GetComponent<MonsterPrefab>();
        switch (monsterType)
        {
            case MonsterType.Goblin:
                //mp.Init(
                break;
        }
        
    
    }   
}

[System.Serializable]
/*public class MonsterDB
{
    public string Name;
    public float Speed;
    public float Hp;
}*/

public enum MonsterType
{
    Goblin,
    MushRoom
}
