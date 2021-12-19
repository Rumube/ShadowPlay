using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{

    public Light _prueba;
    [Header("Conf")]
    public float _hitZoneDistance;
    GameObject[] _createZoneList;
    bool _isCreatingZone;
    bool _leftTrigger;
    bool _rightTrigger;
    Vector3 _leftRotation;
    Vector3 _rightRotation;
    Vector3 _leftPosition;
    Vector3 _rightPosition;
    Vector3 _headPosition;

    //ACTIONS CONTROLLER
    bool _isCreateCloud = false;
    bool _isCreateBook = false;
    bool _isCreateCube = false;
    bool _isCreateSpring = false;

    public GameObject _visorHelp;
    //Zona de creaci�n en la que nos encontramos
    GameObject _currentCreateZone;

    // Start is called before the first frame update
    void Start()
    {
        _createZoneList = GameObject.FindGameObjectsWithTag("CreateZone");
        _currentCreateZone = _createZoneList[0];
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if(_leftTrigger && _rightTrigger)
        {
            _visorHelp.SetActive(true);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.gameObject.transform.TransformDirection(Vector3.forward- new Vector3(0, 0.35f, 0)), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.gameObject.transform.TransformDirection(Vector3.forward - new Vector3(0, 0.35f, 0)) * hit.distance, Color.yellow);
                _visorHelp.transform.position = hit.point;
                _isCreatingZone = false;
                float distanceMin = _hitZoneDistance;
                foreach (GameObject currentGO in _createZoneList)
                {
                    float currentDistance = Vector3.Distance(hit.point, currentGO.transform.position);
                    if (currentDistance <= _hitZoneDistance && currentDistance <= distanceMin)
                    {
                        distanceMin = currentDistance;
                        _isCreatingZone = true;
                        //currentGO.transform.GetChild(0).gameObject.SetActive(true);
                        _currentCreateZone = currentGO;
                    }
                    else
                    {
                        DesactiveCreationZone(currentGO);
                    }
                }
                foreach(GameObject currentGO in _createZoneList)
                {
                    if(_currentCreateZone != currentGO)
                        DesactiveCreationZone(currentGO);
                }
                _currentCreateZone.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                if (_currentCreateZone.activeSelf)
                    _currentCreateZone.GetComponent<CreateObjectZone>().StopCreation();

                _isCreatingZone = false;
            }
        }
        else
        {
            if (_currentCreateZone.activeSelf)
                _currentCreateZone.GetComponent<CreateObjectZone>().StopCreation();

            _isCreatingZone = false;
            _visorHelp.SetActive(false);
            foreach (GameObject currentGO in _createZoneList)
            {
                currentGO.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

    }

    private void DesactiveCreationZone(GameObject currentGO)
    {
        if (_currentCreateZone.activeSelf)
            _currentCreateZone.GetComponent<CreateObjectZone>().StopCreation();
        currentGO.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        InputDetection();
        VRActions();

        if(_isCreateBook || _isCreateCloud || _isCreateCube || _isCreateSpring)
        {
            _prueba.gameObject.SetActive(true);
        }
        else
        {
            _prueba.gameObject.SetActive(false);
        }

    }

    void InputDetection()
    {
        if (!GameManager.Instance._Test2D)
        {
            //ROTACION CONTROLLER
            if (GameManager.Instance._lefDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion leftRotation))
                _leftRotation = leftRotation.eulerAngles;

            if (GameManager.Instance._rigDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rightRotation))
                _rightRotation = rightRotation.eulerAngles;

            //POSICION CONTROLLER
            if (GameManager.Instance._lefDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 leftPosition))
                _leftPosition = leftPosition;

            if (GameManager.Instance._rigDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 rightPosition))
                _rightPosition = rightPosition;

            //POSICION HEAD
            if (GameManager.Instance._headDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 headPosition))
                _headPosition = headPosition;


            //PRIMARY BUTTOM
            if (GameManager.Instance._lefDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryLeftButtomValue) && primaryLeftButtomValue)
            {
            }
            if (GameManager.Instance._rigDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryRightButtomValue) && primaryRightButtomValue)
            {
            }

            //TRIGGER
            if (GameManager.Instance._lefDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerLeftValue) && triggerLeftValue > 0.1f)
                _leftTrigger = true;
            else
                _leftTrigger = false;

            if (GameManager.Instance._rigDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerRightValue) && triggerRightValue > 0.1f)
                _rightTrigger = true;
            else
                _rightTrigger = false;

            //TOUCHPAD
            if (GameManager.Instance._lefDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primaryLeft2DAxisValue) && primaryLeft2DAxisValue != Vector2.zero)
            {
                GameManager.Instance._cubo.transform.RotateAround(GameManager.Instance._cubo.transform.position, -GameManager.Instance._cubo.transform.up, Time.deltaTime * 90f * primaryLeft2DAxisValue.x);
            }
            if (GameManager.Instance._rigDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primaryRight2DAxisValue) && primaryRight2DAxisValue != Vector2.zero)
            {
            }
        }
    }

    /*
     Muelle:
        I.R 324.6 357.9 4.2
        D.R 322.1 2.7 342.4

        D.P 0 0.9 1
        I.P 0 1.1 0.1

     Muro
        I.R 288 78.5 213.4
        D.R 300.2 243.5 192.4
    Caja
        D.I.R 337 347.2 14.5

        I.P -0.1 1 0.1
        D.P 0.2 1 0.2
     */

    void VRActions()
    {
        //CREAR OBJETOS
        if (_isCreatingZone)
        {
            //CLOUD INPUT
            if (_leftTrigger && _rightTrigger && _rightRotation.z >= 190 && _rightRotation.z <= 310 && _leftRotation.z >= 20 && _leftRotation.z <= 140)
            {
                _isCreateCloud = true;
                //MOSTRAR ELEMENTO
                _currentCreateZone.GetComponent<CreateObjectZone>().InitCreation(0);
            }
            else
            {
                if (_isCreateCloud && _rightRotation.z >= 190 && _rightRotation.z <= 310 && _leftRotation.z >= 20 && _leftRotation.z <= 140)
                {
                    createCloud();
                }
                _isCreateCloud = false;
            }

            //BOOK INPUT
            if (_leftTrigger && _rightTrigger && _rightRotation.z >= 20 && _rightRotation.z <= 120 && _leftRotation.z >= 240 && _leftRotation.z <= 340)
            {
                _isCreateBook = true;
                //MOSTRAR ELEMENTO
                _currentCreateZone.GetComponent<CreateObjectZone>().InitCreation(2);
            }
            else
            {
                if (_isCreateBook && _rightRotation.z >= 20 && _rightRotation.z <= 120 && _leftRotation.z >= 240 && _leftRotation.z <= 340)
                {
                    createBook();
                }
                _isCreateBook = false;
            }
            float dist = Vector3.Distance(_rightPosition, _leftPosition);

            //CUBE INPUT
            if (_leftTrigger && _rightTrigger && _rightTrigger && _rightRotation.z <= 360 && _rightRotation.z >= 340 && _leftRotation.z <= 30 && _leftRotation.z >= 0 &&
                dist > 0.3f && dist <= 0.7f)
            {
                _isCreateCube = true;
                //MOSTRAR ELEMENTO
                _currentCreateZone.GetComponent<CreateObjectZone>().InitCreation(1);
            }
            else
            {
                if (_isCreateCube && _rightRotation.z <= 360 && _rightRotation.z >= 340 && _leftRotation.z <= 30 && _leftRotation.z >= 0 &&
                   dist > 0.3f && dist <= 0.7f)
                {
                    createCube();
                }
                _isCreateCube = false;
            }
            //SpringInput
            if (_leftTrigger && _rightTrigger && _rightRotation.z <= 360 && _rightRotation.z >= 340 && _leftRotation.z <= 30 && _leftRotation.z >= 0 &&
                dist >= 0f && dist <= 0.2f && Mathf.Abs(Mathf.Abs(_leftPosition.y) - Mathf.Abs(_rightPosition.y)) >= 0.15f && Mathf.Abs(Mathf.Abs(_leftPosition.y) - Mathf.Abs(_rightPosition.y)) <= 0.25f)
            {
                _isCreateSpring = true;
                //MOSTRAR ELEMENTO
                _currentCreateZone.GetComponent<CreateObjectZone>().InitCreation(3);
            }
            else
            {
                if (_isCreateSpring && _rightRotation.z <= 360 && _rightRotation.z >= 340 && _leftRotation.z <= 30 && _leftRotation.z >= 0 &&
                dist >= 0f && dist <= 0.2f)
                {
                    createSpring();
                }
                _isCreateSpring = false;
            }
        }

    }
    void createCloud()
    {
        print("Crear nube");
        GameObject newGO = Instantiate(_currentCreateZone.GetComponent<CreateObjectZone>()._objectsToCreate[0], _currentCreateZone.transform);
        _currentCreateZone.GetComponent<CreateObjectZone>().StopCreation();
        _currentCreateZone.GetComponent<CreateObjectZone>()._isObjectCreated = true;
        newGO.SetActive(true);
    }

    void createBook()
    {
        print("Crear libro");
        GameObject newGO = Instantiate(_currentCreateZone.GetComponent<CreateObjectZone>()._objectsToCreate[2], _currentCreateZone.transform);
        _currentCreateZone.GetComponent<CreateObjectZone>().StopCreation();
        _currentCreateZone.GetComponent<CreateObjectZone>()._isObjectCreated = true;
        newGO.SetActive(true);
    }

    void createCube()
    {
        print("Crear cubo");
        GameObject newGO = Instantiate(_currentCreateZone.GetComponent<CreateObjectZone>()._objectsToCreate[1], _currentCreateZone.transform);
        _currentCreateZone.GetComponent<CreateObjectZone>().StopCreation();
        _currentCreateZone.GetComponent<CreateObjectZone>()._isObjectCreated = true;
        newGO.SetActive(true);
    }

    void createSpring()
    {
        print("Crear muelle");
        GameObject newGO = Instantiate(_currentCreateZone.GetComponent<CreateObjectZone>()._objectsToCreate[3], _currentCreateZone.transform);
        _currentCreateZone.GetComponent<CreateObjectZone>().StopCreation();
        _currentCreateZone.GetComponent<CreateObjectZone>()._isObjectCreated = true;
        newGO.SetActive(true);
    }
}
