using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private void Start()
    {
        GameObject obj = GameObject.Find("Sphere");
        if (!obj)
        {
            Debug.Log("객체를 찾을수 없습니다");
        }
        //Debug.Log(obj.transform.position.x);
        Debug.Log(obj.GetComponent<Sphere>().WhoAmI("JIHUN"));
        

        GameObject obj2 = GameObject.FindWithTag("Sphere");
        if (!obj2)
        {
            Debug.Log("객체를 찾을수 없습니다");
        }
       // Debug.Log(obj.transform.position.x);

        
    }
}
/*GetComponent<>() 함수를 사용하여 해당 오브젝트에 연결된 함수를 실행할 때,
public 이 아닌 private로 선언된 함수를 실행하면, 어떤 결과가 나오는지 확인
하세요*/

//: Assets\Script\Learn\Cube.cs(15, 36): error CS0122: 'Sphere.WhoAmI(string)' is inaccessible due to its protection level 오류 

/*GetComponent<>() 함수를 사용하여 해당 오브젝트에 연결된 함수를 실행하는 방
법과 SendMessage() 함수를 사용하여 해당 오브젝트에 연결된 함수를 실행하는
방법의 차이를 확인하세요.*/

//외부스크립트 함수 호출방법 1: GetComponent , 2: SendMessage
//매개변수가 2개 이상이면 SendMessage를 사용하지 못함
