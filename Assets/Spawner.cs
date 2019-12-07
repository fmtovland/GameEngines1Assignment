using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject tree;

    void Start()
    {
        System.Random r  = new System.Random();
        ulong seed = (ulong)((r.NextDouble() * 2.0 - 1.0) * long.MaxValue);
        GameObject t = Instantiate(tree);
        TreeScript ts = t.GetComponent<TreeScript>();
        ts.seed=seed;
    }
}
