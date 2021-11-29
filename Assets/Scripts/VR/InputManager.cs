using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{

    bool _leftTrigger;
    bool _rightTrigger;
    Vector3 _leftRotation;
    Vector3 _rightRotation;
    Vector3 _leftPosition;
    Vector3 _rightPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputDetection();
        VRActions();
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


    //7.4 13.5 264.4 D
    //11.5 328.8 87.9 I
    void VRActions()
    {
        if (_leftTrigger && _rightTrigger && _rightRotation.z >=240 && _rightRotation.z <= 280  && _leftRotation.z>= 50 && _leftRotation.z <= 110)
        {
            print("ESTOY ENTRANDO!!");
        }
        /*
        if(_leftTrigger && _rightTrigger && VRActionsOffset(_rightRotation, GameManager.Instance._rotationOffset) && VRActionsOffset(_leftRotation, GameManager.Instance._rotationOffset))
        {
            print("ESTOY ENTRANDO!!");
        }
        */
    }

    bool VRActionsOffset(Vector3 value, Vector3 offset)
    {
        Vector3 auxVector = value;
        if(value.x + offset.x > 360)
        {
            auxVector.x =  value.x - 360;
        }
        if(value.y + offset.y> 360)
        {
            auxVector.y = value.y - 360;
        }
        if (value.z + offset.z > 360)
        {
            auxVector.z = value.z - 360;
        }

        if (value.x - offset.x < 0)
        {
            auxVector.x = value.x + 360;
        }
        if (value.y - offset.y < 0)
        {
            auxVector.y = value.y + 360;
        }
        if (value.z - offset.z < 0)
        {
            auxVector.z = value.z + 360;
        }

        print("Value => " + auxVector);
        print("Mas => " + (auxVector + offset));
        print("Menos => " + (auxVector - offset));

        return auxVector.x <= auxVector.x + offset.x && auxVector.y <= auxVector.y + offset.y && auxVector.z <= auxVector.z + offset.z &&
            auxVector.x >= auxVector.x - offset.x && auxVector.y >= auxVector.y - offset.y && auxVector.z >= auxVector.z - offset.z;
    }

    void createCloud()
    {
        print("Crear nube");
    }

    void createBook()
    {
        print("Crear libro");
    }

    void createCube()
    {
        print("Crear cubo");
    }

    void createSpring()
    {
        print("Crear muelle");
    }
}
