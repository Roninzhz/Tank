using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullect : MonoBehaviour {

	public float moveSpeed = 10;//速度 属性值

	public bool isPlayerBullect;//判断子弹类型
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//子弹移动
		transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}


    //子弹触发检测
    void OnTriggerEnter2D(Collider2D collider)
    {

        switch (collider.tag)
        {
            case "Tank"://坦克
                if (!isPlayerBullect)
                {
                    collider.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart"://老家
                collider.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy"://敌人
                if(isPlayerBullect)
                {
                    collider.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall"://墙壁
                Destroy(collider.gameObject);//销毁墙
                Destroy(gameObject);//销毁自身
                break;
            case "Barrier"://障碍
                if (isPlayerBullect)
                {
                    collider.SendMessage("PlayAudio");
                }
                Destroy(gameObject);//销毁自身
                break;
            default:
                break;
        }

    }
}
