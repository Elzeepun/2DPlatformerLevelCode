using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingRight = true;
    public Transform groundDetection;

    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                anim.SetBool("isRunning", true);
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else
            {
                anim.SetBool("IsRunning", true);
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

}
