using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;

    public static GameManager Instance { get { return _Instance; } }

    public InputDevice _lefDevice { private set; get; }
    public InputDevice _rigDevice { private set; get; }
    public GameObject _cubo;

    public bool _Test2D;

    [Header("Controles")]
    public Vector3 _rotationOffset;
    public Vector3 _positionOffset;

    private void Awake()
    {
        if(_Instance != null &&  _Instance != this)
        {
            Destroy(this.gameObject);
        }
        else{
            _Instance = this;
        }
    }

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

            _lefDevice = leftController[0];
            _rigDevice = rigthController[0];
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
