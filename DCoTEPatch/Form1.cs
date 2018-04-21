//#undef FX1_1

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

using System.Xml;
using System.Collections.Generic;

//using System.Security.Permissions;
//[assembly: PermissionSetAttribute(SecurityAction.RequestMinimum, Name = "LocalIntranet")]
namespace DCoTEPatch
{
    public class Form1 : Form
    {
        public enum ReturnStatus
        {
            Failed,
            BackedUp,
            OK
        };

        public enum BatNames
        {
            _01_house,
            _02_streets_one,
            _03_streets_two,
            _04_jail_break,
            _05_streets_three,
            _06_refinery,
            _06_refinery_explosion,
            _07_esoteric_order,
            _08_boat,
            _09_reef,
            _10_air_filled_tunnels,
            misc01_city_of_dreams_01,
            misc01_city_of_dreams_02,
            misc01_city_of_dreams_03,
            misc02_jacks_office_cutscene,
            misc03_asylum_cutscene,
            misc03_asylum_cutscene_feds
        };

        public class BatFile
        {
            public BatNames Name { get; set; }
            public string Filename { get; set; }
            public int Filesize { get; set; }
            public byte[] Contents;
        }

        private Dictionary<BatNames, BatFile> BatFiles = new Dictionary<BatNames, BatFile>();

        public enum ShaderNames
        {
            psModulateColorByAlpha,
            Skydome_NoColor
        };
        
        public class ShaderFile
        {
            public ShaderNames Name { get; set; }
            public string Filename { get; set; }
            public int Filesize { get; set; }
            public byte[] Contents;
        }
        
        private Dictionary<ShaderNames, ShaderFile> ShaderFiles = new Dictionary<ShaderNames, ShaderFile>();
        
        private System.Windows.Forms.OpenFileDialog openExeFileDialog;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;

        private string filename;
        private byte[] programCode;

        //private string mapCityOfDreams03name;
        //private byte[] mapCityOfDreams03;

        
        //private string map08Boatname;
        //private byte[] map08Boat;


        private DataTable xmlSetting;
        private DataView xmlView;

        private bool SteamVersion = false;
        private bool D2DVersion = false;

        private int id = 0;

        #region Members

        private DataGrid dgSettings;
        private TextBox textBox1;
        private Button btnResetToDefaults;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private GroupBox groupBoxEnhance;
        private DCoTECheckBox checkBoxSkipVideos;
        private DCoTECheckBox checkBoxReachArm;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private GroupBox groupBoxWeapons;
        private Label label94;
        private Label label95;
        private DCoTEFloatBox dCoTEFloatBox40;
        private Label label96;
        private Label label97;
        private DCoTEFloatBox dCoTEFloatBox41;
        private Label label98;
        private Label label99;
        private DCoTEFloatBox dCoTEFloatBox42;
        private Label label88;
        private Label label89;
        private DCoTEFloatBox dCoTEFloatBox37;
        private Label label90;
        private Label label91;
        private DCoTEFloatBox dCoTEFloatBox38;
        private Label label92;
        private Label label93;
        private DCoTEFloatBox dCoTEFloatBox39;
        private Label label82;
        private Label label83;
        private DCoTEFloatBox dCoTEFloatBox34;
        private Label label84;
        private Label label85;
        private DCoTEFloatBox dCoTEFloatBox35;
        private Label label86;
        private Label label87;
        private DCoTEFloatBox dCoTEFloatBox36;
        private Label label76;
        private Label label77;
        private DCoTEFloatBox dCoTEFloatBox31;
        private Label label78;
        private Label label79;
        private DCoTEFloatBox dCoTEFloatBox32;
        private Label label80;
        private Label label81;
        private DCoTEFloatBox dCoTEFloatBox33;
        private Label label74;
        private Label label75;
        private DCoTEFloatBox dCoTEFloatBox30;
        private Label label72;
        private Label label73;
        private DCoTEFloatBox dCoTEFloatBox29;
        private Label label70;
        private Label label71;
        private DCoTEFloatBox dCoTEFloatBox28;
        private Label label68;
        private Label label69;
        private DCoTEFloatBox dCoTEFloatBox27;
        private Label label66;
        private Label label67;
        private DCoTEFloatBox dCoTEFloatBox26;
        private Label label64;
        private Label label65;
        private DCoTEFloatBox dCoTEFloatBox25;
        private Label label62;
        private Label label63;
        private DCoTEFloatBox dCoTEFloatBox24;
        private Label label60;
        private Label label61;
        private DCoTEFloatBox dCoTEFloatBox23;
        private GroupBox groupBoxDamage;
        private Label label32;
        private Label label33;
        private DCoTEFloatBox DCoTEFloatBox9;
        private Label label22;
        private Label label23;
        private DCoTEFloatBox DCoTEFloatBox1;
        private Label label24;
        private Label label25;
        private DCoTEFloatBox DCoTEFloatBox2;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label30;
        private DCoTEFloatBox DCoTEFloatBox6;
        private DCoTEFloatBox DCoTEFloatBox7;
        private Label label31;
        private DCoTEFloatBox DCoTEFloatBox8;
        private GroupBox groupBoxMelee;
        private Label label34;
        private Label label35;
        private DCoTEFloatBox DCoTEFloatBox10;
        private Label label36;
        private Label label37;
        private DCoTEFloatBox DCoTEFloatBox11;
        private Label label38;
        private Label label39;
        private DCoTEFloatBox DCoTEFloatBox12;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private DCoTEFloatBox DCoTEFloatBox13;
        private DCoTEFloatBox DCoTEFloatBox14;
        private Label label45;
        private DCoTEFloatBox DCoTEFloatBox15;
        private DCoTECheckBox checkBoxInfiniteAmmo;
        private GroupBox groupBoxInfinite;
        private DCoTECheckBox checkBoxExtendedMovie;
        private DCoTECheckBox checkBoxAllowLegalSkip;
        private GroupBox groupboxXbill;
        private LinkLabel linkLabelCoCF;
        private TextBox textBox2;
        private DCoTECheckBox checkBoxFilmEffects;
        private TabPage tabPage4;
        private GroupBox groupBoxAdvancedXML;
        private Button btnExtractXML;

        #endregion Members
        private ProgressBar pbXML;
        private CheckBox cbBreakStuff;
        private TextBox textBox3;
        private GroupBox gbBugFixes;
        private TabPage tabPage5;
        private GroupBox groupBoxCheats;
        private DCoTECheckBox checkBoxGodMode;
        private DCoTECheckBox checkBoxDebugMode;
        private DCoTECheckBox checkBoxFlyMode;
        private DCoTECheckBox checkBoxPickStartMap;
        private DCoTECheckBox checkBoxUnlockAllContent;
        private DCoTECheckBox checkBoxReleaseMode;
        private Button btnRestoreToOriginals;
        private LinkLabel linkLabel1;
        private GroupBox groupBox3;
        private Label label1;
        private Label lblFOV;
        private DCoTEFloatBox mebFOV;
        private Label label2;
        private Label label3;
        private DCoTEFloatBox mebFOVSneak;
        private GroupBox groupBox2;
        private Label label108;
        private Label label109;
        private DCoTEFloatBox dCoTEFloatBox47;
        private Label label106;
        private Label label107;
        private DCoTEFloatBox dCoTEFloatBox46;
        private Label label104;
        private Label label105;
        private DCoTEFloatBox dCoTEFloatBox45;
        private Label label102;
        private Label label103;
        private DCoTEFloatBox dCoTEFloatBox44;
        private Label label100;
        private Label label101;
        private DCoTEFloatBox dCoTEFloatBox43;
        private Label label5;
        private Label label4;
        private DCoTEFloatBox mebMorphine;
        private Label label11;
        private Label label6;
        private Label label10;
        private Label label7;
        private Label label9;
        private DCoTEFloatBox mebBandages;
        private DCoTEFloatBox mebSplints;
        private Label label8;
        private DCoTEFloatBox mebSutures;
        private GroupBox groupBox8;
        private Label label46;
        private Label label47;
        private DCoTEFloatBox DCoTEFloatBox16;
        private Label label50;
        private Label label52;
        private DCoTEFloatBox DCoTEFloatBox20;
        private GroupBox groupBox9;
        private Label label56;
        private Label label57;
        private DCoTEFloatBox DCoTEFloatBox21;
        private Label label58;
        private Label label59;
        private DCoTEFloatBox DCoTEFloatBox22;
        private GroupBox groupBox7;
        private Label label48;
        private Label label49;
        private DCoTEFloatBox DCoTEFloatBox17;
        private Label label51;
        private Label label53;
        private Label label54;
        private DCoTEFloatBox DCoTEFloatBox18;
        private DCoTEFloatBox DCoTEFloatBox19;
        private Label label55;
        private GroupBox groupBox4;
        private Label label20;
        private Label label21;
        private DCoTEFloatBox DCoTEFloatBox5;
        private Label label12;
        private Label label13;
        private DCoTEFloatBox mebCrawlSpeed;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private DCoTEFloatBox mebWalkSpeed;
        private DCoTEFloatBox DCoTEFloatBox3;
        private Label label19;
        private DCoTEFloatBox DCoTEFloatBox4;
        private TextBox tbVersion;
        private TextBox textBox5;
        private GroupBox group15Features;
        private CheckBox cbGiveAllWeapons;
        private CheckBox cbUnlockCinematics;
        private CheckBox cbUnlockDifficultyLevels;
        private CheckBox cbDoubleTimeEscapeSequence;
        private CheckBox checkBoxFixBlueLightShader;
        private CheckBox checkBoxModifySkydomeShader;
        private GroupBox gbLegacyBugFixes;
        private CheckBox checkBoxFixBlueLight;
        private CheckBox checkBoxFixedFPS;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private void CreateDataTable()
        {
            xmlSetting = new DataTable("xmlsetting");
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            //DataRow row;

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = false;
            column.Unique = true;
            xmlSetting.Columns.Add(column);
            xmlSetting.PrimaryKey = new DataColumn[] {xmlSetting.Columns[0]}; 

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Area";
            column.ReadOnly = false;
            xmlSetting.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Path";
            column.ReadOnly = false;
            xmlSetting.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType   = System.Type.GetType("System.String");
            column.ColumnName = "Node";
            column.ReadOnly = false;
            xmlSetting.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Value";
            column.ReadOnly = false;
            xmlSetting.Columns.Add(column);

            xmlView = new DataView(xmlSetting,"ISNULL(Value,-1) = Value","id",DataViewRowState.CurrentRows);
            xmlView.Table.Columns[0].ReadOnly = true;
            xmlView.Table.Columns[1].ReadOnly = true;
            xmlView.Table.Columns[2].ReadOnly = true;
            xmlView.Table.Columns[3].ReadOnly = true;
            xmlView.AllowDelete = false;
            xmlView.AllowNew = false;

            dgSettings.SetDataBinding(xmlView,"");
            

        }

