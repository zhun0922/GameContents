using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 inputVec;
    float speed;
    public int point;
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        speed = 3;
        point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector3 myVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(myVec + gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            point++;
        }
    }

}
