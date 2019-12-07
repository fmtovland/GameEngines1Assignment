using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    public IEnumerator grow()
    {
        while(transform.localPosition.y < 1f)
        {
            Debug.Log(transform.localPosition);
            transform.Translate(0,0,.1f);
            yield return new WaitForSeconds(.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(grow());
    }
}
