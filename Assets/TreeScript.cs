using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public enum Side {North, South, West, East, Below};
    public Side spawnSide=Side.Below;
    public int[] limits = {4,4,4,4,10};
    public int[] ancestors = {0,0,0,0,0};

    public IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);

        int sidenum=(int)Side.Below;

        if(ancestors[sidenum]<limits[sidenum])
        {
            GameObject g = Instantiate(gameObject);
            g.transform.SetParent(transform);
            TreeScript t = g.GetComponent<TreeScript>();
            t.ancestors[sidenum]+=1;
        }
    }

    public IEnumerator grow()
    {
        while(transform.localPosition.y < 1f)
        {
            transform.Translate(0,.1f,0);
            yield return new WaitForSeconds(.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
        StartCoroutine(grow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
