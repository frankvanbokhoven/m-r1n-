////////////////////////////////////////////
///Class Name: App                       ///
///--------------------------------------///
///Author: Gildeoni Nogueira Santos      ///
///--------------------------------------///
///E-Mail: gildeoni@hotmail.com          ///
///--------------------------------------///
///Date: may/12/2002					 ///
///--------------------------------------///
///Description: General.                 ///
////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;  

namespace FgDotNetControls
{
	/// <summary>
	/// Summary description for Control.
	/// </summary>
	internal sealed class App
	{
		public App()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static StringFormat StringFormatAlignment(ContentAlignment alignment) {
			StringFormat format = new StringFormat();
 
			switch (alignment) {
				case ContentAlignment.BottomCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Far; 
					break;

				case ContentAlignment.BottomLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Far; 
					break;

				case ContentAlignment.BottomRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Far; 
					break;

				case ContentAlignment.MiddleLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Center; 
					break;

				case ContentAlignment.MiddleRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Center;  
					break;

				case ContentAlignment.TopCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Near; 
					break;

				case ContentAlignment.TopLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Near; 
					break;

				case ContentAlignment.TopRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Near; 
					break;

				default:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Center; 
					break;
			}

			return format;
		}

		public static FormatValues FormatValueAlignment(ContentAlignment alignment)
		{
			switch (alignment) {
				case ContentAlignment.BottomCenter:
					return FormatValues.Bottom | FormatValues.Center | FormatValues.SingleLine;

				case ContentAlignment.BottomLeft:
					return FormatValues.Bottom | FormatValues.Left | FormatValues.SingleLine;

				case ContentAlignment.BottomRight:
					return FormatValues.Bottom | FormatValues.Right | FormatValues.SingleLine;

				case ContentAlignment.MiddleLeft:
					return FormatValues.VCenter | FormatValues.Left | FormatValues.SingleLine;

				case ContentAlignment.MiddleRight:
					return FormatValues.VCenter | FormatValues.Right | FormatValues.SingleLine;

				case ContentAlignment.TopCenter:
					return FormatValues.Top | FormatValues.Center | FormatValues.SingleLine;

				case ContentAlignment.TopLeft:
					return FormatValues.Top | FormatValues.Left | FormatValues.SingleLine;

				case ContentAlignment.TopRight:
					return FormatValues.Top | FormatValues.Right  | FormatValues.SingleLine;

				default:
					return FormatValues.VCenter | FormatValues.Center | FormatValues.SingleLine;
			}
		}
	}
}
