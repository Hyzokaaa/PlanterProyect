using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterSpawn : MonoBehaviour
{
    private PlanterStatus planterStatus;
    private PlanterIncrease planterIncrease;
    public GameObject cartel;

    void Start()
    {
        planterStatus = GetComponent<PlanterStatus>();
        planterIncrease = GetComponent<PlanterIncrease>();
        cartel.SetActive(false);
        
    }

    void Update()
    {
        planterStatus.level = planterIncrease.SpawnBranch(planterStatus.level, planterStatus.toWater);
        planterStatus.level = planterIncrease.SpawnTree(planterStatus.level, planterStatus.toWater);
        planterStatus.level = planterIncrease.FallingFruits(planterStatus.level, planterStatus.toWater);
        planterStatus.level = planterIncrease.FruitIncrease(planterStatus.level, planterStatus.toWater);
    }
    void OnTriggerStay(Collider other)
    {
        Seed(other);
        Towater(other);
    }
    void OnTriggerEnter(Collider other)
    {
        OnPlanter(other);
    }
    void OnTriggerExit(Collider other)
    {
        OutPlanter(other);
    }

    private void OnPlanter(Collider other)
    {
        if (other.CompareTag("Player") && !planterStatus.enable)
        {
            cartel.SetActive(true);
        }
    }
    private void OutPlanter(Collider other)
    {
        if (other.CompareTag("Player") && !planterStatus.enable)
        {
            cartel.SetActive(false);
            Debug.Log("Saliste del rango de la Huerta");
        }
    }
    private void Seed(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.K) && !planterStatus.enable)
        {
            planterStatus.enable = true;
            planterStatus.level = planterIncrease.SpawnSeed(planterStatus.level);
            cartel.SetActive(false);
        }
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

