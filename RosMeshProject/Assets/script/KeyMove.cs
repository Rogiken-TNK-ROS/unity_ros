using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMove : MonoBehaviour {

    private CharacterController characterController;
    private Animator animator;
    private Vector3 last_posi;
    
    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += this.transform.forward;
            animator.SetBool("walkFlag", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += this.transform.forward * -1;
            animator.SetBool("walkFlag", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += this.transform.right;
            animator.SetBool("walkFlag", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += this.transform.right * -1;
            animator.SetBool("walkFlag", true);
        }

        if(transform.position == last_posi)
            animator.SetBool("walkFlag", false);

        last_posi = transform.position;
    }
}