        public Form1()
        {

            InitializeComponent();

            CreateDataTable();

#if !FX1_1
            SetVisualStyles();
#endif
        }

#if !FX1_1
        private void SetVisualStyles()
        {
            linkLabelCoCF.BackColor = textBox2.BackColor;

            foreach (Control tab in tabControl1.Controls)
            {
                if (tab is TabPage)
                {
                    ((TabPage)tab).UseVisualStyleBackColor = true;

                    foreach (Control controlBox in tab.Controls)
                    {
                        if (controlBox is GroupBox)
                        {
                            foreach (Control control in controlBox.Controls)
                            {
                                if (control is DCoTECheckBox)
                                {
                                    ((DCoTECheckBox)control).UseVisualStyleBackColor = true;
                                }
                                else if (control is Button)
                                {
                                    ((Button)control).UseVisualStyleBackColor = true;
                                }
                            }
                        }
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dgSettings = new System.Windows.Forms.DataGrid();
            this.openExeFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRestoreToOriginals = new System.Windows.Forms.Button();
            this.btnResetToDefaults = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.group15Features = new System.Windows.Forms.GroupBox();
            this.cbDoubleTimeEscapeSequence = new System.Windows.Forms.CheckBox();
            this.cbGiveAllWeapons = new System.Windows.Forms.CheckBox();
            this.cbUnlockCinematics = new System.Windows.Forms.CheckBox();
            this.cbUnlockDifficultyLevels = new System.Windows.Forms.CheckBox();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.gbBugFixes = new System.Windows.Forms.GroupBox();
            this.checkBoxModifySkydomeShader = new System.Windows.Forms.CheckBox();
            this.checkBoxFixBlueLightShader = new System.Windows.Forms.CheckBox();
            this.linkLabelCoCF = new System.Windows.Forms.LinkLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupboxXbill = new System.Windows.Forms.GroupBox();
            this.groupBoxEnhance = new System.Windows.Forms.GroupBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBoxCheats = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFOV = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBoxInfinite = new System.Windows.Forms.GroupBox();
            this.groupBoxWeapons = new System.Windows.Forms.GroupBox();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.groupBoxDamage = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBoxMelee = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBoxAdvancedXML = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.cbBreakStuff = new System.Windows.Forms.CheckBox();
            this.pbXML = new System.Windows.Forms.ProgressBar();
            this.btnExtractXML = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.gbLegacyBugFixes = new System.Windows.Forms.GroupBox();
            this.checkBoxFixBlueLight = new System.Windows.Forms.CheckBox();
            this.checkBoxFixedFPS = new System.Windows.Forms.CheckBox();
            this.checkBoxFilmEffects = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxExtendedMovie = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxAllowLegalSkip = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxSkipVideos = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxReachArm = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxGodMode = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxDebugMode = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxFlyMode = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxPickStartMap = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxUnlockAllContent = new DCoTEPatch.DCoTECheckBox();
            this.checkBoxReleaseMode = new DCoTEPatch.DCoTECheckBox();
            this.mebFOV = new DCoTEPatch.DCoTEFloatBox();
            this.mebFOVSneak = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox47 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox46 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox45 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox44 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox43 = new DCoTEPatch.DCoTEFloatBox();
            this.mebMorphine = new DCoTEPatch.DCoTEFloatBox();
            this.mebBandages = new DCoTEPatch.DCoTEFloatBox();
            this.mebSplints = new DCoTEPatch.DCoTEFloatBox();
            this.mebSutures = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox16 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox20 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox21 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox22 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox17 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox18 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox19 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox5 = new DCoTEPatch.DCoTEFloatBox();
            this.mebCrawlSpeed = new DCoTEPatch.DCoTEFloatBox();
            this.mebWalkSpeed = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox3 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox4 = new DCoTEPatch.DCoTEFloatBox();
            this.checkBoxInfiniteAmmo = new DCoTEPatch.DCoTECheckBox();
            this.dCoTEFloatBox40 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox41 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox42 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox37 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox38 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox39 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox34 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox35 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox36 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox31 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox32 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox33 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox30 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox29 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox28 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox27 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox26 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox25 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox24 = new DCoTEPatch.DCoTEFloatBox();
            this.dCoTEFloatBox23 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox9 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox1 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox2 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox6 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox7 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox8 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox10 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox11 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox12 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox13 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox14 = new DCoTEPatch.DCoTEFloatBox();
            this.DCoTEFloatBox15 = new DCoTEPatch.DCoTEFloatBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgSettings)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.group15Features.SuspendLayout();
            this.gbBugFixes.SuspendLayout();
            this.groupboxXbill.SuspendLayout();
            this.groupBoxEnhance.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBoxCheats.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBoxInfinite.SuspendLayout();
            this.groupBoxWeapons.SuspendLayout();
            this.groupBoxDamage.SuspendLayout();
            this.groupBoxMelee.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBoxAdvancedXML.SuspendLayout();
            this.gbLegacyBugFixes.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSettings
            // 
            this.dgSettings.AlternatingBackColor = System.Drawing.Color.LightGray;
            this.dgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSettings.BackColor = System.Drawing.Color.DarkGray;
            this.dgSettings.CaptionBackColor = System.Drawing.Color.White;
            this.dgSettings.CaptionFont = new System.Drawing.Font("Verdana", 10F);
            this.dgSettings.CaptionForeColor = System.Drawing.Color.Navy;
            this.dgSettings.CaptionVisible = false;
            this.dgSettings.DataMember = "";
            this.dgSettings.Enabled = false;
            this.dgSettings.ForeColor = System.Drawing.Color.Black;
            this.dgSettings.GridLineColor = System.Drawing.Color.Black;
            this.dgSettings.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
            this.dgSettings.HeaderBackColor = System.Drawing.Color.Silver;
            this.dgSettings.HeaderForeColor = System.Drawing.Color.Black;
            this.dgSettings.LinkColor = System.Drawing.Color.Navy;
            this.dgSettings.Location = new System.Drawing.Point(6, 48);
            this.dgSettings.Name = "dgSettings";
            this.dgSettings.ParentRowsBackColor = System.Drawing.Color.White;
            this.dgSettings.ParentRowsForeColor = System.Drawing.Color.Black;
            this.dgSettings.SelectionBackColor = System.Drawing.Color.Navy;
            this.dgSettings.SelectionForeColor = System.Drawing.Color.White;
            this.dgSettings.Size = new System.Drawing.Size(544, 426);
            this.dgSettings.TabIndex = 0;
            // 
            // openExeFileDialog
            // 
            this.openExeFileDialog.FileName = "CoCMainWin32.exe";
            this.openExeFileDialog.Filter = "CoCMainWin32.exe|CoCMainWin32.exe";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(10, 49);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load...";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(6, 41);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnRestoreToOriginals);
            this.panel1.Controls.Add(this.btnResetToDefaults);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(12, 595);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 66);
            this.panel1.TabIndex = 1;
            // 
            // btnRestoreToOriginals
            // 
            this.btnRestoreToOriginals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestoreToOriginals.Location = new System.Drawing.Point(106, 41);
            this.btnRestoreToOriginals.Name = "btnRestoreToOriginals";
            this.btnRestoreToOriginals.Size = new System.Drawing.Size(116, 23);
            this.btnRestoreToOriginals.TabIndex = 2;
            this.btnRestoreToOriginals.Text = "Restore original files";
            this.btnRestoreToOriginals.UseVisualStyleBackColor = true;
            this.btnRestoreToOriginals.Click += new System.EventHandler(this.btnRestoreToOriginals_Click);
            // 
            // btnResetToDefaults
            // 
            this.btnResetToDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetToDefaults.Location = new System.Drawing.Point(461, 41);
            this.btnResetToDefaults.Name = "btnResetToDefaults";
            this.btnResetToDefaults.Size = new System.Drawing.Size(103, 23);
            this.btnResetToDefaults.TabIndex = 1;
            this.btnResetToDefaults.Text = "Reset To Defaults";
            this.btnResetToDefaults.Click += new System.EventHandler(this.btnResetToDefaults_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 405);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(556, 38);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "This is NOT an official modification to DCoTE!  PLEASE play the game through as i" +
    "t was meant to be played if you can before using this patcher.  All rights remai" +
    "n with the original copyright holders.";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(12, 102);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 528);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbLegacyBugFixes);
            this.tabPage1.Controls.Add(this.group15Features);
            this.tabPage1.Controls.Add(this.tbVersion);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.gbBugFixes);
            this.tabPage1.Controls.Add(this.linkLabelCoCF);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.groupboxXbill);
            this.tabPage1.Controls.Add(this.groupBoxEnhance);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(568, 502);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General Settings";
            // 
            // group15Features
            // 
            this.group15Features.Controls.Add(this.cbDoubleTimeEscapeSequence);
            this.group15Features.Controls.Add(this.cbGiveAllWeapons);
            this.group15Features.Controls.Add(this.cbUnlockCinematics);
            this.group15Features.Controls.Add(this.cbUnlockDifficultyLevels);
            this.group15Features.Location = new System.Drawing.Point(6, 335);
            this.group15Features.Name = "group15Features";
            this.group15Features.Size = new System.Drawing.Size(556, 64);
            this.group15Features.TabIndex = 15;
            this.group15Features.TabStop = false;
            this.group15Features.Text = "1.5 Features";
            // 
            // cbDoubleTimeEscapeSequence
            // 
            this.cbDoubleTimeEscapeSequence.AutoSize = true;
            this.cbDoubleTimeEscapeSequence.Location = new System.Drawing.Point(21, 42);
            this.cbDoubleTimeEscapeSequence.Name = "cbDoubleTimeEscapeSequence";
            this.cbDoubleTimeEscapeSequence.Size = new System.Drawing.Size(185, 17);
            this.cbDoubleTimeEscapeSequence.TabIndex = 3;
            this.cbDoubleTimeEscapeSequence.Text = "Double time for escape sequence";
            this.cbDoubleTimeEscapeSequence.UseVisualStyleBackColor = true;
            // 
            // cbGiveAllWeapons
            // 
            this.cbGiveAllWeapons.AutoSize = true;
            this.cbGiveAllWeapons.Location = new System.Drawing.Point(310, 42);
            this.cbGiveAllWeapons.Name = "cbGiveAllWeapons";
            this.cbGiveAllWeapons.Size = new System.Drawing.Size(206, 17);
            this.cbGiveAllWeapons.TabIndex = 2;
            this.cbGiveAllWeapons.Text = "Give all weapons at start of each level";
            this.cbGiveAllWeapons.UseVisualStyleBackColor = true;
            // 
            // cbUnlockCinematics
            // 
            this.cbUnlockCinematics.AutoSize = true;
            this.cbUnlockCinematics.Location = new System.Drawing.Point(310, 19);
            this.cbUnlockCinematics.Name = "cbUnlockCinematics";
            this.cbUnlockCinematics.Size = new System.Drawing.Size(235, 17);
            this.cbUnlockCinematics.TabIndex = 1;
            this.cbUnlockCinematics.Text = "Unlock cinematics after starting a new game";
            this.cbUnlockCinematics.UseVisualStyleBackColor = true;
            // 
            // cbUnlockDifficultyLevels
            // 
            this.cbUnlockDifficultyLevels.AutoSize = true;
            this.cbUnlockDifficultyLevels.Location = new System.Drawing.Point(21, 19);
            this.cbUnlockDifficultyLevels.Name = "cbUnlockDifficultyLevels";
            this.cbUnlockDifficultyLevels.Size = new System.Drawing.Size(253, 17);
            this.cbUnlockDifficultyLevels.TabIndex = 0;
            this.cbUnlockDifficultyLevels.Text = "Unlock difficulty levels after starting a new game";
            this.cbUnlockDifficultyLevels.UseVisualStyleBackColor = true;
            // 
            // tbVersion
            // 
            this.tbVersion.BackColor = System.Drawing.SystemColors.Control;
            this.tbVersion.Location = new System.Drawing.Point(6, 4);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.ReadOnly = true;
            this.tbVersion.Size = new System.Drawing.Size(556, 20);
            this.tbVersion.TabIndex = 17;
            // 
            // gbBugFixes
            // 
            this.gbBugFixes.Controls.Add(this.checkBoxFixedFPS);
            this.gbBugFixes.Controls.Add(this.checkBoxModifySkydomeShader);
            this.gbBugFixes.Controls.Add(this.checkBoxFixBlueLightShader);
            this.gbBugFixes.Location = new System.Drawing.Point(6, 30);
            this.gbBugFixes.Name = "gbBugFixes";
            this.gbBugFixes.Size = new System.Drawing.Size(556, 95);
            this.gbBugFixes.TabIndex = 14;
            this.gbBugFixes.TabStop = false;
            this.gbBugFixes.Text = "Bug fixes";
            // 
            // checkBoxModifySkydomeShader
            // 
            this.checkBoxModifySkydomeShader.AutoSize = true;
            this.checkBoxModifySkydomeShader.Location = new System.Drawing.Point(21, 65);
            this.checkBoxModifySkydomeShader.Name = "checkBoxModifySkydomeShader";
            this.checkBoxModifySkydomeShader.Size = new System.Drawing.Size(175, 17);
            this.checkBoxModifySkydomeShader.TabIndex = 2;
            this.checkBoxModifySkydomeShader.Text = "Modify skybox shader [by Guzz]";
            this.checkBoxModifySkydomeShader.UseVisualStyleBackColor = true;
            // 
            // checkBoxFixBlueLightShader
            // 
            this.checkBoxFixBlueLightShader.AutoSize = true;
            this.checkBoxFixBlueLightShader.Location = new System.Drawing.Point(21, 42);
            this.checkBoxFixBlueLightShader.Name = "checkBoxFixBlueLightShader";
            this.checkBoxFixBlueLightShader.Size = new System.Drawing.Size(524, 17);
            this.checkBoxFixBlueLightShader.TabIndex = 1;
            this.checkBoxFixBlueLightShader.Text = "Modify blue light shader to allow normal completion of sorcerers (no need for abo" +
    "ve workaround) [by Guzz]";
            this.checkBoxFixBlueLightShader.UseVisualStyleBackColor = true;
            // 
            // linkLabelCoCF
            // 
            this.linkLabelCoCF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelCoCF.AutoSize = true;
            this.linkLabelCoCF.Location = new System.Drawing.Point(24, 473);
            this.linkLabelCoCF.Name = "linkLabelCoCF";
            this.linkLabelCoCF.Size = new System.Drawing.Size(327, 13);
            this.linkLabelCoCF.TabIndex = 13;
            this.linkLabelCoCF.TabStop = true;
            this.linkLabelCoCF.Text = "http://forums.bethsoft.com/index.php?/topic/1072120-dcotepatch/";
            this.linkLabelCoCF.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCoCF_LinkClicked);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(6, 449);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(556, 46);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "Additional information about this patch can be found at:";
            // 
            // groupboxXbill
            // 
            this.groupboxXbill.Controls.Add(this.checkBoxFilmEffects);
            this.groupboxXbill.Controls.Add(this.checkBoxExtendedMovie);
            this.groupboxXbill.Location = new System.Drawing.Point(6, 257);
            this.groupboxXbill.Name = "groupboxXbill";
            this.groupboxXbill.Size = new System.Drawing.Size(556, 72);
            this.groupboxXbill.TabIndex = 10;
            this.groupboxXbill.TabStop = false;
            this.groupboxXbill.Text = "Extras discovered by xbill";
            // 
            // groupBoxEnhance
            // 
            this.groupBoxEnhance.Controls.Add(this.checkBoxAllowLegalSkip);
            this.groupBoxEnhance.Controls.Add(this.checkBoxSkipVideos);
            this.groupBoxEnhance.Controls.Add(this.checkBoxReachArm);
            this.groupBoxEnhance.Location = new System.Drawing.Point(6, 187);
            this.groupBoxEnhance.Name = "groupBoxEnhance";
            this.groupBoxEnhance.Size = new System.Drawing.Size(556, 64);
            this.groupBoxEnhance.TabIndex = 0;
            this.groupBoxEnhance.TabStop = false;
            this.groupBoxEnhance.Text = "Standard Enhancements";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.groupBoxCheats);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(568, 502);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Cheats";
            // 
            // groupBoxCheats
            // 
            this.groupBoxCheats.Controls.Add(this.checkBoxGodMode);
            this.groupBoxCheats.Controls.Add(this.checkBoxDebugMode);
            this.groupBoxCheats.Controls.Add(this.checkBoxFlyMode);
            this.groupBoxCheats.Controls.Add(this.checkBoxPickStartMap);
            this.groupBoxCheats.Controls.Add(this.checkBoxUnlockAllContent);
            this.groupBoxCheats.Controls.Add(this.checkBoxReleaseMode);
            this.groupBoxCheats.Location = new System.Drawing.Point(6, 3);
            this.groupBoxCheats.Name = "groupBoxCheats";
            this.groupBoxCheats.Size = new System.Drawing.Size(556, 186);
            this.groupBoxCheats.TabIndex = 12;
            this.groupBoxCheats.TabStop = false;
            this.groupBoxCheats.Text = "Cheats";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(568, 502);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Player Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblFOV);
            this.groupBox3.Controls.Add(this.mebFOV);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.mebFOVSneak);
            this.groupBox3.Location = new System.Drawing.Point(9, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(288, 68);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FOV";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "(Default is 55.0)";
            // 
            // lblFOV
            // 
            this.lblFOV.AutoSize = true;
            this.lblFOV.Location = new System.Drawing.Point(61, 16);
            this.lblFOV.Name = "lblFOV";
            this.lblFOV.Size = new System.Drawing.Size(28, 13);
            this.lblFOV.TabIndex = 6;
            this.lblFOV.Text = "FOV";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "FOV sneak mode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "(Default is 68.0)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label108);
            this.groupBox2.Controls.Add(this.label109);
            this.groupBox2.Controls.Add(this.dCoTEFloatBox47);
            this.groupBox2.Controls.Add(this.label106);
            this.groupBox2.Controls.Add(this.label107);
            this.groupBox2.Controls.Add(this.dCoTEFloatBox46);
            this.groupBox2.Controls.Add(this.label104);
            this.groupBox2.Controls.Add(this.label105);
            this.groupBox2.Controls.Add(this.dCoTEFloatBox45);
            this.groupBox2.Controls.Add(this.label102);
            this.groupBox2.Controls.Add(this.label103);
            this.groupBox2.Controls.Add(this.dCoTEFloatBox44);
            this.groupBox2.Controls.Add(this.label100);
            this.groupBox2.Controls.Add(this.label101);
            this.groupBox2.Controls.Add(this.dCoTEFloatBox43);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.mebMorphine);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.mebBandages);
            this.groupBox2.Controls.Add(this.mebSplints);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.mebSutures);
            this.groupBox2.Location = new System.Drawing.Point(8, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 226);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Medical";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(201, 202);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(75, 13);
            this.label108.TabIndex = 37;
            this.label108.Text = "(Default is 2.0)";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(13, 202);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(125, 13);
            this.label109.TabIndex = 36;
            this.label109.Text = "Treatment time overhead";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(201, 180);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(75, 13);
            this.label106.TabIndex = 34;
            this.label106.Text = "(Default is 2.0)";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(13, 180);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(115, 13);
            this.label107.TabIndex = 33;
            this.label107.Text = "Antidote treatment time";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(201, 157);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(75, 13);
            this.label104.TabIndex = 31;
            this.label104.Text = "(Default is 2.0)";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(13, 157);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(112, 13);
            this.label105.TabIndex = 30;
            this.label105.Text = "Sutures treatment time";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(201, 133);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(75, 13);
            this.label102.TabIndex = 28;
            this.label102.Text = "(Default is 2.0)";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(13, 133);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(102, 13);
            this.label103.TabIndex = 27;
            this.label103.Text = "Splint treatment time";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(200, 109);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(75, 13);
            this.label100.TabIndex = 25;
            this.label100.Text = "(Default is 2.0)";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(12, 109);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(133, 13);
            this.label101.TabIndex = 24;
            this.label101.Text = "Bandage treatment time (s)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "(Default is 20.0)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Morphine time";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(201, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "(Default is 5)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Max Bandages";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Max Sutures";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "(Default is 10)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(201, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "(Default is 5)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Max Splints";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label46);
            this.groupBox8.Controls.Add(this.label47);
            this.groupBox8.Controls.Add(this.DCoTEFloatBox16);
            this.groupBox8.Controls.Add(this.label50);
            this.groupBox8.Controls.Add(this.label52);
            this.groupBox8.Controls.Add(this.DCoTEFloatBox20);
            this.groupBox8.Location = new System.Drawing.Point(8, 316);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(289, 60);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Sanity";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(219, 16);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(54, 13);
            this.label46.TabIndex = 14;
            this.label46.Text = "(Def 0.08)";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(9, 16);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(166, 13);
            this.label47.TabIndex = 12;
            this.label47.Text = "Sanity recovered per 0.1 seconds";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(11, 39);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(132, 13);
            this.label50.TabIndex = 15;
            this.label50.Text = "Morphine affects Sanity by";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(192, 40);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(81, 13);
            this.label52.TabIndex = 16;
            this.label52.Text = "(Default is 21.0)";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label56);
            this.groupBox9.Controls.Add(this.label57);
            this.groupBox9.Controls.Add(this.DCoTEFloatBox21);
            this.groupBox9.Controls.Add(this.label58);
            this.groupBox9.Controls.Add(this.label59);
            this.groupBox9.Controls.Add(this.DCoTEFloatBox22);
            this.groupBox9.Location = new System.Drawing.Point(9, 382);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(289, 60);
            this.groupBox9.TabIndex = 14;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Poison";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(198, 16);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(71, 13);
            this.label56.TabIndex = 14;
            this.label56.Text = "(Default 0.05)";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(9, 16);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(140, 13);
            this.label57.TabIndex = 12;
            this.label57.Text = "Poison Damage per Second";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(11, 39);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(68, 13);
            this.label58.TabIndex = 15;
            this.label58.Text = "Poison Timer";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(198, 40);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(72, 13);
            this.label59.TabIndex = 16;
            this.label59.Text = "(Default is 15)";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label48);
            this.groupBox7.Controls.Add(this.label49);
            this.groupBox7.Controls.Add(this.DCoTEFloatBox17);
            this.groupBox7.Controls.Add(this.label51);
            this.groupBox7.Controls.Add(this.label53);
            this.groupBox7.Controls.Add(this.label54);
            this.groupBox7.Controls.Add(this.DCoTEFloatBox18);
            this.groupBox7.Controls.Add(this.DCoTEFloatBox19);
            this.groupBox7.Controls.Add(this.label55);
            this.groupBox7.Location = new System.Drawing.Point(303, 172);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(257, 86);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Falling Damage";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(150, 16);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(87, 13);
            this.label48.TabIndex = 14;
            this.label48.Text = "(Default is 995.0)";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(11, 16);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(75, 13);
            this.label49.TabIndex = 12;
            this.label49.Text = "Fatal Distance";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(11, 39);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(81, 13);
            this.label51.TabIndex = 15;
            this.label51.Text = "Heavy Damage";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(150, 39);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(87, 13);
            this.label53.TabIndex = 16;
            this.label53.Text = "(Default is 595.0)";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(150, 62);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(87, 13);
            this.label54.TabIndex = 20;
            this.label54.Text = "(Default is 395.0)";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(11, 62);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(73, 13);
            this.label55.TabIndex = 18;
            this.label55.Text = "Light Damage";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.DCoTEFloatBox5);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.mebCrawlSpeed);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.mebWalkSpeed);
            this.groupBox4.Controls.Add(this.DCoTEFloatBox3);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.DCoTEFloatBox4);
            this.groupBox4.Location = new System.Drawing.Point(303, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(257, 149);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Player Movement";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(151, 115);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(81, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "(Default is 25.0)";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(19, 115);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 13);
            this.label21.TabIndex = 24;
            this.label21.Text = "Climbing Speed";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(151, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "(Default is 180.0)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Crawl Speed";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(151, 91);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "(Default is 590.0)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Walking Speed";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(19, 91);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Jumping Speed";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(151, 45);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 13);
            this.label17.TabIndex = 16;
            this.label17.Text = "(Default is 380.0)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(151, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "(Default is 560.0)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(19, 68);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "Running Speed";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBoxInfinite);
            this.tabPage3.Controls.Add(this.groupBoxWeapons);
            this.tabPage3.Controls.Add(this.groupBoxDamage);
            this.tabPage3.Controls.Add(this.groupBoxMelee);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(568, 502);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Weapons and Damage";
            // 
            // groupBoxInfinite
            // 
            this.groupBoxInfinite.Controls.Add(this.checkBoxInfiniteAmmo);
            this.groupBoxInfinite.Location = new System.Drawing.Point(282, 330);
            this.groupBoxInfinite.Name = "groupBoxInfinite";
            this.groupBoxInfinite.Size = new System.Drawing.Size(280, 64);
            this.groupBoxInfinite.TabIndex = 11;
            this.groupBoxInfinite.TabStop = false;
            this.groupBoxInfinite.Text = "Extras discovered by Fraya";
            // 
            // groupBoxWeapons
            // 
            this.groupBoxWeapons.Controls.Add(this.label94);
            this.groupBoxWeapons.Controls.Add(this.label95);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox40);
            this.groupBoxWeapons.Controls.Add(this.label96);
            this.groupBoxWeapons.Controls.Add(this.label97);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox41);
            this.groupBoxWeapons.Controls.Add(this.label98);
            this.groupBoxWeapons.Controls.Add(this.label99);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox42);
            this.groupBoxWeapons.Controls.Add(this.label88);
            this.groupBoxWeapons.Controls.Add(this.label89);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox37);
            this.groupBoxWeapons.Controls.Add(this.label90);
            this.groupBoxWeapons.Controls.Add(this.label91);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox38);
            this.groupBoxWeapons.Controls.Add(this.label92);
            this.groupBoxWeapons.Controls.Add(this.label93);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox39);
            this.groupBoxWeapons.Controls.Add(this.label82);
            this.groupBoxWeapons.Controls.Add(this.label83);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox34);
            this.groupBoxWeapons.Controls.Add(this.label84);
            this.groupBoxWeapons.Controls.Add(this.label85);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox35);
            this.groupBoxWeapons.Controls.Add(this.label86);
            this.groupBoxWeapons.Controls.Add(this.label87);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox36);
            this.groupBoxWeapons.Controls.Add(this.label76);
            this.groupBoxWeapons.Controls.Add(this.label77);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox31);
            this.groupBoxWeapons.Controls.Add(this.label78);
            this.groupBoxWeapons.Controls.Add(this.label79);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox32);
            this.groupBoxWeapons.Controls.Add(this.label80);
            this.groupBoxWeapons.Controls.Add(this.label81);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox33);
            this.groupBoxWeapons.Controls.Add(this.label74);
            this.groupBoxWeapons.Controls.Add(this.label75);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox30);
            this.groupBoxWeapons.Controls.Add(this.label72);
            this.groupBoxWeapons.Controls.Add(this.label73);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox29);
            this.groupBoxWeapons.Controls.Add(this.label70);
            this.groupBoxWeapons.Controls.Add(this.label71);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox28);
            this.groupBoxWeapons.Controls.Add(this.label68);
            this.groupBoxWeapons.Controls.Add(this.label69);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox27);
            this.groupBoxWeapons.Controls.Add(this.label66);
            this.groupBoxWeapons.Controls.Add(this.label67);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox26);
            this.groupBoxWeapons.Controls.Add(this.label64);
            this.groupBoxWeapons.Controls.Add(this.label65);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox25);
            this.groupBoxWeapons.Controls.Add(this.label62);
            this.groupBoxWeapons.Controls.Add(this.label63);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox24);
            this.groupBoxWeapons.Controls.Add(this.label60);
            this.groupBoxWeapons.Controls.Add(this.label61);
            this.groupBoxWeapons.Controls.Add(this.dCoTEFloatBox23);
            this.groupBoxWeapons.Location = new System.Drawing.Point(6, 6);
            this.groupBoxWeapons.Name = "groupBoxWeapons";
            this.groupBoxWeapons.Size = new System.Drawing.Size(270, 446);
            this.groupBoxWeapons.TabIndex = 9;
            this.groupBoxWeapons.TabStop = false;
            this.groupBoxWeapons.Text = "Weapons";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(58, 422);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(55, 13);
            this.label94.TabIndex = 81;
            this.label94.Text = "Projectiles";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(176, 422);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(66, 13);
            this.label95.TabIndex = 82;
            this.label95.Text = "(Default is 1)";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(58, 401);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(47, 13);
            this.label96.TabIndex = 78;
            this.label96.Text = "Damage";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(176, 401);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(75, 13);
            this.label97.TabIndex = 79;
            this.label97.Text = "(Default is 8.0)";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(58, 380);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(47, 13);
            this.label98.TabIndex = 75;
            this.label98.Text = "Clip Size";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(176, 380);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(72, 13);
            this.label99.TabIndex = 76;
            this.label99.Text = "(Default is 50)";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(58, 337);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(55, 13);
            this.label88.TabIndex = 72;
            this.label88.Text = "Projectiles";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(176, 337);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(66, 13);
            this.label89.TabIndex = 73;
            this.label89.Text = "(Default is 1)";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(58, 316);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(47, 13);
            this.label90.TabIndex = 69;
            this.label90.Text = "Damage";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(176, 316);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(81, 13);
            this.label91.TabIndex = 70;
            this.label91.Text = "(Default is 80.0)";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(57, 295);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(47, 13);
            this.label92.TabIndex = 66;
            this.label92.Text = "Clip Size";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(175, 295);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(66, 13);
            this.label93.TabIndex = 67;
            this.label93.Text = "(Default is 5)";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(57, 253);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(55, 13);
            this.label82.TabIndex = 63;
            this.label82.Text = "Projectiles";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(176, 253);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(66, 13);
            this.label83.TabIndex = 64;
            this.label83.Text = "(Default is 8)";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(57, 232);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(47, 13);
            this.label84.TabIndex = 60;
            this.label84.Text = "Damage";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(176, 232);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(81, 13);
            this.label85.TabIndex = 61;
            this.label85.Text = "(Default is 20.0)";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(57, 210);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(47, 13);
            this.label86.TabIndex = 57;
            this.label86.Text = "Clip Size";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(176, 210);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(66, 13);
            this.label87.TabIndex = 58;
            this.label87.Text = "(Default is 2)";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(57, 81);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(55, 13);
            this.label76.TabIndex = 54;
            this.label76.Text = "Projectiles";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(176, 81);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(66, 13);
            this.label77.TabIndex = 55;
            this.label77.Text = "(Default is 1)";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(57, 60);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(47, 13);
            this.label78.TabIndex = 51;
            this.label78.Text = "Damage";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(176, 60);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(81, 13);
            this.label79.TabIndex = 52;
            this.label79.Text = "(Default is 20.0)";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(57, 39);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(47, 13);
            this.label80.TabIndex = 48;
            this.label80.Text = "Clip Size";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(176, 39);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(66, 13);
            this.label81.TabIndex = 49;
            this.label81.Text = "(Default is 9)";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(57, 167);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(55, 13);
            this.label74.TabIndex = 45;
            this.label74.Text = "Projectiles";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(176, 167);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(66, 13);
            this.label75.TabIndex = 46;
            this.label75.Text = "(Default is 1)";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(57, 145);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(47, 13);
            this.label72.TabIndex = 42;
            this.label72.Text = "Damage";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(176, 145);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(81, 13);
            this.label73.TabIndex = 43;
            this.label73.Text = "(Default is 32.0)";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(57, 124);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(47, 13);
            this.label70.TabIndex = 39;
            this.label70.Text = "Clip Size";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(176, 124);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(66, 13);
            this.label71.TabIndex = 40;
            this.label71.Text = "(Default is 6)";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(6, 358);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(114, 13);
            this.label68.TabIndex = 36;
            this.label68.Text = "Tommygun Max Ammo";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(176, 358);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(78, 13);
            this.label69.TabIndex = 37;
            this.label69.Text = "(Default is 200)";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 274);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(111, 13);
            this.label66.TabIndex = 33;
            this.label66.Text = "Springfield Max Ammo";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(176, 274);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(72, 13);
            this.label67.TabIndex = 34;
            this.label67.Text = "(Default is 30)";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 189);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(102, 13);
            this.label64.TabIndex = 30;
            this.label64.Text = "Shotgun Max Ammo";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(176, 189);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(72, 13);
            this.label65.TabIndex = 31;
            this.label65.Text = "(Default is 30)";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(6, 102);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(105, 13);
            this.label62.TabIndex = 27;
            this.label62.Text = "Revolver Max Ammo";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(176, 102);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(72, 13);
            this.label63.TabIndex = 28;
            this.label63.Text = "(Default is 50)";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(5, 18);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(106, 13);
            this.label60.TabIndex = 24;
            this.label60.Text = "Colt Semi Max Ammo";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(176, 18);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(72, 13);
            this.label61.TabIndex = 25;
            this.label61.Text = "(Default is 50)";
            // 
            // groupBoxDamage
            // 
            this.groupBoxDamage.Controls.Add(this.label32);
            this.groupBoxDamage.Controls.Add(this.label33);
            this.groupBoxDamage.Controls.Add(this.DCoTEFloatBox9);
            this.groupBoxDamage.Controls.Add(this.label22);
            this.groupBoxDamage.Controls.Add(this.label23);
            this.groupBoxDamage.Controls.Add(this.DCoTEFloatBox1);
            this.groupBoxDamage.Controls.Add(this.label24);
            this.groupBoxDamage.Controls.Add(this.label25);
            this.groupBoxDamage.Controls.Add(this.DCoTEFloatBox2);
            this.groupBoxDamage.Controls.Add(this.label26);
            this.groupBoxDamage.Controls.Add(this.label27);
            this.groupBoxDamage.Controls.Add(this.label28);
            this.groupBoxDamage.Controls.Add(this.label29);
            this.groupBoxDamage.Controls.Add(this.label30);
            this.groupBoxDamage.Controls.Add(this.DCoTEFloatBox6);
            this.groupBoxDamage.Controls.Add(this.DCoTEFloatBox7);
            this.groupBoxDamage.Controls.Add(this.label31);
            this.groupBoxDamage.Controls.Add(this.DCoTEFloatBox8);
            this.groupBoxDamage.Location = new System.Drawing.Point(282, 6);
            this.groupBoxDamage.Name = "groupBoxDamage";
            this.groupBoxDamage.Size = new System.Drawing.Size(280, 156);
            this.groupBoxDamage.TabIndex = 5;
            this.groupBoxDamage.TabStop = false;
            this.groupBoxDamage.Text = "Damage Multipliers";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(150, 133);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(75, 13);
            this.label32.TabIndex = 28;
            this.label32.Text = "(Default is 0.4)";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(11, 133);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 13);
            this.label33.TabIndex = 27;
            this.label33.Text = "Right Leg";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(150, 109);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(75, 13);
            this.label22.TabIndex = 25;
            this.label22.Text = "(Default is 0.4)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 109);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(46, 13);
            this.label23.TabIndex = 24;
            this.label23.Text = "Left Leg";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(150, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(75, 13);
            this.label24.TabIndex = 14;
            this.label24.Text = "(Default is 4.0)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(11, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(33, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "Head";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(150, 85);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(75, 13);
            this.label26.TabIndex = 22;
            this.label26.Text = "(Default is 0.6)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(11, 39);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(34, 13);
            this.label27.TabIndex = 15;
            this.label27.Text = "Torso";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(11, 85);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 13);
            this.label28.TabIndex = 21;
            this.label28.Text = "Right Arm";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(150, 39);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(75, 13);
            this.label29.TabIndex = 16;
            this.label29.Text = "(Default is 1.0)";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(150, 62);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(75, 13);
            this.label30.TabIndex = 20;
            this.label30.Text = "(Default is 0.6)";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(11, 62);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(46, 13);
            this.label31.TabIndex = 18;
            this.label31.Text = "Left Arm";
            // 
            // groupBoxMelee
            // 
            this.groupBoxMelee.Controls.Add(this.label34);
            this.groupBoxMelee.Controls.Add(this.label35);
            this.groupBoxMelee.Controls.Add(this.DCoTEFloatBox10);
            this.groupBoxMelee.Controls.Add(this.label36);
            this.groupBoxMelee.Controls.Add(this.label37);
            this.groupBoxMelee.Controls.Add(this.DCoTEFloatBox11);
            this.groupBoxMelee.Controls.Add(this.label38);
            this.groupBoxMelee.Controls.Add(this.label39);
            this.groupBoxMelee.Controls.Add(this.DCoTEFloatBox12);
            this.groupBoxMelee.Controls.Add(this.label40);
            this.groupBoxMelee.Controls.Add(this.label41);
            this.groupBoxMelee.Controls.Add(this.label42);
            this.groupBoxMelee.Controls.Add(this.label43);
            this.groupBoxMelee.Controls.Add(this.label44);
            this.groupBoxMelee.Controls.Add(this.DCoTEFloatBox13);
            this.groupBoxMelee.Controls.Add(this.DCoTEFloatBox14);
            this.groupBoxMelee.Controls.Add(this.label45);
            this.groupBoxMelee.Controls.Add(this.DCoTEFloatBox15);
            this.groupBoxMelee.Location = new System.Drawing.Point(282, 168);
            this.groupBoxMelee.Name = "groupBoxMelee";
            this.groupBoxMelee.Size = new System.Drawing.Size(280, 156);
            this.groupBoxMelee.TabIndex = 6;
            this.groupBoxMelee.TabStop = false;
            this.groupBoxMelee.Text = "Melee Damage Multipliers";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(150, 133);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(75, 13);
            this.label34.TabIndex = 28;
            this.label34.Text = "(Default is 0.5)";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(11, 133);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(53, 13);
            this.label35.TabIndex = 27;
            this.label35.Text = "Right Leg";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(150, 109);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(75, 13);
            this.label36.TabIndex = 25;
            this.label36.Text = "(Default is 0.5)";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(11, 109);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(46, 13);
            this.label37.TabIndex = 24;
            this.label37.Text = "Left Leg";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(150, 16);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(75, 13);
            this.label38.TabIndex = 14;
            this.label38.Text = "(Default is 1.0)";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(11, 16);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(33, 13);
            this.label39.TabIndex = 12;
            this.label39.Text = "Head";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(150, 85);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(75, 13);
            this.label40.TabIndex = 22;
            this.label40.Text = "(Default is 0.9)";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(11, 39);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(34, 13);
            this.label41.TabIndex = 15;
            this.label41.Text = "Torso";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(11, 85);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(53, 13);
            this.label42.TabIndex = 21;
            this.label42.Text = "Right Arm";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(150, 39);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(75, 13);
            this.label43.TabIndex = 16;
            this.label43.Text = "(Default is 1.0)";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(150, 62);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(75, 13);
            this.label44.TabIndex = 20;
            this.label44.Text = "(Default is 0.9)";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(11, 62);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(46, 13);
            this.label45.TabIndex = 18;
            this.label45.Text = "Left Arm";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBoxAdvancedXML);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(568, 502);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Advanced XML Data";
            // 
            // groupBoxAdvancedXML
            // 
            this.groupBoxAdvancedXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAdvancedXML.Controls.Add(this.textBox3);
            this.groupBoxAdvancedXML.Controls.Add(this.cbBreakStuff);
            this.groupBoxAdvancedXML.Controls.Add(this.pbXML);
            this.groupBoxAdvancedXML.Controls.Add(this.btnExtractXML);
            this.groupBoxAdvancedXML.Controls.Add(this.dgSettings);
            this.groupBoxAdvancedXML.Location = new System.Drawing.Point(6, 3);
            this.groupBoxAdvancedXML.Name = "groupBoxAdvancedXML";
            this.groupBoxAdvancedXML.Size = new System.Drawing.Size(556, 496);
            this.groupBoxAdvancedXML.TabIndex = 1;
            this.groupBoxAdvancedXML.TabStop = false;
            this.groupBoxAdvancedXML.Text = "XML";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(89, 10);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(332, 32);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "Note that once you save after reading the xml you cannot use the simple interface" +
    " options for this version of the exe.";
            // 
            // cbBreakStuff
            // 
            this.cbBreakStuff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBreakStuff.Location = new System.Drawing.Point(427, 21);
            this.cbBreakStuff.Name = "cbBreakStuff";
            this.cbBreakStuff.Size = new System.Drawing.Size(120, 17);
            this.cbBreakStuff.TabIndex = 4;
            this.cbBreakStuff.Text = "I want to break stuff";
            this.cbBreakStuff.CheckedChanged += new System.EventHandler(this.cbBreakStuff_CheckedChanged);
            // 
            // pbXML
            // 
            this.pbXML.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbXML.Location = new System.Drawing.Point(6, 480);
            this.pbXML.Maximum = 25;
            this.pbXML.Name = "pbXML";
            this.pbXML.Size = new System.Drawing.Size(543, 10);
            this.pbXML.Step = 1;
            this.pbXML.TabIndex = 2;
            // 
            // btnExtractXML
            // 
            this.btnExtractXML.Location = new System.Drawing.Point(8, 19);
            this.btnExtractXML.Name = "btnExtractXML";
            this.btnExtractXML.Size = new System.Drawing.Size(75, 23);
            this.btnExtractXML.TabIndex = 1;
            this.btnExtractXML.Text = "Read XML";
            this.btnExtractXML.Click += new System.EventHandler(this.btnExtractXML_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(93, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(335, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Home for patch and other dcote tools http://dcotetools.sucklead.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.Location = new System.Drawing.Point(91, 30);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(489, 66);
            this.textBox5.TabIndex = 18;
            this.textBox5.Text = resources.GetString("textBox5.Text");
            // 
            // gbLegacyBugFixes
            // 
            this.gbLegacyBugFixes.Controls.Add(this.checkBoxFixBlueLight);
            this.gbLegacyBugFixes.Location = new System.Drawing.Point(8, 131);
            this.gbLegacyBugFixes.Name = "gbLegacyBugFixes";
            this.gbLegacyBugFixes.Size = new System.Drawing.Size(556, 50);
            this.gbLegacyBugFixes.TabIndex = 15;
            this.gbLegacyBugFixes.TabStop = false;
            this.gbLegacyBugFixes.Text = "Legacy Bug fixes (Use bug fixes now instead)";
            // 
            // checkBoxFixBlueLight
            // 
            this.checkBoxFixBlueLight.AutoSize = true;
            this.checkBoxFixBlueLight.Location = new System.Drawing.Point(21, 19);
            this.checkBoxFixBlueLight.Name = "checkBoxFixBlueLight";
            this.checkBoxFixBlueLight.Size = new System.Drawing.Size(498, 17);
            this.checkBoxFixBlueLight.TabIndex = 0;
            this.checkBoxFixBlueLight.Text = "Skip the blue light bug! (script will treat sorcerers as killed after 3rd wave) [" +
    "Use blue light shader now]";
            this.checkBoxFixBlueLight.UseVisualStyleBackColor = true;
            // 
            // checkBoxFixedFPS
            // 
            this.checkBoxFixedFPS.AutoSize = true;
            this.checkBoxFixedFPS.Location = new System.Drawing.Point(21, 19);
            this.checkBoxFixedFPS.Name = "checkBoxFixedFPS";
            this.checkBoxFixedFPS.Size = new System.Drawing.Size(298, 17);
            this.checkBoxFixedFPS.TabIndex = 3;
            this.checkBoxFixedFPS.Text = "Fix FPS to 60 to prevent most of the timing bugs [by GOG]";
            this.checkBoxFixedFPS.UseVisualStyleBackColor = true;
            // 
            // checkBoxFilmEffects
            // 
            this.checkBoxFilmEffects.CodeOffset = 0;
            this.checkBoxFilmEffects.DefaultValue = false;
            this.checkBoxFilmEffects.Location = new System.Drawing.Point(21, 42);
            this.checkBoxFilmEffects.Name = "checkBoxFilmEffects";
            this.checkBoxFilmEffects.Size = new System.Drawing.Size(292, 17);
            this.checkBoxFilmEffects.TabIndex = 9;
            this.checkBoxFilmEffects.Text = "Disable Filmgrain/Letterbox on cutscenes";
            // 
            // checkBoxExtendedMovie
            // 
            this.checkBoxExtendedMovie.CodeOffset = 0;
            this.checkBoxExtendedMovie.DefaultValue = false;
            this.checkBoxExtendedMovie.Location = new System.Drawing.Point(21, 19);
            this.checkBoxExtendedMovie.Name = "checkBoxExtendedMovie";
            this.checkBoxExtendedMovie.Size = new System.Drawing.Size(292, 17);
            this.checkBoxExtendedMovie.TabIndex = 8;
            this.checkBoxExtendedMovie.Text = "Always Show Extended End Movie";
            // 
            // checkBoxAllowLegalSkip
            // 
            this.checkBoxAllowLegalSkip.CodeOffset = 0;
            this.checkBoxAllowLegalSkip.DefaultValue = false;
            this.checkBoxAllowLegalSkip.Location = new System.Drawing.Point(21, 19);
            this.checkBoxAllowLegalSkip.Name = "checkBoxAllowLegalSkip";
            this.checkBoxAllowLegalSkip.Size = new System.Drawing.Size(152, 17);
            this.checkBoxAllowLegalSkip.TabIndex = 9;
            this.checkBoxAllowLegalSkip.Text = "Allow skip of legal video";
            // 
            // checkBoxSkipVideos
            // 
            this.checkBoxSkipVideos.CodeOffset = 2382035;
            this.checkBoxSkipVideos.DefaultValue = false;
            this.checkBoxSkipVideos.Location = new System.Drawing.Point(220, 19);
            this.checkBoxSkipVideos.Name = "checkBoxSkipVideos";
            this.checkBoxSkipVideos.Size = new System.Drawing.Size(192, 17);
            this.checkBoxSkipVideos.TabIndex = 1;
            this.checkBoxSkipVideos.Text = "Auto Skip All Startup Videos";
            // 
            // checkBoxReachArm
            // 
            this.checkBoxReachArm.CodeOffset = 2375501;
            this.checkBoxReachArm.DefaultValue = false;
            this.checkBoxReachArm.Location = new System.Drawing.Point(21, 42);
            this.checkBoxReachArm.Name = "checkBoxReachArm";
            this.checkBoxReachArm.Size = new System.Drawing.Size(229, 17);
            this.checkBoxReachArm.TabIndex = 3;
            this.checkBoxReachArm.Text = "Enable Reach Arm (Dropped feature)";
            // 
            // checkBoxGodMode
            // 
            this.checkBoxGodMode.CodeOffset = 2375208;
            this.checkBoxGodMode.DefaultValue = false;
            this.checkBoxGodMode.Location = new System.Drawing.Point(21, 156);
            this.checkBoxGodMode.Name = "checkBoxGodMode";
            this.checkBoxGodMode.Size = new System.Drawing.Size(119, 17);
            this.checkBoxGodMode.TabIndex = 6;
            this.checkBoxGodMode.Text = "God Mode (yukk!)";
            // 
            // checkBoxDebugMode
            // 
            this.checkBoxDebugMode.CodeOffset = 2369703;
            this.checkBoxDebugMode.DefaultValue = false;
            this.checkBoxDebugMode.Location = new System.Drawing.Point(21, 19);
            this.checkBoxDebugMode.Name = "checkBoxDebugMode";
            this.checkBoxDebugMode.Size = new System.Drawing.Size(391, 17);
            this.checkBoxDebugMode.TabIndex = 0;
            this.checkBoxDebugMode.Text = "Enable Debug Mode (F1 to activate then w,x && ENTER to navigate)";
            // 
            // checkBoxFlyMode
            // 
            this.checkBoxFlyMode.CodeOffset = 2380692;
            this.checkBoxFlyMode.DefaultValue = false;
            this.checkBoxFlyMode.Location = new System.Drawing.Point(21, 134);
            this.checkBoxFlyMode.Name = "checkBoxFlyMode";
            this.checkBoxFlyMode.Size = new System.Drawing.Size(510, 17);
            this.checkBoxFlyMode.TabIndex = 5;
            this.checkBoxFlyMode.Text = "Start In Fly Mode (The Debug Mode menu allows fly mode to be toggled on/off at wi" +
    "ll)";
            // 
            // checkBoxPickStartMap
            // 
            this.checkBoxPickStartMap.CodeOffset = 0;
            this.checkBoxPickStartMap.DefaultValue = false;
            this.checkBoxPickStartMap.Location = new System.Drawing.Point(21, 65);
            this.checkBoxPickStartMap.Name = "checkBoxPickStartMap";
            this.checkBoxPickStartMap.Size = new System.Drawing.Size(329, 17);
            this.checkBoxPickStartMap.TabIndex = 4;
            this.checkBoxPickStartMap.Text = "Allow choice of start map when starting a new game";
            // 
            // checkBoxUnlockAllContent
            // 
            this.checkBoxUnlockAllContent.CodeOffset = 0;
            this.checkBoxUnlockAllContent.DefaultValue = false;
            this.checkBoxUnlockAllContent.Location = new System.Drawing.Point(21, 88);
            this.checkBoxUnlockAllContent.Name = "checkBoxUnlockAllContent";
            this.checkBoxUnlockAllContent.Size = new System.Drawing.Size(391, 17);
            this.checkBoxUnlockAllContent.TabIndex = 7;
            this.checkBoxUnlockAllContent.Text = "Unlock All Bonus Content (Requires Debug Mode to also be enabled)";
            // 
            // checkBoxReleaseMode
            // 
            this.checkBoxReleaseMode.CodeOffset = 2370476;
            this.checkBoxReleaseMode.DefaultValue = true;
            this.checkBoxReleaseMode.Location = new System.Drawing.Point(21, 42);
            this.checkBoxReleaseMode.Name = "checkBoxReleaseMode";
            this.checkBoxReleaseMode.Size = new System.Drawing.Size(442, 17);
            this.checkBoxReleaseMode.TabIndex = 2;
            this.checkBoxReleaseMode.Text = "Release Mode (Untick this to allow Dev Continue when you die)";
            // 
            // mebFOV
            // 
            this.mebFOV.CodeOffset = 2374839;
            this.mebFOV.DefaultValue = "55.0";
            this.mebFOV.Location = new System.Drawing.Point(156, 13);
            this.mebFOV.Mask = "##,#";
            this.mebFOV.MaxValue = 99.9F;
            this.mebFOV.Name = "mebFOV";
            this.mebFOV.Size = new System.Drawing.Size(38, 20);
            this.mebFOV.TabIndex = 0;
            this.mebFOV.Text = "__._";
            // 
            // mebFOVSneak
            // 
            this.mebFOVSneak.CodeOffset = 2374887;
            this.mebFOVSneak.DefaultValue = "68.0";
            this.mebFOVSneak.Location = new System.Drawing.Point(156, 35);
            this.mebFOVSneak.Mask = "##,#";
            this.mebFOVSneak.MaxValue = 99.9F;
            this.mebFOVSneak.Name = "mebFOVSneak";
            this.mebFOVSneak.Size = new System.Drawing.Size(38, 20);
            this.mebFOVSneak.TabIndex = 1;
            this.mebFOVSneak.Text = "__._";
            // 
            // dCoTEFloatBox47
            // 
            this.dCoTEFloatBox47.CodeOffset = 2377769;
            this.dCoTEFloatBox47.DefaultValue = "2.0";
            this.dCoTEFloatBox47.Location = new System.Drawing.Point(157, 199);
            this.dCoTEFloatBox47.Mask = "#,#";
            this.dCoTEFloatBox47.MaxValue = 9.9F;
            this.dCoTEFloatBox47.Name = "dCoTEFloatBox47";
            this.dCoTEFloatBox47.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox47.TabIndex = 35;
            this.dCoTEFloatBox47.Text = "_,_";
            // 
            // dCoTEFloatBox46
            // 
            this.dCoTEFloatBox46.CodeOffset = 2377708;
            this.dCoTEFloatBox46.DefaultValue = "2.0";
            this.dCoTEFloatBox46.Location = new System.Drawing.Point(157, 177);
            this.dCoTEFloatBox46.Mask = "#,#";
            this.dCoTEFloatBox46.MaxValue = 9.9F;
            this.dCoTEFloatBox46.Name = "dCoTEFloatBox46";
            this.dCoTEFloatBox46.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox46.TabIndex = 32;
            this.dCoTEFloatBox46.Text = "_,_";
            // 
            // dCoTEFloatBox45
            // 
            this.dCoTEFloatBox45.CodeOffset = 2377648;
            this.dCoTEFloatBox45.DefaultValue = "2.0";
            this.dCoTEFloatBox45.Location = new System.Drawing.Point(157, 154);
            this.dCoTEFloatBox45.Mask = "#,#";
            this.dCoTEFloatBox45.MaxValue = 9.9F;
            this.dCoTEFloatBox45.Name = "dCoTEFloatBox45";
            this.dCoTEFloatBox45.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox45.TabIndex = 29;
            this.dCoTEFloatBox45.Text = "_,_";
            // 
            // dCoTEFloatBox44
            // 
            this.dCoTEFloatBox44.CodeOffset = 2377590;
            this.dCoTEFloatBox44.DefaultValue = "2.0";
            this.dCoTEFloatBox44.Location = new System.Drawing.Point(157, 130);
            this.dCoTEFloatBox44.Mask = "#,#";
            this.dCoTEFloatBox44.MaxValue = 9.9F;
            this.dCoTEFloatBox44.Name = "dCoTEFloatBox44";
            this.dCoTEFloatBox44.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox44.TabIndex = 26;
            this.dCoTEFloatBox44.Text = "_,_";
            // 
            // dCoTEFloatBox43
            // 
            this.dCoTEFloatBox43.CodeOffset = 2377531;
            this.dCoTEFloatBox43.DefaultValue = "2.0";
            this.dCoTEFloatBox43.Location = new System.Drawing.Point(157, 106);
            this.dCoTEFloatBox43.Mask = "#,#";
            this.dCoTEFloatBox43.MaxValue = 9.9F;
            this.dCoTEFloatBox43.Name = "dCoTEFloatBox43";
            this.dCoTEFloatBox43.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox43.TabIndex = 23;
            this.dCoTEFloatBox43.Text = "_,_";
            // 
            // mebMorphine
            // 
            this.mebMorphine.CodeOffset = 2377000;
            this.mebMorphine.DefaultValue = "20.0";
            this.mebMorphine.Location = new System.Drawing.Point(157, 13);
            this.mebMorphine.Mask = "##,#";
            this.mebMorphine.MaxValue = 99.9F;
            this.mebMorphine.Name = "mebMorphine";
            this.mebMorphine.Size = new System.Drawing.Size(38, 20);
            this.mebMorphine.TabIndex = 0;
            this.mebMorphine.Text = "__._";
            // 
            // mebBandages
            // 
            this.mebBandages.CodeOffset = 2377850;
            this.mebBandages.DefaultValue = "10";
            this.mebBandages.Location = new System.Drawing.Point(157, 36);
            this.mebBandages.Mask = "##";
            this.mebBandages.MaxValue = 99F;
            this.mebBandages.Name = "mebBandages";
            this.mebBandages.Size = new System.Drawing.Size(38, 20);
            this.mebBandages.TabIndex = 1;
            this.mebBandages.Text = "__";
            // 
            // mebSplints
            // 
            this.mebSplints.CodeOffset = 2377890;
            this.mebSplints.DefaultValue = "5";
            this.mebSplints.Location = new System.Drawing.Point(157, 59);
            this.mebSplints.Mask = "#";
            this.mebSplints.MaxValue = 9F;
            this.mebSplints.Name = "mebSplints";
            this.mebSplints.Size = new System.Drawing.Size(38, 20);
            this.mebSplints.TabIndex = 2;
            this.mebSplints.Text = "_";
            // 
            // mebSutures
            // 
            this.mebSutures.CodeOffset = 2377928;
            this.mebSutures.DefaultValue = "5";
            this.mebSutures.Location = new System.Drawing.Point(157, 82);
            this.mebSutures.Mask = "#";
            this.mebSutures.MaxValue = 9F;
            this.mebSutures.Name = "mebSutures";
            this.mebSutures.Size = new System.Drawing.Size(38, 20);
            this.mebSutures.TabIndex = 3;
            this.mebSutures.Text = "_";
            // 
            // DCoTEFloatBox16
            // 
            this.DCoTEFloatBox16.CodeOffset = 2387234;
            this.DCoTEFloatBox16.DefaultValue = "0.08";
            this.DCoTEFloatBox16.Location = new System.Drawing.Point(181, 13);
            this.DCoTEFloatBox16.Mask = "#,##";
            this.DCoTEFloatBox16.MaxValue = 9.99F;
            this.DCoTEFloatBox16.Name = "DCoTEFloatBox16";
            this.DCoTEFloatBox16.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox16.TabIndex = 0;
            this.DCoTEFloatBox16.Text = "_.__";
            // 
            // DCoTEFloatBox20
            // 
            this.DCoTEFloatBox20.CodeOffset = 2387404;
            this.DCoTEFloatBox20.DefaultValue = "21.0";
            this.DCoTEFloatBox20.Location = new System.Drawing.Point(148, 36);
            this.DCoTEFloatBox20.Mask = "##,#";
            this.DCoTEFloatBox20.MaxValue = 99.9F;
            this.DCoTEFloatBox20.Name = "DCoTEFloatBox20";
            this.DCoTEFloatBox20.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox20.TabIndex = 1;
            this.DCoTEFloatBox20.Text = "__._";
            // 
            // DCoTEFloatBox21
            // 
            this.DCoTEFloatBox21.CodeOffset = 2377146;
            this.DCoTEFloatBox21.DefaultValue = "0.05";
            this.DCoTEFloatBox21.Location = new System.Drawing.Point(157, 13);
            this.DCoTEFloatBox21.Mask = "#,##";
            this.DCoTEFloatBox21.MaxValue = 9.99F;
            this.DCoTEFloatBox21.Name = "DCoTEFloatBox21";
            this.DCoTEFloatBox21.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox21.TabIndex = 0;
            this.DCoTEFloatBox21.Text = "_.__";
            // 
            // DCoTEFloatBox22
            // 
            this.DCoTEFloatBox22.CodeOffset = 2377340;
            this.DCoTEFloatBox22.DefaultValue = "15";
            this.DCoTEFloatBox22.Location = new System.Drawing.Point(157, 36);
            this.DCoTEFloatBox22.Mask = "##";
            this.DCoTEFloatBox22.MaxValue = 99F;
            this.DCoTEFloatBox22.Name = "DCoTEFloatBox22";
            this.DCoTEFloatBox22.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox22.TabIndex = 1;
            this.DCoTEFloatBox22.Text = "__";
            // 
            // DCoTEFloatBox17
            // 
            this.DCoTEFloatBox17.CodeOffset = 2383548;
            this.DCoTEFloatBox17.DefaultValue = "995.0";
            this.DCoTEFloatBox17.Location = new System.Drawing.Point(106, 13);
            this.DCoTEFloatBox17.Mask = "###,#";
            this.DCoTEFloatBox17.MaxValue = 999.9F;
            this.DCoTEFloatBox17.Name = "DCoTEFloatBox17";
            this.DCoTEFloatBox17.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox17.TabIndex = 0;
            this.DCoTEFloatBox17.Text = "___._";
            // 
            // DCoTEFloatBox18
            // 
            this.DCoTEFloatBox18.CodeOffset = 2383602;
            this.DCoTEFloatBox18.DefaultValue = "595.0";
            this.DCoTEFloatBox18.Location = new System.Drawing.Point(106, 36);
            this.DCoTEFloatBox18.Mask = "###,#";
            this.DCoTEFloatBox18.MaxValue = 999.9F;
            this.DCoTEFloatBox18.Name = "DCoTEFloatBox18";
            this.DCoTEFloatBox18.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox18.TabIndex = 1;
            this.DCoTEFloatBox18.Text = "___._";
            // 
            // DCoTEFloatBox19
            // 
            this.DCoTEFloatBox19.CodeOffset = 2383662;
            this.DCoTEFloatBox19.DefaultValue = "395.0";
            this.DCoTEFloatBox19.Location = new System.Drawing.Point(106, 59);
            this.DCoTEFloatBox19.Mask = "###,#";
            this.DCoTEFloatBox19.MaxValue = 999.9F;
            this.DCoTEFloatBox19.Name = "DCoTEFloatBox19";
            this.DCoTEFloatBox19.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox19.TabIndex = 2;
            this.DCoTEFloatBox19.Text = "___._";
            // 
            // DCoTEFloatBox5
            // 
            this.DCoTEFloatBox5.CodeOffset = 2374245;
            this.DCoTEFloatBox5.DefaultValue = "25.0";
            this.DCoTEFloatBox5.Location = new System.Drawing.Point(106, 112);
            this.DCoTEFloatBox5.Mask = "##,#";
            this.DCoTEFloatBox5.MaxValue = 99.9F;
            this.DCoTEFloatBox5.Name = "DCoTEFloatBox5";
            this.DCoTEFloatBox5.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox5.TabIndex = 4;
            this.DCoTEFloatBox5.Text = "__._";
            // 
            // mebCrawlSpeed
            // 
            this.mebCrawlSpeed.CodeOffset = 2373900;
            this.mebCrawlSpeed.DefaultValue = "180.0";
            this.mebCrawlSpeed.Location = new System.Drawing.Point(106, 19);
            this.mebCrawlSpeed.Mask = "###,#";
            this.mebCrawlSpeed.MaxValue = 999.9F;
            this.mebCrawlSpeed.Name = "mebCrawlSpeed";
            this.mebCrawlSpeed.Size = new System.Drawing.Size(38, 20);
            this.mebCrawlSpeed.TabIndex = 0;
            this.mebCrawlSpeed.Text = "___._";
            // 
            // mebWalkSpeed
            // 
            this.mebWalkSpeed.CodeOffset = 2373967;
            this.mebWalkSpeed.DefaultValue = "380.0";
            this.mebWalkSpeed.Location = new System.Drawing.Point(106, 42);
            this.mebWalkSpeed.Mask = "###,#";
            this.mebWalkSpeed.MaxValue = 999.9F;
            this.mebWalkSpeed.Name = "mebWalkSpeed";
            this.mebWalkSpeed.Size = new System.Drawing.Size(38, 20);
            this.mebWalkSpeed.TabIndex = 1;
            this.mebWalkSpeed.Text = "___._";
            // 
            // DCoTEFloatBox3
            // 
            this.DCoTEFloatBox3.CodeOffset = 2374032;
            this.DCoTEFloatBox3.DefaultValue = "560.0";
            this.DCoTEFloatBox3.Location = new System.Drawing.Point(106, 65);
            this.DCoTEFloatBox3.Mask = "###,#";
            this.DCoTEFloatBox3.MaxValue = 999.9F;
            this.DCoTEFloatBox3.Name = "DCoTEFloatBox3";
            this.DCoTEFloatBox3.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox3.TabIndex = 2;
            this.DCoTEFloatBox3.Text = "___._";
            // 
            // DCoTEFloatBox4
            // 
            this.DCoTEFloatBox4.CodeOffset = 2374098;
            this.DCoTEFloatBox4.DefaultValue = "590.0";
            this.DCoTEFloatBox4.Location = new System.Drawing.Point(106, 88);
            this.DCoTEFloatBox4.Mask = "###,#";
            this.DCoTEFloatBox4.MaxValue = 999.9F;
            this.DCoTEFloatBox4.Name = "DCoTEFloatBox4";
            this.DCoTEFloatBox4.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox4.TabIndex = 3;
            this.DCoTEFloatBox4.Text = "___._";
            // 
            // checkBoxInfiniteAmmo
            // 
            this.checkBoxInfiniteAmmo.CodeOffset = 0;
            this.checkBoxInfiniteAmmo.DefaultValue = false;
            this.checkBoxInfiniteAmmo.Location = new System.Drawing.Point(14, 23);
            this.checkBoxInfiniteAmmo.Name = "checkBoxInfiniteAmmo";
            this.checkBoxInfiniteAmmo.Size = new System.Drawing.Size(211, 17);
            this.checkBoxInfiniteAmmo.TabIndex = 10;
            this.checkBoxInfiniteAmmo.Text = "Infinite Ammo";
            // 
            // dCoTEFloatBox40
            // 
            this.dCoTEFloatBox40.CodeOffset = 2398961;
            this.dCoTEFloatBox40.DefaultValue = "1";
            this.dCoTEFloatBox40.Location = new System.Drawing.Point(132, 419);
            this.dCoTEFloatBox40.Mask = "#";
            this.dCoTEFloatBox40.MaxValue = 9F;
            this.dCoTEFloatBox40.Name = "dCoTEFloatBox40";
            this.dCoTEFloatBox40.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox40.TabIndex = 80;
            this.dCoTEFloatBox40.Text = "_";
            // 
            // dCoTEFloatBox41
            // 
            this.dCoTEFloatBox41.CodeOffset = 2398928;
            this.dCoTEFloatBox41.DefaultValue = "8.0";
            this.dCoTEFloatBox41.Location = new System.Drawing.Point(132, 398);
            this.dCoTEFloatBox41.Mask = "#,#";
            this.dCoTEFloatBox41.MaxValue = 9.9F;
            this.dCoTEFloatBox41.Name = "dCoTEFloatBox41";
            this.dCoTEFloatBox41.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox41.TabIndex = 77;
            this.dCoTEFloatBox41.Text = "_._";
            // 
            // dCoTEFloatBox42
            // 
            this.dCoTEFloatBox42.CodeOffset = 2398906;
            this.dCoTEFloatBox42.DefaultValue = "50";
            this.dCoTEFloatBox42.Location = new System.Drawing.Point(132, 377);
            this.dCoTEFloatBox42.Mask = "##";
            this.dCoTEFloatBox42.MaxValue = 99F;
            this.dCoTEFloatBox42.Name = "dCoTEFloatBox42";
            this.dCoTEFloatBox42.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox42.TabIndex = 74;
            this.dCoTEFloatBox42.Text = "__";
            // 
            // dCoTEFloatBox37
            // 
            this.dCoTEFloatBox37.CodeOffset = 2398555;
            this.dCoTEFloatBox37.DefaultValue = "1";
            this.dCoTEFloatBox37.Location = new System.Drawing.Point(132, 334);
            this.dCoTEFloatBox37.Mask = "#";
            this.dCoTEFloatBox37.MaxValue = 9F;
            this.dCoTEFloatBox37.Name = "dCoTEFloatBox37";
            this.dCoTEFloatBox37.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox37.TabIndex = 71;
            this.dCoTEFloatBox37.Text = "_";
            // 
            // dCoTEFloatBox38
            // 
            this.dCoTEFloatBox38.CodeOffset = 2398521;
            this.dCoTEFloatBox38.DefaultValue = "80.0";
            this.dCoTEFloatBox38.Location = new System.Drawing.Point(132, 313);
            this.dCoTEFloatBox38.Mask = "##,#";
            this.dCoTEFloatBox38.MaxValue = 99.9F;
            this.dCoTEFloatBox38.Name = "dCoTEFloatBox38";
            this.dCoTEFloatBox38.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox38.TabIndex = 68;
            this.dCoTEFloatBox38.Text = "__._";
            // 
            // dCoTEFloatBox39
            // 
            this.dCoTEFloatBox39.CodeOffset = 2398500;
            this.dCoTEFloatBox39.DefaultValue = "5";
            this.dCoTEFloatBox39.Location = new System.Drawing.Point(132, 292);
            this.dCoTEFloatBox39.Mask = "#";
            this.dCoTEFloatBox39.MaxValue = 9F;
            this.dCoTEFloatBox39.Name = "dCoTEFloatBox39";
            this.dCoTEFloatBox39.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox39.TabIndex = 65;
            this.dCoTEFloatBox39.Text = "_";
            // 
            // dCoTEFloatBox34
            // 
            this.dCoTEFloatBox34.CodeOffset = 2398149;
            this.dCoTEFloatBox34.DefaultValue = "8";
            this.dCoTEFloatBox34.Location = new System.Drawing.Point(132, 250);
            this.dCoTEFloatBox34.Mask = "#";
            this.dCoTEFloatBox34.MaxValue = 9F;
            this.dCoTEFloatBox34.Name = "dCoTEFloatBox34";
            this.dCoTEFloatBox34.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox34.TabIndex = 62;
            this.dCoTEFloatBox34.Text = "_";
            // 
            // dCoTEFloatBox35
            // 
            this.dCoTEFloatBox35.CodeOffset = 2398115;
            this.dCoTEFloatBox35.DefaultValue = "20.0";
            this.dCoTEFloatBox35.Location = new System.Drawing.Point(132, 229);
            this.dCoTEFloatBox35.Mask = "##,#";
            this.dCoTEFloatBox35.MaxValue = 99.9F;
            this.dCoTEFloatBox35.Name = "dCoTEFloatBox35";
            this.dCoTEFloatBox35.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox35.TabIndex = 59;
            this.dCoTEFloatBox35.Text = "__._";
            // 
            // dCoTEFloatBox36
            // 
            this.dCoTEFloatBox36.CodeOffset = 2398094;
            this.dCoTEFloatBox36.DefaultValue = "2";
            this.dCoTEFloatBox36.Location = new System.Drawing.Point(132, 207);
            this.dCoTEFloatBox36.Mask = "#";
            this.dCoTEFloatBox36.MaxValue = 9F;
            this.dCoTEFloatBox36.Name = "dCoTEFloatBox36";
            this.dCoTEFloatBox36.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox36.TabIndex = 56;
            this.dCoTEFloatBox36.Text = "_";
            // 
            // dCoTEFloatBox31
            // 
            this.dCoTEFloatBox31.CodeOffset = 2397739;
            this.dCoTEFloatBox31.DefaultValue = "1";
            this.dCoTEFloatBox31.Location = new System.Drawing.Point(132, 78);
            this.dCoTEFloatBox31.Mask = "#";
            this.dCoTEFloatBox31.MaxValue = 9F;
            this.dCoTEFloatBox31.Name = "dCoTEFloatBox31";
            this.dCoTEFloatBox31.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox31.TabIndex = 53;
            this.dCoTEFloatBox31.Text = "_";
            // 
            // dCoTEFloatBox32
            // 
            this.dCoTEFloatBox32.CodeOffset = 2397705;
            this.dCoTEFloatBox32.DefaultValue = "20.0";
            this.dCoTEFloatBox32.Location = new System.Drawing.Point(132, 57);
            this.dCoTEFloatBox32.Mask = "##,#";
            this.dCoTEFloatBox32.MaxValue = 99.9F;
            this.dCoTEFloatBox32.Name = "dCoTEFloatBox32";
            this.dCoTEFloatBox32.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox32.TabIndex = 50;
            this.dCoTEFloatBox32.Text = "__._";
            // 
            // dCoTEFloatBox33
            // 
            this.dCoTEFloatBox33.CodeOffset = 2397684;
            this.dCoTEFloatBox33.DefaultValue = "9";
            this.dCoTEFloatBox33.Location = new System.Drawing.Point(132, 36);
            this.dCoTEFloatBox33.Mask = "#";
            this.dCoTEFloatBox33.MaxValue = 9F;
            this.dCoTEFloatBox33.Name = "dCoTEFloatBox33";
            this.dCoTEFloatBox33.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox33.TabIndex = 47;
            this.dCoTEFloatBox33.Text = "_";
            // 
            // dCoTEFloatBox30
            // 
            this.dCoTEFloatBox30.CodeOffset = 2397328;
            this.dCoTEFloatBox30.DefaultValue = "1";
            this.dCoTEFloatBox30.Location = new System.Drawing.Point(132, 164);
            this.dCoTEFloatBox30.Mask = "#";
            this.dCoTEFloatBox30.MaxValue = 9F;
            this.dCoTEFloatBox30.Name = "dCoTEFloatBox30";
            this.dCoTEFloatBox30.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox30.TabIndex = 44;
            this.dCoTEFloatBox30.Text = "_";
            // 
            // dCoTEFloatBox29
            // 
            this.dCoTEFloatBox29.CodeOffset = 2397294;
            this.dCoTEFloatBox29.DefaultValue = "32.0";
            this.dCoTEFloatBox29.Location = new System.Drawing.Point(132, 142);
            this.dCoTEFloatBox29.Mask = "##,#";
            this.dCoTEFloatBox29.MaxValue = 99.9F;
            this.dCoTEFloatBox29.Name = "dCoTEFloatBox29";
            this.dCoTEFloatBox29.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox29.TabIndex = 41;
            this.dCoTEFloatBox29.Text = "__._";
            // 
            // dCoTEFloatBox28
            // 
            this.dCoTEFloatBox28.CodeOffset = 2397273;
            this.dCoTEFloatBox28.DefaultValue = "6";
            this.dCoTEFloatBox28.Location = new System.Drawing.Point(132, 121);
            this.dCoTEFloatBox28.Mask = "#";
            this.dCoTEFloatBox28.MaxValue = 9F;
            this.dCoTEFloatBox28.Name = "dCoTEFloatBox28";
            this.dCoTEFloatBox28.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox28.TabIndex = 38;
            this.dCoTEFloatBox28.Text = "_";
            // 
            // dCoTEFloatBox27
            // 
            this.dCoTEFloatBox27.CodeOffset = 2519082;
            this.dCoTEFloatBox27.DefaultValue = "200";
            this.dCoTEFloatBox27.Location = new System.Drawing.Point(132, 355);
            this.dCoTEFloatBox27.Mask = "###";
            this.dCoTEFloatBox27.MaxValue = 999F;
            this.dCoTEFloatBox27.Name = "dCoTEFloatBox27";
            this.dCoTEFloatBox27.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox27.TabIndex = 35;
            this.dCoTEFloatBox27.Text = "___";
            // 
            // dCoTEFloatBox26
            // 
            this.dCoTEFloatBox26.CodeOffset = 2518864;
            this.dCoTEFloatBox26.DefaultValue = "30";
            this.dCoTEFloatBox26.Location = new System.Drawing.Point(132, 271);
            this.dCoTEFloatBox26.Mask = "##";
            this.dCoTEFloatBox26.MaxValue = 99F;
            this.dCoTEFloatBox26.Name = "dCoTEFloatBox26";
            this.dCoTEFloatBox26.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox26.TabIndex = 32;
            this.dCoTEFloatBox26.Text = "__";
            // 
            // dCoTEFloatBox25
            // 
            this.dCoTEFloatBox25.CodeOffset = 2518633;
            this.dCoTEFloatBox25.DefaultValue = "30";
            this.dCoTEFloatBox25.Location = new System.Drawing.Point(132, 186);
            this.dCoTEFloatBox25.Mask = "##";
            this.dCoTEFloatBox25.MaxValue = 99F;
            this.dCoTEFloatBox25.Name = "dCoTEFloatBox25";
            this.dCoTEFloatBox25.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox25.TabIndex = 29;
            this.dCoTEFloatBox25.Text = "__";
            // 
            // dCoTEFloatBox24
            // 
            this.dCoTEFloatBox24.CodeOffset = 2518408;
            this.dCoTEFloatBox24.DefaultValue = "50";
            this.dCoTEFloatBox24.Location = new System.Drawing.Point(132, 99);
            this.dCoTEFloatBox24.Mask = "##";
            this.dCoTEFloatBox24.MaxValue = 99F;
            this.dCoTEFloatBox24.Name = "dCoTEFloatBox24";
            this.dCoTEFloatBox24.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox24.TabIndex = 26;
            this.dCoTEFloatBox24.Text = "__";
            // 
            // dCoTEFloatBox23
            // 
            this.dCoTEFloatBox23.CodeOffset = 2518189;
            this.dCoTEFloatBox23.DefaultValue = "50";
            this.dCoTEFloatBox23.Location = new System.Drawing.Point(132, 15);
            this.dCoTEFloatBox23.Mask = "##";
            this.dCoTEFloatBox23.MaxValue = 99F;
            this.dCoTEFloatBox23.Name = "dCoTEFloatBox23";
            this.dCoTEFloatBox23.Size = new System.Drawing.Size(38, 20);
            this.dCoTEFloatBox23.TabIndex = 23;
            this.dCoTEFloatBox23.Text = "__";
            // 
            // DCoTEFloatBox9
            // 
            this.DCoTEFloatBox9.CodeOffset = 2400128;
            this.DCoTEFloatBox9.DefaultValue = "0.4";
            this.DCoTEFloatBox9.Location = new System.Drawing.Point(106, 130);
            this.DCoTEFloatBox9.Mask = "#,#";
            this.DCoTEFloatBox9.MaxValue = 9.9F;
            this.DCoTEFloatBox9.Name = "DCoTEFloatBox9";
            this.DCoTEFloatBox9.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox9.TabIndex = 5;
            this.DCoTEFloatBox9.Text = "_._";
            // 
            // DCoTEFloatBox1
            // 
            this.DCoTEFloatBox1.CodeOffset = 2400079;
            this.DCoTEFloatBox1.DefaultValue = "0.4";
            this.DCoTEFloatBox1.Location = new System.Drawing.Point(106, 106);
            this.DCoTEFloatBox1.Mask = "#,#";
            this.DCoTEFloatBox1.MaxValue = 9.9F;
            this.DCoTEFloatBox1.Name = "DCoTEFloatBox1";
            this.DCoTEFloatBox1.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox1.TabIndex = 4;
            this.DCoTEFloatBox1.Text = "_._";
            // 
            // DCoTEFloatBox2
            // 
            this.DCoTEFloatBox2.CodeOffset = 2399893;
            this.DCoTEFloatBox2.DefaultValue = "4.0";
            this.DCoTEFloatBox2.Location = new System.Drawing.Point(106, 13);
            this.DCoTEFloatBox2.Mask = "#,#";
            this.DCoTEFloatBox2.MaxValue = 9.9F;
            this.DCoTEFloatBox2.Name = "DCoTEFloatBox2";
            this.DCoTEFloatBox2.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox2.TabIndex = 0;
            this.DCoTEFloatBox2.Text = "_._";
            // 
            // DCoTEFloatBox6
            // 
            this.DCoTEFloatBox6.CodeOffset = 2399936;
            this.DCoTEFloatBox6.DefaultValue = "1.0";
            this.DCoTEFloatBox6.Location = new System.Drawing.Point(106, 36);
            this.DCoTEFloatBox6.Mask = "#,#";
            this.DCoTEFloatBox6.MaxValue = 9.9F;
            this.DCoTEFloatBox6.Name = "DCoTEFloatBox6";
            this.DCoTEFloatBox6.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox6.TabIndex = 1;
            this.DCoTEFloatBox6.Text = "_._";
            // 
            // DCoTEFloatBox7
            // 
            this.DCoTEFloatBox7.CodeOffset = 2399982;
            this.DCoTEFloatBox7.DefaultValue = "0.6";
            this.DCoTEFloatBox7.Location = new System.Drawing.Point(106, 59);
            this.DCoTEFloatBox7.Mask = "#,#";
            this.DCoTEFloatBox7.MaxValue = 9.9F;
            this.DCoTEFloatBox7.Name = "DCoTEFloatBox7";
            this.DCoTEFloatBox7.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox7.TabIndex = 2;
            this.DCoTEFloatBox7.Text = "_._";
            // 
            // DCoTEFloatBox8
            // 
            this.DCoTEFloatBox8.CodeOffset = 2400031;
            this.DCoTEFloatBox8.DefaultValue = "0.6";
            this.DCoTEFloatBox8.Location = new System.Drawing.Point(106, 82);
            this.DCoTEFloatBox8.Mask = "#,#";
            this.DCoTEFloatBox8.MaxValue = 9.9F;
            this.DCoTEFloatBox8.Name = "DCoTEFloatBox8";
            this.DCoTEFloatBox8.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox8.TabIndex = 3;
            this.DCoTEFloatBox8.Text = "_._";
            // 
            // DCoTEFloatBox10
            // 
            this.DCoTEFloatBox10.CodeOffset = 2400441;
            this.DCoTEFloatBox10.DefaultValue = "0.5";
            this.DCoTEFloatBox10.Location = new System.Drawing.Point(106, 130);
            this.DCoTEFloatBox10.Mask = "#,#";
            this.DCoTEFloatBox10.MaxValue = 9.9F;
            this.DCoTEFloatBox10.Name = "DCoTEFloatBox10";
            this.DCoTEFloatBox10.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox10.TabIndex = 5;
            this.DCoTEFloatBox10.Text = "_._";
            // 
            // DCoTEFloatBox11
            // 
            this.DCoTEFloatBox11.CodeOffset = 2400414;
            this.DCoTEFloatBox11.DefaultValue = "0.5";
            this.DCoTEFloatBox11.Location = new System.Drawing.Point(106, 106);
            this.DCoTEFloatBox11.Mask = "#,#";
            this.DCoTEFloatBox11.MaxValue = 9.9F;
            this.DCoTEFloatBox11.Name = "DCoTEFloatBox11";
            this.DCoTEFloatBox11.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox11.TabIndex = 4;
            this.DCoTEFloatBox11.Text = "_._";
            // 
            // DCoTEFloatBox12
            // 
            this.DCoTEFloatBox12.CodeOffset = 2400315;
            this.DCoTEFloatBox12.DefaultValue = "1.0";
            this.DCoTEFloatBox12.Location = new System.Drawing.Point(106, 13);
            this.DCoTEFloatBox12.Mask = "#,#";
            this.DCoTEFloatBox12.MaxValue = 9.9F;
            this.DCoTEFloatBox12.Name = "DCoTEFloatBox12";
            this.DCoTEFloatBox12.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox12.TabIndex = 0;
            this.DCoTEFloatBox12.Text = "_._";
            // 
            // DCoTEFloatBox13
            // 
            this.DCoTEFloatBox13.CodeOffset = 2400336;
            this.DCoTEFloatBox13.DefaultValue = "1.0";
            this.DCoTEFloatBox13.Location = new System.Drawing.Point(106, 36);
            this.DCoTEFloatBox13.Mask = "#,#";
            this.DCoTEFloatBox13.MaxValue = 9.9F;
            this.DCoTEFloatBox13.Name = "DCoTEFloatBox13";
            this.DCoTEFloatBox13.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox13.TabIndex = 1;
            this.DCoTEFloatBox13.Text = "_._";
            // 
            // DCoTEFloatBox14
            // 
            this.DCoTEFloatBox14.CodeOffset = 2400360;
            this.DCoTEFloatBox14.DefaultValue = "0.9";
            this.DCoTEFloatBox14.Location = new System.Drawing.Point(106, 59);
            this.DCoTEFloatBox14.Mask = "#,#";
            this.DCoTEFloatBox14.MaxValue = 9.9F;
            this.DCoTEFloatBox14.Name = "DCoTEFloatBox14";
            this.DCoTEFloatBox14.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox14.TabIndex = 2;
            this.DCoTEFloatBox14.Text = "_._";
            // 
            // DCoTEFloatBox15
            // 
            this.DCoTEFloatBox15.CodeOffset = 2400387;
            this.DCoTEFloatBox15.DefaultValue = "0.9";
            this.DCoTEFloatBox15.Location = new System.Drawing.Point(106, 82);
            this.DCoTEFloatBox15.Mask = "#,#";
            this.DCoTEFloatBox15.MaxValue = 9.9F;
            this.DCoTEFloatBox15.Name = "DCoTEFloatBox15";
            this.DCoTEFloatBox15.Size = new System.Drawing.Size(38, 20);
            this.DCoTEFloatBox15.TabIndex = 3;
            this.DCoTEFloatBox15.Text = "_._";
            // 
            // Form1
            // 
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(592, 660);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLoad);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(608, 659);
            this.Name = "Form1";
            this.Text = "DCoTE Patcher 1.7";
            ((System.ComponentModel.ISupportInitialize)(this.dgSettings)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.group15Features.ResumeLayout(false);
            this.group15Features.PerformLayout();
            this.gbBugFixes.ResumeLayout(false);
            this.gbBugFixes.PerformLayout();
            this.groupboxXbill.ResumeLayout(false);
            this.groupBoxEnhance.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBoxCheats.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBoxInfinite.ResumeLayout(false);
            this.groupBoxWeapons.ResumeLayout(false);
            this.groupBoxWeapons.PerformLayout();
            this.groupBoxDamage.ResumeLayout(false);
            this.groupBoxDamage.PerformLayout();
            this.groupBoxMelee.ResumeLayout(false);
            this.groupBoxMelee.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBoxAdvancedXML.ResumeLayout(false);
            this.groupBoxAdvancedXML.PerformLayout();
            this.gbLegacyBugFixes.ResumeLayout(false);
            this.gbLegacyBugFixes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private bool LoadFileToMemory(string loadFilename, out byte[] byteSpace, int fileSize, int steamFileSize, int d2dFileSize)
        {
            byteSpace = null;

            if (!File.Exists(loadFilename))
            {
                return false;
            }


#if !FX1_1
#else
            FileStream fileStream = null;
#endif
            try
            {
#if !FX1_1
                byteSpace = File.ReadAllBytes(loadFilename);

#else
                fileStream = new FileStream(loadFilename,FileMode.Open);
                BinaryReader binaryReader = new BinaryReader(fileStream);

                byteSpace = binaryReader.ReadBytes((int)fileStream.Length);

                fileStream.Close();
#endif
            }
            catch
            {
                MessageBox.Show("Failed to open file check permissions", "ERROR");
            }
            finally
            {
#if !FX1_1
#else
                if (fileStream != null)
                {
                    fileStream.Close();
                }
#endif
            }

            if (byteSpace == null)
            {
                MessageBox.Show("Uknown file read error.", "ERROR");
                return false;
            }
            //check file size
            else if (byteSpace.Length != fileSize
                     && byteSpace.Length != steamFileSize
                     && byteSpace.Length != d2dFileSize)
            {
                MessageBox.Show("Wrong file size[" + byteSpace.Length.ToString() + "] expecting [" + fileSize.ToString() + "] or [" + steamFileSize.ToString() + "] or [" + d2dFileSize.ToString() + "], please select correct exe.", "ERROR");
                return false;
            }

            return true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            this.openExeFileDialog.FileName = "CoCMainWin32.exe";
            this.openExeFileDialog.Filter = "CoCMainWin32.exe|CoCMainWin32.exe";

            if (Directory.Exists(@"C:\Program Files\Steam\steamapps\common\call of cthulhu\Engine\"))
            {
                this.openExeFileDialog.InitialDirectory = @"C:\Program Files\Steam\steamapps\common\call of cthulhu\Engine\";
            }
            else if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common\call of cthulhu\Engine\"))
            {
                this.openExeFileDialog.InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\call of cthulhu\Engine\";
            }
            else if (Directory.Exists(@"C:\Program Files (x86)\Bethesda Softworks\Call Of Cthulhu DCoTE\Engine\"))
            {
                this.openExeFileDialog.InitialDirectory = @"C:\Program Files (x86)\Bethesda Softworks\Call Of Cthulhu DCoTE\Engine\";
            }
            else
            {
                this.openExeFileDialog.InitialDirectory = @"C:\Program Files\Bethesda Softworks\Call Of Cthulhu DCoTE\Engine\";
            }

            if (openExeFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            filename = openExeFileDialog.FileName;

            BatFiles.Clear();
            BatFiles.Add(BatNames._01_house, new BatFile() { Name = BatNames._01_house, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\01_house.bat"), Filesize = 611051 });
            BatFiles.Add(BatNames._02_streets_one, new BatFile() { Name = BatNames._02_streets_one, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\02_streets_one.bat"), Filesize = 850661 });
            BatFiles.Add(BatNames._03_streets_two, new BatFile() { Name = BatNames._03_streets_two, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\03_streets_two.bat"), Filesize = 1198150 });
            BatFiles.Add(BatNames._04_jail_break, new BatFile() { Name = BatNames._04_jail_break, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\04_jail_break.bat"), Filesize = 787062 });
            BatFiles.Add(BatNames._05_streets_three, new BatFile() { Name = BatNames._05_streets_three, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\05_streets_three.bat"), Filesize = 823179 });
            BatFiles.Add(BatNames._06_refinery, new BatFile() { Name = BatNames._06_refinery, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\06_refinery.bat"), Filesize = 1405171 });
            //BatFiles.Add(BatNames._06_refinery_explosion, new BatFile() { Name = BatNames._06_refinery_explosion, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\06_refinery_explosion.bat"), Filesize = 129027 });
            BatFiles.Add(BatNames._07_esoteric_order, new BatFile() { Name = BatNames._07_esoteric_order, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\07_esoteric_order.bat"), Filesize = 991325 });
            BatFiles.Add(BatNames._08_boat, new BatFile() { Name = BatNames._08_boat, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\08_boat.bat"), Filesize = 754408 });
            BatFiles.Add(BatNames._09_reef, new BatFile() { Name = BatNames._09_reef, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\09_reef.bat"), Filesize = 552844 });
            BatFiles.Add(BatNames._10_air_filled_tunnels, new BatFile() { Name = BatNames._10_air_filled_tunnels, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\10_air_filled_tunnels.bat"), Filesize = 959305 });
            BatFiles.Add(BatNames.misc01_city_of_dreams_01, new BatFile() { Name = BatNames.misc01_city_of_dreams_01, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\misc01_city_of_dreams_01.bat"), Filesize = 334490 });
            BatFiles.Add(BatNames.misc01_city_of_dreams_02, new BatFile() { Name = BatNames.misc01_city_of_dreams_02, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\misc01_city_of_dreams_02.bat"), Filesize = 327649 });
            BatFiles.Add(BatNames.misc01_city_of_dreams_03, new BatFile() { Name = BatNames.misc01_city_of_dreams_03, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\misc01_city_of_dreams_03.bat"), Filesize = 385172 });
            BatFiles.Add(BatNames.misc02_jacks_office_cutscene, new BatFile() { Name = BatNames.misc02_jacks_office_cutscene, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\misc02_jacks_office_cutscene.bat"), Filesize = 340639 });
            BatFiles.Add(BatNames.misc03_asylum_cutscene, new BatFile() { Name = BatNames.misc03_asylum_cutscene, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\misc03_asylum_cutscene.bat"), Filesize = 322830 });
            BatFiles.Add(BatNames.misc03_asylum_cutscene_feds, new BatFile() { Name = BatNames.misc03_asylum_cutscene_feds, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\misc03_asylum_cutscene_feds.bat"), Filesize = 374145 });


            ShaderFiles.Clear();
            ShaderFiles.Add(ShaderNames.psModulateColorByAlpha, new ShaderFile() { Name = ShaderNames.psModulateColorByAlpha, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"Resources\PC\SHADERS\psModulateColorByAlpha.pso"), Filesize = 492 });
            ShaderFiles.Add(ShaderNames.Skydome_NoColor, new ShaderFile() { Name = ShaderNames.Skydome_NoColor, Filename = filename.Replace(@"Engine\CoCMainWin32.exe", @"Resources\PC\SHADERS\Skydome_NoColor.vso"), Filesize = 1280 });



            //mapCityOfDreams03name = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\misc01_city_of_dreams_03.bat");

            ////did we do a valid replace?
            //if (!mapCityOfDreams03name.EndsWith("misc01_city_of_dreams_03.bat")
            //    || !File.Exists(mapCityOfDreams03name))
            //{
            //    this.openExeFileDialog.FileName = "misc01_city_of_dreams_03.bat";
            //    this.openExeFileDialog.Filter = "misc01_city_of_dreams_03.bat|misc01_city_of_dreams_03.bat";
            //    this.openExeFileDialog.InitialDirectory = filename.Replace(@"Engine\CoCMainWin32.exe", "");
            //    if (openExeFileDialog.ShowDialog() == DialogResult.Cancel)
            //    {
            //        return;
            //    }
            //    mapCityOfDreams03name = openExeFileDialog.FileName;
            //}

            //map08Boatname = filename.Replace(@"Engine\CoCMainWin32.exe", @"scripts\08_Boat.bat");
            ////did we do a valid replace?
            //if (!map08Boatname.EndsWith("08_Boat.bat")
            //    || !File.Exists(map08Boatname))
            //{
            //    this.openExeFileDialog.FileName = "08_Boat.bat";
            //    this.openExeFileDialog.Filter = "08_Boat.bat|08_Boat.bat";
            //    this.openExeFileDialog.InitialDirectory = filename.Replace(@"Engine\CoCMainWin32.exe", "");
            //    if (openExeFileDialog.ShowDialog() == DialogResult.Cancel)
            //    {
            //        return;
            //    }
            //    map08Boatname = openExeFileDialog.FileName;
            //}

            // load the files            
            if (LoadFileToMemory(filename, out programCode, 2961408, 3313664, 2407425))
            {
                foreach (BatFile batFile in BatFiles.Values)
                {
                    if (!LoadFileToMemory(batFile.Filename, out batFile.Contents, batFile.Filesize, batFile.Filesize, batFile.Filesize))
                    {
                        MessageBox.Show(string.Format("Error, couldn't load {0} ", batFile.Name));
                        return;
                    }
                }

                foreach (ShaderFile shaderFile in ShaderFiles.Values)
                {
                    if (!LoadFileToMemory(shaderFile.Filename, out shaderFile.Contents, shaderFile.Filesize, shaderFile.Filesize, shaderFile.Filesize))
                    {
                        MessageBox.Show(string.Format("Error, couldn't load {0} ", shaderFile.Name));
                        return;
                    }
                }

                //if (!LoadFileToMemory(mapCityOfDreams03name, out mapCityOfDreams03, 385172, 385172, 385172))
                //{
                //    MessageBox.Show(string.Format("Error, couldn't load {0} ", mapCityOfDreams03name));
                //    return;
                //}
                //if (!LoadFileToMemory(map08Boatname, out map08Boat, 754408, 754408, 754408))
                //{
                //    MessageBox.Show(string.Format("Error, couldn't load {0} ", map08Boatname));
                //    return;
                //}
            }
            else
            {
                return;
            }

            if (programCode.Length == 2961408
                && programCode[0x40] == 0x56)
            {
                tbVersion.Text = "GOG version loaded";
                SteamVersion = false;
                D2DVersion = false;
            }
            else if (programCode.Length == 2961408)
            {
                tbVersion.Text = "Original retail version loaded";
                SteamVersion = false;
                D2DVersion = false;
            }
            else if (programCode.Length == 3313664)
            {
                tbVersion.Text = "Steam version loaded";
                SteamVersion = true;
                D2DVersion = false;
            }
            else if (programCode.Length == 2407425)
            {
                tbVersion.Text = "D2D version loaded";
                SteamVersion = false;
                D2DVersion = true;
            }

            Console.WriteLine("{0} {1}", SteamVersion, D2DVersion);

            LoadSettings();

            panel1.Enabled = true;
        }

        private ReturnStatus SaveFileFromMemory(string savefilename, string backupfilename, byte[] byteSpace)
        {
            bool bBackedUp = false;
            // backup the file
            if (!File.Exists(backupfilename))
            {
                bBackedUp = true;
                File.Copy(savefilename, backupfilename);
            }
            // backup the file
            if (!File.Exists(backupfilename))
            {
                MessageBox.Show("Couldn't backup file so NOT writing changes", "ERROR");
                return ReturnStatus.Failed;
            }

            try
            {
                File.WriteAllBytes(savefilename, byteSpace);
            }
            catch
            {
                MessageBox.Show("Patch failed!!! Most likely file (" + savefilename + ") is currently in use.", "ERROR");
                return ReturnStatus.Failed;
            }

            if (bBackedUp)
                return ReturnStatus.BackedUp;
            else
                return ReturnStatus.OK;
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string backupfilename;
            ReturnStatus returnStatus;
            //backupfilename = filename.Replace(".exe", ".original.exe");

            if (!D2DVersion && !SteamVersion)
            {
                if (programCode[2369023] != 0x0A)
                {

                    foreach (Control tab in tabControl1.Controls)
                    {
                        if (tab is TabPage)
                        {
                            foreach (Control controlBox in tab.Controls)
                            {
                                if (controlBox is GroupBox)
                                {
                                    foreach (Control control in controlBox.Controls)
                                    {
                                        if (control is DCoTEFloatBox)
                                        {
                                            if (!((DCoTEFloatBox)control).Validate())
                                            {
                                                tabControl1.SelectedTab = (TabPage)tab;
                                                control.Focus();
                                                MessageBox.Show("Please check all digits are populated before saving.");
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            SaveSettings();

            //write files and backup originals
            returnStatus = SaveFileFromMemory(filename, filename.Replace(".exe", ".original.exe"), programCode);

            foreach (BatFile batFile in BatFiles.Values)
            {
                SaveFileFromMemory(batFile.Filename, batFile.Filename.Replace(".bat", ".original.bat"), batFile.Contents);
            }

            foreach (ShaderFile shaderFile in ShaderFiles.Values)
            {
                SaveFileFromMemory(shaderFile.Filename, shaderFile.Filename.Replace(".vso", ".original.vso").Replace(".pso", ".original.pso"), shaderFile.Contents);
            }

            //SaveFileFromMemory(mapCityOfDreams03name, mapCityOfDreams03name.Replace(".bat", ".original.bat"), mapCityOfDreams03);

            //SaveFileFromMemory(map08Boatname, map08Boatname.Replace(".bat", ".original.bat"), map08Boat);

            if (returnStatus == ReturnStatus.BackedUp)
            {
                MessageBox.Show("Backups saved (CoCMainWin32.original.exe & *.original.bat) and file(s) patched.", "OK");
            }
            else
            {
                MessageBox.Show("File(s) patched.", "OK");
            }

        }

        private void LoadSettings()
        {
            //08 boat change
            //if (!D2DVersion)
            //{
            checkBoxFixBlueLight.Checked = (BatFiles[BatNames._08_boat].Contents[0x1F885] == 0x03);
            checkBoxFixBlueLightShader.Checked = (ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B0] == 0x00);
            checkBoxModifySkydomeShader.Checked = (ShaderFiles[ShaderNames.Skydome_NoColor].Contents[0x45C] == 0x01);
            //}
            //city of dreams change
            checkBoxExtendedMovie.Checked = (BatFiles[BatNames.misc01_city_of_dreams_03].Contents[3790] == 1);

            cbGiveAllWeapons.Checked = (BatFiles[BatNames._01_house].Contents[0x91086] == 1);
            cbUnlockDifficultyLevels.Checked = (BatFiles[BatNames._01_house].Contents[0x9107C] == 1);
            cbUnlockCinematics.Checked = (BatFiles[BatNames._01_house].Contents[0x91081] == 1);
            cbDoubleTimeEscapeSequence.Checked = (BatFiles[BatNames._10_air_filled_tunnels].Contents[0x93898] == 0x30);

            gbBugFixes.Enabled = true;
            if (SteamVersion || D2DVersion)
            {
                groupBoxAdvancedXML.Enabled = false;
                groupBoxWeapons.Enabled = false;
                groupBoxDamage.Enabled = false;
                groupBoxMelee.Enabled = false;
                groupBoxInfinite.Enabled = false;
                tabPage2.Enabled = false;
                groupBoxCheats.Enabled = false;
                groupBoxEnhance.Enabled = false;
                checkBoxFilmEffects.Enabled = false;

                tabControl1.Enabled = true;
            }
            else
            {
                checkBoxInfiniteAmmo.Checked = (programCode[173459] == 0x90);

                checkBoxFilmEffects.Checked = (programCode[2117090] != 0x89);

                checkBoxFixedFPS.Checked = (programCode[25485] != 0xC7);

                if (programCode[2369023] == 0x0A)
                {
                    groupBoxEnhance.Enabled = false;
                    //groupBoxCheats.Enabled = false;
                    checkBoxDebugMode.Enabled = false;
                    checkBoxReleaseMode.Enabled = false;
                    checkBoxPickStartMap.Enabled = false;
                    checkBoxFlyMode.Enabled = false;
                    checkBoxGodMode.Enabled = false;

                    tabPage2.Enabled = false;

                    groupBoxWeapons.Enabled = false;
                    groupBoxDamage.Enabled = false;
                    groupBoxMelee.Enabled = false;

                    checkBoxUnlockAllContent.Enabled = false;
                    btnResetToDefaults.Enabled = false;
                    /*checkBoxPickStartMap.Enabled = false;
                    checkBoxAllowLegalSkip.Enabled = false;*/
                }
                else
                {
                    //custom one
                    checkBoxUnlockAllContent.Checked = (programCode[709783] == 76);
                    checkBoxPickStartMap.Checked = (programCode[2380772] == 32);
                    checkBoxAllowLegalSkip.Checked = (programCode[2382267] != 0x4E);

                    foreach (Control tab in tabControl1.Controls)
                    {
                        if (tab is TabPage)
                        {
                            foreach (Control controlBox in tab.Controls)
                            {
                                if (controlBox is GroupBox)
                                {
                                    foreach (Control control in controlBox.Controls)
                                    {
                                        if (control is DCoTEFloatBox)
                                        {
                                            ((DCoTEFloatBox)control).ReadValue(programCode);
                                        }
                                        else if (control is DCoTECheckBox)
                                        {
                                            ((DCoTECheckBox)control).ReadValue(programCode);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                tabControl1.Enabled = true;
            }
            
        }
        
        private void SaveSettings()
        {
            if (!SteamVersion && !D2DVersion)
            {
                //unlock all content
                if (checkBoxUnlockAllContent.Checked)
                {
                    programCode[709783] = 76; //4C
                    programCode[709784] = 19; //13
                }
                else
                {
                    programCode[709783] = 120; //78
                    programCode[709784] = 20;  //14
                }

                //fixed fps
                if (checkBoxFixedFPS.Checked)
                {
                    programCode[25485] = 0x89;
                    programCode[25486] = 0x3D;

                    programCode[25491] = 0x90;
                    programCode[25492] = 0x90;
                    programCode[25493] = 0x90;
                    programCode[25494] = 0x90;
                }
                else
                {
                    programCode[25485] = 0xC7;
                    programCode[25486] = 0x05;

                    programCode[25491] = 0x00;
                    programCode[25492] = 0x00;
                    programCode[25493] = 0x00;
                    programCode[25494] = 0x80;
                }

                //infinite ammo
                if (checkBoxInfiniteAmmo.Checked)
                {
                    programCode[173459] = 0x90; //90
                    programCode[173460] = 0x90; //90
                }
                else
                {
                    programCode[173459] = 0x8B; //8B
                    programCode[173460] = 0x32; //32
                }

                //film effects?
                if (checkBoxFilmEffects.Checked)
                {
                    programCode[2117090] = 0xEB;
                    programCode[2117091] = 0x6E;
                    programCode[2117092] = 0x74;
                    programCode[2117093] = 0x11;
                    programCode[2117094] = 0x90;
                    programCode[2117095] = 0x90;

                    programCode[2117202] = 0x9C;
                    programCode[2117203] = 0xE8;
                    programCode[2117204] = 0xFC;
                    programCode[2117205] = 0x00;
                    programCode[2117206] = 0x00;
                    programCode[2117207] = 0x00;
                    programCode[2117208] = 0x89;
                    programCode[2117209] = 0x54;
                    programCode[2117210] = 0x24;
                    programCode[2117211] = 0x18;
                    programCode[2117212] = 0x9D;
                    programCode[2117213] = 0xEB;
                    programCode[2117214] = 0x85;

                    programCode[2117460] = 0x83;
                    programCode[2117461] = 0xFA;
                    programCode[2117462] = 0x01;
                    programCode[2117463] = 0x75;
                    programCode[2117464] = 0x02;
                    programCode[2117465] = 0x31;
                    programCode[2117466] = 0xD2;
                    programCode[2117467] = 0xC3;
                    programCode[2117468] = 0x90;
                    programCode[2117469] = 0x90;
                    programCode[2117470] = 0x90;
                    programCode[2117471] = 0x90;
                }
                else
                {
                    programCode[2117090] = 0x89;
                    programCode[2117091] = 0x54;
                    programCode[2117092] = 0x24;
                    programCode[2117093] = 0x14;
                    programCode[2117094] = 0x74;
                    programCode[2117095] = 0x0F;

                    programCode[2117202] = 0xCC;
                    programCode[2117203] = 0xCC;
                    programCode[2117204] = 0xCC;
                    programCode[2117205] = 0xCC;
                    programCode[2117206] = 0xCC;
                    programCode[2117207] = 0xCC;
                    programCode[2117208] = 0xCC;
                    programCode[2117209] = 0xCC;
                    programCode[2117210] = 0xCC;
                    programCode[2117211] = 0xCC;
                    programCode[2117212] = 0xCC;
                    programCode[2117213] = 0xCC;
                    programCode[2117214] = 0xCC;

                    programCode[2117460] = 0xCC;
                    programCode[2117461] = 0xCC;
                    programCode[2117462] = 0xCC;
                    programCode[2117463] = 0xCC;
                    programCode[2117464] = 0xCC;
                    programCode[2117465] = 0xCC;
                    programCode[2117466] = 0xCC;
                    programCode[2117467] = 0xCC;
                    programCode[2117468] = 0xCC;
                    programCode[2117469] = 0xCC;
                    programCode[2117470] = 0xCC;
                    programCode[2117471] = 0xCC;
                }


                if (programCode[2369023] != 0x0A)
                {

                    //custom one
                    if (checkBoxPickStartMap.Checked)
                    {
                        for (int i = 0; i < "MISC03_ASYLUM_CUTSCENE".Length; i++)
                        {
                            programCode[2380772 + i] = Convert.ToByte(' ');
                        }
                    }
                    else
                    {
                        for (int i = 0; i < "MISC03_ASYLUM_CUTSCENE".Length; i++)
                        {
                            programCode[2380772 + i] = Convert.ToByte("MISC03_ASYLUM_CUTSCENE"[i]);
                        }
                    }
                    //allow legal video skip?
                    if (checkBoxAllowLegalSkip.Checked)
                    {
                        for (int i = 0; i < ">legal.xmv</Video>\r\n\r\n\r\n\r\n\r\n\r\n".Length; i++)
                        {
                            programCode[2382267 + i] = Convert.ToByte(">legal.xmv</Video>\r\n\r\n\r\n\r\n\r\n\r\n"[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < "NoSkip>legal.xmv</VideoNoSkip>".Length; i++)
                        {
                            programCode[2382267 + i] = Convert.ToByte("NoSkip>legal.xmv</VideoNoSkip>"[i]);
                        }
                    }

                    foreach (Control tab in tabControl1.Controls)
                    {
                        if (tab is TabPage)
                        {
                            foreach (Control controlBox in tab.Controls)
                            {
                                if (controlBox is GroupBox)
                                {
                                    foreach (Control control in controlBox.Controls)
                                    {
                                        if (control is DCoTEFloatBox)
                                        {
                                            ((DCoTEFloatBox)control).WriteValue(programCode);
                                        }
                                        else if (control is DCoTECheckBox)
                                        {
                                            ((DCoTECheckBox)control).WriteValue(programCode);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (dgSettings.Enabled)
                {
                    UpdateXMLSettings();
                }
            }

            //08 boat change
            if (checkBoxFixBlueLight.Checked)
            {
                /*map08Boat[0x1F624] = 0x5D;
                map08Boat[0x1F62D] = 0x5F;
                map08Boat[0x1F631] = 0x6A;
                map08Boat[0x1F632] = 0x02;
                map08Boat[0x1F634] = 0x64;
                map08Boat[0x1F638] = 0x6B;
                map08Boat[0x1F639] = 0x02;
                map08Boat[0x1F63B] = 0x63;
                map08Boat[0x1F63F] = 0x63;
                map08Boat[0x1F645] = 0x6C;
                map08Boat[0x1F646] = 0x02;
                map08Boat[0x1F648] = 0x6D;
                map08Boat[0x1F649] = 0x02;
                map08Boat[0x1F64F] = 0x62;
                map08Boat[0x1F653] = 0x62;
                map08Boat[0x1F656] = 0x62;
                map08Boat[0x1F657] = 0x02;
                map08Boat[0x1F65D] = 0x6F;
                map08Boat[0x1F65E] = 0x02;
                map08Boat[0x1F660] = 0x62;
                map08Boat[0x1F668] = 0x70;
                map08Boat[0x1F669] = 0x02;
                map08Boat[0x1F673] = 0x71;
                map08Boat[0x1F674] = 0x02;*/

                BatFiles[BatNames._08_boat].Contents[0x1F74E] = 0x24;
                BatFiles[BatNames._08_boat].Contents[0x1F74F] = 0x03;
                BatFiles[BatNames._08_boat].Contents[0x1F750] = 0x04;
                BatFiles[BatNames._08_boat].Contents[0x1F751] = 0x01;
                BatFiles[BatNames._08_boat].Contents[0x1F754] = 0x04;
                BatFiles[BatNames._08_boat].Contents[0x1F756] = 0xFF;
                BatFiles[BatNames._08_boat].Contents[0x1F757] = 0x02;
                BatFiles[BatNames._08_boat].Contents[0x1F75A] = 0xFF;
                BatFiles[BatNames._08_boat].Contents[0x1F75E] = 0xFF;
                BatFiles[BatNames._08_boat].Contents[0x1F763] = 0x10;

                BatFiles[BatNames._08_boat].Contents[0x1F885] = 0x03;
            }
            else
            {
                /*map08Boat[0x1F624] = 0xF8;
                map08Boat[0x1F62D] = 0xFA;
                map08Boat[0x1F631] = 0x05;
                map08Boat[0x1F632] = 0x03;
                map08Boat[0x1F634] = 0xFF;
                map08Boat[0x1F638] = 0x06;
                map08Boat[0x1F639] = 0x03;
                map08Boat[0x1F63B] = 0xFE;
                map08Boat[0x1F63F] = 0xFE;
                map08Boat[0x1F645] = 0x07;
                map08Boat[0x1F646] = 0x03;
                map08Boat[0x1F648] = 0x08;
                map08Boat[0x1F649] = 0x03;
                map08Boat[0x1F64F] = 0xFD;
                map08Boat[0x1F653] = 0xFD;
                map08Boat[0x1F656] = 0x09;
                map08Boat[0x1F657] = 0x03;
                map08Boat[0x1F65D] = 0x0A;
                map08Boat[0x1F65E] = 0x03;
                map08Boat[0x1F660] = 0xFD;
                map08Boat[0x1F668] = 0x0B;
                map08Boat[0x1F669] = 0x03;
                map08Boat[0x1F673] = 0x0C;
                map08Boat[0x1F674] = 0x03;*/

                BatFiles[BatNames._08_boat].Contents[0x1F74E] = 0x85;
                BatFiles[BatNames._08_boat].Contents[0x1F74F] = 0x02;
                BatFiles[BatNames._08_boat].Contents[0x1F750] = 0x09;
                BatFiles[BatNames._08_boat].Contents[0x1F751] = 0x02;
                BatFiles[BatNames._08_boat].Contents[0x1F754] = 0x03;
                BatFiles[BatNames._08_boat].Contents[0x1F756] = 0x24;
                BatFiles[BatNames._08_boat].Contents[0x1F757] = 0x03;
                BatFiles[BatNames._08_boat].Contents[0x1F75A] = 0xFB;
                BatFiles[BatNames._08_boat].Contents[0x1F75E] = 0xFB;
                BatFiles[BatNames._08_boat].Contents[0x1F763] = 0x0E;


                BatFiles[BatNames._08_boat].Contents[0x1F885] = 0x00;
            }

            if (checkBoxFixBlueLightShader.Checked)
            {
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B0] = 0x00;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B1] = 0x00;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B2] = 0x80;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B3] = 0xBF;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B4] = 0x00;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B5] = 0x00;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B6] = 0x80;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B7] = 0xBF;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B8] = 0x00;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B9] = 0x00;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1BA] = 0x80;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1BB] = 0xBF;

            }
            else
            {
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B0] = 0xBC;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B1] = 0x74;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B2] = 0x13;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B3] = 0xBC;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B4] = 0xBC;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B5] = 0x74;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B6] = 0x13;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B7] = 0xBC;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B8] = 0xBC;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1B9] = 0x74;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1BA] = 0x13;
                ShaderFiles[ShaderNames.psModulateColorByAlpha].Contents[0x1BB] = 0xBC;
            }

            if (checkBoxModifySkydomeShader.Checked)
            {
                ShaderFiles[ShaderNames.Skydome_NoColor].Contents[0x45C] = 0x01;
            }
            else
            {
                ShaderFiles[ShaderNames.Skydome_NoColor].Contents[0x45C] = 0x00;
            }

            //city of dreams change
            if (checkBoxExtendedMovie.Checked)
            {
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[3790] = 1;
            }
            else
            {
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[3790] = 0;
            }

            if (cbGiveAllWeapons.Checked)
            {
                BatFiles[BatNames._01_house].Contents[0x91086] = 1;
                BatFiles[BatNames._02_streets_one].Contents[0xCB880] = 1;
                BatFiles[BatNames._03_streets_two].Contents[0x1207B8] = 1;
                BatFiles[BatNames._04_jail_break].Contents[0xBC1E8] = 1;
                BatFiles[BatNames._05_streets_three].Contents[0xC4EFD] = 1;
                BatFiles[BatNames._06_refinery].Contents[0x153065] = 1;
                BatFiles[BatNames._07_esoteric_order].Contents[0xEDDF8] = 1;
                BatFiles[BatNames._08_boat].Contents[0xB425A] = 1;
                BatFiles[BatNames._09_reef].Contents[0x82EFE] = 1;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0xE62BB] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_01].Contents[0x4DA0C] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_02].Contents[0x4BF53] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[0x5A006] = 1;
                BatFiles[BatNames.misc02_jacks_office_cutscene].Contents[0x4F03A] = 1;
                BatFiles[BatNames.misc03_asylum_cutscene].Contents[0x4AAA9] = 1;
                BatFiles[BatNames.misc03_asylum_cutscene_feds].Contents[0x574F3] = 1;
            }
            else
            {
                BatFiles[BatNames._01_house].Contents[0x91086] = 0;
                BatFiles[BatNames._02_streets_one].Contents[0xCB880] = 0;
                BatFiles[BatNames._03_streets_two].Contents[0x1207B8] = 0;
                BatFiles[BatNames._04_jail_break].Contents[0xBC1E8] = 0;
                BatFiles[BatNames._05_streets_three].Contents[0xC4EFD] = 0;
                BatFiles[BatNames._06_refinery].Contents[0x153065] = 0;
                BatFiles[BatNames._07_esoteric_order].Contents[0xEDDF8] = 0;
                BatFiles[BatNames._08_boat].Contents[0xB425A] = 0;
                BatFiles[BatNames._09_reef].Contents[0x82EFE] = 0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0xE62BB] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_01].Contents[0x4DA0C] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_02].Contents[0x4BF53] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[0x5A006] = 0;
                BatFiles[BatNames.misc02_jacks_office_cutscene].Contents[0x4F03A] = 0;
                BatFiles[BatNames.misc03_asylum_cutscene].Contents[0x4AAA9] = 0;
                BatFiles[BatNames.misc03_asylum_cutscene_feds].Contents[0x574F3] = 0;
            }

            if (cbUnlockDifficultyLevels.Checked)
            {
                BatFiles[BatNames._01_house].Contents[0x9107C] = 1;
                BatFiles[BatNames._02_streets_one].Contents[0xCB876] = 1;
                BatFiles[BatNames._03_streets_two].Contents[0x1207AE] = 1;
                BatFiles[BatNames._04_jail_break].Contents[0xBC1DE] = 1;
                BatFiles[BatNames._05_streets_three].Contents[0xC4EF3] = 1;
                BatFiles[BatNames._06_refinery].Contents[0x15305B] = 1;
                BatFiles[BatNames._07_esoteric_order].Contents[0xEDDEE] = 1;
                BatFiles[BatNames._08_boat].Contents[0xB4250] = 1;
                BatFiles[BatNames._09_reef].Contents[0x82EF4] = 1;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0xE62B1] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_01].Contents[0x4DA02] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_02].Contents[0x4BF49] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[0x59FFC] = 1;
                BatFiles[BatNames.misc02_jacks_office_cutscene].Contents[0x4F030] = 1;
                BatFiles[BatNames.misc03_asylum_cutscene].Contents[0x4AA9F] = 1;
                BatFiles[BatNames.misc03_asylum_cutscene_feds].Contents[0x574E9] = 1;
            }
            else
            {
                BatFiles[BatNames._01_house].Contents[0x9107C] = 0;
                BatFiles[BatNames._02_streets_one].Contents[0xCB876] = 0;
                BatFiles[BatNames._03_streets_two].Contents[0x1207AE] = 0;
                BatFiles[BatNames._04_jail_break].Contents[0xBC1DE] = 0;
                BatFiles[BatNames._05_streets_three].Contents[0xC4EF3] = 0;
                BatFiles[BatNames._06_refinery].Contents[0x15305B] = 0;
                BatFiles[BatNames._07_esoteric_order].Contents[0xEDDEE] = 0;
                BatFiles[BatNames._08_boat].Contents[0xB4250] = 0;
                BatFiles[BatNames._09_reef].Contents[0x82EF4] = 0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0xE62B1] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_01].Contents[0x4DA02] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_02].Contents[0x4BF49] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[0x59FFC] = 0;
                BatFiles[BatNames.misc02_jacks_office_cutscene].Contents[0x4F030] = 0;
                BatFiles[BatNames.misc03_asylum_cutscene].Contents[0x4AA9F] = 0;
                BatFiles[BatNames.misc03_asylum_cutscene_feds].Contents[0x574E9] = 0;
            }

            if (cbUnlockCinematics.Checked)
            {
                BatFiles[BatNames._01_house].Contents[0x91081] = 1;
                BatFiles[BatNames._02_streets_one].Contents[0xCB87B] = 1;
                BatFiles[BatNames._03_streets_two].Contents[0x1207B3] = 1;
                BatFiles[BatNames._04_jail_break].Contents[0xBC1E3] = 1;
                BatFiles[BatNames._05_streets_three].Contents[0xC4EF8] = 1;
                BatFiles[BatNames._06_refinery].Contents[0x153060] = 1;
                BatFiles[BatNames._07_esoteric_order].Contents[0xEDDF3] = 1;
                BatFiles[BatNames._08_boat].Contents[0xB4255] = 1;
                BatFiles[BatNames._09_reef].Contents[0x82EF9] = 1;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0xE62B6] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_01].Contents[0x4DA07] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_02].Contents[0x4BF4E] = 1;
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[0x5A001] = 1;
                BatFiles[BatNames.misc02_jacks_office_cutscene].Contents[0x4F035] = 1;
                BatFiles[BatNames.misc03_asylum_cutscene].Contents[0x4AAA4] = 1;
                BatFiles[BatNames.misc03_asylum_cutscene_feds].Contents[0x574EE] = 1;
            }
            else
            {
                BatFiles[BatNames._01_house].Contents[0x91081] = 0;
                BatFiles[BatNames._02_streets_one].Contents[0xCB87B] = 0;
                BatFiles[BatNames._03_streets_two].Contents[0x1207B3] = 0;
                BatFiles[BatNames._04_jail_break].Contents[0xBC1E3] = 0;
                BatFiles[BatNames._05_streets_three].Contents[0xC4EF8] = 0;
                BatFiles[BatNames._06_refinery].Contents[0x153060] = 0;
                BatFiles[BatNames._07_esoteric_order].Contents[0xEDDF3] = 0;
                BatFiles[BatNames._08_boat].Contents[0xB4255] = 0;
                BatFiles[BatNames._09_reef].Contents[0x82EF9] = 0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0xE62B6] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_01].Contents[0x4DA07] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_02].Contents[0x4BF4E] = 0;
                BatFiles[BatNames.misc01_city_of_dreams_03].Contents[0x5A001] = 0;
                BatFiles[BatNames.misc02_jacks_office_cutscene].Contents[0x4F035] = 0;
                BatFiles[BatNames.misc03_asylum_cutscene].Contents[0x4AAA4] = 0;
                BatFiles[BatNames.misc03_asylum_cutscene_feds].Contents[0x574EE] = 0;
            }

            if (cbDoubleTimeEscapeSequence.Checked)
            {
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x93898] = 0x30;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x93899] = 0x42;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938A2] = 0x48;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938A3] = 0x42;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938AC] = 0x20;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938AD] = 0x42;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938B6] = 0x20;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938B7] = 0x42;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938C0] = 0x20;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938C1] = 0x42;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938CA] = 0x20;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938CB] = 0x42;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938D4] = 0x8C;
            }
            else
            {
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x93898] = 0xB0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x93899] = 0x41;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938A2] = 0xC8;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938A3] = 0x41;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938AC] = 0xA0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938AD] = 0x41;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938B6] = 0xA0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938B7] = 0x41;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938C0] = 0xA0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938C1] = 0x41;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938CA] = 0xA0;
                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938CB] = 0x41;

                BatFiles[BatNames._10_air_filled_tunnels].Contents[0x938D4] = 0x0C;
            }

        }

        private void btnResetToDefaults_Click(object sender, EventArgs e)
        {
            foreach (Control tab in tabControl1.Controls)
            {
                if (tab is TabPage)
                {
                    foreach (Control controlBox in tab.Controls)
                    {
                        if (controlBox is GroupBox)
                        {
                            foreach (Control control in controlBox.Controls)
                            {
                                if (control is DCoTEFloatBox)
                                {
                                    ((DCoTEFloatBox)control).ResetToDefault();
                                }
                                else if (control is DCoTECheckBox)
                                {
                                    ((DCoTECheckBox)control).ResetToDefault();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void linkLabelCoCF_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Change the color of the link text by setting LinkVisited 
                // to true.
                linkLabelCoCF.LinkVisited = true;
                //Call the Process.Start method to open the default browser 
                //with a URL:
                System.Diagnostics.Process.Start("http://forums.bethsoft.com/index.php?/topic/1072120-dcotepatch");
            }
            catch
            {
                MessageBox.Show("Unable to open link.");
            }

        }

        private void FixCommentBugs()
        {
            //category 1
            programCode[2552498] = 0x20;
            //AI_Settings
            programCode[2619088] = 0x20;
            //DebugScripts
            programCode[2530545] = 0x20;
            programCode[2530557] = 0x20;
            programCode[2532619] = 0x20;
            programCode[2532634] = 0x20;
            programCode[2535109] = 0x20;
            programCode[2535130] = 0x20;
            programCode[2536069] = 0x20;
            programCode[2536085] = 0x20;
            programCode[2536965] = 0x20;
            programCode[2536977] = 0x20;

            //dagon
            programCode[2646375] = 0x52;

            //Stone cross stray s
            programCode[2522777] = 0x20;
        }

        private void DeFixXMLBugs()
        {
            for (int i = 0; i < programCode.Length; i++)
            {

                // 1STNATIONAL
                if (programCode[i] == 0x46
                    && programCode[i + 1] == 0x53
                    && programCode[i + 2] == 0x54
                    && programCode[i + 3] == 0x4E
                    && programCode[i + 4] == 0x41
                    && programCode[i + 5] == 0x54

                    && programCode[i + 6] == 0x49
                    && programCode[i + 7] == 0x4F
                    && programCode[i + 8] == 0x4E
                    && programCode[i + 9] == 0x41
                    && programCode[i + 10] == 0x4C

                    && programCode[i + 11] == 0x47
                    && programCode[i + 12] == 0x52
                    && programCode[i + 13] == 0x4F)
                {
                    programCode[i] = 0x31;
                }

                // <Bip01dagon L 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31

                    && programCode[i + 6] == 0x64
                    && programCode[i + 7] == 0x61
                    && programCode[i + 8] == 0x67
                    && programCode[i + 9] == 0x6F
                    && programCode[i + 10] == 0x6E

                    && programCode[i + 11] == 0x5F
                    && programCode[i + 12] == 0x4C
                    && programCode[i + 13] == 0x5F)
                {
                    programCode[i + 11] = 0x20;
                    programCode[i + 13] = 0x20;
                }
                // </Bip01dagon L 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31

                    && programCode[i + 7] == 0x64
                    && programCode[i + 8] == 0x61
                    && programCode[i + 9] == 0x67
                    && programCode[i + 10] == 0x6F
                    && programCode[i + 11] == 0x6E

                    && programCode[i + 12] == 0x5F
                    && programCode[i + 13] == 0x4C
                    && programCode[i + 14] == 0x5F)
                {
                    programCode[i + 12] = 0x20;
                    programCode[i + 14] = 0x20;
                }
                // <Bip01dagon R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31

                    && programCode[i + 6] == 0x64
                    && programCode[i + 7] == 0x61
                    && programCode[i + 8] == 0x67
                    && programCode[i + 9] == 0x6F
                    && programCode[i + 10] == 0x6E

                    && programCode[i + 11] == 0x5F
                    && programCode[i + 12] == 0x52
                    && programCode[i + 13] == 0x5F)
                {
                    programCode[i + 11] = 0x20;
                    programCode[i + 13] = 0x20;
                }
                // </Bip01dagon R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31

                    && programCode[i + 7] == 0x64
                    && programCode[i + 8] == 0x61
                    && programCode[i + 9] == 0x67
                    && programCode[i + 10] == 0x6F
                    && programCode[i + 11] == 0x6E

                    && programCode[i + 12] == 0x5F
                    && programCode[i + 13] == 0x52
                    && programCode[i + 14] == 0x5F)
                {
                    programCode[i + 12] = 0x20;
                    programCode[i + 14] = 0x20;
                }
                // <Bip01dagon 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31

                    && programCode[i + 6] == 0x64
                    && programCode[i + 7] == 0x61
                    && programCode[i + 8] == 0x67
                    && programCode[i + 9] == 0x6F
                    && programCode[i + 10] == 0x6E

                    && programCode[i + 11] == 0x5F)
                {
                    programCode[i + 11] = 0x20;
                }
                // </Bip01dagon 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31

                    && programCode[i + 7] == 0x64
                    && programCode[i + 8] == 0x61
                    && programCode[i + 9] == 0x67
                    && programCode[i + 10] == 0x6F
                    && programCode[i + 11] == 0x6E

                    && programCode[i + 12] == 0x5F)
                {
                    programCode[i + 12] = 0x20;
                }
                // <Bip01 L 
                else if (programCode[i] == 0x3C
                       && programCode[i + 1] == 0x42
                       && programCode[i + 2] == 0x69
                       && programCode[i + 3] == 0x70
                       && programCode[i + 4] == 0x30
                       && programCode[i + 5] == 0x31
                       && programCode[i + 6] == 0x5F
                       && programCode[i + 7] == 0x4C
                       && programCode[i + 8] == 0x5F)
                {
                    programCode[i + 6] = 0x20;
                    programCode[i + 8] = 0x20;
                }
                // </Bip01 L 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31
                    && programCode[i + 7] == 0x5F
                    && programCode[i + 8] == 0x4C
                    && programCode[i + 9] == 0x5F)
                {
                    programCode[i + 7] = 0x20;
                    programCode[i + 9] = 0x20;
                }
                // <Bip01 R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31
                    && programCode[i + 6] == 0x5F
                    && programCode[i + 7] == 0x52
                    && programCode[i + 8] == 0x5F)
                {
                    programCode[i + 6] = 0x20;
                    programCode[i + 8] = 0x20;
                }
                // </Bip01 R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31
                    && programCode[i + 7] == 0x5F
                    && programCode[i + 8] == 0x52
                    && programCode[i + 9] == 0x5F)
                {
                    programCode[i + 7] = 0x20;
                    programCode[i + 9] = 0x20;
                }
                // <Bip01 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31
                    && programCode[i + 6] == 0x5F)
                {
                    programCode[i + 6] = 0x20;
                }
                // </Bip01 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31
                    && programCode[i + 7] == 0x5F)
                {
                    programCode[i + 7] = 0x20;
                }


