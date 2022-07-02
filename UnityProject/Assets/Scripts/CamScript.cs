using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Animator anim;
    bool isReady = false;

    private void Update()
    {

        if(GameObject.FindGameObjectWithTag("Lewiatan").GetComponent<LewiatanScript>().isCalled && !isReady)
        {
            anim.Play("shake");
            isReady = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isReady)
        {
            isReady = false;
        }
    }
}
