using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //GameObject gamemanager2D;
    public int vida;
    private void Awake()
    {
       // gamemanager2D = GameObject.FindGameObjectWithTag("GameManager2D");
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemigo" )
        {
           vida -= 1;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if ( collision.gameObject.tag == "Trampa")
        {
           vida -= 1;
        }
    }
}
