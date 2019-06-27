using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceLabelBackgroundScaler : MonoBehaviour
{

	void Start ()
	{
		var textMesh = transform.parent.GetComponent<TextMesh>();
		float lineWidth = 0;
		float width = 0;
		float lineHeight = 0;
		float height = 0;
		foreach(char c in textMesh.text)
		{
			CharacterInfo characterInfo;
			bool success = textMesh.font.GetCharacterInfo(c, out characterInfo, textMesh.fontSize, textMesh.fontStyle);
			Debug.Assert(success);
			lineWidth += characterInfo.advance;
			lineHeight = Mathf.Max(lineHeight, characterInfo.glyphHeight);
			if (c == '\n')
			{
				width = Mathf.Max(width, lineWidth);
				lineWidth = 0;
				height += lineHeight;
				height += textMesh.lineSpacing;
				lineHeight = 0;
			}
		}
		width = Mathf.Max(width, lineWidth);
		height += lineHeight;

		// add a bit of a border
		width += width/textMesh.text.Length;
		height *= 1.5f;
		// scale with magic numbers
		width *= textMesh.characterSize * textMesh.transform.localScale.x * 0.05f;
		height *= textMesh.characterSize * textMesh.transform.localScale.x * 0.05f;
		transform.localScale = new Vector3(width, 1, height);
	}

}
