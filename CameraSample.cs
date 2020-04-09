using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumを使うために必要
using System;
/*----------------------------------------------------------------------------------------*/
//Input.GetButtonとかInput.GetKeyとかと、Input.GetAxisは何が違う？
//参考文献
//https://qiita.com/RyotaMurohoshi/items/7868752a3f056affa2df

public class CameraSample : MonoBehaviour
{
    private GameObject player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用
    private Rigidbody rigd;
    // Use this for initialization
    void Start()
    {
        //unitychanの情報を取得
        this.player = GameObject.Find("unitychan");

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransformInstance = this.transform;
        Vector3 pos = myTransformInstance.position;
        Vector3 localAngle = myTransformInstance.localEulerAngles;
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
        
        //回転しない設定
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //新しいトランスフォームの値を代入する
        transform.position = player.transform.position + offset;

        //ユニティちゃんの向きと同じようにカメラの向きを変更する
        transform.rotation = player.transform.rotation;

    }
    //キャラクターを中心として移動させるのでローカル座標を基準にする

}