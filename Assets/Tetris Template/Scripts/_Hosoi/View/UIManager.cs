using UnityEngine;
using System.Collections;
using UniRx;

namespace hosoi
{
public class UIManager : MonoBehaviour {

	[SerializeField] private MainMenu _mainMenu;
	[SerializeField] private InGameUI _inGameUI;
	[SerializeField] private PopUp _popUps;
	[SerializeField] private GameObject _activePopUp;
	[SerializeField] private GameObject _bgPanel;

	void Start()
	{
		_popUps.OnActivePopUpObservable.Subscribe(activePopUp => 
		{
			_activePopUp = activePopUp;
			Debug.Log("active:" + activePopUp);
		}).AddTo(this);
	}
	void Update()
	{
		// Popupの外側をクリックすると非表示にする機能
		if (_activePopUp != null)
			HideIfClickedOutside(_activePopUp);
	}
	public void StateAction(State state)
	{
		switch (state)
		{
			case State.Menu:
				StartCoroutine(ActivateMainMenu());
				_mainMenu.MainMenuStartAnimation();
				MainMenuArrange();
				break;
			case State.Play:
				_bgPanel.SetActive(false);
				StartCoroutine(ActivateInGameUI());
				break;
			case State.Pause:
				StartCoroutine(ActivateMainMenu());
				_mainMenu.MainMenuStartAnimation();
				MainMenuArrangeInPause();
				break;
			case State.Gameover:
				_popUps.ActivateGameOverPopUp();
				_bgPanel.SetActive(true);
				break;
			default:
				break;
		}
	}
	public void ButtonAction(string btn)
	{
		switch (btn)
		{
			case "restart":
				_popUps.InactivateGameOverPopUp();
				break;
			case "setting":
				_popUps.ActivateSettingsPopUp();
				_bgPanel.SetActive(true);
				break;
			case "stats":
				_popUps.ActivatePlayerStatsPopUp();
				_bgPanel.SetActive(true);
				break;
			case "home":
				_popUps.InactivateGameOverPopUp();
				_bgPanel.SetActive(false);
				break;
			case "sound":
				_popUps.SetSoundCross();
				break;
			default:
				break;
		}
	}

	IEnumerator ActivateMainMenu()
	{
		_inGameUI.InGameUIEndAnimation();
		_inGameUI.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		_mainMenu.gameObject.SetActive(true);
		_mainMenu.MainMenuStartAnimation();
	}

	IEnumerator ActivateInGameUI()
	{
		_mainMenu.MainMenuEndAnimation();
		yield return new WaitForSeconds(0.3f);
		_mainMenu.gameObject.SetActive(false);
		_inGameUI.gameObject.SetActive(true);
		_inGameUI.InGameUIStartAnimation();
	}

	// MenuとPauseでMain Menuの配置変える
	public void MainMenuArrangeInPause()
	{
		_mainMenu.layout.spacing = 20;
		_mainMenu.layout.padding.left = _mainMenu.layout.padding.right = 200;
		_mainMenu.restartButton.SetActive(true);
	}
	public void MainMenuArrange()
	{
		_mainMenu.layout.spacing = 50;
		_mainMenu.layout.padding.left = _mainMenu.layout.padding.right = 250;
		_mainMenu.restartButton.SetActive(false);
	}

	private void HideIfClickedOutside(GameObject outsidePanel)
	{
		// 左クリック + Popup表示中 + クリックした位置がPopup矩形内ではない場合
		if (Input.GetMouseButton(0) && outsidePanel.activeSelf &&
			!RectTransformUtility.RectangleContainsScreenPoint(
				outsidePanel.GetComponent<RectTransform>(),
				Input.mousePosition,
				Camera.main))
		{
			outsidePanel.SetActive(false);
			outsidePanel.transform.parent.gameObject.SetActive(false);
			_bgPanel.SetActive(false);
			_popUps.NullActivePopUp();
		}
	}

	public void UpdateCurrentScoreUI(int score)
	{
		_inGameUI.UpdateCurrentScoreUI(score);
	}
	public void UpdateHighScoreUI(int score)
	{
		_inGameUI.UpdateHighScoreUI(score);
		_popUps.SetScore(score);
	}
}
}