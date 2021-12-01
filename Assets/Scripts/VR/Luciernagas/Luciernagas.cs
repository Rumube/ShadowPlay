using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luciernagas : MonoBehaviour
{
    Vector3 newposition;
    Vector3 direccion;
    public Vector3 target;
    public bool Gototarget;
    //Vector3 direccionNormalizada;
    public float velocidad;
    bool moviendose=true;
    

   // public float wanderRadius = 5f; //Radio en el que se mueve el murciélago
    public Rigidbody luciernaga;
   
    //void Girar()
    //{
    //    direccionNormalizada = direccion.normalized;

    //    transform.position += direccionNormalizada * velocidad * Time.deltaTime;

    //}
    private IEnumerator coroutine;

    void Start()
    {
        //CambiarDireccion();
        // - After 0 seconds, prints "Starting 0.0 seconds"
        // - After 0 seconds, prints "Coroutine started"
        // - After 2 seconds, prints "Coroutine ended: 2.0 seconds"
       
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        //Volar();
        if (Input.GetKey(KeyCode.Space))
        {
            float distance = Vector3.Distance(transform.position, target);
            transform.position = Vector3.Lerp(transform.position, target, velocidad);
            //luciernaga.velocity = transform.TransformDirection(velocidad * (transform.position - target));
            //transform.position += (transform.position - target).normalized * velocidad * Time.deltaTime;
            //Gototarget = true;
           
        }
        else
        {
            if (moviendose == true)
            {
                direccion = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

                luciernaga.velocity = transform.TransformDirection(velocidad * direccion);
                luciernaga.angularVelocity = transform.TransformDirection(velocidad * direccion);


                coroutine = Movimiento(Random.Range(2f, 5f));

                StartCoroutine(coroutine);
            }
        }
        ////luciernaga.
        //if (Gototarget == true)
        //{
        //    //luciernaga.velocity = transform.TransformDirection(velocidad * (transform.position - target));

        //    //transform.position += (transform.position - target).normalized * velocidad ;
        //   // Debug.Log("semueve");
        //    //luciernaga.velocity = transform.TransformDirection(velocidad * target);
        //}



    }
    //void CambiarDireccion()//Asigna una posicion nueva dentro de un circulo
    //{
    //    newposition = transform.position + Random.insideUnitSphere * wanderRadius;

    //    newposition.z = 0;
    //}
    void Volar()
    {
        //Debug.Log (newposition);
        //direccion = newposition - transform.position;

        //if (direccion.magnitude < 0.25)//ha llegado a la posicion
        //{
        //    CambiarDireccion();
        //}
        //transform.LookAt(newposition);
        //transform.position += transform.forward + direccion.normalized * velocidad;

    }
    private IEnumerator Movimiento(float waitTime)
    {
        moviendose = false;
        yield return new WaitForSeconds(waitTime);
        moviendose = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Luciernaga")
        {
            Debug.Log("collision");
            luciernaga.velocity = transform.TransformDirection(velocidad * direccion*-1);
            luciernaga.angularVelocity = transform.TransformDirection(velocidad * direccion*-1); 
            coroutine = Movimiento(Random.Range(2f, 4f));

            StartCoroutine(coroutine);
        }
    }

}
