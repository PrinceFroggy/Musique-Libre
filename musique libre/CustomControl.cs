using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Drawing.Drawing2D;

namespace musique_libre
{
    #region Credit

    /*

     *    Custom Controls
     *   ----------------
     *  /   CREATED BY   \
     * /    ILLUMINATI    \
     * --------------------
     * ANDREW JUSTIN SOLESA
     * --------------------
     * GOOMBA SHROOM KASMER
     * --------------------
     * 
     * for gathering,
     * slightly changing,
     * coding,
     * and
     * organizing
     * nicely
     * custom
     * windows
     * form
     * controls...
     * that
     * are
     * needed
     * &
     * COSTLY
     * to
     * give
     * away
     * for
     * free.
     * 
     * ^ ^ ^ 6^ 6^ 6^ ^ ^ ^ 

     */

    #endregion

    // ----------------

    #region Deprecated code

    /*

    #region EnhancedListView

    partial class ColorfulListView
    {
        #region Variable

        private System.ComponentModel.IContainer components = null;

        #endregion

        #region Override

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            
            base.Dispose(disposing);
        }

        #endregion

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.EnhancedListView_ColumnClick);
            this.ResumeLayout(false);
        }

        #endregion
    }

    public partial class ColorfulListView : ListView
    {
        #region Variables

        enum lvEvents : uint
        {
            ItemAdded = 0x104D,
            ItemRemoved = 0x1008
        }

        private ListViewColumnSorter lvwColumnSorter;

        public event EventHandler ItemAdded;
        public event EventHandler ItemRemoved;

        public Color AlternateColor { get; set; }
        public string DateTimeFormat { set; get; }

        #endregion

        public ColorfulListView()
        {
            AlternateColor = Color.Empty;
            lvwColumnSorter = new ListViewColumnSorter();

            this.ListViewItemSorter = lvwColumnSorter;
            ItemAdded += ItemAddedToLV;
            ItemRemoved += ItemRemovedFromLV;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            InitializeComponent();
        }

        #region Overrides

        protected override void InitLayout()
        {
            lvwColumnSorter.timeFormat = DateTimeFormat;

            base.InitLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lvwColumnSorter.timeFormat = DateTimeFormat;

            base.OnPaint(e);
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch ((lvEvents)m.Msg)
            {
                case lvEvents.ItemAdded:
                    if (ItemAdded != null)
                        ItemAdded(this, null);
                    break;

                case lvEvents.ItemRemoved:
                    if (ItemRemoved != null)
                        ItemRemoved(this, null);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Functions/Event Handlers/Events

        public void SetAlternateColor()
        {
            try
            {
                if (!AlternateColor.IsEmpty && this.Items.Count != 0)
                {
                    if (this.Items.Count > 1)
                    {
                        for (int ix = 0; ix < this.Items.Count; ++ix)
                        {
                            var item = this.Items[ix];

                            item.BackColor = (ix % 2 == 0) ? AlternateColor : Color.White;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        void ItemAddedToLV(object sender, EventArgs e)
        {
            SetAlternateColor();
        }

        void ItemRemovedFromLV(object sender, EventArgs e)
        {
            MessageBox.Show("Removed");

            SetAlternateColor();
        }

        private void EnhancedListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.Items.Count != 0)
            {
                if (e.Column == lvwColumnSorter.SortColumn)
                {
                    if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    lvwColumnSorter.SortColumn = e.Column;
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }

                this.Sort();

                SetAlternateColor();
            }
        }

        #endregion
    }

    public class ListViewColumnSorter : IComparer
    {
        #region Variables

        public string timeFormat { set; get; }

        private int columnToSort;

        private SortOrder orderOfSort;

        private CaseInsensitiveComparer objectCompare;

        #endregion

        public ListViewColumnSorter()
        {
            columnToSort = 0;

            orderOfSort = SortOrder.None;

            objectCompare = new CaseInsensitiveComparer();
        }

        #region Event Handlers

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            int valueX;
            int valueY;

            string txtValueX = listviewX.SubItems[columnToSort].Text;
            string txtValueY = listviewY.SubItems[columnToSort].Text;

            double xNumber, yNumber;

            bool parsed = double.TryParse(txtValueX, out xNumber);

            parsed = parsed && double.TryParse(txtValueY, out yNumber);

            if (parsed)
            {
                compareResult = objectCompare.Compare((int.TryParse(listviewX.SubItems[columnToSort].Text, out valueX) ? valueX : 0), (int.TryParse(listviewY.SubItems[columnToSort].Text, out valueY) ? valueY : 0));
            }
            else
            {
                compareResult = objectCompare.Compare(listviewX.SubItems[columnToSort].Text, listviewY.SubItems[columnToSort].Text);
            }

            DateTime dateX;
            DateTime dateY;

            if (timeFormat != "" && timeFormat != null)
            {
                if (DateTime.TryParseExact(listviewX.SubItems[columnToSort].Text, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateX) && DateTime.TryParseExact(listviewY.SubItems[columnToSort].Text, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateY))
                {
                    compareResult = objectCompare.Compare(dateX, dateY);
                }
            }

            if (orderOfSort == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if (orderOfSort == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }

        public int SortColumn
        {
            set { columnToSort = value; }
            get { return columnToSort; }
        }

        public SortOrder Order
        {
            set { orderOfSort = value; }
            get { return orderOfSort; }
        }

        #endregion
    }

    #endregion

    */

