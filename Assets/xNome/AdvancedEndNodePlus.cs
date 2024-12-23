using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEndNodePlus : BaseNode
{
	[Input] public int entry;
	public string dialogue;

	public override string GetString()
	{
		string returnString = "ADV_Endplus/" + dialogue;
		return returnString;
	}
}
