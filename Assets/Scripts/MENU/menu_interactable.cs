using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_interactable : MonoBehaviour
{

    public GameObject[] designers;
    private GameObject myprefab;
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()


    {
        pos = new Vector3(-9, 5, 0);
        StartCoroutine(temp());
       
       
    }


    IEnumerator temp()
    {
        

        for (int i = 0; i < 7; i++)
        {
            myprefab = designers[i];
            Debug.Log("Entro");
            Instantiate(myprefab, pos, Quaternion.identity);
            pos.x = pos.x + 3f;
            Debug.Log("Creado");

            yield return new WaitForSeconds(1f);
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
