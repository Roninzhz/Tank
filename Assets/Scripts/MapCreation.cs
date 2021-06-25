using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//场景

public class MapCreation : MonoBehaviour {

	public GameObject[] item;//友来装饰初始化地图的数组
							 //0 老家 1 墙 2 障碍 3 出生效果 4 河流 5 草 6 空气墙

	//已有东西的位置列表
	private List<Vector3> itemPositionList = new List<Vector3>();
	private void Awake()
    {
		InitMap();
	}

	//初始化地图
	private void InitMap()
    {
		//0实例化老家
		CreateItem(item[0], new Vector3(0, -7, 0), Quaternion.identity);
		//1用墙将老家围起来
		CreateItem(item[1], new Vector3(-1, -7, 0), Quaternion.identity);
		CreateItem(item[1], new Vector3(1, -7, 0), Quaternion.identity);
		for (int i = -1; i < 2; i++)
		{
			CreateItem(item[1], new Vector3(i, -6, 0), Quaternion.identity);
		}

		//实例化围墙
		for (int i = -11; i < 12; i++)//上
		{
			CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
		}
		for (int i = -11; i < 12; i++)//下
		{
			CreateItem(item[6], new Vector3(i, -8, 0), Quaternion.identity);
		}
		for (int i = -8; i < 9; i++)//左
		{
			CreateItem(item[6], new Vector3(-10, i, 0), Quaternion.identity);
		}
		for (int i = -8; i < 9; i++)//右
		{
			CreateItem(item[6], new Vector3(10, i, 0), Quaternion.identity);
		}

		//实例化玩家
		GameObject go = Instantiate(item[3], new Vector3(6, -6, 0), Quaternion.identity);
		go.GetComponent<Born>().createrPlayer = true;
		//产生敌人
		CreateItem(item[3], new Vector3(-8, 8, 0), Quaternion.identity);
		CreateItem(item[3], new Vector3(0, 0, 0), Quaternion.identity);
		CreateItem(item[3], new Vector3(8, 8, 0), Quaternion.identity);

		//随机生成敌人
		InvokeRepeating("CreateEnemy", 4, 5);
		//实例化地图
		//不可破的墙
		for (int i = 0; i < 15; i++)
		{
			CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
		}
		//可打破的墙
		for (int i = 0; i < 15; i++)
		{
			CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
		}
		//河流
		for (int i = 0; i < 15; i++)
		{
			CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
		}
		//草
		for (int i = 0; i < 15; i++)
		{
			CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
		}
	}

	//封装
	private void CreateItem(GameObject createGameObject,Vector3 createPosition, Quaternion createRotation)
    {
		GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
		itemGo.transform.SetParent(gameObject.transform);
		itemPositionList.Add(createPosition);
    }

	//产生随机位置的方法
	private Vector3 CreateRandomPosition()
    {
		//不生成x=-10,10的两列，y=-8,8正两行的位置
		while(true)
        {
			Vector3 createPosition = new Vector3(Random.Range(-9, 10),Random.Range(-7,8),0);
            if (!HasThePosition(createPosition))
            {
				return createPosition;
			}
        }
    }

	//用来判断位置列表中是否有这个位置

	private bool HasThePosition(Vector3 createPos)
    {
		for(int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
				return true;
            }
        }
		return false;
    }
	//产生敌人的方法
	private void CreateEnemy()
	{
		int num = Random.Range(0, 3);
		Vector3 EnemyPos = new Vector3();
		if (num == 0)
		{
			EnemyPos = new Vector3(-8, 8, 0);
		}
		else if (num == 1)
		{
			EnemyPos = new Vector3(0, 8, 0);
		}
		else
		{
			EnemyPos = new Vector3(8, 8, 0);
		}
		CreateItem(item[3], EnemyPos, Quaternion.identity);
	}
}


