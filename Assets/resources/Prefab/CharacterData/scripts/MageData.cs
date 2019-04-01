﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Types;

//[CreateAssetMenuAttribute( fileName = "New Mage Data", menuName = "CharaData") ]
[CreateAssetMenu( fileName = "New Mage Data", menuName = "CharaData") ]
public class MageData : CharacterData {

	public MageDmgType dmgType;
	public MageWpnType wpnType;


}
