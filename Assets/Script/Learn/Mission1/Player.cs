using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Vector3 inputVec;
    public float Speed { get; private set; }

    private int point; 
    public int Point
    {
        get { return point; }
        set
        {
            if(value < 0 ) point = 0;

            else point = value;
        }
    }
    public int CoinNum { get; private set; }

    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Speed = 5;
        Point = 0;
        CoinNum= 8;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        if(CoinNum <= 0)
        {
            CanvasManager.Instance.gameOver.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        Vector3 myVec = inputVec.normalized * Speed * Time.fixedDeltaTime;
        rigid.MovePosition(myVec + gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        //ParticleSystem particle;
        if (other.CompareTag("Sphere"))
        {
            Point++;
            CoinNum--;
            Destroy(other.gameObject);
           // particle = other.gameObject.GetComponentInChildren<ParticleSystem>();
           // particle.Play();
            CanvasManager.Instance.UpdateText(Point.ToString());
        }
        if (other.CompareTag("Wall"))
        {
            Point--;
            gameObject.transform.position = new Vector3(0, 0.5f, 0);
            CanvasManager.Instance.UpdateText(Point.ToString());
        }
    }

}
