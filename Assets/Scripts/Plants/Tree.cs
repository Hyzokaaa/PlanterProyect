using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Plant
{
    public GameObject branch;
    public GameObject tree;
    public GameObject fruit;

    void Start()
    {
        planterStatus = GetComponentInParent<PlanterStatus>();
        spawnOrigin = transform.position;
        spawnOrigin.y += 0.34f;
    }

    void Awake()
    {
        
    }
    void Update()
    
    {
    }


    //Methods

    public int SpawnSeed(int level)
    {

        int numSeed = 0;
        if (level == 0)
        {
            while (numSeed < 25)
            {
                Vector3 locate = spawnOrigin;
                locate.z += Random.Range(-1.4f, 1.4f);
                locate.x += Random.Range(-1.4f, 1.4f);
                Instantiate(seed, locate, seed.transform.rotation,gameObject.transform);
                locate = spawnOrigin;
                numSeed++;
            }
            level = 1;
        }
        return level;
    }


    public int SpawnBranch(int level, int toWater)
    {
        int numBranch = 0;
        int max = Random.Range(1, 5);
        if (level == 1 && toWater == 3)
        {
            while (numBranch < max)
            {
                Vector3 locate = spawnOrigin;
                locate.z += Random.Range(-1f, 1f);
                locate.x += Random.Range(-1f, 1f);
                Instantiate(branch, locate, branch.transform.rotation, transform);
                locate = spawnOrigin;
                numBranch++;
            }
            level = 2;
            planterStatus.toWater = 0;
        }
        return level;
    }
    public int SpawnTree(int level, int toWater)
    {
        if (level == 2 && toWater == 5)
        {

            Vector3 locate = spawnOrigin;
            locate.y -= 0.34f;
            Quaternion locateRotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);

            locate.z += Random.Range(-0.6f, 0.6f);
            locate.x += Random.Range(-0.6f, 0.6f);
            GameObject esceneTree = Instantiate(tree, locate, locateRotation, transform);
            esceneTree.name = "Tree";
            locate = spawnOrigin;
            level = 3;
            planterStatus.toWater = 0;
        }
        return level;
    }


    public int FruitIncrease(int level, int toWater)
    {
        if (level == 3 && toWater == 4 && planterStatus.canIncreaseFruit)
        {
            List<Vector3> positions = SearchPositions();

            foreach (Vector3 item in positions)
            {
                GameObject realApple = Instantiate(fruit, item, fruit.transform.rotation, transform);
                realApple.transform.localScale = new Vector3(0, 0, 0);
                realApple.name = "Apple";
            }
            planterStatus.canIncreaseFruit = false;
            planterStatus.toWater = 0;
            level++;
        }
        return level;
    }

    public List<Vector3> SearchPositions()
    {
        GameObject tree = transform.gameObject;
        GameObject empty = transform.gameObject;
        bool finded = false;
        List<Vector3> positions = new();

        int total = transform.childCount;
        for (int i = 0; i < total && !finded; i++)
        {
            tree = transform.GetChild(i).gameObject;
            if (tree.name == "Tree")
            {
                finded = true;
            }
        }
        finded = false;
        for (int j = 0; j < tree.transform.childCount && !finded; j++)
        {
            empty = tree.transform.GetChild(j).gameObject;
            if (empty.name == "Manzanas")
            {
                finded = true;
            }
        }
        for (int k = 0; k < empty.transform.childCount; k++)
        {
            positions.Add(empty.transform.GetChild(k).transform.position);
        }

        return positions;
    }



    //Aumenta el nivel del arbol a 4 para comenzar a gotear las frutas
    public int FallingFruits(int level, int toWater)
    {
        if (level == 3 && toWater == 5)
        {
            level = 4;
            planterStatus.toWater = 0;
        }
        return level;
    }




}
