
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    private PlanterStatus planterStatus;
    public List<GameObject> plants;
    public GameObject plant;
    public int type;
    public Tree tree;
    public Vegetable vegetable;
    public GameObject newPlant;
    private int selector = 0;
    

    void Start()
    {
        planterStatus = GetComponent<PlanterStatus>();
        
    }


    void Update()
    {
        Increase();
    }
     //OnTriggers
    void OnTriggerStay(Collider other)
    {
        planterStatus.level = SelectPlant(other, planterStatus.level);
        Towater(other);
    
    }


    //Methods
    public void Increase(){
        if(type == 1){
        planterStatus.level = newPlant.GetComponent<Tree>().SpawnSeed(planterStatus.level);
        planterStatus.level = newPlant.GetComponent<Tree>().SpawnBranch(planterStatus.level, planterStatus.toWater);
        planterStatus.level = newPlant.GetComponent<Tree>().SpawnTree(planterStatus.level, planterStatus.toWater);
        planterStatus.level = newPlant.GetComponent<Tree>().FruitIncrease(planterStatus.level, planterStatus.toWater);
        }else if(type== 2){
            //Proceso de crecimiento de las hortalizas
        }
    }

    public int SelectPlant(Collider other, int level)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (other.CompareTag("Player") && inventory.testSeeds && !planterStatus.enable)
        {
            Debug.Log("Presiona 1 para manzanas, 2 para fresas");
            if (Input.GetKey(KeyCode.Alpha1))
            {
                selector = 1;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                selector = 2;
            }
            if (selector > 0 )
            {
                plant = plants[selector-1];
                InstanciatePlant(plant);
                planterStatus.enable = true;   
                if(newPlant.TryGetComponent<Tree>(out tree)){
                    Debug.Log("Plantaste un arbol bro");
                    type = 1;
                }else if(plant.TryGetComponent<Vegetable>(out vegetable)){
                    Debug.Log("Soy un vegetal bro");
                    type = 2;
                }
            level  = 0;
            }
        }
        return level;
    }
    private void InstanciatePlant(GameObject newObjectPlant)
    {
            newPlant = Instantiate(newObjectPlant, transform.position, plant.transform.rotation, transform);  
            newPlant.name = "Plant";
    }

    private void Towater(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.J) && planterStatus.level > 0)
        {
            if (planterStatus.canToWater)
            {
                planterStatus.toWater++;
                planterStatus.canToWater = false;
                Invoke("TimeWater", planterStatus.toWaterTime);
            }
        }
    }
    public void TimeWater()
    {
        planterStatus.canToWater = true;
    }

   
}
