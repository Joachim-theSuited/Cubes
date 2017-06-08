using System;

/// <summary>
/// This enum contains all Inputs configured in the Input Manager for this project. They map to the string that can be used to access that specific input.
/// Using the enum is advised to avoid magic strings in the code.
/// </summary>
public static class Inputs
{
	public static readonly string Horizontal = "Horizontal";
	public static readonly string Vertical = "Vertical";

	public static readonly string Fire1 = "Fire1";
	public static readonly string Fire2 = "Fire2";
	public static readonly string Fire3 = "Fire3";

	public static readonly string Cancel = "Cancel";

	public static readonly string MouseX = "Mouse X";
	public static readonly string MouseY = "Mouse Y";
}

