using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster 
    {
    
    public string Name { get; private set; }
    public float Speed { get; set; }

    public Monster(string Name, float Speed)
    {
        this.Name = Name;
        this.Speed = Speed;
    }
}


