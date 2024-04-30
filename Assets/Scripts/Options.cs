using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Options", menuName = "OptionsMenu", order = 0)]
public class Options : ScriptableObject
{
	[Range(0.0f, 100.0f)]
	public int SoundAdjustment;

}
