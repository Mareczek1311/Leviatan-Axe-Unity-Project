using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControler : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float x, z;
    LewiatanScript lewiatan;
    public bool isFocused;
    bool r;
    private void Start()
    {
        lewiatan = GameObject.FindGameObjectWithTag("Lewiatan").GetComponent<LewiatanScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        isFocused = lewiatan.isFocus;

        Vector3 mov = new Vector3(x, 0, z) * speed * Time.deltaTime;
        transform.Translate(mov, Space.Self);
        if(Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Time.timeScale = 1;
        }
    }
}
