using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Text; 


namespace FgDotNetControls
{
	/// <summary>
	/// Descrizione di riepilogo per UserControl1.
	/// </summary>
	public class FgButton : System.Windows.Forms.Button
	{
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.Container components = null;

	//	private bool IsCompatibleOS;
		private IntPtr hTheme = IntPtr.Zero;
		private int buttonType = (int)ButtonType.PushButton;
		private ButtonState buttonState;

		private int Offset_X;
        private int Offset_Y;

		public FgButton()
		{
			// Chiamata richiesta da Progettazione form Windows.Forms.
			InitializeComponent();

			// TODO: aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitComponent.

			PlatformID platformId = Environment.OSVersion.Platform;

			Version version = Environment.OSVersion.Version;
			Version targetVersion = new Version("5.1.2600.0");

		//	IsCompatibleOS = ((version >= targetVersion) && (platformId == PlatformID.Win32NT));

		}

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (hTheme != IntPtr.Zero)  
					NativeMethods.CloseThemeData(this.hTheme);

				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Codice generato da Progettazione componenti
		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	

		protected override void OnPaint(PaintEventArgs pevent)
		{
		////	if (!IsCompatibleOS)
			//{
			//	base.OnPaint (pevent);
			//}
			//else
			//{
			//	if (IsCompatibleOS) 
					if ((NativeMethods.IsThemeActive() == 1)  && (this.hTheme == IntPtr.Zero)) 
						this.hTheme = NativeMethods.OpenThemeData(this.Handle, "Button");

				if (this.hTheme != IntPtr.Zero) 
				{
					int oX,oY;
					oX=0;
					oY=0;
					Graphics graphics = pevent.Graphics;
	
					Rectangle bounds = this.ClientRectangle;

					if (buttonState==ButtonState.Pressed)
					{
						oX=1;
						oY=1;
					}

					bounds.Offset (oX,oY);

					IntPtr hDC = graphics.GetHdc();
			
					this.DrawXpButton(hDC, bounds);

					graphics.ReleaseHdc(hDC);

					if (this.Image!=null)
					{
						switch (this.ImageAlign)
						{
							case ContentAlignment.MiddleLeft:
								graphics.DrawImage (this.Image, Offset_X + oX, bounds.Height / 2-this.Image.Height / 2+oY);
								bounds.Offset (this.Image.Width,0);
								bounds.Width-=this.Image.Width;
							break;
							case ContentAlignment.TopCenter:
								graphics.DrawImage (this.Image, oX+bounds.Width / 2  -this.Image.Width / 2, oY + Offset_X);
								bounds.Offset (0,this.Image.Height);
								bounds.Height-=this.Image.Height;
							break;
                            case ContentAlignment.TopLeft:
                                graphics.DrawImage(this.Image, Offset_X + oX, oY + Offset_X + Offset_Y);
                                bounds.Offset(this.Image.Width, 0);
                                bounds.Width -= this.Image.Width;
                            break;

                            //todo complete this switch for all possible contentAlignments
                        }
                    }

					this.DrawXpButtonText(graphics, bounds);

					if (! base.DesignMode) 
					{
						if (base.Focused) 
						{
							bounds.Inflate(-4, -4);
					 
							//ControlPaint.DrawFocusRectangle(graphics, bounds);
						}
					}
				}
		//	}
		}





