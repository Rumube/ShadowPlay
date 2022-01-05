using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2DWorld : MonoBehaviour
{
    [Header("References")]
    public GameObject _VRCamera;
    [Header("Conf")]
    public float _velRotation;
    public float _velDist;
    public float _maxDist;
    public float _minDist;
    public float _velHeight;
    public float _maxHeight;
    public float _minHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate2DWorld(float rotationValue)
    {
        _VRCamera.transform.RotateAround(transform.position, _VRCamera.transform.up, Time.deltaTime * _velRotation * rotationValue);
    }

    public void ChangeDist2DWorld(float distanceValue)
    {
        float newZ = Mathf.Clamp(_VRCamera.transform.position.z + distanceValue * Time.deltaTime * _velDist, _minDist, _maxDist);
        float newY = _VRCamera.transform.position.y;
        _VRCamera.transform.position = new Vector3(_VRCamera.transform.position.x, newY, newZ);
    }

    public void ChangeHeight2DWorld(bool heightValue)
    {
        float newZ = _VRCamera.transform.position.z;
        float newY = 0;
        if (heightValue)
        {
           newY = Mathf.Clamp(_VRCamera.transform.position.y + _velHeight * Time.deltaTime * _velHeight, _minHeight, _maxHeight);
        }
        else
        {
            newY = Mathf.Clamp(_VRCamera.transform.position.y - _velHeight * Time.deltaTime * _velHeight, _minHeight, _maxHeight);
        }
        _VRCamera.transform.position = new Vector3(_VRCamera.transform.position.x, newY, newZ);
    }

}
