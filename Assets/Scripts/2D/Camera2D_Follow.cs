using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Camera2D_Follow : MonoBehaviour
{
    public GameObject _player;
    public float _offsetX;
    public float _offsetY;
    private Vector3 lastPosition;
    public float _smoth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 currentPosition = new Vector3(_player.transform.position.x + _offsetX, _player.transform.position.y + _offsetY, 0);

        float newX = Mathf.Lerp(lastPosition.x, currentPosition.x, _smoth);
        float newY = Mathf.Lerp(lastPosition.y, currentPosition.y, _smoth);

        transform.position = new Vector3(newX, newY, 0);
        lastPosition = transform.position;
    }
}
