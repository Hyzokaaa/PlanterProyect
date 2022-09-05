
using System.Runtime.CompilerServices;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    public float moveSpeed = 6f;

    private float jumpBase = 0f; // esta es la fuerza del salto 3
    public float jumpForce = 5;
    public bool activ = false; // test salto 3
    private float heigth = 0;
    public float heigthValue = 2f;

    private Ray ray;
    private RaycastHit hit;

    private Ray rayUp;
    private RaycastHit hitUp;
    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement();
        jump();

    }
    private void FixedUpdate()
    {

    }

    public void movement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if (hor != 0 || ver != 0)
        {

            direction = new Vector3(hor, 0f, ver);
            direction.Normalize();
            characterController.Move(direction * moveSpeed * Time.deltaTime);

            if (hor > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (hor < 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            if (ver > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (ver < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (hor > 0 && ver > 0)
            {
                transform.rotation = Quaternion.Euler(0, 45, 0);
            }
            if (hor > 0 && ver < 0)
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
            }
            if (hor < 0 && ver > 0)
            {
                transform.rotation = Quaternion.Euler(0, -45, 0);
            }
            if (hor < 0 && ver < 0)
            {
                transform.rotation = Quaternion.Euler(0, -135, 0);
            }
        }
    }




    private bool flying()
    {
        bool fly = true;
        float moveY = transform.position.y - 0.5f;
        ray.origin = new Vector3(transform.position.x, moveY, transform.position.z);
        ray.direction = new Vector3(0, -1f, 0);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray, out hit, 0.1f))
        {
            fly = false;
        }
        return fly;
    }
    private bool collitionUp()
    {
        bool colition = false;
        float moveY = transform.position.y + 0.5f;
        rayUp.origin = new Vector3(transform.position.x, moveY, transform.position.z);
        rayUp.direction = new Vector3(0, 1f, 0);
        Debug.DrawRay(rayUp.origin, rayUp.direction, Color.blue);
        if (Physics.Raycast(rayUp, out hitUp, 0.2f))
        {
            colition = true;
        }
        return colition;
    }




    void jump()
    {
        bool upCollition = collitionUp();
        Vector3 jumpVector = new Vector3(0f, jumpBase, 0f);
        Gravity grav = gameObject.GetComponent<Gravity>();
        if (Input.GetKeyUp(KeyCode.L) && !flying())
        {

            jumpBase = jumpForce;
            jumpVector.y = jumpBase;
            activ = true;
            heigth = transform.position.y + heigthValue;
        }
        if (transform.position.y < heigth && activ)
        {
            grav.enabled = false;
            characterController.Move(jumpVector * Time.deltaTime);
            jumpBase += 0.2f;
        }
        if (transform.position.y >= heigth || upCollition)
        {
            grav.enabled = true;
            jumpBase = 0f;
            activ = false;

        }


    }



}
