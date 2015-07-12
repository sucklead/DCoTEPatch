using System;
//using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCoTEPatch
{
    public class DCoTECheckBox : System.Windows.Forms.CheckBox
    {

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

        private bool mDefaultValue;
        public bool DefaultValue
        {
            get {return mDefaultValue;}
            set {mDefaultValue = value;}
        }

        private int mCodeOffset = 0;
        public int CodeOffset
        {
            get { return mCodeOffset; }
            set { mCodeOffset = value; }
        }

        public void ResetToDefault()
        {
            this.Checked = mDefaultValue;
        }

        public void WriteValue(byte[] byteArray)
        {
            if (mCodeOffset == 0)
                return;
            //set
            if (this.Checked)
                byteArray[mCodeOffset] = 49; //1
            else
                byteArray[mCodeOffset] = 48; //0
        }

        public void ReadValue(byte[] byteArray)
        {
            if (mCodeOffset == 0)
                return;

            this.Checked = (byteArray[mCodeOffset] == 49);
        }

    }
}
