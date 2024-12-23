using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class AdvancedSentenceNode : BaseNode
{
	[Input] public int entry;
	[Output] public int yes;
	[Output] public int no;
	public string dialogue;

	public override string GetString()
	{
		string returnString = "ADV_sentence/" + dialogue;
		return returnString;
	}
}
