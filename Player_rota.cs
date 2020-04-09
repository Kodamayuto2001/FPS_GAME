using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*---------------------UnityでRotation（Quaternion）をうまく使いたい----------------------*/
//参考文献
//http://spi8823.hatenablog.com/entry/2015/05/31/025903
/*----------------------------------------------------------------------------------------*/
//Quaternion構造体(四元数=複素数)[高度な]
//transform.rotation = new Quaternion(x, y, z, w);
/*----------------------------------------------------------------------------------------*/
//Quaternion.Euler関数(度数法を用いた回転)[低度な]
//transform.rotation = Quaternion.Euler(90, 30, 10);
/*----------------------------------------------------------------------------------------*/
//transform.LookAt関数(とにかくある方向に向かせたい)
//transform.LookAt(10, 20, 30);
//transform.LookAtについて
//[参考文献]
//https://www.sejuku.net/blog/69635
/*----------------------------------------------------------------------------------------*/
//transform.Rotate関数(ある軸の周りにいくらか回転させたい)
//transform.Rotate(new Vector3(0, 1, 0), 90);
//
//[解説]
// (0, 1, 0）というベクトル（すなわちY軸）を軸にして90度回転させた
//（0, 30, 60）といった変なベクトルでも同じ
//transform.forward  //物体が向いている方向
//transform.right　　//物体から見て右側
//transform.up       //物体から見て上側
//
//[具体例]戦闘機のスピン
//float angle = 1;
//transform.Rotate(transform.forward, angle);
//
/*----------------------------------------------------------------------------------------*/
//QuaternionとVector3との掛け算
//あるベクトルをある軸で回転させたい
//
//すなわち、（horizontal, vertical）という入力を受け取った時、
//Y軸に対してangle度回転しているキャラクターの正面に向かってvertical、
//右側に向かってhorizontal分進ませたい
//
//float horizontal = Input.GetAxis("Horizontal");
//float vertical = Input.GetAxis("Vertical");
//
//transform.Translate(Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(horizontal, 0, vertical));
//
//[解説]
//何をしているのかというと「Quaternion.AngleAxis(angle, Vector3.up)」という関数を使って、
//Y軸にangle度だけ回転させるQuaternionを取得し、
//それを「new Vector3(horizontal, 0, vertical)」というベクトルにかけることによってこのベクトルを回転させている
//これが本来のQuaternionの使い方である。
//順番は必ず「Quaternion　×　Vector3」の順でなければいけない。
//
//"transform.forward"などを用いて簡単に
//transform.Translate(transform.forward * vertical + transform.right * horizontal);
//
/*----------------------------------------------------------------------------------------*/
//オブジェクトを連続的に回転させたい
//Quaternion.Slerp
//想像するならば、物音に気付いてこちらを振り返る敵や、戦闘機がスピンするとき。
//
//[具体例]
//Quaternion from;
//Quaternion to;
//float t = 0;
//public void Update()
//{
//    if (t < 1)
//        t += Time.deltaTime;
//    transform.rotation = Quaternion.Slerp(from, to, t);
//}
//
//[解説]
//このサンプルでは、初め"from"という角度を向いていたオブジェクトが、
//1秒かけてゆっくりと"to"という角度を向く、という処理を行っている。
//
/*----------------------------------------------------------------------------------------*/

public class Player_rota : MonoBehaviour
{

    private GameObject player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用
    private Rigidbody rigd;

    // Start is called before the first frame update
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

        //新しいトランスフォームの値を代入する
        transform.position = player.transform.position + offset;

        //ユニティちゃんの向きと同じようにカメラの向きを変更する
        transform.rotation = player.transform.rotation;

    }
}
