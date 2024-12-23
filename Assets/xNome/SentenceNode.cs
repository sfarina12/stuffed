using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SentenceNode : BaseNode {

	[Input] public int entry;
	[Output] public int exit;
	public string dialogue;
	public bool isAnimated;
	public List<string> animationNames;

	public override string GetString()
	{
		string returnString = "SentenceNode/" + dialogue + "/" + isAnimated;

		for (int i = 0; i < animationNames.Count; i++)
			returnString += "/" + animationNames[i];

		return returnString;
	}
}