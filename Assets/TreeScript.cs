using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public ulong seed=432819432778;
    public int limit=10,branchLimit=2;
    public GameObject rootCell;

    public void Start()
    {
        GameObject cell = Instantiate(rootCell);
        cell.transform.SetParent(transform);
        CellScript cs = cell.GetComponent<CellScript>();
        cs.tree=this;
    }
}
