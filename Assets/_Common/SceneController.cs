﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace UnityCommon
{
	public enum EscBehavior
	{
		Nothing,
		ExitScene,
		ExitGame,
		OpenMenu,
	}

	public class SceneController : MonoBehaviour {

		public static float loadingProgress = 0f;
		public static float previousVolume = 1f;

		public SceneField defaultExitScene;
		public bool asychronousLoad;
		[Space]
		public bool fadeIn = true;
		public float fadeInDuration = 1f;
		public Color fadeInColor = Color.black;
		public bool fadeInAudio = false;
		[Space]
		public bool fadeOut = true;
		public float fadeOutDuration = 1f;
		public Color fadeOutColor = Color.black;
		[Tooltip("Note: if using Fade Out Audio, the scene being loaded must also have a SceneController component to restore the volume level!")]
		public bool fadeOutAudio = false;
		[Space]

		public EscBehavior escapeBehavior;
		public GameObject menu;


		private static SceneController instance;

		void Start()
		{
			//Only to allow a static method access this instance
			if (instance != null)
				Debug.Log (string.Format("Warning: There should only be one instance of {0} per scene", name) );
			else
				instance = this;

			//Audio
			if (fadeInAudio)
				AudioListener.volume = 0f;
			else
				AudioListener.volume = previousVolume;

			//Option Menu
			if (menu != null)
				menu.SetActive (false);

			//Start
			StartScene ();

		}

		void Update()
		{
			if (Input.GetButtonDown ("Cancel"))
			{
				if (escapeBehavior == EscBehavior.ExitGame)
					Application.Quit ();
				else if (escapeBehavior == EscBehavior.ExitScene && defaultExitScene != "")
					ChangeLevel ();
				else if (menu != null)
				{
					if (! CloseOnEscape.CloseTop() && escapeBehavior == EscBehavior.OpenMenu)
					{
						menu.SetActive (true);
					}

				}

			}

		}

		void StartScene()
		{
			StartCoroutine (StartSceneCoroutine ());
		}

		IEnumerator StartSceneCoroutine()
		{
			if (fadeIn)
			{
				Fader.FadeIn (fadeInColor, fadeInDuration);
			}

			float startTime = Time.time;
			float endTime = startTime + fadeInDuration;

			while (Time.time < endTime)
			{
				yield return null;

				if (fadeInAudio)
				{
					float t = Mathf.InverseLerp (startTime, endTime, Time.time);
					AudioListener.volume = Mathf.Lerp (0f, previousVolume, t);
				}
			}

		}

		public static void ChangeScene(SceneField scene = null)
		{
			instance.StartCoroutine(instance.ChangeSceneCoroutine(scene));
		}

		public void ChangeLevel(string sceneName = "")
		{
			if (sceneName == "")
				StartCoroutine (ChangeSceneCoroutine ());
			else
			{
				StartCoroutine (ChangeSceneCoroutine (new SceneField (sceneName)));
			}
		}
		public IEnumerator ChangeSceneCoroutine(SceneField scene = null)
		{
			float startTime = Time.time;
			float endTime = startTime + fadeOutDuration;

			if (fadeOut)
			{
				Fader.FadeOut (fadeOutColor, fadeOutDuration);
			}

			previousVolume = AudioListener.volume;

			while (Time.time < endTime)
			{
				yield return null;

				if (fadeOutAudio)
				{
					float t = Mathf.InverseLerp (startTime, endTime, Time.time);
					AudioListener.volume = Mathf.Lerp (previousVolume, 0f, t);
				}

			}

			if (scene == null)
				scene = defaultExitScene;

			//LoadScene
			if (asychronousLoad)
				AsynchronousLoad (scene);
			else
				SceneManager.LoadScene (scene);

		}

		void AsynchronousLoad (SceneField sceneName)
		{
			StartCoroutine(AsynchronousLoadCoroutine (sceneName));
		}
		IEnumerator AsynchronousLoadCoroutine (SceneField sceneName)
		{
			yield return null;

			AsyncOperation ao = SceneManager.LoadSceneAsync (sceneName);
			ao.allowSceneActivation = false;

			while (! ao.isDone)
			{
				float progress = Mathf.Clamp01(ao.progress / 0.9f);

				Debug.Log("Loading progress: " + (progress * 100) + "%");

				if (ao.progress >= 0.9f)
				{
					ao.allowSceneActivation = true;
				}

				yield return null;
			}
		}

	}
}