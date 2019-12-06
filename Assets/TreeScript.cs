using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public enum Side {North, South, West, East, Below};
    public Side spawnSide;

    public static Quaternion[] rotations={new Quaternion(90,0,0,0)
                                        ,new Quaternion(90,90,0,0)
                                        ,new Quaternion(90,180,0,0)
                                        ,new Quaternion(90,270,0,0)};

    public GameObject Cell;

    public int[] limits = {2,2,2,2,10};
    public int[] ancestors = {0,0,0,0,0};

    public IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);

        int sidenum=(int)spawnSide;

        if(ancestors[sidenum]<limits[sidenum])
        {
            GameObject g = Instantiate(Cell,transform);
            TreeScript t = g.GetComponent<TreeScript>();
            t.ancestors[sidenum]=ancestors[sidenum]+1;
            t.spawnSide=spawnSide;
            t.StartCoroutine(t.grow());
        }

        if(spawnSide == Side.Below)
        {
            GameObject g;
            TreeScript t;

            for(int i=0; i<4; i++)
            {
                g = Instantiate(Cell,transform);
                g.transform.rotation=rotations[i];
                t = g.GetComponent<TreeScript>();
                t.spawnSide = (Side)i;
                t.ancestors[i]=ancestors[i]+1;
                Debug.Log((Side)i);
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
