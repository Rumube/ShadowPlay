using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Assign a Rigidbody component in the inspector to instantiate

    public GameObject projectile;
    bool disparando;
    public float vel;
    public Animator animator;
    

    void Update()
    {
        if (disparando==false)
        {
            animator.Play("InicioAtaque");
            StartCoroutine("Espera");

        } 
    }

    IEnumerator Espera()
    {
        disparando = true;
        GameObject clone;

        clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.SetActive(true);

        clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.left * vel*Time.deltaTime);
        yield return new WaitForSeconds(3f);
        disparando = false;
       
    }
}
