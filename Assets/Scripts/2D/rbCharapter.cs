using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rbCharapter : MonoBehaviour
{
    [Header("REFS")]
    public GameObject _playerLight;
    public GameObject _centro;
    [Header("STATS")]
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 1f;
    public float DashDistance = 5f;
    public float health = 5;
    public float charge= 5;
    public float muelleSalto;
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

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _isRight = true;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        carga();


        if (health <= 0)
        {
            anim.SetBool("dead", true);

            StartCoroutine(waitForDead());

        }
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



        if ((Input.GetButtonDown("Jump")) && (_isGrounded == true))
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


        }

        _playerLight.transform.LookAt(transform);


        //AGARRAR

        Vector3 direccion = Vector3.forward;
        if (_isRight)
            direccion = -Vector3.forward;


        if (Physics.Raycast(_centro.transform.position, transform.TransformDirection(direccion), out hit, objectdistance) && Input.GetKey(KeyCode.E))
        {
            if (hit.transform.gameObject.tag == "wall")
            {
                Debug.DrawRay(_centro.transform.position , transform.TransformDirection(direccion) * hit.distance, Color.yellow);
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                print("Grab: " + hit.collider.gameObject.name);
                _lastGrab = hit.collider.gameObject.GetComponent<Rigidbody>();
                anim.SetBool("empuje", true);

            }
            
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direccion) * 1000, Color.green);
            if (_lastGrab != null)
            {
                _lastGrab.isKinematic = true;
                _lastGrab = null;
            }
            anim.SetBool("empuje", false);

        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            health -= 1;
            anim.SetBool("golpe", true);
            GameObject luz = GameObject.FindGameObjectWithTag("luz");
            luz.GetComponent<Light>().intensity -= 0.2f;

        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Trampa")
        {
            health -= 1;
            anim.SetBool("golpe", true);
            GameObject luz = GameObject.FindGameObjectWithTag("luz");
            luz.GetComponent<Light>().intensity -= 0.2f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Muelle")
        {


            _body.AddForce(transform.up * Mathf.Sqrt(JumpHeight * muelleSalto * Physics.gravity.y), ForceMode.Impulse);
            //collision.gameObject.GetComponent<Animator>().SetBool("Touch", true);
            other.gameObject.GetComponent<Animator>().Play("Muelle");

            anim.SetBool("Salto2", true);

        }
       
    }
    public float Get_health
    {
        get { return health; }
        set { health = value; }
    }

    IEnumerator waitForDead()
    {

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("LostScreen");


    }

    public void carga()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            charge = 5;
            charge -= Time.deltaTime;
            anim.SetBool("carga", true);

            if (charge <= 0)
            {
                health += 1;
                GameObject luz = GameObject.FindGameObjectWithTag("luz");
                luz.GetComponent<Light>().intensity += 0.2f;
            }
        }
        else
        {
            anim.SetBool("carga", false);

        }
    }


}