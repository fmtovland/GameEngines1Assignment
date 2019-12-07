using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    static Vector3 growthRate = new Vector3(.0005f,.0005f,.0005f);
    public CellScript parentCell;

    public IEnumerator drop()
    {
        float grown=0;
        while(grown < 1f)
        {
            transform.Translate(0,-.1f,0);
            grown+=.1f;
            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator grow()
    {
        float grown=0;
        int leaves = parentCell.countLeaves();

        if(leaves>0) while(grown < 2f)
        {
            transform.localScale+=(growthRate * leaves);
            grown += (.1f * leaves);
            yield return new WaitForSeconds(.1f);
        }

        Debug.Log(parentCell.getPedigree());
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation=Quaternion.AngleAxis(0,Vector3.up);
        StartCoroutine(grow());
        StartCoroutine(drop());
    }
}
