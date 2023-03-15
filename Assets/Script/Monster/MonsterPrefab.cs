using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPrefab : MonoBehaviour
{

    public string Name { get; private set; }
    public float Speed { get; set; }

    public void Init(string Name, float Speed)
    {
        this.Name = Name;
        this.Speed = Speed;
    }
}
