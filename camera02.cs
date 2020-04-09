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


public class camera02 : MonoBehaviour
{
    GameObject targetObj;
    Vector3 targetPos;
    Rigidbody myrigidbody;
    Vector3 n_rota;
    Vector3 o_rota;
    void Start()
    {
        //myrigidbody.centerOfMass = new Vector3(0, 0, 1);

        targetObj = GameObject.Find("unitychan");
        targetPos = targetObj.transform.position;
    }
    //Rigidbody 付きのオブジェクトを回転させるには
    //[参考文献]
    //https://loumo.jp/wp/archive/20131130214626/
    void Update()
    {
        //回転軸指定
        //myrigidbody.angularVelocity = new Vector3(0, 1, 0);

        var rota = this.transform.localEulerAngles;
        n_rota = rota;
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
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            //float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            //transform.RotateAround(targetPos, new Vector3(0.0f,1.0f,0.0f), mouseInputX * Time.deltaTime * 200f);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
            this.transform.rotation = Quaternion.Euler(0.0f, rota.y, 0.0f);
            rota.y = rota.y + mouseInputX * Time.deltaTime * 200f;
            this.transform.localEulerAngles = rota;
            
        }
        else
        {
            
            this.transform.rotation = Quaternion.Euler(0.0f, rota.y, 0.0f);
        }
        o_rota = n_rota;
    }
}
