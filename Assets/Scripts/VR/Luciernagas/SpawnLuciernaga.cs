using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLuciernaga : MonoBehaviour
{
    public Rigidbody luciernaga;
    bool disparando;
    public int numluciernagas;
    public List<GameObject> luciernagas;
    private void Start()
    {
        StartCoroutine("Espera");
    }
    void Update()
    {
        if (luciernagas.Count>0&& disparando == false && numluciernagas != luciernagas.Count)
        {
            int counter = 0;
            for (int i = 0; i < luciernagas.Count; i++)
            {
                if ( Mathf.Abs(luciernagas[i].transform.position.x - transform.position.x) > 2 || Mathf.Abs(luciernagas[i].transform.position.y - transform.position.y) > 2)
                {
                    counter += 1;
                    if (counter== luciernagas.Count)
                    {
                        StartCoroutine("Espera");
                    }
                   
                }
            }
        }
        
    }

    IEnumerator Espera()
    {
        disparando = true;
        Rigidbody clone;

        clone = Instantiate(luciernaga, transform.position, transform.rotation);
        clone.gameObject.SetActive(true);

        luciernagas.Add(clone.gameObject);

        
        yield return new WaitForSeconds(1f);
        disparando = false;

    }
}
