using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rbCharapter : MonoBehaviour
{
    [Header("REFS")]
    public GameObject _playerLight;

    [Header("STATS")]
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 1f;
    public float DashDistance = 5f;
    public float health= 5;
   
    
    [Header("CHECK")]
    //MOVIMIENTO
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    public bool _isGrounded = false;
    public LayerMask ground;
    bool _isRight;
    //AGARRAR
    public LayerMask wall;
    public float objectdistance;
    Rigidbody _lastGrab;


    //ANIMATIONS
    public Animator anim;
    public new AnimationClip[] animations;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _isRight = true;
        anim = GetComponent<Animator>();

    }

    void Update()
    {

        //MOVIMIENTO
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, GroundDistance, ground))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            _isGrounded = true;

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            
            _isGrounded = false;
            
        }
        
        
        
        if ((Input.GetButtonDown("Jump"))&&( _isGrounded==true))
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
            anim.SetBool("salto", true);


        }
        else
        {
            anim.SetBool("salto", false);

        }


        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal2D");
        _inputs = Vector3.ClampMagnitude(_inputs, 1);


        if (_inputs != Vector3.zero)
        {
            if (_inputs.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.1f);
                _isRight = false;
                anim.SetBool("run", true);


            }
            else if (_inputs.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -0.1f);
                _isRight = true;
                anim.SetBool("run", true);



            }
            transform.localPosition += new Vector3(_inputs.x * Time.deltaTime * Speed, 0, 0);
        }
        else
        {
            anim.SetBool("run", false);

            //anim.enabled = false;
            //anim.enabled = true;
            //anim.Play("idle");
        }

        _playerLight.transform.LookAt(transform);


        //AGARRAR

        Vector3 direccion = Vector3.forward;
        if (_isRight)
            direccion = -Vector3.forward;
            

        if (Physics.Raycast(transform.position, transform.TransformDirection(direccion), out hit, objectdistance, wall) && Input.GetKey(KeyCode.E))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direccion) * hit.distance, Color.yellow);
            hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            print("Grab: " + hit.collider.gameObject.name);
            _lastGrab = hit.collider.gameObject.GetComponent<Rigidbody>();
            anim.Play("Empuje1");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direccion) * 1000, Color.green);
            if (_lastGrab != null)
            {
                _lastGrab.isKinematic = true;
                _lastGrab = null;
            }
                
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Muelle")
        {
            

            _body.AddForce(transform.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
            //collision.gameObject.GetComponent<Animator>().SetBool("Touch", true);
            collision.gameObject.GetComponent<Animator>().Play("Muelle");
        }
    }

  
    public float Get_health
    {
        get { return health; }
        set { health = value; }
    }
    
    
}
