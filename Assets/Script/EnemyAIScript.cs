﻿using UnityEngine;
using System.Collections;

public class EnemyAIScript : MonoBehaviour {

	public Assets.Script.CharacterController2D.AbsCharacterController characterController;


	public int WaitFramesMin = 10;
	public int WaitFramesMax = 60;
	public int KeyDownMin = 30;
	public int KeyDownMax = 120;

	private int _waiting;
	private int _keyCnt;
	private bool _isPerforming;
	private int _key;
	private string _descr;

	private const int JUMP_LEFT = -1;
	private const int JUMP_RIGHT = -2;

	private int[] _operations = { JoypadCode.JUMP, JoypadCode.LEFT, JoypadCode.RIGHT, JUMP_LEFT, JUMP_RIGHT };
	private string[] _descriptions = { "jump", "left", "right", "jump left", "jump right" };

	// Use this for initialization
	void Start () {
		_isPerforming = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_isPerforming) {
		
			if (--_keyCnt > 0) {
				if (_key == JUMP_LEFT) {
					characterController.OnKeyDown(JoypadCode.JUMP);
					characterController.OnKeyDown(JoypadCode.LEFT);
				} else if (_key == JUMP_RIGHT) {
					characterController.OnKeyDown(JoypadCode.JUMP);
					characterController.OnKeyDown(JoypadCode.RIGHT);
				} else {
					characterController.OnKeyDown(_key);
				}

			} else {
				_isPerforming = false;
				_waiting = Random.Range(WaitFramesMin, WaitFramesMax);
			}

		} else {

			if (--_waiting <= 0 && characterController.data.collisionInfo.IsOnGround) {
				_isPerforming = true;
				int i = Random.Range(0, _operations.Length);
				_key = _operations[i];
				_descr = _descriptions[i];
				
				_keyCnt = Random.Range(KeyDownMin, KeyDownMax);
			}
		}

		if (_waiting > 0) {
			characterController.debug.AddLine("WAITING: " + _waiting);
		} else {
			characterController.debug.AddLine("EXECUTING: " + _descr);
		}

		
	}
}
