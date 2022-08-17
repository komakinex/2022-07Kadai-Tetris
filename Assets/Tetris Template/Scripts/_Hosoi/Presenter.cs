using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace hosoi
{
public class Presenter : MonoBehaviour
{
	// View
	// [SerializeField] private CameraManager _cameraManager;
	// [SerializeField] private ColorManager _colorManager;
	// [SerializeField] private GridManager _gridManager;
	// [SerializeField] private SpawnManager _spawnManager;
	// [SerializeField] private UIManager _uiManager;

	// Model
	// [SerializeField] private GameManager _gameManager;
	[SerializeField] private PlayerInputManager _inputManager;
	// [SerializeField] private ScoreManager _scoreManager;
	// // other
	// [SerializeField] private AudioManager _audioManager;

	void Awake ()
	{
		// シーンを切り替えても破棄されないようにしている
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		// いろんなイベントが飛んできた時の対応を書いておく
		// キー入力
		_inputManager.OnKeyInputObservable.Subscribe(key =>
		{
			switch (key)
			{
				case "up":
					break;
				case "left":
					break;
				default:
					break;
			}
			Debug.Log(key);
		}).AddTo(this);
	}
}
}