using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;
    public Vector2 startTouchPosition;
    public Vector2 endTouchPosition;
    public float swipeThreshold = 50f;
    public Vector2 delta;
    public float JumpPower;
    public bool IsGrounded;
    public float speed;
    public bool GoRight=false;
    public bool GoLeft=false;
    public float road=0;
    public int RoadState=2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SwipeDedect();
    }
    private void FixedUpdate()
    {
        MoveRight();
        MoveLeft();
    }
    public void MoveRight()
    {
        if (GoRight)
        {
            if (road < 4.5f && RoadState != 3)
            {
                Debug.Log("asdf");
                transform.position = new Vector3(transform.position.x+0.1f, transform.position.y, transform.position.z);
                road += 0.1f;
            }
            else
            {
                GoRight = false;
                if (road != 0) { RoadState++; }
                road = 0;
            }
        }
    }
    public void MoveLeft()
    {
        if (GoLeft)
        {
            if (road < 4.5f && RoadState !=1)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
                road += 0.1f;
            }
            else
            {
                GoLeft = false;
                if(road!=0) {RoadState--;}
                road = 0;
                
            }
        }
    }

    public void SwipeDedect()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // Fare ile sürükleme (PC testi)
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("a");
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();
            }
#endif
    }
    public void DetectSwipe()
    {
        Vector2 delta = endTouchPosition - startTouchPosition;

        //if (delta.magnitude < swipeThreshold) return; // Küçük hareketleri sayma

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            Debug.Log("x>y");
            if (delta.x > 0 && !GoLeft)
            {
                Debug.Log("move right");
                GoRight = true;
            }
            else if(delta.x < 0 && !GoRight)
            {
                Debug.Log("move left");
                GoLeft = true;
            }
        }
        else
        {
            if (delta.y > 0)
            {
                Jump();
            }
            else
            {
                roll();
            }
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            Debug.Log("asd");
            rb.AddForce(Vector3.up*JumpPower,ForceMode.Impulse);
        }
    }

    public void roll()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            IsGrounded = false;
        }
    }
}
