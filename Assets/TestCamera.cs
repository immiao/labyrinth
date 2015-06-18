using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TestCamera : MonoBehaviour {
	public GameObject center;
	private bool rotateFlag;
	private float tDegree, tTime;
	private float rRate, dRate;
	private Vector3 up;
	private Dictionary<Vector3, bool> dic;
	Vector2 p1,p2;//用来记录鼠标的位置，以便计算旋转幅度
	// Use this for initialization
	void Start () {
		center=GameObject.Find("Center");// Update is called once per frame
		//Debug.Log (center.transform.position);
		rotateFlag = false;
		tDegree = 0;

		//Application.targetFrameRate = 10;
		//framePerSecond = 
	}
	
	// Update is called once per frame
	void Update () {
		if (!rotateFlag ) {
			if (Input.GetKeyUp (KeyCode.Q)) {
				NewBehaviourScript.state = (NewBehaviourScript.state + 1) % 4;
				up = Vector3.up;
				rotateFlag = true;
				rRate = 360;
				tTime = 0;
				tDegree = 0;
			}
			else if (Input.GetKeyUp (KeyCode.E)) {
				NewBehaviourScript.state = (NewBehaviourScript.state + 3) % 4;
				up = -Vector3.up;
				rotateFlag = true;
				rRate = 360;
				tTime = 0;
				tDegree = 0;
			}
		}
		//Debug.Log (tTime);
		//Debug.Log (rRate);
		if (rotateFlag) {
			float degree = rRate * Time.deltaTime;
			transform.RotateAround (center.transform.position, up, degree);
			tDegree += degree;
			tTime += Time.deltaTime;

			if (tDegree >= 90) {
				rotateFlag = false;
			}
			else
				rRate = 360/(10*tTime + 1);
		}
//		if(Input.GetMouseButtonDown(1))
//		{
//			p1=new Vector2(Input.mousePosition.x,Input.mousePosition.y);//鼠标右键按下时记录鼠标位置p1
//		}
//		if(Input.GetMouseButton(1))
//		{
//			p2=new Vector2(Input.mousePosition.x,Input.mousePosition.y);//鼠标右键拖动时记录鼠标位置p2
//			//下面开始旋转，仅在水平方向上进行旋转
//			float dx=p2.x-p1.x;
//			Debug.Log (cube.transform.position);
//			transform.RotateAround(cube.transform.position,-Vector3.up,dx*Time.deltaTime);
//		}
	}
}