    #endregion

    // ----------------

    #region CueTextBox

    public class CueTextBox : TextBox
    {
        #region PInvoke Helpers

        private static uint ECM_FIRST = 0x1500;
        private static uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

        #endregion PInvoke Helpers

        #region CueText

        private string _cueText = String.Empty;

        [Description("The text value to be displayed as a cue to the user.")]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CueText
        {
            get { return _cueText; }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }

                if (!_cueText.Equals(value, StringComparison.CurrentCulture))
                {
                    _cueText = value;
                    UpdateCue();
                    OnCueTextChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler CueTextChanged;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCueTextChanged(EventArgs e)
        {
            EventHandler handler = CueTextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion CueText

        #region ShowCueTextOnFocus

        private bool _showCueTextWithFocus = false;

        [Description("Indicates whether the CueText will be displayed even when the control has focus.")]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool ShowCueTextWithFocus
        {
            get { return _showCueTextWithFocus; }
            set
            {
                if (_showCueTextWithFocus != value)
                {
                    _showCueTextWithFocus = value;
                    UpdateCue();
                    OnShowCueTextWithFocusChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler ShowCueTextWithFocusChanged;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnShowCueTextWithFocusChanged(EventArgs e)
        {
            EventHandler handler = ShowCueTextWithFocusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion ShowCueTextOnFocus

        #region Override

        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateCue();

            base.OnHandleCreated(e);
        }

        #endregion Overrides

        #region Event

        private void UpdateCue()
        {
            if (this.IsHandleCreated)
            {
                SendMessage(new HandleRef(this, this.Handle), EM_SETCUEBANNER, (_showCueTextWithFocus) ? new IntPtr(1) : IntPtr.Zero, _cueText);
            }
        }

        #endregion
    }

    #endregion

    #region TransparentPanel

    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        { 

        }

        #region Events

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020;
                return createParams;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        #endregion
    }

    #endregion

    #region PanelProgressBar
     
    public partial class PanelProgressBar
    {
        #region Variables

        private int ProgressUnit = 20;
        private int ProgressValue = 1;
        private int MaximumValue = 10;
        private int GlossIntensity = 100;

        private bool IsBorderEnabled = true;
        private bool ShowGloss = true;

        #endregion

        #region Functions

        private bool UpdateProgress()
        {
            try 
            {
                ProgressUnit = (this.Width / MaximumValue);
			    ProgressFull.Width = (ProgressValue * ProgressUnit);

			    GlossPanelLeft.BackColor = Color.FromArgb(GlossIntensity, 255, 255, 255);
			    GlossPanelRight.BackColor = Color.FromArgb(GlossIntensity, 255, 255, 255);

			    if (ShowGloss) 
                {
				    GlossPanelLeft.Visible = true;
				    GlossPanelRight.Visible = true;
			    } 
                else 
                {
				    GlossPanelLeft.Visible = false;
				    GlossPanelRight.Visible = false;
			    }

			    if (ProgressValue == MaximumValue) 
                {
				    if (IsBorderEnabled) 
                    {
					    ProgressFull.Width = (this.Width - 2);
				    } 
                    else 
                    {
					    ProgressFull.Width = this.Width;
				    }
			    }

			    if (IsBorderEnabled) 
                {
				    this.Padding = new System.Windows.Forms.Padding(1);
			    } 
                else 
                {
				    this.Padding = new System.Windows.Forms.Padding(0);
			    }
            
            } 
            catch (Exception) 
            {
                return false;
            }
            
            return true;
	}

	    private bool UpdateGloss()
	    {
		    try 
            {
                GlossPanelLeft.Height = (this.Height / 3);
			    GlossPanelRight.Height = GlossPanelLeft.Height;
		    } 
            catch (Exception) 
            {
			    return false;
		    }
            
            return true;
        }

        #endregion

        #region Properties

        [DefaultValue(1), Description("The total progress of the ProgressBar."), Category("ProgressBar Properties")]
        
        public int Value 
        {
            get { return ProgressValue; }

            set 
            {
                if (value <= MaximumValue) 
                {
                    ProgressValue = value;
                } 
                else 
                {
                    ProgressValue = MaximumValue;
                }
                UpdateProgress();
            }
        }

        [DefaultValue(10), Description("The maximum value of the ProgressBar."), Category("ProgressBar Properties")]
        public int ProgressMaximumValue 
        {
            get { return MaximumValue; }

            set 
            {
                if (value > this.Width) 
                {
                    MaximumValue = this.Width;
                } 
                else 
                {
                    MaximumValue = value;
                }
                
                UpdateProgress();
            }
        }

	    [DefaultValue(typeof(Color), "WindowFrame"), Description("The color of the ProgressBar border."), Category("ProgressBar Properties")]
	    public Color BorderColor 
        {
            get { return this.BackColor; }

		    set 
            {
                this.BackColor = value;

			    UpdateProgress();
		    }
	    }

	    [DefaultValue(true), Description("Sets whether the colour border is displayed around the ProgressBar."), Category("ProgressBar Properties")]
	    public bool ShowBorder 
        {
		    get { return IsBorderEnabled; }

		    set 
            {
			    IsBorderEnabled = value;

			    UpdateProgress();
		    }
	    }

	    [DefaultValue(true), Description("Sets whether the glossy highlights are displayed on the ProgressBar."), Category("ProgressBar Properties")]
	    public bool IsGlossy 
        {
		    get { return ShowGloss; }

		    set 
            {
			    ShowGloss = value;

			    UpdateProgress();
		    }
	    }

	    [DefaultValue(typeof(Color), "ActiveCaption"), Description("Sets the colour of the ProgressBar."), Category("ProgressBar Properties")]
	    public Color BarColor 
        {
		    get { return ProgressFull.BackColor; }

		    set 
            {
			    ProgressFull.BackColor = value;

			    UpdateProgress();
		    }
	    }

	    [DefaultValue(typeof(Color), "Control"), Description("Sets the colour of the empty space of the ProgressBar."), Category("ProgressBar Properties")]
	    public Color EmptyColor 
        {
		    get { return ProgressEmpty.BackColor; }

		    set 
            {
			    ProgressEmpty.BackColor = value;

			    UpdateProgress();
		    }
	    }

	    [DefaultValue(100), Description("Sets the opacity of the gloss on the progressbar."), Category("ProgressBar Properties")]
	    public int GlossOpacity 
        {
            get { return GlossIntensity; }

            set
            {
                if (value > 255)
                {
                    value = 255;
                }

                GlossIntensity = value;

                UpdateProgress();
            }
	    }

	    #endregion

	    #region Event Handlers

	    private void ColourProgressBar_DockChanged(object sender, System.EventArgs e)
	    {
		    UpdateProgress();
	    }

	    private void ColourProgressBar_Load(System.Object sender, System.EventArgs e)
	    {
		    UpdateProgress();
	    }

	    private void ColourProgressBar_PaddingChanged(object sender, System.EventArgs e)
	    {
		    if (IsBorderEnabled) 
            {
			    this.Padding = new System.Windows.Forms.Padding(1);
		    } 
            else 
            {
			    this.Padding = new System.Windows.Forms.Padding(0);
		    }
	    }

	    private void ColourProgressBar_Resize(object sender, System.EventArgs e)
	    {
		    if (Value <= MaximumValue) 
            {
			    UpdateProgress();
		    }
		    UpdateGloss();
	    }

	    private void GlossPanelLeft_Click(object sender, System.EventArgs e)
	    {
		    if (Click != null) 
            {
			    Click(this, e);
		    }
	    }

	    private void GlossPanelRight_Click(object sender, System.EventArgs e)
	    {
		    if (Click != null) 
            {
			    Click(this, e);
		    }
	    }

	    private void ProgressEmpty_Click(object sender, System.EventArgs e)
	    {
		    if (Click != null) 
            {
			    Click(this, e);
		    }
	    }

	    private void ProgressFull_Click(object sender, System.EventArgs e)
	    {
		    if (Click != null) 
            {
			    Click(this, e);
		    }
	    }

	    #endregion

	    #region Events

	    public new event ClickEventHandler Click;
	    public delegate void ClickEventHandler(System.Object sender, System.EventArgs e);

	    public PanelProgressBar()
	    {
		    Resize += ColourProgressBar_Resize;
		    PaddingChanged += ColourProgressBar_PaddingChanged;
		    Load += ColourProgressBar_Load;
		    DockChanged += ColourProgressBar_DockChanged;
            InitializeComponent();
	    }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020;
                return createParams;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

	    #endregion
    }

    #endregion
}
