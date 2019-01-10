using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Display some data from the current save game with formatted strings.
/// </summary>
public class SaveDataDisplay : MonoBehaviour {

	public string template;

	private SaveData saveData;

	private TextMesh textMesh;

	void Start () {
		saveData = GameProgressionPersistence.loadProgress();
		textMesh = GetComponent<TextMesh>();

		textMesh.text = AdvancedStringFormatting.format(template, saveData.toFormattedDictionary());
	}
}
