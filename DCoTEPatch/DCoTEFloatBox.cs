using System;
//using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

using System.Globalization;
using System.Threading;


namespace DCoTEPatch
{
    public class DCoTEFloatBox : System.Windows.Forms.TextBox
    {
        private string mstrMask = "";
        private char[] mParams = { '#','&','!' };

        //CultureInfo ci = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        

        //System.Threading.Thread
        /*
        Dim ci As New CultureInfo("it-IT")
        Dim nfi As New NumberFormatInfo
        nfi.CurrencyDecimalSeparator = ","
        nfi.CurrencyGroupSeparator = "."
        ci.NumberFormat = nfi
        Thread.CurrentThread.CurrentCulture = ci
        */

        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        #endregion

        private string mDefaultValue;
        public string DefaultValue
        {
            get {return mDefaultValue;}
            set {mDefaultValue = value;}
        }

        private float mMaxValue;
        public float MaxValue
        {
            get {return mMaxValue;}
            set {mMaxValue = value;}
        }

        private int mCodeOffset;
        public int CodeOffset
        {
            get { return mCodeOffset; }
            set { mCodeOffset = value; }
        }

        public void ResetToDefault()
        {
            this.Text = mDefaultValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        public void WriteValue(byte[] byteArray)
        {
            string newText = this.Text.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
            for (int i=0;i < newText.Length;i++)
            {
                byteArray[mCodeOffset + i] = Convert.ToByte(newText[i]);
            }
        }

        public void ReadValue(byte[] byteArray)
        {
            char[] newText = this.Text.ToCharArray();
            for (int i = 0; i < this.Text.Length; i++)
            {
                newText[i] = Convert.ToChar(byteArray[mCodeOffset + i]);
            }

            this.Text = new string(newText).Replace(".",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        public string UnformatedText
        {
            get
            {
                int i;
                string strText = "";

                for (i = 0; i < mstrMask.Length; i++)
                {
                    if (Array.IndexOf(mParams, mstrMask[i]) > -1 && this.Text[i] != '_')
                    {
                        strText += this.Text[i];
                    }
                }


                return strText;
            }
        }

        public string Mask
        {
            get { return mstrMask; }
            set
            { 
                //Use # for Digit only
                //Use & for Letter only
                //Use ! for Letter or Digit
                mstrMask = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                //this.Text = mstrMask;
                this.Text = mstrMask.Replace("#", "_").Replace("&", "_").Replace("!", "_");
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //Disables Delete Key
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
            }
        }

        //protected override void OnValidating(CancelEventArgs e)
        public bool Validate()
        {
            float floatTest;

            //e.Cancel = true;
            try {
                floatTest = float.Parse(this.Text);
                if ((floatTest > 0) && (floatTest <= mMaxValue))                 {
                    return true;
                    //e.Cancel = false;
                }
            }
            catch { 
            }
            //base.OnValidating(e);
            return false;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //key pressed
            char chrKeyPressed = e.KeyChar;
            //Original cursor position
            int intSelStart = this.SelectionStart;
            //In case of a selection, delete text to this position
            int intDelTo = this.SelectionStart + this.SelectionLength - 1;

            string strText = this.Text;
            //Used to avoid deletion of the selection when an invalid key is pressed
            bool bolDelete = false;

            int i;

            e.Handled = true;

            if ((int)chrKeyPressed == 8) //Back
            {
                bolDelete = true;
                if (intSelStart > 0 && intDelTo < intSelStart)
                {
                    intSelStart -= 1;
                }
            }


            //Find the Next Insertion point
            for (i = this.SelectionStart;i < mstrMask.Length;i++)
            {
                //Test for # or &
                if ((mstrMask[i] == '#' && char.IsDigit(chrKeyPressed))
                    || (mstrMask[i] == '&' && char.IsLetter(chrKeyPressed))
                    || (mstrMask[i] == '!' && char.IsLetterOrDigit(chrKeyPressed))
                    )
                {
                    strText = strText.Remove(i, 1).Insert(i, chrKeyPressed.ToString());
                    intSelStart = i + 1;
                    bolDelete = true;
                }

                //Prevent looping unitl the next available match when mixing # & ! on the same mask
                if (Array.IndexOf(mParams, mstrMask[i]) > -1)
                {
                    break;
                }

            }

            //Delete remaining chars from selection or previous char if backspace
            if (bolDelete)
            {

                for (i = intSelStart;i <= intDelTo;i++)
                {
                    if (Array.IndexOf(mParams, mstrMask[i]) > -1)
                    {
                        strText = strText.Remove(i, 1).Insert(i, "_");
                    }

                }

                this.Text = strText;
                this.SelectionStart = intSelStart;
                this.SelectionLength = 0;
            }


        }
    }
}
