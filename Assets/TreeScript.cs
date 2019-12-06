using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public int[] limits={0,0,0,0,0};
    public GameObject rootCell;

    public void Start()
    {
        GameObject cell = Instantiate(rootCell);
        cell.transform.SetParent(transform);
        CellScript cs = cell.GetComponent<CellScript>();
        cs.tree=this;
    }
}
