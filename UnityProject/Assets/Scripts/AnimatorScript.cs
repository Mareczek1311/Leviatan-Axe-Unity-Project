using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public bool isRunning;
    public bool isAiming;
    public Animator anim;
    GameObject player;
    GameObject lewiatan;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        lewiatan = GameObject.FindGameObjectWithTag("Lewiatan");
    }

    private void Update()
    {
        anim.SetFloat("x", player.GetComponent<PlayerMovementControler>().x);
        anim.SetFloat("y", player.GetComponent<PlayerMovementControler>().z);
        anim.SetBool("run", isRunning);
        anim.SetBool("aim", isAiming);
    }
}
