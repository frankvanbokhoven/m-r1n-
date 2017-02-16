using System;

namespace FgDotNetControls
{
	/// <summary>
	/// The style to draw the button.
	/// </summary>
	public enum Style
	{
		XpStyle, Normal
	}

	/// <summary>
	/// The type of the button.
	/// </summary>
	internal enum ButtonType 
	{
		PushButton = 1, RadioButton,
		CheckBox, GroupBox, 
		UserButton
	}

	/// <summary>
	/// The state of the button.
	/// </summary>
	internal enum ButtonState
	{
		Normal = 1, Hot, 
		Pressed, Disabled,
		Defaulted
	}

	internal enum FormatValues
	{
		Left = 0,
		Top = 0,
		Center = 1,
		Right = 2,
		VCenter = 4,
		Bottom = 8,
		WordBreak = 16,
		SingleLine = 32,
		ExpandTabs = 64,
		TabStop = 128,
		NoClip = 256,
		ExternalLeading = 512,
		CalcRect = 1024,
		NoPrefix = 2048,
		EditControl = 8192,
		PathEllipses = 16384,
		EndEllipses = 32768,
		ModifyString = 65536,
		RtlReading = 131072,
		WordEllipses = 262144
	}

	internal enum WindowsMessage 
	{
		WmThemeChanged = 794
	}
}
