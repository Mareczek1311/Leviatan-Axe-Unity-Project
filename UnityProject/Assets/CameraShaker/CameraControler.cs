using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject target;
    public GameObject cam;
    public float speed = 1, rotSpeed;
    float mouseX, mouseY;
    public float mY;
    public bool isFocus;
    public bool run;
    public bool aim;
    LewiatanScript l;
    
    private void Start()
    {
        l = GameObject.FindGameObjectWithTag("Lewiatan").GetComponent<LewiatanScript>();

    }
    private void LateUpdate()
    {
        isFocus = l.isFocus;
        cam.transform.LookAt(target.transform);
        mouseX += Input.GetAxis("Mouse X") * speed;
        mouseY -= Input.GetAxis("Mouse Y") * speed;
        mY = Mathf.Clamp(mouseY, -35, 60);

        target.transform.rotation = Quaternion.Euler(mY, mouseX, 0);
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, mouseX, 0), rotSpeed);
       
        if (isFocus)
        {
            cam.transform.localPosition = new Vector3(0, 0, -0.9f);

        }
        else
        {
            cam.transform.localPosition = new Vector3(0, 0, -1.2f);
        }
    }
}
