using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    
    public float speed = 5f;

    private Rigidbody2D m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float vert = Input.GetAxis("Vertical");
        float hor  = Input.GetAxis("Horizontal");

        m_rigidBody.velocity = new Vector2(hor, vert) * speed;

        // Look direction
        if (Input.GetJoystickNames().Length <= 0)
        {
            Vector3 playerToCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            playerToCamera.z = 0;
            transform.up = playerToCamera;
        }
        else
        {
            Vector3 lookDir = new Vector3(Input.GetAxis("LookX"), Input.GetAxis("LookY"), 0f);
            if (lookDir.sqrMagnitude > .05)
            {
                transform.up = lookDir.normalized;
            }
        }
    }
}
