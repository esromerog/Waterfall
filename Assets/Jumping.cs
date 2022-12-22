using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Jumping : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private float jumpForce = 500.0f;

    //private bool IsGrounded => Physics.RayCast(
    //    new Vector2(transform.position.x, transform.position.y+2.0f)
    //);
    private Rigidbody _body;
    // Start is called before the first frame update
    void Start()
    {
        _body=GetComponent<Rigidbody>();
        jumpActionReference.action.performed+=OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnJump(InputAction.CallbackContext obj) {
        //if (!IsGrounded) return;
        _body.AddForce(Vector3.up*jumpForce);
    }
}
