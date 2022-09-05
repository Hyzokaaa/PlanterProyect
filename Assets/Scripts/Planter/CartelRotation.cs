using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelRotation : MonoBehaviour
{
    public new Camera camera;
    
    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {  
        transform.LookAt(camera.transform.position);
    }
}
