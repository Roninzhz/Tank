using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

	public AudioClip hitAudio;//障碍物碰撞音效

	public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
