using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public float movespeed = 3;//定义移动速度 属性值

	private Vector3 bullectEulerAngles;//子弹欧拉角度

	private float v=-1;//初始往下移动
	private float h;

	private SpriteRenderer sr;//渲染器 引用

	public Sprite[] tankSprite;//图片引用 上右下左数组 从0开始

	public GameObject bullecyPrefab;//子弹引用

	public GameObject explosionPrefab;//爆炸体引用

	//计时器
	private float timeVal;//子弹发射时间间隔定时器

	private float timeValChangeDirection=0;//调转方向计时器
										 //引用
	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		

		//设置攻击间隔
		if (timeVal >= 3)
		{
			Attack();
		}
		else
		{
			timeVal += Time.deltaTime;
		}
	}

	//去抖(固定物理帧)
	private void FixedUpdate()
	{
		Move();//移动
	}


	//坦克攻击
	private void Attack()
	{
			//子弹产生的角度：当前坦克的角度+子弹应该旋转的角度 （api EulerAngles）
			Instantiate(bullecyPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));

			//计时器清0
			timeVal = 0;
	}

	//坦克移动
	private void Move()
	{

        if (timeValChangeDirection >= 4)
        {
			int num = Random.Range(0, 8);
			if(num>5)
            {
				v = -1;
				h = 0;
            }
			else if(num==0)
            {
				v = 1;
				h = 0;
            }
			else if(num>0&&num<=2)
            {
				h = -1;
				v = 0;
            }
			else if (num > 2 && num <= 4)
			{
				h = 1;
				v = 0;
			}
			timeValChangeDirection = 0;
		}
		else
		{
			timeValChangeDirection += Time.fixedDeltaTime;
		}
		//控制玩家移动x轴正方向 WS方向上下 
		transform.Translate(Vector3.up * v * movespeed * Time.fixedDeltaTime, Space.World);

		//坦克调转方向
		if (v < 0)//下
		{
			sr.sprite = tankSprite[2];//坦克方向
			bullectEulerAngles = new Vector3(0, 0, -180);//子弹角度
		}
		else if (v > 0)//上
		{
			sr.sprite = tankSprite[0];//坦克方向
			bullectEulerAngles = new Vector3(0, 0, 0);//子弹角度
		}
		//设置优先级防止两键冲突
		if (v != 0)
		{
			return;
		}

		//控制玩家移动y轴正方向 AD方向左右 
		transform.Translate(Vector3.right * h * movespeed * Time.fixedDeltaTime, Space.World);

		//坦克调转方向通过调换图片
		if (h < 0)//左
		{
			sr.sprite = tankSprite[3];//坦克方向
			bullectEulerAngles = new Vector3(0, 0, 90);//子弹角度
		}
		else if (h > 0)//右
		{
			sr.sprite = tankSprite[1];//坦克方向
			bullectEulerAngles = new Vector3(0, 0, -90);//子弹角度
		}
	}

	//坦克死亡
	private void Die()
	{
		//击杀得分
		PlayerManager.Instance.playScore++;
		//产生爆炸特效
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		//死亡
		Destroy(gameObject);
	}

    //触碰到顺势改变方向
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timeValChangeDirection = 4;
        }
    }

}
