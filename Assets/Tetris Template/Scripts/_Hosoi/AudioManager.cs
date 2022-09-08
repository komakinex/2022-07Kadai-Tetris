using UnityEngine;

namespace hosoi
{
public class AudioManager : MonoBehaviour
{
	#region Game Spesific
	public AudioClip dropSound;
	public AudioClip lineClearSound;
	#endregion

	#region Template Fields
	// public AudioSource musicSource;
	public AudioSource soundSource;

	// public AudioClip gameMusic;
	public AudioClip uiClick;
	// public AudioClip winSound;
	public AudioClip loseSound;
	// public AudioClip popUpOpen;
	// public AudioClip popUpClose;
	#endregion

	public void StateAction(State state)
	{
		switch (state)
		{
			case State.Menu:
				break;
			case State.Play:
				break;
			case State.Pause:
				break;
			case State.Gameover:
				PlayLoseSound();
				break;
			default:
				break;
		}
	}
	public void ButtonAction(string btn)
	{
		switch (btn)
		{
			case "sound":
				if (AudioListener.volume == 0)
				{
					AudioListener.volume = 1.0f;
					PlayUIClick();
				}
				else if (AudioListener.volume == 1.0f)
				{
					AudioListener.volume = 0f;
				}
				break;
			default:
				PlayUIClick();
				break;
		}
	}

	public void PlaySound(string audio)
	{
		switch (audio)
		{
			case "drop":
				PlayDropSound();
				break;
			case "clear":
				PlayLineClearSound();
				break;
			default:
				break;
		}
	}
	private void PlayDropSound()
	{
		soundSource.clip = dropSound;
		soundSource.Play ();
	}
	private void PlayLineClearSound()
	{
		soundSource.clip = lineClearSound;
		soundSource.Play();
	}
	private void PlayLoseSound()
	{
		// StopGameMusic ();
		soundSource.clip = loseSound;
		soundSource.Play ();
	}

	private void PlayUIClick()
	{
		soundSource.clip = uiClick;
		soundSource.Play ();
	}

	// 使ってません！

	// public void PlayWinSound()
	// {
	// 	StopGameMusic ();
	// 	soundSource.clip = winSound;
	// 	soundSource.Play ();
	// }
	// public void SetSoundFxVolume(float value)
	// {
	// 	float temp = value + soundSource.volume;
	// 	if (temp < 0 || temp > 1)
	// 		return;
	// 	else
	// 		soundSource.volume += value;
	// }

	// 	public void PlayGameMusic()
	// 	{
	// 		musicSource.clip = gameMusic;
	// 		musicSource.Play ();
	// 	}

	// 	public void StopGameMusic()
	// 	{
	// 		musicSource.Stop ();
	// 	}

	// 	public void SetSoundMusicVolume(float value)
	// 	{
	// 		float temp = value + musicSource.volume;
	// 		if (temp < 0 || temp > 1)
	// 			return;
	// 		else
	// 			musicSource.volume += value;
	// 	}

}
}