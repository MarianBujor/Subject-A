using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController playerController;
    public float speed = 12f;
    public float airSpeed = 0.3f;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;
    public float jumpLength = 0.3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator anim;
    public bool jumpAnim;
    public Vector3 velocity;
    public Vector3 oldVelocity;
    public Vector3 move;
    public bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    bool isMoving(Vector3 move)
    {
        if (playerController.transform.position != move)
            return true;
        return false;
    }
    // Update is called once per frame
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
            velocity.x = 0;
            velocity.z = 0;
            
            anim.SetBool("startjump", false);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        //move.Normalize();
        playerController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
       
                anim.SetBool("startjump", true);
            
               // anim.Play("Jump");
            // velocity.z = move.z * jumpLength * speed/10;
            //  velocity.x = move.x * jumpLength * speed/10;

        }
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            velocity.z = 0;
            velocity.x = 0;
        }
        if(!isGrounded) 
        {
            anim.SetBool("midAir", true);
            
            if (velocity.z <= 18 && velocity.x <= 18)
            {
                //velocity.z += move.z * 0.1f;
                velocity.z = move.z * jumpLength * speed / 10;
                if (velocity.x < velocity.z)
                    //.x += move.x * 0.1f;
                     velocity.x = move.x * jumpLength * speed / 10;
            }
        }
        velocity.y += gravity * Time.deltaTime;
        bool air = anim.GetBool("midAir");
        if (isGrounded && air == true)
        {
            anim.SetBool("midAir", false);
        }
        if ( Input.GetKey(KeyCode.Escape))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }
        playerController.Move(velocity  * Time.deltaTime);
    }
    
    void Respawn()
    {
        playerController.transform.position = Vector3.zero;
    }
    void LaunchPlayer()
    {
        velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch(hit.gameObject.tag)
        {
            case "Respawn":
                Respawn();
                break;
            case "Launcher":
                LaunchPlayer();
                break;
            case "Final":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                break;
        }
    }
}   