using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace hosoi
{
public class PlayerInputManager : MonoBehaviour
{
	// キー入力が有効か
	private bool _isActive;

	Subject<string> OnKeyInput = new Subject<string>();
	public IObservable<string> OnKeyInputObservable { get { return OnKeyInput; } }

	void Start()
	{
		EnableKeyInput();

		// キー入力
		this.UpdateAsObservable()
			.Where(_ => _isActive)
			.Where(_ => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
			.Subscribe(_ =>
			{
				// Aか↑を押したよ通知
				Debug.Log("push");
				OnKeyInput.OnNext("up");
			});
		// this.UpdateAsObservable()
		// 	.Where(_ => _isActive)
		// 	.Where(_ => Input.GetKeyDown(KeyCode.D))
		// 	.Subscribe(_ =>
		// 	{
		// 		// Dを押したよ通知
		// 		Debug.Log("d");
		// 	});
		// this.UpdateAsObservable()
		// 	.Where(_ => _isActive)
		// 	.Where(_ => Input.GetKeyDown(KeyCode.LeftArrow))
		// 	.Subscribe(_ =>
		// 	{
		// 		// ←を押したよ通知
		// 		Debug.Log("←");
		// 	});
		// this.UpdateAsObservable()
		// 	.Where(_ => _isActive)
		// 	.Where(_ => Input.GetKeyDown(KeyCode.RightArrow))
		// 	.Subscribe(_ =>
		// 	{
		// 		// →を押したよ通知
		// 	});
		// this.UpdateAsObservable()
		// 	.Where(_ => _isActive)
		// 	.Where(_ => Input.GetKeyDown(KeyCode.DownArrow))
		// 	.Subscribe(_ =>
		// 	{
		// 		// ↓を押したよ通知
		// 	});

	}

	public void EnableKeyInput()
	{
		_isActive = true;
	}


    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
            Managers.Game.currentShape.movementController.RotateClockWise(false);
        else if (Input.GetKeyDown(KeyCode.D))
            Managers.Game.currentShape.movementController.RotateClockWise(true);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.right);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Managers.Game.currentShape != null)
            {
                _isActive = false;
                Managers.Game.currentShape.movementController.InstantFall();
            }
        }
    }
}
}