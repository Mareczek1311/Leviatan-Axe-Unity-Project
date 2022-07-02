using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiScript : MonoBehaviour
{
    public float speed = 5;
    Animator anim;
    bool isDead;
    public GameObject thisGuy;
    public BoxCollider mainCollider;
    public BoxCollider secondCollider;

    private void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        GetRagdollBits();
        RagdolModeOff();
    }
    private void Update()
    {
        if (!isDead)
        {
            var pos = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            pos.y = 0;
            var rotation = Quaternion.LookRotation(-pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1);
            transform.Translate(Vector3.forward * speed, Space.Self);
        }
    }

    Collider[] ragdollColliders;
    Rigidbody[] rbs;

    void GetRagdollBits()
    {
        ragdollColliders = thisGuy.GetComponentsInChildren<Collider>();
        rbs = thisGuy.GetComponentsInChildren<Rigidbody>();


    }

    void RagdolModeOn()
    {
        anim.enabled = false;
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rigid in rbs)
        {
            rigid.isKinematic = false;

        }
        mainCollider.enabled = false;
        isDead = true;
        secondCollider.enabled = false;


        GetComponent<Rigidbody>().isKinematic = true;
    }

    void RagdolModeOff()
    {
        foreach(Collider col in ragdollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rigid in rbs)
        {
            rigid.isKinematic = true;
        }
        anim.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        secondCollider.enabled = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lewiatan")
        {
            RagdolModeOn();
        }
    }

}
