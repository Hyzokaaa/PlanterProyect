using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFruits : MonoBehaviour
{
    private PlanterStatus planterStatus;
    [Header("Caida")]
    public bool canFalling = true;
    public int aleatoryFalling = 15;
    public int fallingTime = 5;
    public bool fall = false;
    [Header("Recolectable")]
    public bool isCollectable = false;

    [Header("Crecimiento")]
    public float speedIncrease = 0.005f;
    private float increaseTime = 0;
    private bool canIncrease = true;
    public float increaseInterval = 5;
    public float increaseDuration = 0.7f;

    void Start()
    {
        planterStatus = GetComponentInParent<PlanterStatus>();
    }

    void Update()
    {
        IncreaseScale();
        Falling();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && fall)
        {
            isCollectable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCollectable = false;
        }
    }


    //verifica que el nivel del planter sea el adecuado asi como que la cantidad de veces regado
    //sea mayor a 2 y activa de forma aleatoria la gravedad de la manzana
    //va restando de dos a dos la cantidad de veces regado por cada caida de manzana
    //con tun tiempo de espera entre caidas de 5 segundos
    public void Falling()
    {
        if (planterStatus.level == 5 && canFalling && !fall && planterStatus.toWater >= 2)
        {
            GetComponent<Rigidbody>().useGravity = RandomFall();
            canFalling = false;
            if (GetComponent<Rigidbody>().useGravity == true)
            {
                planterStatus.toWater -= 2;
                fall = true;
                planterStatus.collectables++;
            }
            Invoke("CanFalling", fallingTime);
        }
    }

    //metodo que genera aleatoriamente el valor booleano de las caidas
    //la variable aleatoryFalling determina cuan aleatorio es el resultado del bool
    public bool RandomFall()
    {
        bool result = false;
        int num = Random.Range(0, aleatoryFalling);
        if (num == 0)
        {
            result = true;
        }
        return result;
    }
    public void CanFalling()
    {
        canFalling = true;
    }

    //aumenta la escala de las manzanas en un tiempo determinado
    public void IncreaseScale()
    {
        float maxScale = 1;
        if (planterStatus.level == 4)
        {
            if (increaseTime < increaseDuration && canIncrease && transform.localScale.x <= maxScale)
            {
                increaseTime += Time.deltaTime;
                Increase();
            }
            if (increaseTime >= increaseDuration)
            {
                increaseTime = 0;
                canIncrease = false;
                Invoke("CanIncrease", increaseInterval);
            }
            if (transform.localScale.x >= maxScale)
            {
                planterStatus.level = 5;
                planterStatus.toWater = 0;
            }
        }
    }
    private void Increase()
    {
        transform.localScale = new Vector3(transform.localScale.x + speedIncrease, transform.localScale.y + speedIncrease, transform.localScale.z + speedIncrease);
    }
    private void CanIncrease()
    {
        canIncrease = true;
    }

}
