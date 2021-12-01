using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLuciernagsa : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody luciernaga;
    bool disparando;
    

    void Update()
    {
        if (disparando == false)
        {
            StartCoroutine("Espera");
        }
    }

    IEnumerator Espera()
    {
        disparando = true;
        Rigidbody clone;

        clone = Instantiate(luciernaga, transform.position, transform.rotation);
        clone.gameObject.SetActive(true);

       
        yield return new WaitForSeconds(3f);
        disparando = false;

    }
}
