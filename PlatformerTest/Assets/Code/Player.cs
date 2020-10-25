using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    Animator _animator;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private bool grounded;
    [SerializeField] private Transform feetPoint;
    [SerializeField] private LayerMask groundLayer;
    
    private SpriteRenderer sprite;
    public static bool flashlight;
    // change player sprite 

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        flashlight = false; 
    }


    private void Update()
    {

        float xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));

        _rigidBody.velocity = new Vector2(xSpeed, _rigidBody.velocity.y);

        if((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        }


        grounded = Physics2D.OverlapCircle(feetPoint.position, 0.8f, groundLayer);

        if(Input.GetButtonDown("Jump") && grounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            _rigidBody.AddForce(new Vector3(0,jumpForce));
        }

        _animator.SetBool("Grounded", grounded); 

    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "flashlight") {

            flashlight = true;
            print("flashlight obtained!");
           
        }


    }


}