		/// <summary>
		/// Draws a Windows XP style button.
		/// </summary>
		/// <param name="hDC">A managed pointer containing the handle to a control's device context.</param>
		/// <param name="bounds">A Rectangle structure containing the bounds of the control to draw on.</param>
		protected virtual void DrawXpButton(IntPtr hDC, Rectangle bounds) 
		{
			RECT rect = new RECT(bounds);
			
			NativeMethods.DrawThemeParentBackground(this.Handle, hDC, ref rect);

			if (! base.Enabled) 
			{
				NativeMethods.DrawThemeBackground(hTheme, hDC, this.buttonType, (int)ButtonState.Disabled, ref rect, ref rect); 

				return;
			}

			if (base.IsDefault)
				NativeMethods.DrawThemeBackground(hTheme, hDC, this.buttonType, (int)ButtonState.Defaulted, ref rect, ref rect); 
			
			switch (this.buttonState) 
			{
				case ButtonState.Hot:
					NativeMethods.DrawThemeBackground(hTheme, hDC, this.buttonType, (int)ButtonState.Hot, ref rect, ref rect);
					break;

				case ButtonState.Pressed:
					NativeMethods.DrawThemeBackground(hTheme, hDC, this.buttonType, (int)ButtonState.Pressed, ref rect, ref rect);
					break;

				default:
					if (! base.IsDefault)
						NativeMethods.DrawThemeBackground(hTheme, hDC, this.buttonType, (int)ButtonState.Normal, ref rect, ref rect);
					break;
			}
		}
		
		/// <summary>
		/// Draws a Windows XP style text.
		/// </summary>
		/// <param name="hDC">A managed pointer containing the handle to a control's device context.</param>
		/// <param name="bounds">A Rectangle structure containing the bounds of the control to draw on.</param>
		protected virtual void DrawXpButtonText(IntPtr hDC, Rectangle bounds) 
		{
			bounds.Inflate(-5, -5);

			RECT rect = new RECT(bounds);

			FormatValues format = App.FormatValueAlignment(this.TextAlign);  
		
			format |= FormatValues.WordEllipses; 
 
			if (base.Enabled)
				NativeMethods.DrawThemeText(hTheme, hDC, this.buttonType, (int)ButtonState.Normal, base.Text, -1, (uint)format, 0, ref rect);
			else
				NativeMethods.DrawThemeText(hTheme, hDC, this.buttonType, (int)ButtonState.Disabled, base.Text, -1, (uint)format, 0, ref rect);
		}
		
		/// <summary>
		/// Draws a Windows XP style text.
		/// </summary>
		/// <param name="graphics">A Graphics object containing the surface to draw on.</param>
		/// <param name="bounds">A Rectangle structure containing the bounds of the control to draw on.</param>
		protected virtual void DrawXpButtonText(Graphics graphics, Rectangle bounds)
		{
			StringFormat format = new StringFormat(App.StringFormatAlignment(base.TextAlign));  

			format.HotkeyPrefix = HotkeyPrefix.Show;
 
			bounds.Inflate(-5, -5);
			
			if (base.Enabled)
				graphics.DrawString(base.Text, base.Font, new SolidBrush(base.ForeColor), bounds, format);
			else 
				graphics.DrawString(base.Text, base.Font, new SolidBrush(SystemColors.GrayText), bounds, format);
		}

		/// <summary>
		/// Offset X van de image
		/// </summary>
		public int Offset
		{
			get
			{
				return Offset_X;
			}
			set
			{
				Offset_X=value;
				this.Invalidate ();
			}
		}

        /// <summary>
        /// Offset Y van de image
        /// </summary>
        public int OffsetY
        {
            get
            {
                return Offset_Y;
            }
            set
            {
                Offset_Y = value;
                this.Invalidate();
            }
        }
	
		protected override void OnMouseEnter(EventArgs eventargs)
		{
			// TODO: aggiungere l'implementazione di FgButton.OnMouseEnter
			base.OnMouseEnter (eventargs);
			this.buttonState = ButtonState.Hot; 
			base.Invalidate(); 
		}
	
		protected override void OnMouseLeave(EventArgs eventargs)
		{
			// TODO: aggiungere l'implementazione di FgButton.OnMouseLeave
			base.OnMouseLeave (eventargs);
			this.buttonState = ButtonState.Normal; 
			base.Invalidate(); 
		}
	
		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			// TODO: aggiungere l'implementazione di FgButton.OnMouseDown
			base.OnMouseDown (mevent);
			this.buttonState = ButtonState.Pressed; 
			base.Invalidate(); 
		}
	
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			// TODO: aggiungere l'implementazione di FgButton.OnMouseUp
			base.OnMouseUp (mevent);
			this.buttonState = ButtonState.Normal; 
			base.Invalidate();
		}
	}
}
