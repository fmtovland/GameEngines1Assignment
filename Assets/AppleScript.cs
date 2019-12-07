using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    static Vector3 growthRate = new Vector3(.0005f,.0005f,.0005f);
    public CellScript parentCell;
    public ulong seed;

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
        int leaves = parentCell.countLeaves();

        if(leaves>0) while(transform.localScale.x<1f)
        {
            leaves = parentCell.countLeaves();
            transform.localScale+=(growthRate * leaves);
            yield return new WaitForSeconds(.1f);
        }

        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(fall());
    }

    public IEnumerator fall()
    {
        seed=parentCell.getPedigree()*(ulong)parentCell.countLeaves();
        yield return new WaitForSeconds(seed%20);
        Rigidbody r = gameObject.AddComponent<Rigidbody>();
        r.mass=transform.localScale.x;
        r.isKinematic=false;
        transform.parent = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation=Quaternion.AngleAxis(0,Vector3.up);
        StartCoroutine(grow());
        StartCoroutine(drop());
    }
}
