using UnityEngine;
using UniRx;
using System;

namespace hosoi
{
public class PopUp : MonoBehaviour
{
	[SerializeField] private GameObject _gameOverPopUp;
	[SerializeField] private GameObject _settingsPopUp;
	[SerializeField] private GameObject _playerStatsPopUp;
	[SerializeField] private GameObject _soundCross;

	private GameObject _activePopUp;
	private Subject<GameObject> OnActivePopUp = new Subject<GameObject>();
	public IObservable<GameObject> OnActivePopUpObservable { get { return OnActivePopUp; } }

	void Start()
	{
		// ActivePopUpの変更の登録
		this.ObserveEveryValueChanged(_ => _activePopUp)
			.Subscribe(activePopUp =>
			{
				OnActivePopUp.OnNext(activePopUp);
			})
			.AddTo(this);
	}
	public void ActivateGameOverPopUp()
	{
		_gameOverPopUp.transform.parent.gameObject.SetActive(true);
		_gameOverPopUp.SetActive(true);
		_activePopUp = _gameOverPopUp;
	}
	public void InactivateGameOverPopUp()
	{
		_gameOverPopUp.SetActive(false);
	}

	public void ActivateSettingsPopUp()
	{
		_settingsPopUp.transform.parent.gameObject.SetActive(true);
		_settingsPopUp.SetActive(true);
		_activePopUp = _settingsPopUp;
	}

	public void ActivatePlayerStatsPopUp()
	{
		_playerStatsPopUp.transform.parent.gameObject.SetActive(true);
		_playerStatsPopUp.SetActive(true);
		_activePopUp = _playerStatsPopUp;
	}

	public void SetScore(int score)
	{
		GameOverPopUp gameOver = _gameOverPopUp.GetComponent<GameOverPopUp>();
		gameOver.SetScore(score);
	}
	public void NullActivePopUp()
	{
		_activePopUp = null;
	}

	// Soundマークの表示
	public void SetSoundCross()
	{
		if (AudioListener.volume == 0)
		{
			_soundCross.SetActive(true);
		}
		else if (AudioListener.volume == 1.0f)
		{
			_soundCross.SetActive(false);
		}
	}



}
}