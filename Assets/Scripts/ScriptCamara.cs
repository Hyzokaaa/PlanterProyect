using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamara : MonoBehaviour
{
    public GameObject personaje; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float postitionY = personaje.transform.position.y + 13f;
        transform.position = new Vector3(personaje.transform.position.x, postitionY , personaje.transform.position.z-13f);
    }
}
