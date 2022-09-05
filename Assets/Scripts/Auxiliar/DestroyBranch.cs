using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBranch : MonoBehaviour
{
    private PlanterStatus planterStatus;
    void Start()
    {
        planterStatus = GetComponentInParent<PlanterStatus>();
    }
    void Update()
    {
        ClearBranch();
    }
    public void ClearBranch(){
        if(planterStatus.level > 2){
            Destroy(this.gameObject);
        }
    }
}
