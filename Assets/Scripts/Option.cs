using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//引用以便加载游戏场景

public class Option : MonoBehaviour {

	//设置选项
	private int choice = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
        {
			choice = 1;
			//获取相对位置
			transform.position = new Vector3(201f, 88f, 0);
		}
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			choice = 2;
			//获取相对位置
			transform.position = new Vector3(201f, 68f, 0); 
		}
        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
			SceneManager.LoadScene(1);
        }
	}
}
