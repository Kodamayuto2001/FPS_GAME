using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[参考文献]
//https://www.sejuku.net/blog/69635
public class MainCamera_LookAt : MonoBehaviour
{
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //常にオブジェクトに注視する
        this.transform.LookAt(targetObject.transform);
    }
}
