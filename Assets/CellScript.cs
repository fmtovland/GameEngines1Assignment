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
    public CellScript parent;
    public int ancestors = 0;
    public int branches = 0;

    public IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);

        if(ancestors<tree.limit)
        {
            GameObject g = Instantiate(tree.rootCell,transform);
            CellScript t = g.GetComponent<CellScript>();
            t.ancestors=ancestors+1;
            t.spawnSide=spawnSide;
            t.tree=tree;
            t.parent=this;
            t.StartCoroutine(t.grow());
        }

        if(branches<tree.branchLimit && ancestors>tree.lowestBranch)
        {
            GameObject g;
            CellScript t;
            int branchPenalty = tree.limit/(ancestors-tree.lowestBranch);

            for(int i=0; i<4; i++) if(getPedigree() % ((ulong)i+4ul) == 1)
            {
                g = Instantiate(tree.rootCell,transform);
                g.transform.rotation*=rotations[i];
                t = g.GetComponent<CellScript>();
                t.spawnSide = (Side)i;
                t.ancestors=ancestors+(branchPenalty*(1+branches));
                t.branches=branches+1;
                t.tree=tree;
                t.parent=this;
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

    ulong getPedigree()
    {
        ulong pedigree = tree.seed%(transform.childCount==0 ? 127ul:(ulong)transform.childCount);;
        if(parent != null) pedigree+=parent.getPedigree();

        pedigree += (ulong)spawnSide;
        pedigree += (transform.childCount==0 ? 1ul:(ulong)transform.childCount);

        return pedigree;
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
