using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//引用
using UnityEngine.SceneManagement;//场景

public class PlayerManager : MonoBehaviour {

	//属性值
	public int lifeValue = 3;//生命值
	public int playScore = 0;//得分
    public bool isDead;//生死
    public bool isDefeat;//战败

    //引用
    public GameObject born;//出生
    public Text PlayerScoreText;//击杀
    public Text PlayerLifeValueText;//生命值
    public GameObject isDefaultUI;//战败图片

	//单例
	private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    //游戏一开始

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //实时监听
        if (isDefeat)//战败
        {
            isDefaultUI.SetActive(true);
            //调用菜单方法
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead)//死亡
        {
            Recover();
        }
        //更新生命值和击杀
        PlayerLifeValueText.text = lifeValue.ToString();
        PlayerScoreText.text = playScore.ToString();
	}

    //复活方法
    private void Recover()
    {
        if (lifeValue <= 0)
        {
            //游戏失败,返回主界面,显示失败图片
            isDefeat = true;
            //调用菜单方法
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            //生命减一
            lifeValue--;
            //接收出生
            GameObject go = Instantiate(born, new Vector3(-2, 8, 0), Quaternion.identity);
            //拿取born脚本
            go.GetComponent<Born>().createrPlayer = true;
            isDead = false;
        }
    }

    //加载场景
    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
