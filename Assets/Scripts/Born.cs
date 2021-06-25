using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

	public GameObject playerPrefab;//玩家引用

	public GameObject[] enemyPrefabList;//敌人预置体列表

	public bool createrPlayer = false;//判断生成自己或敌人
	// Use this for initialization
	void Start () {
		//延时调用出生效果
		Invoke("BornTank", 1f);
		//延时销毁
		Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//出生效果
	private void BornTank()
    {
        if (createrPlayer)
        {
			Instantiate(playerPrefab, transform.position, Quaternion.identity);
		}
        else
        {
			int num = Random.Range(0, 2);//随机数产生敌人
			Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
		}
    }
}
