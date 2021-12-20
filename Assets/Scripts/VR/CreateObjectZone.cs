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
    public bool _isObjectCreated;
    GameObject newGO;
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

    public void DestroyObject()
    {
        if(newGO != null)
        {
            _isObjectCreated = false;
            Destroy(newGO);
            StopCreation();
        }
    }

    #region InstantiateObjects

    public void createCloud()
    {
        print("Crear nube");
        newGO = Instantiate(_objectsToCreate[0], transform);
        StopCreation();
        _isObjectCreated = true;
        newGO.SetActive(true);
    }

    public void createBook()
    {
        print("Crear libro");
        newGO = Instantiate(_objectsToCreate[2], transform);
        StopCreation();
        _isObjectCreated = true;
        newGO.SetActive(true);
    }

    public void createCube()
    {
        print("Crear cubo");
        newGO = Instantiate(_objectsToCreate[1], transform);
        StopCreation();
        _isObjectCreated = true;
        newGO.SetActive(true);
    }

    public void createSpring()
    {
        print("Crear muelle");
        newGO = Instantiate(_objectsToCreate[3], transform);
        StopCreation();
        _isObjectCreated = true;
        newGO.SetActive(true);
    }

    #endregion



}
