using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMoveManager : MonoBehaviour {

	public GameObject monsterDance1;
	public GameObject monsterDance2;
	public GameObject monsterDance3;

	private Animation anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MonsterDance1() {
		anim = monsterDance1.GetComponent<Animation>();
		monsterDance1.SetActive(true);
		monsterDance2.SetActive(false);
		monsterDance3.SetActive(false);
		anim.Play();
	}

	public void MonsterDance2() {
		anim = monsterDance2.GetComponent<Animation>();
        monsterDance1.SetActive(false);
        monsterDance2.SetActive(true);
        monsterDance3.SetActive(false);
        anim.Play();
	}

	public void MonsterDance3() {
		anim = monsterDance2.GetComponent<Animation>();
        monsterDance1.SetActive(false);
        monsterDance2.SetActive(false);
        monsterDance3.SetActive(true);
        anim.Play();
	}
}
