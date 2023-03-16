using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    private void Start()
    {
        GameObject obj = GameObject.Find("Snowman");

        //첫번째 자식 오브젝트 찾기
        Transform getChild = obj.transform.GetChild(0);
        //Debug.Log(getChild.name);

        //자신을 포함한(index = 0) 자식오브젝트 모두 찾기 (1~ allChildren.Length-1)
        Transform[] allChildren = obj.GetComponentsInChildren<Transform>();
        if (allChildren == null) return;

        //INDEX 1부터 FOR문돌려서 자기 자신 배제
        for (int i = 1; i < allChildren.Length; i++)
        {
            Debug.Log(allChildren[i].name);
        }

        //name이 자신일때 continue 자기 자신 배제
        foreach (Transform t in allChildren)
        {
            if (t.name == "Snowman")
            {
                continue;
            }
            Debug.Log(t.name);
        }
    }
}
