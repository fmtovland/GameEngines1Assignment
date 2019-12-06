using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public enum Side {North, South, West, East, Below};
    public Side side=Side.Below;

    public IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);
        GameObject g = Instantiate(gameObject);
    }

    public IEnumerator grow()
    {
        while(transform.position.y < .2f)
        {
            transform.Translate(0,.02f,0);
            yield return new WaitForSeconds(.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
//        StartCoroutine(spawn());
//        StartCoroutine(grow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
