using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private CharacterController characterController;
    public float gravityPower = -9.8f;

    private float aceleration = 1f;
    public float mass = 1f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        down();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void down()
    {
        if (!characterController.isGrounded)
        {
            Vector3 direction = new Vector3(0f, gravityPower * aceleration * mass, 0f);
            characterController.Move(direction * Time.deltaTime);
            aceleration += 0.05f;
        }
        if (characterController.isGrounded)
        {
            aceleration = 0f;
        }
    }


}
