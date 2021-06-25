using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float movespeed=3;//定义移动速度 属性值

	private Vector3 bullectEulerAngles;//子弹欧拉角度

	private float timeVal;//子弹发射时间间隔定时器

	private float defendTimeVal=3;//被保护状态定时器

	private bool isDefended=true;//被保护状态

	private SpriteRenderer sr;//渲染器 引用
	
	public Sprite[] tankSprite;//图片引用 上右下左数组 从0开始

	public GameObject bullecyPrefab;//子弹引用

	public GameObject explosionPrefab;//爆炸体引用

	public GameObject defenfEffectPrefab;//受保护预制体引用

	public AudioSource moveAudio;//音效

	public AudioClip[] tankAudio;//音效数组
	//引用
	private void Awake()
    {
		sr = GetComponent<SpriteRenderer>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//是否处于无敌状态
		if(isDefended)
        {
			//预制体显示和时间
			defenfEffectPrefab.SetActive(true);
			defendTimeVal -= Time.deltaTime;
			if(defendTimeVal<=0)
            {
				isDefended = false;
				defenfEffectPrefab.SetActive(false);
            }
        }
	}

	//去抖(固定物理帧)
	private void FixedUpdate()
    {
		//如果战败，无法移动和攻击
		if(PlayerManager.Instance.isDefeat==true)
        {
			return;
        }

		Move();//移动

		//设置攻击间隔CD
		if (timeVal >= 0.4)
		{
			Attack();
		}
		else
		{
			timeVal += Time.fixedDeltaTime;
		}
	}


	//坦克攻击
	private void Attack()
    {
		//空格键实例化子弹
		if (Input.GetKeyDown(KeyCode.Space))
        {
			//子弹产生的角度：当前坦克的角度+子弹应该旋转的角度 （api EulerAngles）
			Instantiate(bullecyPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bullectEulerAngles));

			//计时器清0
			timeVal = 0;
        }
    }

	//坦克移动
	private void Move()
    {
		//监听用户垂直轴的输入
		float v = Input.GetAxisRaw("Vertical");
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

		//判断移动播放音效
        if (Mathf.Abs(v) > 0.05f)
        {
			//获取音效
			moveAudio.clip = tankAudio[1];
			//播放音效
            if (!moveAudio.isPlaying)//判断是否播放
            {
				moveAudio.Play();
			}
        }
		//设置优先级防止两键冲突
		if (v!=0)
        {
			return;
        }

		//监听用户水平轴的输入
		float h = Input.GetAxisRaw("Horizontal");

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

		//判断移动播放音效
		if (Mathf.Abs(h) > 0.05f)
		{
			//获取音效
			moveAudio.clip = tankAudio[1];
			//播放音效
			if (!moveAudio.isPlaying)//判断是否播放
			{
				moveAudio.Play();
			}
		}
        else
        {
			//获取音效
			moveAudio.clip = tankAudio[0];
			//播放音效
			if (!moveAudio.isPlaying)//判断是否播放
			{
				moveAudio.Play();
			}
		}
	}

	//坦克死亡
	private void Die()
    {
		//判断无敌状态
		if(isDefended)
        {
			return;
        }
		//玩家管理
		PlayerManager.Instance.isDead = true;
		//产生爆炸特效
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		//死亡
		Destroy(gameObject);
    }
}
