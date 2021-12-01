using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Assign a Rigidbody component in the inspector to instantiate

    public Rigidbody projectile;
    bool disparando;
    

    void Update()
    {
        if (disparando==false)
        {
            StartCoroutine("Espera");
        } 
    }

    IEnumerator Espera()
    {
        disparando = true;
        Rigidbody clone;

        clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.gameObject.SetActive(true);

        clone.velocity = transform.TransformDirection(Vector3.left * 10);
        yield return new WaitForSeconds(2f);
        disparando = false;
       
    }
}
