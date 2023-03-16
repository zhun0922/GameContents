using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster 
    {

    public Monster(string name, float speed)
    {
        this.Name = name;
        this.Speed = speed;
    }

    public string Name { get; private set; }
    public float Speed { get; set; }

   
}


