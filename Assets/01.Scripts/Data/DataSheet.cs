using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DataSheet : ScriptableObject
{
	public List<MonsterData> MonsterList; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemData> ItemList; // Replace 'EntityType' to an actual type that is serializable.
}
