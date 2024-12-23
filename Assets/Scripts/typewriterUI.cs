using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class typewriterUI : MonoBehaviour
{
	Text _text;
	TMP_Text _tmpProText;
	string writer;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;
	public bool atStart = false;
	[Space]
	public bool useSlider=false;
	public Slider slider;

	[HideInInspector]
	public string textToWrite;
	[HideInInspector]
	public bool restart=false;
	Coroutine _parser;

	// Use this for initialization
	void Start()
	{
		if (atStart)
		{
			_text = GetComponent<Text>()!;
			_tmpProText = GetComponent<TMP_Text>()!;

			if (_text != null)
			{
				writer = _text.text;
				_text.text = "";

				StartCoroutine("TypeWriterText");
			}

			if (_tmpProText != null)
			{
				writer = _tmpProText.text;
				_tmpProText.text = "";

				StartCoroutine("TypeWriterTMP");
			}
		}
	}

    private void Update()
    {
		if(restart)
		{
			try
			{
				StopCoroutine(_parser);
			}
			catch (Exception e) { }

			_text = GetComponent<Text>()!;
			_tmpProText = GetComponent<TMP_Text>()!;

			if (_tmpProText != null)
			{
				writer = textToWrite;
				if (useSlider)
				{ slider.maxValue = writer.Length; slider.value = 0; }
				_parser = StartCoroutine("TypeWriterTMP");
			}

			restart = false;
		}
	}

    IEnumerator TypeWriterText()
	{
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (_text.text.Length > 0)
			{
				_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
			}
			_text.text += c;
			_text.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
		}
	}

	IEnumerator TypeWriterTMP()
	{
		_tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (_tmpProText.text.Length > 0)
			{
				_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
			}
			_tmpProText.text += c;
			_tmpProText.text += leadingChar;

			if (useSlider)
				slider.value += 1;

			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
		}
	}
}
