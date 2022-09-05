using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int length;
    private List<GameObject> invetory;
    public List<GameObject> collectables;
    public bool testSeeds = true;
    public new string tag = "Fruits";
    void Start()
    {
        invetory = new List<GameObject>();
    }
    void Update()
    {
        Collect();
   
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tag)){
            collectables.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(tag)){
            collectables.Remove(other.gameObject);
        }
    }
 

    //Metodo que Verifica que el recolectable se encuentre
    //en rango y se presione el Input para recolectar
    public void Collect()
    {
        GameObject prox = SearchProx();
        if (Input.GetKeyUp(KeyCode.M) && prox != null && prox.GetComponent<FallingFruits>().isCollectable)
        {
            addCollected(prox);  
        }
    }

    //agrega el recolectable al inventario, se le pasa por parametro el GameObject
    public void addCollected(GameObject item)
    {
        if (invetory.Count < length)
        {
            invetory.Add(item);
            Destroy(item);
            collectables.Remove(item);
        }
    }
    
    //Busca entre todos los GameObjects con la etiqueta determinada y devuelve unicamente el mas cercano
    public GameObject SearchProx()
    {
        GameObject prox = null;
        float distanceMin = 9999;
        float distance;
        if (collectables.Count > 0)
        {
            for (int i = 0; i < collectables.Count ; i++)
            {
                distance = Vector3.Distance(transform.position, collectables[i].transform.position);
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    prox = collectables[i];
                }
            }
        }
        return prox;
    }
}
