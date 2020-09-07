using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class KodeUILog : MonoBehaviour
{
	public static KodeUILog Instance { get; private set; }

	StreamWriter fileStream;

	void Awake ()
	{
		if (Instance != null && Instance != this) {
			Debug.LogError ("[KodeUILog] Instance already exists");
			Destroy (this);
			return;
		}
		Instance = this;
	}

	void OnDestroy ()
	{
		if (Instance == this) {
			Instance = null;
		}
		if (fileStream != null) {
			fileStream.Flush ();
			fileStream.Close ();
		}
	}

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		Application.logMessageReceivedThreaded += LogCallback;
		fileStream = new StreamWriter ("KodeUI.log", false);
	}

	static string []logTypes = {
		"ERR",
		"ASR",
		"WRN",
		"LOG",
		"EXC",
	};

	void LogCallback(string logString, string stackTrace, LogType type)
	{
		string timeString = System.DateTime.Now.ToString("HH:mm:ss.fff");
		string logTypeStr = logTypes[(int) type];
		fileStream.WriteLine ($"[{logTypeStr} {timeString}] {logString}");
		try {
			if (type != LogType.Log) {
				fileStream.WriteLine (IndentLines (stackTrace));
			}
			fileStream.Flush ();
		} catch (Exception e) {
			if (e is ObjectDisposedException) {
				return;
			}
			Debug.LogError (e.Message);
		}
	}

	static string IndentLines (string text)
	{
		return ("\t" + text).Replace ("\n", "\n\t").TrimEnd ();
	}
}
