using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public enum Side {North, South, West, East, Below};
    public Side spawnSide;

    public static Quaternion[] rotations={Quaternion.AngleAxis(90,Vector3.up) * Quaternion.AngleAxis(75,Vector3.forward)
                                        ,Quaternion.AngleAxis(180,Vector3.up) * Quaternion.AngleAxis(75,Vector3.forward)
                                        ,Quaternion.AngleAxis(270,Vector3.up) * Quaternion.AngleAxis(75,Vector3.forward)
                                        ,Quaternion.AngleAxis(360,Vector3.up) * Quaternion.AngleAxis(75,Vector3.forward)};

    public TreeScript tree;
    public CellScript parent;
    public int ancestors = 0;
    public int branches = 0;
    public int nextBranch = 0;

    public IEnumerator spawn()
    {
        tree.seed *= getPedigree();
        yield return new WaitForSeconds(tree.spawnSleep);
        GameObject g;
        CellScript t;

        if(ancestors<tree.limit)
        {
            g = Instantiate(tree.rootCell,transform);
            t = g.GetComponent<CellScript>();
            t.ancestors=ancestors+1;
            t.branches=branches;
            t.nextBranch=nextBranch;
            t.spawnSide=spawnSide;
            t.tree=tree;
            t.parent=this;
            t.StartCoroutine(t.grow());

            if(branches<tree.branchLimit)
            {
                int num = ancestors-tree.lowestBranch;
                int branchPenalty = tree.limit/(num==0?1:num);

                if(branches>0) for(int i=0; i<3; i+=2) 
                {
                    g = Instantiate(tree.leaf,transform);
                    g.transform.rotation*=rotations[i];
                }

                for(int i=0; i<4; i++) 
                {
                    if(ancestors>nextBranch && getPedigree() % ((ulong)i+3ul) == 1)
                    {
                        t.nextBranch=ancestors+tree.branchGap;
                        g = Instantiate(tree.rootCell,transform);
                        g.transform.rotation*=rotations[i];
                        t = g.GetComponent<CellScript>();
                        t.spawnSide = (Side)i;
                        t.ancestors=ancestors+(branchPenalty*(1+branches));
                        t.nextBranch=t.ancestors+tree.lowestBranch;
                        t.branches=branches+1;
                        t.tree=tree;
                        t.parent=this;
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }

        else if(branches>0)
        {
            g=Instantiate(tree.apple,transform);
            AppleScript a = g.GetComponent<AppleScript>();
            a.parentCell=this;
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

    public ulong getPedigree()
    {
        ulong pedigree = tree.seed;
        if(parent != null) pedigree*=parent.getPedigree();

        pedigree += (ulong)spawnSide+13ul;
        pedigree *= (transform.childCount==0 ? 7ul:(ulong)transform.childCount);

        return pedigree;
    }

    public int countLeaves()
    {
        int leaves=GetComponentsInChildren<LeafScript>().Length;
        if(parent != null) leaves+=parent.countLeaves();

        return leaves;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }
}
