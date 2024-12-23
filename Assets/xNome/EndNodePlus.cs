using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class EndNodePlus : BaseNode
{

	[Input] public int entry;
	public bool isEnabled;
	public override string GetString()
	{
		string returnString = "EndPlus";
		returnString += "/" + isEnabled;

		return returnString;
	}
}