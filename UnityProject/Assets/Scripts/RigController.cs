using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    bool isCalled;
    bool isFocus;
    bool isCalling;
    public Animator anim;
    LewiatanScript l;
    public TwoBoneIKConstraint lewiatanRig;
    public TwoBoneIKConstraint aimRig;
    public MultiAimConstraint bodyRig;
    public MultiAimConstraint headRig;
    public Transform noAim;
    public Transform aim;
    public Transform calling;
    public Transform hand;
    public Transform thow;


    bool throwing;

    public void Start()
    {
        l = GameObject.FindGameObjectWithTag("Lewiatan").GetComponent<LewiatanScript>();

    }

    IEnumerator Wait()
    {
     
        yield return new WaitForSeconds(0.05f);
        throwing = false;
    }

    private void Update()
    {
        isFocus = l.isFocus;
        isCalling = l.calling;
        isCalled = l.isCalled;
        
        if (isFocus && Input.GetKeyDown(KeyCode.Mouse0))
        {
            throwing = true;
            StartCoroutine(Wait());
        }
        if(!throwing)
        {
            if (!isCalled)
            {
                aimRig.weight = 0;
            }
            if (isFocus && !isCalling)
            {
                aimRig.weight = 1;
                bodyRig.weight = 1;
                headRig.weight = 1;
                lewiatanRig.data.target = aim;
                hand.position = aim.position;
                hand.rotation = aim.rotation;
            }
            else if (isCalling)
            {
                hand.position = calling.position;
                hand.rotation = calling.rotation;
            }
            else
            {
                bodyRig.weight = 0;
                headRig.weight = 0;
                aimRig.weight = 0;
                lewiatanRig.data.target = noAim;
                hand.position = noAim.position;
                hand.rotation = noAim.rotation;
            }
        }

    }
}
