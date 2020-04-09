using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//カメラの回転は全方向、右マウスを押している間
//キャラクターの移動はTransform.RotateAroundなどを使って
//y軸を回転の軸として、マウスの移動したx成分の距離だけ回転させる
public class NewBehaviourScript : MonoBehaviour
{
    private GameObject targetObj;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0.0f, 0.0f, 0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0.0f, 0.0f, -0.1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(0.1f, 0.0f, 0.0f);
        }
    }
}
