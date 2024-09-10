using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    GameManager manager;
    Rigidbody rb;
    public bool right;
    public float speed;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();

        ResetVariables();
    }


    void Update()
    {
        if (!manager.gameStarted) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (right) right = false;
            else right = true;
        }

        
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    if (EventSystem.current.currentSelectedGameObject.tag != "Button")
                    {
                        if (right) right = false;
                        else right = true;
                    }
                }
                else
                {
                    if (right) right = false;
                    else right = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!manager.gameStarted) return;

        if (right) rb.velocity = (Vector3.right * speed) + Physics.gravity;
        else rb.velocity = (Vector3.forward * speed) + Physics.gravity;

        speed += Time.fixedDeltaTime * 0.01f;
    }

    public void ResetVariables()
    {
        right = true;
        speed = 4.25f;
    }
}
