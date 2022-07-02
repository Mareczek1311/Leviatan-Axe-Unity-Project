using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LewiatanScript : MonoBehaviour
{
    public bool calling = false;
    public bool isCalled = false;
    public bool isFocus = false;
    Transform pos;
    public float speed, speedCap, throwSpeed = 1;
    float dur;
    Rigidbody rb;
    GameObject player;
    public float rotSpeed = 5;
    float mY;
    public GameObject orientation;
    public AnimatorScript anim;
    public float backDist = 3;
    public GameObject pointObject;
    bool hit;
    GameObject particle;
    Collider coll;


    public Animator axeAnim;
    private void Start()
    {
        coll = gameObject.GetComponent<Collider>();
        particle = gameObject.transform.GetChild(1).gameObject;
        speedCap = speed;
        pos = GameObject.FindGameObjectWithTag("LewiatanPosition").transform;
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void Attack()
    {
        anim.anim.Play("throw");
        rb.isKinematic = false;
        rb.useGravity = true;
        isCalled = false;
        gameObject.transform.rotation = Quaternion.Euler(mY, player.transform.rotation.eulerAngles.y, -65);
        rb.AddForce(orientation.transform.forward * throwSpeed, ForceMode.Force);
    }
    private void FixedUpdate()
    {
        rb.maxAngularVelocity = rotSpeed;
        
    }
    void Rotation()
    {
        rb.AddTorque(transform.right * 50000, ForceMode.Acceleration);
    }

    IEnumerator AddMass()
    {
        yield return new WaitForSeconds(0.1f);
        rb.AddTorque(transform.right * 50000, ForceMode.Acceleration);
        yield return new WaitForSeconds(0.7f);
        rb.AddForce(orientation.transform.up * -10, ForceMode.Impulse);
    }

    void FindRotation(float xRotation, float yRotation, float zRotation)
    {
        transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    }
    float time = 0;
    public int hitTimes = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (!isCalled)
        {
            Debug.Log(collision.collider.name);
            time = 0;
            if (!hit)
            {
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z),
                    Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -65), time);
                rb.AddExplosionForce(5, transform.position, 5);
                StartCoroutine(AddMass());
                FindRotation(14, orientation.transform.eulerAngles.y, -28);
                hit = true;
            }

            if(collision.collider.tag == "gr" && hitTimes != 0)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                FindRotation(120, 0, transform.eulerAngles.z);
            }
            if (collision.collider.tag == "sticky")
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            if (collision.collider.tag == "notsticky")
            {
                rb.AddForce(orientation.transform.up * 200, ForceMode.Force);
                rb.AddForce(orientation.transform.forward * -10, ForceMode.Force);
                hitTimes += 1;
            }
        }
    }
    bool isReady = false;
    public List<Transform> points;
    int d = 0;

    private void Update()
    {
        if (time <= 1)
        {
            time += Time.deltaTime * 2;
        }
        orientation.transform.rotation = Quaternion.Euler(mY, player.transform.eulerAngles.y, 0);

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            isFocus = true;
        }
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            isFocus = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isCalled && isFocus)
        {
            Attack();
            Rotation();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            calling = true;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        Calling();

    }

    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }

    float returnTime;
    Transform pullPosition;
    AnimatorClipInfo[] animatorinfo;
    string current_animation;
    void Calling()
    {
        if (calling)
        {
            if (!isReady)
            {
            
                returnTime = 0;
                pullPosition = transform;
                
                    isReady = true;
                
            }
            if (isReady)
            {
                rb.AddTorque(transform.right * 50000, ForceMode.Acceleration);

                if (returnTime < 1)
                {
                    transform.position = GetQuadraticCurvePoint(returnTime, pullPosition.position, points[0].transform.position, points[1].transform.position);
                    returnTime += Time.deltaTime * 1.5f;
                    coll.enabled = false;

                }
                else
                {
                    isCalled = true;
                }
            }
        }
        if (isCalled)
        {
            speed = speedCap;
            isReady = false;
            mY = player.GetComponent<CameraControler>().mY;
            gameObject.transform.position = pos.transform.position;
            gameObject.transform.rotation = pos.transform.rotation;
            rb.useGravity = false;
            rb.isKinematic = true;
            calling = false;
            Destroy(GameObject.FindGameObjectWithTag("Point"));
            coll.enabled = true;
            hit = false;
            hitTimes = 0;

        }
        if(isCalled && !isFocus)
        {
            var em = particle.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = 0;
        }
        else
        {
            var em = particle.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = 18;
        }
    }
}
