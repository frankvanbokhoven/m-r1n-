////////////////////////////////////////////
///Struct Name: RECT                     ///
///--------------------------------------///
///Author: Gildeoni Nogueira Santos      ///
///--------------------------------------///
///E-Mail: gildeoni@hotmail.com          ///
///--------------------------------------///
///Date: may/12/2002					 ///
///--------------------------------------///
///Description: A rectangle structure to ///
///            be used in the Windows API///
///            Methods.                  ///
////////////////////////////////////////////

using System;
using System.Drawing;
using System.Runtime.InteropServices;  

namespace FgDotNetControls
{
	/// <summary>
	/// Summary description for RECT.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)] 
	public struct RECT
	{
		[FieldOffset(0)] public int Left;

		[FieldOffset(4)] public int Top;

		[FieldOffset(8)] public int Right;

		[FieldOffset(12)] public int Bottom;

		public RECT(int left, int top, int right, int bottom) 
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public RECT(Rectangle rect) 
		{
			Left = rect.Left; 
			Top = rect.Top;
			Right = rect.Right;
			Bottom = rect.Bottom;
		}

		public Rectangle ToRectangle() 
		{
			return new Rectangle(Left, Top, Right, Bottom - 1);
		}
	}
}
