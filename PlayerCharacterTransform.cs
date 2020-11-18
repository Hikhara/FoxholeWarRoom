using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterTransform : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    public float rotationRate = 360;
    public float moveSpeed = 1;

    Vector2 rotation = new Vector2(0, 0);

    //rotation.y += Input.GetAxis("Mouse X");
    //rotation.x += -Input.GetAxis("Mouse Y");
    //    transform.eulerAngles = (Vector2) rotation * speed;

    // Update is called once per frame
    void Update()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        ApplyInput(moveAxis);
    }

    private void ApplyInput(float moveInput)
    {
        Move(moveInput);
        Turn(rotation);
    }
    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed);
    }
    private void Turn(Vector2 rotation)
    {
        transform.eulerAngles = rotation * rotationRate;
    }
}