//ID><Name>0
                else if (
               programCode[i] == 0x49
               && programCode[i + 1] == 0x44
               && programCode[i + 2] == 0x3E

               && programCode[i + 3] == 0x0A

               && programCode[i + 4] == 0x3C
               && programCode[i + 5] == 0x4E
               && programCode[i + 6] == 0x61
               && programCode[i + 7] == 0x6D
               && programCode[i + 8] == 0x65
               && programCode[i + 9] == 0x3E)
                {
                    if (programCode[i + 10] == 0x41)
                    {
                        if (programCode[i + 11] == 0x30)
                        {
                            programCode[i + 10] = 0x31;
                        }
                        else
                        {
                            programCode[i + 10] = 0x30;
                        }
                        
                    }
                }
                //<01
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x41
                    && programCode[i + 2] == 0x31)
                {
                    programCode[i + 1] = 0x30;
                }
                //</01
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x41
                    && programCode[i + 3] == 0x31)
                {
                    programCode[i + 2] = 0x30;
                }
                //<02
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x41
                         && programCode[i + 2] == 0x32)
                {
                    programCode[i + 1] = 0x30;
                }
                //</02
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x32)
                {
                    programCode[i + 2] = 0x30;
                }
                //<03
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x41
                         && programCode[i + 2] == 0x33)
                {
                    programCode[i + 1] = 0x30;
                }
                //</03
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x33)
                {
                    programCode[i + 2] = 0x30;
                }
                //<04
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x41
                         && programCode[i + 2] == 0x34)
                {
                    programCode[i + 1] = 0x30;
                }
                //</04
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x34)
                {
                    programCode[i + 2] = 0x30;
                }
                //<05
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x41
                    && programCode[i + 2] == 0x35)
                {
                    programCode[i + 1] = 0x30;
                }
                //</05
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x35)
                {
                    programCode[i + 2] = 0x30;
                }
                //<06
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x41
                    && programCode[i + 2] == 0x36)
                {
                    programCode[i + 1] = 0x30;
                }
                //</06
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x36)
                {
                    programCode[i + 2] = 0x30;
                }
                //<07
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x41
                    && programCode[i + 2] == 0x37)
                {
                    programCode[i + 1] = 0x30;
                }
                //</07
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x37)
                {
                    programCode[i + 2] = 0x30;
                }
                //<08
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x41
                    && programCode[i + 2] == 0x38)
                {
                    programCode[i + 1] = 0x30;
                }
                //</08
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x38)
                {
                    programCode[i + 2] = 0x30;
                }
                //<09
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x41
                    && programCode[i + 2] == 0x39)
                {
                    programCode[i + 1] = 0x30;
                }
                //</09
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x39)
                {
                    programCode[i + 2] = 0x30;
                }
                //<10
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x41
                    && programCode[i + 2] == 0x30)
                {
                    programCode[i + 1] = 0x31;
                }
                //</10
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x41
                         && programCode[i + 3] == 0x30)
                {
                    programCode[i + 2] = 0x31;
                }







            }
        }

        private void FixXMLBugs()
        {

            #region PatchLevelNumbers
            for (int i = 0; i < programCode.Length;i++)
            {
                // 1STNATIONAL
                if (programCode[i] == 0x31
                    && programCode[i + 1] == 0x53
                    && programCode[i + 2] == 0x54
                    && programCode[i + 3] == 0x4E
                    && programCode[i + 4] == 0x41
                    && programCode[i + 5] == 0x54

                    && programCode[i + 6] == 0x49
                    && programCode[i + 7] == 0x4F
                    && programCode[i + 8] == 0x4E
                    && programCode[i + 9] == 0x41
                    && programCode[i + 10] == 0x4C

                    && programCode[i + 11] == 0x47
                    && programCode[i + 12] == 0x52
                    && programCode[i + 13] == 0x4F)
                {
                    programCode[i] = 0x46;
                }

                // <Bip01dagon L 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31

                    && programCode[i + 6] == 0x64
                    && programCode[i + 7] == 0x61
                    && programCode[i + 8] == 0x67
                    && programCode[i + 9] == 0x6F
                    && programCode[i + 10] == 0x6E

                    && programCode[i + 11] == 0x20
                    && programCode[i + 12] == 0x4C
                    && programCode[i + 13] == 0x20)
                {
                    programCode[i + 11] = 0x5F;
                    programCode[i + 13] = 0x5F;
                }
                // </Bip01dagon L 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31

                    && programCode[i + 7] == 0x64
                    && programCode[i + 8] == 0x61
                    && programCode[i + 9] == 0x67
                    && programCode[i + 10] == 0x6F
                    && programCode[i + 11] == 0x6E

                    && programCode[i + 12] == 0x20
                    && programCode[i + 13] == 0x4C
                    && programCode[i + 14] == 0x20)
                {
                    programCode[i + 12] = 0x5F;
                    programCode[i + 14] = 0x5F;
                }
                // <Bip01dagon R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31

                    && programCode[i + 6] == 0x64
                    && programCode[i + 7] == 0x61
                    && programCode[i + 8] == 0x67
                    && programCode[i + 9] == 0x6F
                    && programCode[i + 10] == 0x6E

                    && programCode[i + 11] == 0x20
                    && programCode[i + 12] == 0x52
                    && programCode[i + 13] == 0x20)
                {
                    programCode[i + 11] = 0x5F;
                    programCode[i + 13] = 0x5F;
                }
                // </Bip01dagon R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31

                    && programCode[i + 7] == 0x64
                    && programCode[i + 8] == 0x61
                    && programCode[i + 9] == 0x67
                    && programCode[i + 10] == 0x6F
                    && programCode[i + 11] == 0x6E

                    && programCode[i + 12] == 0x20
                    && programCode[i + 13] == 0x52
                    && programCode[i + 14] == 0x20)
                {
                    programCode[i + 12] = 0x5F;
                    programCode[i + 14] = 0x5F;
                }
                // <Bip01dagon 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31

                    && programCode[i + 6] == 0x64
                    && programCode[i + 7] == 0x61
                    && programCode[i + 8] == 0x67
                    && programCode[i + 9] == 0x6F
                    && programCode[i + 10] == 0x6E

                    && programCode[i + 11] == 0x20)
                {
                    programCode[i + 11] = 0x5F;
                }
                // </Bip01dagon 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31

                    && programCode[i + 7] == 0x64
                    && programCode[i + 8] == 0x61
                    && programCode[i + 9] == 0x67
                    && programCode[i + 10] == 0x6F
                    && programCode[i + 11] == 0x6E

                    && programCode[i + 12] == 0x20)
                {
                    programCode[i + 12] = 0x5F;
                }
                // <Bip01 L 
                else if (programCode[i] == 0x3C
                       && programCode[i + 1] == 0x42
                       && programCode[i + 2] == 0x69
                       && programCode[i + 3] == 0x70
                       && programCode[i + 4] == 0x30
                       && programCode[i + 5] == 0x31
                       && programCode[i + 6] == 0x20
                       && programCode[i + 7] == 0x4C
                       && programCode[i + 8] == 0x20)
                {
                    programCode[i + 6] = 0x5F;
                    programCode[i + 8] = 0x5F;
                }
                // </Bip01 L 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31
                    && programCode[i + 7] == 0x20
                    && programCode[i + 8] == 0x4C
                    && programCode[i + 9] == 0x20)
                {
                    programCode[i + 7] = 0x5F;
                    programCode[i + 9] = 0x5F;
                }
                // <Bip01 R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31
                    && programCode[i + 6] == 0x20
                    && programCode[i + 7] == 0x52
                    && programCode[i + 8] == 0x20)
                {
                    programCode[i + 6] = 0x5F;
                    programCode[i + 8] = 0x5F;
                }
                // </Bip01 R 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31
                    && programCode[i + 7] == 0x20
                    && programCode[i + 8] == 0x52
                    && programCode[i + 9] == 0x20)
                {
                    programCode[i + 7] = 0x5F;
                    programCode[i + 9] = 0x5F;
                }
                // <Bip01 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x42
                    && programCode[i + 2] == 0x69
                    && programCode[i + 3] == 0x70
                    && programCode[i + 4] == 0x30
                    && programCode[i + 5] == 0x31
                    && programCode[i + 6] == 0x20)
                {
                    programCode[i + 6] = 0x5F;
                }
                // </Bip01 
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x42
                    && programCode[i + 3] == 0x69
                    && programCode[i + 4] == 0x70
                    && programCode[i + 5] == 0x30
                    && programCode[i + 6] == 0x31
                    && programCode[i + 7] == 0x20)
                {
                    programCode[i + 7] = 0x5F;
                }

                //Wounded_walk
                else if (programCode[i] == 0x57
                    && programCode[i + 1] == 0x6F
                    && programCode[i + 2] == 0x75
                    && programCode[i + 3] == 0x6E
                    && programCode[i + 4] == 0x64
                    && programCode[i + 5] == 0x65
                    && programCode[i + 6] == 0x64
                    && programCode[i + 7] == 0x5F
                    && programCode[i + 8] == 0x57
                    && programCode[i + 9] == 0x61
                    && programCode[i + 10] == 0x6C
                    && programCode[i + 11] == 0x6B)
                {
                    programCode[i + 8] = 0x77;
                }
                //<Level>0
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x4C
                    && programCode[i + 2] == 0x65
                    && programCode[i + 3] == 0x76
                    && programCode[i + 4] == 0x65
                    && programCode[i + 5] == 0x6C
                    && programCode[i + 6] == 0x3E)
                {
                    if (programCode[i + 7] == 0x30
                        || programCode[i + 7] == 0x31)
                    {
                        programCode[i + 7] = 0x41;
                    }
                }
                //ID>    <Name>0
                else if (
                    programCode[i] == 0x49
                    && programCode[i + 1] == 0x44
                    && programCode[i + 2] == 0x3E
                    && programCode[i + 3] == 0x0D
                    && programCode[i + 4] == 0x0A
                    && programCode[i + 5] == 0x09
                    && programCode[i + 6] == 0x09
                    && programCode[i + 7] == 0x3C
                    && programCode[i + 8] == 0x4E
                    && programCode[i + 9] == 0x61
                    && programCode[i + 10] == 0x6D
                    && programCode[i + 11] == 0x65
                    && programCode[i + 12] == 0x3E)
                {
                    if (programCode[i + 13] == 0x30
                        || programCode[i + 13] == 0x31)
                    {
                        programCode[i + 13] = 0x41;
                    }
                }
                //<01
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x30
                    && programCode[i + 2] == 0x31)
                {
                    programCode[i + 1] = 0x41;
                }
                //</01
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x2F
                    && programCode[i + 2] == 0x30
                    && programCode[i + 3] == 0x31)
                {
                    programCode[i + 2] = 0x41;
                }
                //<02
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x30
                         && programCode[i + 2] == 0x32)
                {
                    programCode[i + 1] = 0x41;
                }
                //</02
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x32)
                {
                    programCode[i + 2] = 0x41;
                }
                //<03
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x30
                         && programCode[i + 2] == 0x33)
                {
                    programCode[i + 1] = 0x41;
                }
                //</03
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x33)
                {
                    programCode[i + 2] = 0x41;
                }
                //<04
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x30
                         && programCode[i + 2] == 0x34)
                {
                    programCode[i + 1] = 0x41;
                }
                //</04
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x34)
                {
                    programCode[i + 2] = 0x41;
                }
                //<05
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x30
                    && programCode[i + 2] == 0x35)
                {
                    programCode[i + 1] = 0x41;
                }
                //</05
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x35)
                {
                    programCode[i + 2] = 0x41;
                }
                //<06
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x30
                    && programCode[i + 2] == 0x36)
                {
                    programCode[i + 1] = 0x41;
                }
                //</06
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x36)
                {
                    programCode[i + 2] = 0x41;
                }
                //<07
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x30
                    && programCode[i + 2] == 0x37)
                {
                    programCode[i + 1] = 0x41;
                }
                //</07
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x37)
                {
                    programCode[i + 2] = 0x41;
                }
                //<08
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x30
                    && programCode[i + 2] == 0x38)
                {
                    programCode[i + 1] = 0x41;
                }
                //</08
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x38)
                {
                    programCode[i + 2] = 0x41;
                }
                //<09
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x30
                    && programCode[i + 2] == 0x39)
                {
                    programCode[i + 1] = 0x41;
                }
                //</09
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x30
                         && programCode[i + 3] == 0x39)
                {
                    programCode[i + 2] = 0x41;
                }
                //<10
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x31
                    && programCode[i + 2] == 0x30)
                {
                    programCode[i + 1] = 0x41;
                }
                //</10
                else if (programCode[i] == 0x3C
                         && programCode[i + 1] == 0x2F
                         && programCode[i + 2] == 0x31
                         && programCode[i + 3] == 0x30)
                {
                    programCode[i + 2] = 0x41;
                }

                //<core
                else if (programCode[i] == 0x3C
                    && programCode[i + 1] == 0x63
                    && programCode[i + 2] == 0x6F
                    && programCode[i + 3] == 0x72
                    && programCode[i + 4] == 0x65)
                {
                    programCode[i + 1] = 0x43;
                }


            }
            #endregion PatchLevelNumbers

        }

        private void AddMissingElements()
        {
            DataRow xmlsettingRow;

            //AllContentUnlocked
            xmlsettingRow = xmlSetting.NewRow();
            xmlsettingRow[0] = 455;
            xmlsettingRow[1] = "DCoTESettings";
            xmlsettingRow[2] = @"\DCoTESettings\Application\";
            xmlsettingRow[3] = "<!--Comment-->";
            xmlsettingRow[4] = "Auto Added - Unlock all bonus material.";
            xmlSetting.Rows.Add(xmlsettingRow);

            xmlsettingRow = xmlSetting.NewRow();
            xmlsettingRow[0] = 456;
            xmlsettingRow[1] = "DCoTESettings";
            xmlsettingRow[2] = @"\DCoTESettings\Application\";
            xmlsettingRow[3] = "AllContentUnlocked";
            xmlsettingRow[4] = "0";
            xmlSetting.Rows.Add(xmlsettingRow);

            //PlayerInvisible
            xmlsettingRow = xmlSetting.NewRow();
            xmlsettingRow[0] = 1025;
            xmlsettingRow[1] = "DCoTESettings";
            xmlsettingRow[2] = @"\DCoTESettings\Player\";
            xmlsettingRow[3] = "<!--Comment-->";
            xmlsettingRow[4] = "Auto Added - Player invisible to AI.";
            xmlSetting.Rows.Add(xmlsettingRow);

            xmlsettingRow = xmlSetting.NewRow();
            xmlsettingRow[0] = 1026;
            xmlsettingRow[1] = "DCoTESettings";
            xmlsettingRow[2] = @"\DCoTESettings\Player\";
            xmlsettingRow[3] = "PlayerInvisible";
            xmlsettingRow[4] = "0";
            xmlSetting.Rows.Add(xmlsettingRow);

            //AllowInsanityEffects
            xmlsettingRow = xmlSetting.NewRow();
            xmlsettingRow[0] = 3835;
            xmlsettingRow[1] = "DCoTESettings";
            xmlsettingRow[2] = @"\DCoTESettings\Sanity\";
            xmlsettingRow[3] = "<!--Comment-->";
            xmlsettingRow[4] = "Auto Added - Allow funky insanity effects.";
            xmlSetting.Rows.Add(xmlsettingRow);

            xmlsettingRow = xmlSetting.NewRow();
            xmlsettingRow[0] = 3836;
            xmlsettingRow[1] = "DCoTESettings";
            xmlsettingRow[2] = @"\DCoTESettings\Sanity\";
            xmlsettingRow[3] = "AllowInsanityEffects";
            xmlsettingRow[4] = "1";
            xmlSetting.Rows.Add(xmlsettingRow);

            //enable legal video skipping
            bool bWasReadonly = xmlView.Table.Columns[3].ReadOnly;
            xmlView.Table.Columns[2].ReadOnly = false;
            xmlView.Table.Columns[3].ReadOnly = false;

            xmlsettingRow = xmlSetting.Rows.Find(2920);
            xmlsettingRow[2] = @"\DCoTESettings\IntroVideos\ENGLISH\Videos\Video";
            xmlsettingRow[3] = "Video";
            xmlsettingRow = xmlSetting.Rows.Find(3010);
            xmlsettingRow[2] = @"\DCoTESettings\IntroVideos\FRENCH\Videos\Video";
            xmlsettingRow[3] = "Video";
            xmlsettingRow = xmlSetting.Rows.Find(3100);
            xmlsettingRow[2] = @"\DCoTESettings\IntroVideos\GERMAN\Videos\Video";
            xmlsettingRow[3] = "Video";

            xmlView.Table.Columns[2].ReadOnly = bWasReadonly;
            xmlView.Table.Columns[3].ReadOnly = bWasReadonly;
        }

        private void btnExtractXML_Click(object sender, EventArgs e)
        {
            pbXML.Value = 0;
            id = 0;

            if (programCode[2369023] != 0x0A)
            {
                FixCommentBugs();
            }

            FixXMLBugs();

            xmlSetting.Clear();

            //DCoTESettings
            ProcessXML("DCoTESettings", "DCoTESettings", 2369008, 2388295);
            //LangRemap
            ProcessXML("LanguageRemap", "LangRemap", 2395928, 2396562);
            //WeaponAndDamageData
            ProcessXML("WeaponAndDamageData", "WeaponAndDamageData", 2396568, 2400578);
            //SoundData
            ProcessXML("SoundData", "SoundData", 2400584, 2401924);
            //PoolSizes
            ProcessXML("PoolSizes", "PoolSizes", 2401936, 2419345);
            //MythosRanking
            ProcessXML("MythosRanking", "MythosRanking", 2419360, 2423708);
            //SoundData
            ProcessXML("MusicData", "SoundData", 2423720, 2440279);
            //MemoryLayout
            ProcessXML("MemoryLayout", "MemoryLayout", 2440288, 2466404);
            //Materials
            ProcessXML("Materials", "Materials", 2466416, 2483352);
            //LevelSettings
            ProcessXML("LevelSettings", "LevelSettings", 2483360, 2517590);
            //Journal
            ProcessXML("Journal", "Journal", 2517600, 2518100);
            //Inventory
            ProcessXML("Inventory", "Inventory", 2518112, 2530047);
            //DebugSettings
            ProcessXML("DebugSettings", "DebugSettings", 2530056, 2530128);
            //DebugScripts
            ProcessXML("DebugScripts", "DebugScripts", 2530136, 2537597);
            //DebugCulling
            ProcessXML("DebugCulling", "DebugCulling", 2537608, 2537828);
            //Cinematics
            ProcessXML("Cinematics", "Cinematics", 2537840, 2538506);
            //Category0
            ProcessXML("Category0", "Category", 2538520, 2543333);
            //Category1
            ProcessXML("Category1", "Category", 2543344, 2562311);
            //Category2
            ProcessXML("Category2", "Category", 2562320, 2576933);
            //BonusItems
            ProcessXML("BonusItems", "BonusItems", 2576944, 2578221);
            //BloodData
            ProcessXML("BloodData", "BloodData", 2578232, 2578340);
            //Anim_Settings
            ProcessXML("Anim_Settings", "Anim_Settings", 2578352, 2595597);
            //Tactics_Settings
            ProcessXML("Tactics_Settings", "Tactics_Settings", 2595608, 2600485);
            //AI_Speed_Settings
            ProcessXML("AI_Speed_Settings", "AI_Speed_Settings", 2600496, 2618969);
            //AI_Settings
            ProcessXML("AI_Settings", "AI_Settings", 2620320, 2661040);

            if (programCode[2369023] != 0x0A)
            {
                AddMissingElements();
            }

            dgSettings.Enabled = true;
        }

        private void UpdateXMLSettings()
        {
            pbXML.Value = 0;

            //DCoTESettings
            WriteBackXML("DCoTESettings", "DCoTESettings", 2369008, 2388295);
            //LangRemap
            WriteBackXML("LanguageRemap", "LangRemap", 2395928, 2396562);
            //WeaponAndDamageData
            WriteBackXML("WeaponAndDamageData", "WeaponAndDamageData", 2396568, 2400578);
            //SoundData
            WriteBackXML("SoundData", "SoundData", 2400584, 2401924);
            //PoolSizes
            WriteBackXML("PoolSizes", "PoolSizes", 2401936, 2419345);
            //MythosRanking
            WriteBackXML("MythosRanking", "MythosRanking", 2419360, 2423708);
            //SoundData
            WriteBackXML("MusicData", "SoundData", 2423720, 2440279);
            //MemoryLayout
            WriteBackXML("MemoryLayout", "MemoryLayout", 2440288, 2466404);
            //Materials
            WriteBackXML("Materials", "Materials", 2466416, 2483352);
            //LevelSettings
            WriteBackXML("LevelSettings", "LevelSettings", 2483360, 2517590);
            //Journal
            WriteBackXML("Journal", "Journal", 2517600, 2518100);
            //Inventory
            WriteBackXML("Inventory", "Inventory", 2518112, 2530047);
            //DebugSettings
            WriteBackXML("DebugSettings", "DebugSettings", 2530056, 2530128);
            //DebugScripts
            WriteBackXML("DebugScripts", "DebugScripts", 2530136, 2537597);
            //DebugCulling
            WriteBackXML("DebugCulling", "DebugCulling", 2537608, 2537828);
            //Cinematics
            WriteBackXML("Cinematics", "Cinematics", 2537840, 2538506);
            //Category0
            WriteBackXML("Category0", "Category", 2538520, 2543333);
            //Category1
            WriteBackXML("Category1", "Category", 2543344, 2562311);
            //Category2
            WriteBackXML("Category2", "Category", 2562320, 2576933);
            //BonusItems
            WriteBackXML("BonusItems", "BonusItems", 2576944, 2578221);
            //BloodData
            WriteBackXML("BloodData", "BloodData", 2578232, 2578340);
            //Anim_Settings
            WriteBackXML("Anim_Settings", "Anim_Settings", 2578352, 2595597);
            //Tactics_Settings
            WriteBackXML("Tactics_Settings", "Tactics_Settings", 2595608 , 2600485);
            //AI_Speed_Settings
            WriteBackXML("AI_Speed_Settings", "AI_Speed_Settings", 2600496, 2618969);
            //AI_Settings
            WriteBackXML("AI_Settings", "AI_Settings", 2620320, 2661040);
            
            DeFixXMLBugs();

        }

        private void WriteBackXML(string xmlArea, string mainTag, int xmlStart, int xmlEnd)
        {
            StringBuilder xmlString = new StringBuilder();
            DataRow[] xmlRows = xmlSetting.Select("Area = '" + xmlArea + "'", "id");
            foreach (DataRow xmlRow in xmlRows)
            {
                if (xmlRow[4] == System.DBNull.Value)
                {
                    xmlString.Append("<" + xmlRow[3] + ">" + "\n");
                }
                else if (xmlRow[3].ToString() == "<!--Comment-->")
                {
                    xmlString.Append("<!-- " + xmlRow[4] + "-->" + "\n");
                }
                else
                {
                    xmlString.Append("<" + xmlRow[3] + ">" + xmlRow[4] + "</" + xmlRow[3] + ">" + "\n");
                }
            }
            //xmlString.Append("  ");

            //write new bytes out
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] xmlBytes = encoding.GetBytes(xmlString.ToString());
            //xmlBytes[xmlBytes.Length - 2] = 0;
            //xmlBytes[xmlBytes.Length - 1] = 0;

            if (xmlBytes.Length > (xmlEnd + 2) - xmlStart) // 2?
            {
                MessageBox.Show("Length of " + xmlArea + " xml is too big.");
                return;
            }

            xmlBytes.CopyTo(programCode, xmlStart);

            //xmlEnd += 3;
            //blank the rest with 00
            for (int i = xmlStart + xmlBytes.Length - 1; i <= xmlEnd; i++)
            {
                programCode[i] = 0x00;
            }

            pbXML.PerformStep();
        }

        private void ProcessXML(string xmlArea, string mainTag, int xmlStart, int xmlEnd)
        {
            StringBuilder nodePath = new StringBuilder("");

            MemoryStream memoryStream = new MemoryStream(programCode, xmlStart, (xmlEnd - xmlStart) + 1);

            XmlTextReader xmlReader = new XmlTextReader(memoryStream);

            bool hasText = false;

            DataRow xmlsettingRow = null;
            while (true)
            {
                try
                {
                    if (!xmlReader.Read())
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (!ex.Message.StartsWith("'.', hexadecimal value 0x00, is an invalid character"))
                    {
                        throw ex;
                    }
                }

                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    nodePath.Append("\\");
                    nodePath.Append(xmlReader.LocalName);

                    xmlsettingRow = xmlSetting.NewRow();
                    xmlsettingRow[0] = id;
                    id += 10;
                    xmlsettingRow[1] = xmlArea;
                    xmlsettingRow[2] = nodePath.ToString();
                    xmlsettingRow[3] = xmlReader.LocalName;
                    xmlSetting.Rows.Add(xmlsettingRow);
                    hasText = false;
                }
                else if (xmlReader.NodeType == XmlNodeType.Comment)
                {
                    xmlsettingRow = xmlSetting.NewRow();
                    xmlsettingRow[0] = id;
                    id += 10;
                    xmlsettingRow[1] = xmlArea;
                    xmlsettingRow[2] = nodePath.ToString();
                    xmlsettingRow[3] = "<!--Comment-->";
                    xmlsettingRow[4] = xmlReader.Value;
                    xmlSetting.Rows.Add(xmlsettingRow);
                    hasText = false;
                }
                else if (xmlReader.NodeType == XmlNodeType.Text)
                {
                    xmlsettingRow[4] = xmlReader.Value;
                    hasText = true;
                }
                else if (xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    if (!hasText)
                    {
                        xmlsettingRow = xmlSetting.NewRow();
                        xmlsettingRow[0] = id;
                        id += 10;
                        xmlsettingRow[1] = xmlArea;
                        xmlsettingRow[2] = nodePath.ToString();
                        xmlsettingRow[3] = "/" + xmlReader.LocalName;
                        xmlSetting.Rows.Add(xmlsettingRow);
                    }
                    hasText = false;

                    if (nodePath.ToString().LastIndexOf("\\") > 0)
                    {
                        nodePath.Remove(nodePath.ToString().LastIndexOf("\\"), xmlReader.LocalName.Length + 1);
                    }

                    //found end tag?
                    if (xmlReader.LocalName == mainTag)
                    {
                        break;
                    }
                }
            }
             xmlReader.Close();

             pbXML.PerformStep();
        }

        private void cbBreakStuff_CheckedChanged(object sender, EventArgs e)
        {

            if (cbBreakStuff.Checked)
            {
                xmlView = new DataView(xmlSetting, "", "id", DataViewRowState.CurrentRows);
            }
            else
            {
                xmlView = new DataView(xmlSetting, "ISNULL(Value,-1) = Value", "id", DataViewRowState.CurrentRows);
            }
            xmlView.Table.Columns[0].ReadOnly = !cbBreakStuff.Checked;

            xmlView.Table.Columns[1].ReadOnly = !cbBreakStuff.Checked;

            xmlView.Table.Columns[2].ReadOnly = !cbBreakStuff.Checked;

            xmlView.Table.Columns[3].ReadOnly = !cbBreakStuff.Checked;

            xmlView.AllowDelete = cbBreakStuff.Checked;
            xmlView.AllowNew = cbBreakStuff.Checked;

            dgSettings.SetDataBinding(xmlView, "");

        }

        private void btnRestoreToOriginals_Click(object sender, EventArgs e)
        {
            //write files and backup originals
            ReturnStatus returnStatus = RestoreOriginalFile(filename, filename.Replace(".exe", ".original.exe"));

            //RestoreOriginalFile(BatFiles[BatNames.misc01_city_of_dreams_03].Filename, BatFiles[BatNames.misc01_city_of_dreams_03].Filename.Replace(".bat", ".original.bat"));

            //RestoreOriginalFile(BatFiles[BatNames._08_boat].Filename, BatFiles[BatNames._08_boat].Filename.Replace(".bat", ".original.bat"));
            
            foreach (BatFile batFile in BatFiles.Values)
            {
                RestoreOriginalFile(batFile.Filename, batFile.Filename.Replace(".bat", ".original.bat"));
            }

            if (returnStatus == ReturnStatus.OK)
            {
                MessageBox.Show("Original files restored (CoCMainWin32.original.exe, *.original.bat).", "OK");
            }
            else
            {
                MessageBox.Show("Original files restore failed! (CoCMainWin32.original.exe, *.original.bat).", "FAIL");
            }
        }

        private ReturnStatus RestoreOriginalFile(string filename, string originalFilename)
        {
            try
            {
                if (File.Exists(originalFilename))
                {
                    File.Copy(originalFilename, filename, true);
                }
            }
            catch (Exception)
            {
                return ReturnStatus.Failed;
            }

            return ReturnStatus.OK;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Change the color of the link text by setting LinkVisited 
                // to true.
                linkLabelCoCF.LinkVisited = true;
                //Call the Process.Start method to open the default browser 
                //with a URL:
                System.Diagnostics.Process.Start("http://dcotetools.sucklead.com");
            }
            catch
            {
                MessageBox.Show("Unable to open link.");
            }
        }

    }
}