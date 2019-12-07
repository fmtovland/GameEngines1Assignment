using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    public IEnumerator grow()
    {
        float grown=0;
        while(grown < 1f)
        {
            transform.Translate(0,0,.1f);
            grown+=.1f;
            yield return new WaitForSeconds(.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(grow());
    }
}
