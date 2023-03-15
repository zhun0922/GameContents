using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
     public void ManageButton(string Name)
    {
        switch (Name)
        {
            case "SpawnMonster":
                // MonsterFactory.Instance.Spawn(MonsterType.Goblin);
                Debug.Log(MonsterType.Goblin);
                Debug.Log((int)MonsterType.Goblin); //0 Ãâ·Â
                
                break;
        }
    }
}
