using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	private SpriteRenderer sr;//游戏组件引用

	public GameObject explosionPrefab;//爆炸效果引用
	
	public Sprite BrokenSprite;//获取死亡图片

	public AudioClip dieAudio;//音频
	// Use this for initialization
	void Start () {

		//指定控件
		sr = GetComponent<SpriteRenderer>();//得到组件
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//死亡
	public void Die()
    {
		//死亡
		sr.sprite = BrokenSprite;
		//产生爆炸特效 
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		//战败
		PlayerManager.Instance.isDefeat = true;
		//播放死亡音效
		AudioSource.PlayClipAtPoint(dieAudio, transform.position);
	}
}
