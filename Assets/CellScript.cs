using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public enum Side {North, South, West, East, Below};
    public Side spawnSide;

    public static Quaternion[] rotations={Quaternion.AngleAxis(90,Vector3.up) * Quaternion.AngleAxis(90,Vector3.forward)
                                        ,Quaternion.AngleAxis(180,Vector3.up) * Quaternion.AngleAxis(90,Vector3.forward)
                                        ,Quaternion.AngleAxis(270,Vector3.up) * Quaternion.AngleAxis(90,Vector3.forward)
                                        ,Quaternion.AngleAxis(360,Vector3.up) * Quaternion.AngleAxis(90,Vector3.forward)};

    public TreeScript tree;
    public int[] ancestors = {0,0,0,0,0};

    public IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);

        int sidenum=(int)spawnSide;

        if(ancestors[sidenum]<tree.limits[sidenum])
        {
            GameObject g = Instantiate(tree.rootCell,transform);
            CellScript t = g.GetComponent<CellScript>();
            t.ancestors[sidenum]=ancestors[sidenum]+1;
            t.spawnSide=spawnSide;
            t.tree=tree;
            t.StartCoroutine(t.grow());
        }

        if(spawnSide == Side.Below)
        {
            GameObject g;
            CellScript t;

            for(int i=0; i<4; i++)
            {
                g = Instantiate(tree.rootCell,transform);
                g.transform.rotation=rotations[i];
                t = g.GetComponent<CellScript>();
                t.spawnSide = (Side)i;
                t.ancestors[i]=ancestors[i]+1;
                t.tree=tree;
                yield return new WaitForEndOfFrame();
            }
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
