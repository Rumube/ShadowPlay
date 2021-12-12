using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectZone : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject _objectsParent;
    public GameObject[] _objectsToCreate = new GameObject[4];
    bool _isCreating;
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _isCreating = false;
        _anim = _objectsParent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCreation(int objectToCreate)
    {
        _anim.Play("Pawn");
        _isCreating = true;
        setFalseObjects();
        _objectsToCreate[objectToCreate].SetActive(true);
    }

    public void StopCreation()
    {
        _isCreating = false;
        setFalseObjects();
    }

    void setFalseObjects()
    {
        foreach (GameObject currentObject in _objectsToCreate)
        {
            currentObject.SetActive(false);
        }
    }

}
