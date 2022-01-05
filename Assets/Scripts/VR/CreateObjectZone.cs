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

    AudioSource _audioS;
    public AudioClip _creandoSound;
    public AudioClip _crearSound;

    // Start is called before the first frame update
    void Start()
    {
        _isCreating = false;
        _anim = _objectsParent.GetComponent<Animator>();
        _audioS = GetComponent<AudioSource>();
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
        _audioS.clip = _creandoSound;
        if (!_audioS.isPlaying)
        {
            _audioS.Play();
        }
    }

    public void StopCreation()
    {
        _isCreating = false;
        setFalseObjects();
        _audioS.Stop();
        print("Apagarse");
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
        _audioS.clip = _crearSound;
        _audioS.Play();
    }

    public void createBook()
    {
        print("Crear libro");
        newGO = Instantiate(_objectsToCreate[2], transform);
        StopCreation();
        _isObjectCreated = true;
        _audioS.clip = _crearSound;
        _audioS.Play();
        newGO.SetActive(true);
    }

    public void createCube()
    {
        print("Crear cubo");
        newGO = Instantiate(_objectsToCreate[1], transform);
        StopCreation();
        _isObjectCreated = true;
        _audioS.clip = _crearSound;
        _audioS.Play();
        newGO.SetActive(true);
    }

    public void createSpring()
    {
        print("Crear muelle");
        newGO = Instantiate(_objectsToCreate[3], transform);
        StopCreation();
        _isObjectCreated = true;
        _audioS.clip = _crearSound;
        _audioS.Play();
        newGO.SetActive(true);
    }

    #endregion



}
