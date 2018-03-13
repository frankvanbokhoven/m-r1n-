using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UNET_Button
{

    /// <summary>
    /// Er zijn 5 mogelijkheden wat betreft p2p (type3(1)),
    /// 1: geen p2p call, 
    /// 2: de instructeur maakt p2p
    /// 3: de trainee maakt p2p
    /// 4: er is een p2p verbinding actief
    /// </summary>
    public enum P2PState
    {
        psNoP2PCall = -1,
        psCalledByInstructor = 0,
        psCalledByTrainee = 1,
        psP2PInProgress = 2,
        psP2PCallPending = 3
        
    };

    /// <summary>
	/// Summary description for cuteButton.
	/// </summary>
	public partial class UNET_Button : System.Windows.Forms.Button
    {
        private Color m_color1 = Color.LightGreen;  //first color
        private Color m_color2 = Color.DarkBlue;    //second color
        private int m_color1Transparent = 64;   //transparency degree (applies to the 1st color)
        private int m_color2Transparent = 64;   //transparency degree (applies to the 2nd color)
        private int _id;
        private string _state;
        private string _role;
        private P2PState _p2pCallState;


        [Description("Account ID voor bijv Radio of Rol. Dus NIET de id van de component!!")]
        public int ID
        {
            get { return _id; }
            set { _id = value;  Invalidate();}
        }

        [Description("Radio state")]
        public string State
        {
            get { return _state; }
            set { _state = value; Invalidate(); }
        }


        [Description("Placeholder voor de rol die een button kan hebben")]
        public string Role
        {
            get { return _role; }
            set { _role = value; Invalidate(); }
        }

        [Description("Status van een p2p call button")]
        public P2PState P2PCallState
        {
            get { return _p2pCallState; }
            set { _p2pCallState = value; Invalidate(); }
        }

        public UNET_Button()
        {
            P2PCallState = P2PState.psNoP2PCall;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);
            // Create two semi-transparent colors
            Color c1 = Color.FromArgb
                (m_color1Transparent, m_color1);
            Color c2 = Color.FromArgb
                (m_color2Transparent, m_color2);
            Brush b = new System.Drawing.Drawing2D.LinearGradientBrush
                (ClientRectangle, c1, c2, 10);
            pe.Graphics.FillRectangle(b, ClientRectangle);
            b.Dispose();
        }

    }
}
