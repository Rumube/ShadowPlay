using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    private InputDevice lefDevice;
    private InputDevice rigDevice;
    [SerializeField]
    private GameObject cubo;

    public bool _Test2D;
    // Start is called before the first frame update
    void Start()
    {
        if (!_Test2D)
        {
            List<InputDevice> leftController = new List<InputDevice>();
            List<InputDevice> rigthController = new List<InputDevice>();
            List<InputDevice> devices = new List<InputDevice>();

            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftController);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rigthController);
            InputDevices.GetDevices(devices);

            foreach (InputDevice device in devices)
            {
                Debug.Log(device.name + device.characteristics);
            }

            lefDevice = leftController[0];
            rigDevice = rigthController[0];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!_Test2D)
        {
            //ROTACION CONTROLLER
            if (lefDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion leftRotation))
            {
                //print("Rotacion: " + leftRotation.eulerAngles);
            }
            if (rigDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rightRotation))
            {
                //print("Rotacion: " + rightRotation.eulerAngles);
            }
            //POSICION CONTROLLER
            if (lefDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 leftPosition))
            {
                print("L--> " + leftPosition);
            }
            if (rigDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 rightPositionS))
            {
                print("R--> " + rightPositionS);
            }




            //PRIMARY BUTTOM
            if (lefDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryLeftButtomValue) && primaryLeftButtomValue)
            {
            }
            if (rigDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryRightButtomValue) && primaryRightButtomValue)
            {
            }

            //TRIGGER
            if (lefDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerLeftValue) && triggerLeftValue > 0.1f)
            {
            }
            if (rigDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerRightValue) && triggerRightValue > 0.1f)
            {
            }

            //TOUCHPAD
            if (lefDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primaryLeft2DAxisValue) && primaryLeft2DAxisValue != Vector2.zero)
            {
                cubo.transform.RotateAround(cubo.transform.position, -cubo.transform.up, Time.deltaTime * 90f * primaryLeft2DAxisValue.x);
            }
            if (rigDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primaryRight2DAxisValue) && primaryRight2DAxisValue != Vector2.zero)
            {
            }
        }
    }
}
