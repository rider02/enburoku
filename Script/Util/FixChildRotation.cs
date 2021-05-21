using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//210211 親のオブジェクトの回転を無効化するが、少し動いてしまう
public class FixChildRotation : MonoBehaviour
{

    Vector3 def;

    void Awake()
    {
        def = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        Vector3 _parent = transform.parent.transform.localRotation.eulerAngles;

        //修正箇所
        transform.rotation = Quaternion.Euler(90,0,0);

    }


}