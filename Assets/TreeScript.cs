using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public ulong seed=432819432778;
    public int limit=10,branchLimit=2,lowestBranch=3,branchGap=2,leaves=0;
    public float spawnSleep=3f;
    public GameObject rootCell,leaf,apple;

    public void Start()
    {
        GameObject cell = Instantiate(rootCell,transform);
        CellScript cs = cell.GetComponent<CellScript>();
        cs.nextBranch=lowestBranch;
        cs.tree=this;
    }
}
