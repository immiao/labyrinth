using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class range {
	public float s,e;
	public range(float x, float y) {
		s = x;
		e = y;
	}
}

public class NewBehaviourScript : MonoBehaviour {

	public float m_speed = 1;
	public static int state;
	private Dictionary<Vector2, range> [] d;
	private bool winFlag;
	private GameObject aim;
//	private Dictionary<Vector2, range> xxx;
	// Use this for initialization
	void Start () {
		winFlag = false;
		aim = GameObject.Find ("Aim");
		d = new Dictionary<Vector2, range>[20];
//		xxx = new Dictionary<Vector2, range>();
//		xxx.Add(new Vector2(0, 0), new range(-10, 0));
		for (int i = 0; i < 20; i++)
			d[i] = new Dictionary<Vector2, range>();

		state = 0;
		d[0].Add (new Vector2(0, 0), new range(0, 100));
		d[0].Add (new Vector2(0, -100), new range(-100, 0));
		d[0].Add (new Vector2(0, -200), new range(-100, 100));
		d[0].Add (new Vector2(100, -100), new range(-100, 0));
		d[0].Add (new Vector2(100, -200), new range(0, 100));
		d[0].Add (new Vector2(200, -100), new range(-100, 0));
		d[0].Add (new Vector2(200, -200), new range(-100, 0));

		d[1].Add (new Vector2(100, 0), new range(100, 200));
		d[1].Add (new Vector2(100, -100), new range(0, 200));
		d[1].Add (new Vector2(100, -200), new range(0, 200));
		d[1].Add (new Vector2(0, -100), new range(0, 100));
		d[1].Add (new Vector2(0, -200), new range(100, 200));
		d[1].Add (new Vector2(-100, -200), new range(0, 200));
		d[1].Add (new Vector2(-100, -100), new range(0, 200));
		 
		d[2].Add (new Vector2(-100, 0), new range(-100, 0)); 
		d[2].Add (new Vector2(0, 0), new range(-100, 0)); 
		d[2].Add (new Vector2(0, 200), new range(-200, -100));
		d[2].Add (new Vector2(100, 100), new range(-100, 0)); 
		d[2].Add (new Vector2(100, 0), new range(-200, -100)); 
		d[2].Add (new Vector2(100, 200), new range(-200, -100)); 
	}
	void xForward(ref float movex, ref Vector3 p) {
		int ry = Mathf.RoundToInt (p.y);
		int rz = Mathf.RoundToInt (p.z);
		Vector2 v = new Vector2(ry, rz);
		transform.position = new Vector3(p.x, ry, rz);
		if (d[0].ContainsKey (v) && p.x >= d[0][v].s && p.x <= d[0][v].e) {
			movex = m_speed * Time.deltaTime;
			if (movex + p.x >= d[0][v].e) {
				transform.position = new Vector3(d[0][v].e, ry, rz);
				movex = 0;
			}
		}
	}
	void xBack(ref float movex, ref Vector3 p) {
		int ry = Mathf.RoundToInt (p.y);
		int rz = Mathf.RoundToInt (p.z);
		Vector2 v = new Vector2(ry, rz);
		transform.position = new Vector3(p.x, ry, rz);
		if (d[0].ContainsKey (v) && p.x >= d[0][v].s && p.x <= d[0][v].e) {
			movex = -m_speed * Time.deltaTime;
			if (movex + p.x <= d[0][v].s) {
				transform.position = new Vector3(d[0][v].s, ry, rz);
				movex = 0;
			}
		}
	}
	void yForward(ref float movey, ref Vector3 p) {
		int rx = Mathf.RoundToInt (p.x);
		int rz = Mathf.RoundToInt (p.z);
		Vector2 v = new Vector2(rx, rz);
		transform.position = new Vector3(rx, p.y, rz);
		if (d[1].ContainsKey (v) && p.y >= d[1][v].s && p.y <= d[1][v].e) {
			movey = m_speed * Time.deltaTime;
			if (movey + p.y >= d[1][v].e) {
				transform.position = new Vector3(rx, d[1][v].e, rz);
				movey = 0;
			}
		}
	}
	void yBack(ref float movey, ref Vector3 p) {
		int rx = Mathf.RoundToInt (p.x);
		int rz = Mathf.RoundToInt (p.z);
		Vector2 v = new Vector2(rx, rz);
		transform.position = new Vector3(rx, p.y, rz);
		if (d[1].ContainsKey (v) && p.y >= d[1][v].s && p.y <= d[1][v].e) {
			movey = -m_speed * Time.deltaTime;
			if (movey + p.y <= d[1][v].s) {
				transform.position = new Vector3(rx, d[1][v].s, rz);
				movey = 0;
			}
		}
	}
	void zForward(ref float movez, ref Vector3 p) {

		int rx = Mathf.RoundToInt (p.x);
		int ry = Mathf.RoundToInt (p.y);
		Vector2 v = new Vector2(rx, ry);
		transform.position = new Vector3(rx, ry, p.z);
		if (d[2].ContainsKey (v) && p.z >= d[2][v].s && p.z <= d[2][v].e) {
			movez = m_speed * Time.deltaTime;
			if (movez + p.z >= d[2][v].e) {
				transform.position = new Vector3(rx, ry, d[2][v].e);
				movez = 0;
			}
		}
	}
	void zBack(ref float movez, ref Vector3 p) {
		int rx = Mathf.RoundToInt (p.x);
		int ry = Mathf.RoundToInt (p.y);
		Vector2 v = new Vector2(rx, ry);
		transform.position = new Vector3(rx, ry, p.z);
		if (d[2].ContainsKey (v) && p.z >= d[2][v].s && p.z <= d[2][v].e) {
			movez = -m_speed * Time.deltaTime;
			if (movez + p.z <= d[2][v].s) {
				transform.position = new Vector3(rx, ry, d[2][v].s);
				movez = 0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale > 0 && Input.GetKeyDown (KeyCode.Escape))
			Time.timeScale = 0;
		Vector3 p = transform.position;
		Vector3 pAim = aim.transform.position;

		if (Mathf.Abs (p.x - pAim.x) < 2 && Mathf.Abs (p.y - pAim.y) < 2 && Mathf.Abs (p.z - pAim.z) < 2)
			winFlag = true;

		float movex = 0;
		float movez = 0;
		float movey = 0;

		if (Input.GetKey (KeyCode.W)) {
			if (state == 0) 
				zForward (ref movez, ref p);
			else if (state == 1) 
				xForward (ref movex, ref p);
			else if (state == 2) 
				zBack (ref movez, ref p);
			else 
				xBack(ref movex, ref p);
		}
		else if (Input.GetKey (KeyCode.S)) {
			if (state == 0)
				zBack (ref movez, ref p);
			else if (state == 1)
				xBack(ref movex, ref p);
			else if (state == 2)
				zForward (ref movez, ref p);
			else 
				xForward (ref movex, ref p);
		}
		else if (Input.GetKey (KeyCode.A)) {
			if (state == 0) 
				xBack(ref movex, ref p);
			else if (state == 1) 
				zForward (ref movez, ref p);
			else if (state == 2) 
				xForward (ref movex, ref p);
			else 
				zBack (ref movez, ref p);
		}
		else if (Input.GetKey (KeyCode.D)) {
			if (state == 0) 
				xForward (ref movex, ref p);
			else if (state == 1)
				zBack (ref movez, ref p);
			else if (state == 2) 
				xBack(ref movex, ref p);
			else 
				zForward (ref movez, ref p);
		}

		else if (Input.GetKey (KeyCode.UpArrow)) 
			yForward (ref movey, ref p);
		else if (Input.GetKey (KeyCode.DownArrow)) 
			yBack (ref movey, ref p);
		if (Input.GetKey (KeyCode.G))
			movex = -m_speed * Time.deltaTime;
		//Debug.Log (transform.position);
		transform.Translate (new Vector3 (movex, movey, movez));
		//Debug.Log (transform.position);
	}

	void OnGUI() {
		if (Time.timeScale == 0) {
			if (GUI.Button (new Rect(Screen.width * 0.5f - 50, Screen.height * 0.4f, 100, 30), "继续游戏"))
				Time.timeScale = 1;
			if (GUI.Button (new Rect(Screen.width * 0.5f -50, Screen.height * 0.6f, 100, 30), "退出游戏"))
				Application.Quit ();
		}

		if (winFlag == true) {
			GUI.skin.label.fontSize = 50;
			GUI.skin.label.alignment = TextAnchor.LowerCenter;
			GUI.Label (new Rect(0, Screen.height * 0.2f, Screen.width, 60), "恭喜通关！");
			if (GUI.Button (new Rect(Screen.width * 0.5f -50, Screen.height * 0.6f, 100, 30), "退出游戏"))
				Application.Quit ();
		}
	}
}
