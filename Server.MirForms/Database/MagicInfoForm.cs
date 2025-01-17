﻿using System;
using System.Drawing;
using Server.MirDatabase;
using System.Windows.Forms;
using Server.MirEnvir;

namespace Server
{
    public partial class MagicInfoForm : Form
    {

        public Envir Envir => SMain.EditEnvir;

        private MagicInfo _selectedMagicInfo;

        public MagicInfoForm()
        {
            InitializeComponent();
            InitMagicListBox();
        }

        private void InitMagicListBox()
        {
            for (int i = 0; i < Envir.MagicInfoList.Count; i++)
                MagiclistBox.Items.Add(Envir.MagicInfoList[i]);
            UpdateMagicForm();
        }

        private void UpdateMagicForm(byte field = 0)
        {
             _selectedMagicInfo = (MagicInfo)MagiclistBox.SelectedItem;

             lblBookValid.BackColor = SystemColors.Window;

             if (_selectedMagicInfo == null)
             {
                 tabControl1.Enabled = false;
                 lblBookValid.Text = "Searching";
                 lblSelected.Text = "Selected Skill: none";
                 lblDamageExample.Text = "";
                 lblDamageExplained.Text = "";
                 txtSkillIcon.Text = "0";
                 txtSkillLvl1Points.Text = "0";
                 txtSkillLvl1Req.Text = "0";
                 txtSkillLvl2Points.Text = "0";
                 txtSkillLvl2Req.Text = "0";
                 txtSkillLvl3Points.Text = "0";
                 txtSkillLvl3Req.Text = "0";
                 txtMPBase.Text = "0";
                 txtMPIncrease.Text = "0";
                 txtDelayBase.Text = "0";
                 txtDelayReduction.Text = "0";
                 txtDmgBaseMin.Text = "0";
                 txtDmgBaseMax.Text = "0";
                 txtDmgBonusMin.Text = "0";
                 txtDmgBonusMax.Text = "0";
             }
             else
             {
                 tabControl1.Enabled = true;
                 lblSelected.Text = "Selected Skill: " + _selectedMagicInfo.ToString();
                 lblDamageExample.Text =
                     $"Damage @ Skill level 0: {GetMinPower(0):000}-{GetMaxPower(0):000}   |||   level 1: {GetMinPower(1):000}-{GetMaxPower(1):000}   |||   level 2: {GetMinPower(2):000}-{GetMaxPower(2):000}   |||   level 3: {GetMinPower(3):000}-{GetMaxPower(3):000}";
                 lblDamageExplained.Text =
                     $"Damage: {{Random(minstat-maxstat) + [<(random({_selectedMagicInfo.MPowerBase}-{_selectedMagicInfo.MPowerBase + _selectedMagicInfo.MPowerBonus})/4) X (skill level +1)> + random<{_selectedMagicInfo.PowerBase}-{_selectedMagicInfo.PowerBonus + _selectedMagicInfo.PowerBase}>]}}  X  {{{_selectedMagicInfo.MultiplierBase} + (skill level * {_selectedMagicInfo.MultiplierBonus})}}";
                 txtSkillIcon.Text = _selectedMagicInfo.Icon.ToString();
                 txtSkillLvl1Points.Text = _selectedMagicInfo.Need1.ToString();
                 txtSkillLvl1Req.Text = _selectedMagicInfo.Level1.ToString();
                 txtSkillLvl2Points.Text = _selectedMagicInfo.Need2.ToString();
                 txtSkillLvl2Req.Text = _selectedMagicInfo.Level2.ToString();
                 txtSkillLvl3Points.Text = _selectedMagicInfo.Need3.ToString();
                 txtSkillLvl3Req.Text = _selectedMagicInfo.Level3.ToString();
                 txtMPBase.Text = _selectedMagicInfo.BaseCost.ToString();
                 txtMPIncrease.Text = _selectedMagicInfo.LevelCost.ToString();
                 txtDelayBase.Text = _selectedMagicInfo.DelayBase.ToString();
                 txtDelayReduction.Text = _selectedMagicInfo.DelayReduction.ToString();
                 txtDmgBaseMin.Text = _selectedMagicInfo.PowerBase.ToString();
                 txtDmgBaseMax.Text = (_selectedMagicInfo.PowerBase + _selectedMagicInfo.PowerBonus).ToString();
                 txtDmgBonusMin.Text = _selectedMagicInfo.MPowerBase.ToString();
                 txtDmgBonusMax.Text = (_selectedMagicInfo.MPowerBase + _selectedMagicInfo.MPowerBonus).ToString();
                 if (field != 1)
                    txtDmgMultBase.Text = _selectedMagicInfo.MultiplierBase.ToString();
                 if (field != 2)
                 txtDmgMultBoost.Text = _selectedMagicInfo.MultiplierBonus.ToString();
                 txtRange.Text = _selectedMagicInfo.Range.ToString();
                 ItemInfo Book = Envir.GetBook((short)_selectedMagicInfo.Spell);
                 if (Book != null)
                 {
                     lblBookValid.Text = Book.Name;
                 }
                 else
                 {
                     lblBookValid.Text = "No book found";
                     lblBookValid.BackColor = Color.Red;
                 }
                this.textBoxName.Text = _selectedMagicInfo.Name;
             }
        }

        private int GetMaxPower(byte level)
        {
            if (_selectedMagicInfo == null) return 0;
            return (int)Math.Round((((_selectedMagicInfo.MPowerBase + _selectedMagicInfo.MPowerBonus) / 4F) * (level + 1) + (_selectedMagicInfo.PowerBase + _selectedMagicInfo.PowerBonus))* (_selectedMagicInfo.MultiplierBase + (level * _selectedMagicInfo.MultiplierBonus)));
        }
        private int GetMinPower(byte level)
        {
            if (_selectedMagicInfo == null) return 0;
            return (int)Math.Round(((_selectedMagicInfo.MPowerBase / 4F) * (level + 1) + _selectedMagicInfo.PowerBase) * (_selectedMagicInfo.MultiplierBase + (level * _selectedMagicInfo.MultiplierBonus)));
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MagiclistBox = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.lblDamageExample = new System.Windows.Forms.Label();
            this.lblDamageExplained = new System.Windows.Forms.Label();
            this.lblSelected = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtDmgMultBoost = new System.Windows.Forms.TextBox();
            this.txtDmgMultBase = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtDmgBonusMax = new System.Windows.Forms.TextBox();
            this.txtDmgBonusMin = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDmgBaseMax = new System.Windows.Forms.TextBox();
            this.txtDmgBaseMin = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.txtDelayReduction = new System.Windows.Forms.TextBox();
            this.txtDelayBase = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMPIncrease = new System.Windows.Forms.TextBox();
            this.txtMPBase = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSkillLvl3Points = new System.Windows.Forms.TextBox();
            this.txtSkillLvl2Points = new System.Windows.Forms.TextBox();
            this.txtSkillLvl1Points = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSkillLvl3Req = new System.Windows.Forms.TextBox();
            this.txtSkillLvl2Req = new System.Windows.Forms.TextBox();
            this.txtSkillLvl1Req = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSkillIcon = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBookValid = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbxSearchMagic = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.btnAddSkill = new System.Windows.Forms.Button();
            this.btnDeleteSkill = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MagiclistBox
            // 
            this.MagiclistBox.FormattingEnabled = true;
            this.MagiclistBox.ItemHeight = 12;
            this.MagiclistBox.Location = new System.Drawing.Point(-2, 60);
            this.MagiclistBox.Name = "MagiclistBox";
            this.MagiclistBox.Size = new System.Drawing.Size(225, 496);
            this.MagiclistBox.TabIndex = 0;
            this.MagiclistBox.SelectedIndexChanged += new System.EventHandler(this.MagiclistBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(225, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(702, 521);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.textBoxName);
            this.tabPage1.Controls.Add(this.lblDamageExample);
            this.tabPage1.Controls.Add(this.lblDamageExplained);
            this.tabPage1.Controls.Add(this.lblSelected);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.txtSkillIcon);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblBookValid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(694, 495);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basics";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(20, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 12;
            this.label24.Text = "SkillName:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(181, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 12);
            this.label23.TabIndex = 11;
            this.label23.Text = "book:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(89, 18);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(112, 21);
            this.textBoxName.TabIndex = 10;
            this.textBoxName.TextChanged += new System.EventHandler(this.TextBoxName_TextChanged);
            // 
            // lblDamageExample
            // 
            this.lblDamageExample.AutoSize = true;
            this.lblDamageExample.Location = new System.Drawing.Point(11, 394);
            this.lblDamageExample.Name = "lblDamageExample";
            this.lblDamageExample.Size = new System.Drawing.Size(89, 12);
            this.lblDamageExample.TabIndex = 0;
            this.lblDamageExample.Text = "Damage example";
            // 
            // lblDamageExplained
            // 
            this.lblDamageExplained.AutoSize = true;
            this.lblDamageExplained.Location = new System.Drawing.Point(11, 366);
            this.lblDamageExplained.Name = "lblDamageExplained";
            this.lblDamageExplained.Size = new System.Drawing.Size(47, 12);
            this.lblDamageExplained.TabIndex = 9;
            this.lblDamageExplained.Text = "Damage:";
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(20, 3);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(101, 12);
            this.lblSelected.TabIndex = 8;
            this.lblSelected.Text = "Selected skill: ";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.txtDmgMultBoost);
            this.panel4.Controls.Add(this.txtDmgMultBase);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.txtDmgBonusMax);
            this.panel4.Controls.Add(this.txtDmgBonusMin);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.txtDmgBaseMax);
            this.panel4.Controls.Add(this.txtDmgBaseMin);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Location = new System.Drawing.Point(14, 166);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(281, 191);
            this.panel4.TabIndex = 6;
            // 
            // txtDmgMultBoost
            // 
            this.txtDmgMultBoost.Location = new System.Drawing.Point(220, 157);
            this.txtDmgMultBoost.Name = "txtDmgMultBoost";
            this.txtDmgMultBoost.Size = new System.Drawing.Size(46, 21);
            this.txtDmgMultBoost.TabIndex = 14;
            this.toolTip1.SetToolTip(this.txtDmgMultBoost, "extra multiplyer apply\'d for every skill level");
            this.txtDmgMultBoost.TextChanged += new System.EventHandler(this.TxtDmgMultBoost_TextChanged);
            // 
            // txtDmgMultBase
            // 
            this.txtDmgMultBase.Location = new System.Drawing.Point(220, 131);
            this.txtDmgMultBase.Name = "txtDmgMultBase";
            this.txtDmgMultBase.Size = new System.Drawing.Size(46, 21);
            this.txtDmgMultBase.TabIndex = 13;
            this.toolTip1.SetToolTip(this.txtDmgMultBase, "multiplier apply\'d to total skill dmg");
            this.txtDmgMultBase.TextChanged += new System.EventHandler(this.TxtDmgMultBase_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 160);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(197, 12);
            this.label21.TabIndex = 12;
            this.label21.Text = "Damage multiplyer boost/skilllvl";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 134);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(137, 12);
            this.label22.TabIndex = 11;
            this.label22.Text = "Damage multiplyer base";
            // 
            // txtDmgBonusMax
            // 
            this.txtDmgBonusMax.Location = new System.Drawing.Point(220, 105);
            this.txtDmgBonusMax.Name = "txtDmgBonusMax";
            this.txtDmgBonusMax.Size = new System.Drawing.Size(46, 21);
            this.txtDmgBonusMax.TabIndex = 10;
            this.toolTip1.SetToolTip(this.txtDmgBonusMax, "Damage bonus at skill level \'4\' ");
            this.txtDmgBonusMax.TextChanged += new System.EventHandler(this.TxtDmgBonusMax_TextChanged);
            // 
            // txtDmgBonusMin
            // 
            this.txtDmgBonusMin.Location = new System.Drawing.Point(220, 79);
            this.txtDmgBonusMin.Name = "txtDmgBonusMin";
            this.txtDmgBonusMin.Size = new System.Drawing.Size(46, 21);
            this.txtDmgBonusMin.TabIndex = 9;
            this.toolTip1.SetToolTip(this.txtDmgBonusMin, "Damage bonus at skill level \'4\' \r\nyou will get 1/4th of this bonus for every skil" +
        "l level\r\nnote ingame level 0 = 1 bonus, so level 3 = max bonus (4)");
            this.txtDmgBonusMin.TextChanged += new System.EventHandler(this.TxtDmgBonusMin_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 108);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(161, 12);
            this.label18.TabIndex = 8;
            this.label18.Text = "Maximum skill lvl 3 damage";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 82);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(167, 12);
            this.label19.TabIndex = 7;
            this.label19.Text = "Minimum skill lvl 3 damage:";
            // 
            // txtDmgBaseMax
            // 
            this.txtDmgBaseMax.Location = new System.Drawing.Point(220, 53);
            this.txtDmgBaseMax.Name = "txtDmgBaseMax";
            this.txtDmgBaseMax.Size = new System.Drawing.Size(46, 21);
            this.txtDmgBaseMax.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtDmgBaseMax, "Damage at skill level 0");
            this.txtDmgBaseMax.TextChanged += new System.EventHandler(this.TxtDmgBaseMax_TextChanged);
            // 
            // txtDmgBaseMin
            // 
            this.txtDmgBaseMin.Location = new System.Drawing.Point(220, 27);
            this.txtDmgBaseMin.Name = "txtDmgBaseMin";
            this.txtDmgBaseMin.Size = new System.Drawing.Size(46, 21);
            this.txtDmgBaseMin.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtDmgBaseMin, "Damage at skill level 0");
            this.txtDmgBaseMin.TextChanged += new System.EventHandler(this.TxtDmgBaseMin_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 12);
            this.label17.TabIndex = 2;
            this.label17.Text = "Maximum base damage";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "Minimum base damage:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "Damage settings";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.txtRange);
            this.panel3.Controls.Add(this.txtDelayReduction);
            this.panel3.Controls.Add(this.txtDelayBase);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Location = new System.Drawing.Point(301, 166);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(247, 191);
            this.panel3.TabIndex = 5;
            this.toolTip1.SetToolTip(this.panel3, "delay = <base delay> - (skill level * <decrease>)");
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 77);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(113, 12);
            this.label20.TabIndex = 15;
            this.label20.Text = "Range (0 No limit)";
            // 
            // txtRange
            // 
            this.txtRange.Location = new System.Drawing.Point(156, 74);
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(79, 21);
            this.txtRange.TabIndex = 14;
            this.txtRange.TextChanged += new System.EventHandler(this.TxtRange_TextChanged);
            // 
            // txtDelayReduction
            // 
            this.txtDelayReduction.Location = new System.Drawing.Point(156, 47);
            this.txtDelayReduction.Name = "txtDelayReduction";
            this.txtDelayReduction.Size = new System.Drawing.Size(79, 21);
            this.txtDelayReduction.TabIndex = 13;
            this.toolTip1.SetToolTip(this.txtDelayReduction, "delay = <base delay> - (skill level * <decrease>)");
            this.txtDelayReduction.TextChanged += new System.EventHandler(this.TxtDelayReduction_TextChanged);
            // 
            // txtDelayBase
            // 
            this.txtDelayBase.Location = new System.Drawing.Point(156, 22);
            this.txtDelayBase.Name = "txtDelayBase";
            this.txtDelayBase.Size = new System.Drawing.Size(79, 21);
            this.txtDelayBase.TabIndex = 12;
            this.toolTip1.SetToolTip(this.txtDelayBase, "delay = <base delay> - (skill level * <decrease>)");
            this.txtDelayBase.TextChanged += new System.EventHandler(this.TxtDelayBase_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "Decrease / skill level";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "Base delay";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "Delay (in milliseconds!)";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtMPIncrease);
            this.panel2.Controls.Add(this.txtMPBase);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(301, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(247, 107);
            this.panel2.TabIndex = 4;
            // 
            // txtMPIncrease
            // 
            this.txtMPIncrease.Location = new System.Drawing.Point(189, 47);
            this.txtMPIncrease.Name = "txtMPIncrease";
            this.txtMPIncrease.Size = new System.Drawing.Size(46, 21);
            this.txtMPIncrease.TabIndex = 12;
            this.toolTip1.SetToolTip(this.txtMPIncrease, "extra amount of mp used each level");
            this.txtMPIncrease.TextChanged += new System.EventHandler(this.TxtMPIncrease_TextChanged);
            // 
            // txtMPBase
            // 
            this.txtMPBase.Location = new System.Drawing.Point(189, 22);
            this.txtMPBase.Name = "txtMPBase";
            this.txtMPBase.Size = new System.Drawing.Size(46, 21);
            this.txtMPBase.TabIndex = 11;
            this.toolTip1.SetToolTip(this.txtMPBase, "Mp usage when skill is level 0");
            this.txtMPBase.TextChanged += new System.EventHandler(this.TxtMPBase_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "MP increase each level";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "Base mp usage";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "MP usage";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtSkillLvl3Points);
            this.panel1.Controls.Add(this.txtSkillLvl2Points);
            this.panel1.Controls.Add(this.txtSkillLvl1Points);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtSkillLvl3Req);
            this.panel1.Controls.Add(this.txtSkillLvl2Req);
            this.panel1.Controls.Add(this.txtSkillLvl1Req);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(13, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 107);
            this.panel1.TabIndex = 3;
            // 
            // txtSkillLvl3Points
            // 
            this.txtSkillLvl3Points.Location = new System.Drawing.Point(220, 72);
            this.txtSkillLvl3Points.Name = "txtSkillLvl3Points";
            this.txtSkillLvl3Points.Size = new System.Drawing.Size(46, 21);
            this.txtSkillLvl3Points.TabIndex = 12;
            this.txtSkillLvl3Points.TextChanged += new System.EventHandler(this.TxtSkillLvl3Points_TextChanged);
            // 
            // txtSkillLvl2Points
            // 
            this.txtSkillLvl2Points.Location = new System.Drawing.Point(220, 47);
            this.txtSkillLvl2Points.Name = "txtSkillLvl2Points";
            this.txtSkillLvl2Points.Size = new System.Drawing.Size(46, 21);
            this.txtSkillLvl2Points.TabIndex = 11;
            this.txtSkillLvl2Points.TextChanged += new System.EventHandler(this.TxtSkillLvl2Points_TextChanged);
            // 
            // txtSkillLvl1Points
            // 
            this.txtSkillLvl1Points.Location = new System.Drawing.Point(220, 22);
            this.txtSkillLvl1Points.Name = "txtSkillLvl1Points";
            this.txtSkillLvl1Points.Size = new System.Drawing.Size(46, 21);
            this.txtSkillLvl1Points.TabIndex = 10;
            this.txtSkillLvl1Points.TextChanged += new System.EventHandler(this.TxtSkillLvl1Points_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Skill points";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(137, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "Skill points";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(137, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Skill points";
            // 
            // txtSkillLvl3Req
            // 
            this.txtSkillLvl3Req.Location = new System.Drawing.Point(71, 72);
            this.txtSkillLvl3Req.Name = "txtSkillLvl3Req";
            this.txtSkillLvl3Req.Size = new System.Drawing.Size(46, 21);
            this.txtSkillLvl3Req.TabIndex = 6;
            this.txtSkillLvl3Req.TextChanged += new System.EventHandler(this.TxtSkillLvl3Req_TextChanged);
            // 
            // txtSkillLvl2Req
            // 
            this.txtSkillLvl2Req.Location = new System.Drawing.Point(71, 47);
            this.txtSkillLvl2Req.Name = "txtSkillLvl2Req";
            this.txtSkillLvl2Req.Size = new System.Drawing.Size(46, 21);
            this.txtSkillLvl2Req.TabIndex = 5;
            this.txtSkillLvl2Req.TextChanged += new System.EventHandler(this.TxtSkillLvl2Req_TextChanged);
            // 
            // txtSkillLvl1Req
            // 
            this.txtSkillLvl1Req.Location = new System.Drawing.Point(71, 22);
            this.txtSkillLvl1Req.Name = "txtSkillLvl1Req";
            this.txtSkillLvl1Req.Size = new System.Drawing.Size(46, 21);
            this.txtSkillLvl1Req.TabIndex = 4;
            this.txtSkillLvl1Req.TextChanged += new System.EventHandler(this.TxtSkillLvl1Req_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "level 3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "level 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "level 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Skill level increase requirements";
            // 
            // txtSkillIcon
            // 
            this.txtSkillIcon.Location = new System.Drawing.Point(330, 18);
            this.txtSkillIcon.Name = "txtSkillIcon";
            this.txtSkillIcon.Size = new System.Drawing.Size(41, 21);
            this.txtSkillIcon.TabIndex = 2;
            this.txtSkillIcon.TextChanged += new System.EventHandler(this.TxtSkillIcon_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Skill icon: ";
            // 
            // lblBookValid
            // 
            this.lblBookValid.AutoSize = true;
            this.lblBookValid.Location = new System.Drawing.Point(222, 3);
            this.lblBookValid.Name = "lblBookValid";
            this.lblBookValid.Size = new System.Drawing.Size(119, 12);
            this.lblBookValid.TabIndex = 0;
            this.lblBookValid.Text = "Searching for books";
            // 
            // tbxSearchMagic
            // 
            this.tbxSearchMagic.Location = new System.Drawing.Point(45, 38);
            this.tbxSearchMagic.Name = "tbxSearchMagic";
            this.tbxSearchMagic.Size = new System.Drawing.Size(174, 21);
            this.tbxSearchMagic.TabIndex = 2;
            this.tbxSearchMagic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxSearchMagic_KeyDown);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 42);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(29, 12);
            this.label25.TabIndex = 3;
            this.label25.Text = "搜索";
            // 
            // btnAddSkill
            // 
            this.btnAddSkill.Location = new System.Drawing.Point(481, 9);
            this.btnAddSkill.Name = "btnAddSkill";
            this.btnAddSkill.Size = new System.Drawing.Size(75, 23);
            this.btnAddSkill.TabIndex = 4;
            this.btnAddSkill.Text = "Add";
            this.btnAddSkill.UseVisualStyleBackColor = true;
            // 
            // btnDeleteSkill
            // 
            this.btnDeleteSkill.Location = new System.Drawing.Point(581, 9);
            this.btnDeleteSkill.Name = "btnDeleteSkill";
            this.btnDeleteSkill.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteSkill.TabIndex = 4;
            this.btnDeleteSkill.Text = "Delete";
            this.btnDeleteSkill.UseVisualStyleBackColor = true;
            this.btnDeleteSkill.Click += new System.EventHandler(this.btnDeleteSkill_Click);
            // 
            // MagicInfoForm
            // 
            this.ClientSize = new System.Drawing.Size(927, 559);
            this.Controls.Add(this.btnDeleteSkill);
            this.Controls.Add(this.btnAddSkill);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.tbxSearchMagic);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MagiclistBox);
            this.Name = "MagicInfoForm";
            this.Text = "Magic Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MagicInfoForm_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MagicInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //do something to save it all
            Envir.SaveDB();
        }

        private void MagiclistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMagicForm();
        }

        private bool IsValid(ref byte input)
        {
            if (!byte.TryParse(ActiveControl.Text, out input))
            {
                ActiveControl.BackColor = Color.Red;
                return false;
            }
            return true;
        }
        private bool IsValid(ref ushort input)
        {
            if (!ushort.TryParse(ActiveControl.Text, out input))
            {
                ActiveControl.BackColor = Color.Red;
                return false;
            }
            return true;
        }

        private bool IsValid(ref uint input)
        {
            if (!uint.TryParse(ActiveControl.Text, out input))
            {
                ActiveControl.BackColor = Color.Red;
                return false;
            }
            return true;
        }

        private bool IsValid(ref float input)
        {
            if (!float.TryParse(ActiveControl.Text, out input))
            {
                ActiveControl.BackColor = Color.Red;
                return false;
            }
            return true;
        }

        private void TxtSkillIcon_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            byte temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Icon = temp;
        }

        private void TxtSkillLvl1Req_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            byte temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Level1 = temp;
        }

        private void TxtSkillLvl2Req_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            byte temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Level2 = temp;
        }

        private void TxtSkillLvl3Req_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            byte temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Level3 = temp;
        }

        private void TxtSkillLvl1Points_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ushort temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Need1 = temp;
        }

        private void TxtSkillLvl2Points_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ushort temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Need2 = temp;
        }

        private void TxtSkillLvl3Points_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ushort temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Need3 = temp;
        }

        private void TxtMPBase_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            byte temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.BaseCost = temp;
        }

        private void TxtMPIncrease_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            byte temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.LevelCost = temp;
        }

        private void TxtDmgBaseMin_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ushort temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.PowerBase = temp;
            UpdateMagicForm();
        }

        private void TxtDmgBaseMax_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ushort temp = 0;
            if (!IsValid(ref temp)) return;
            if (temp < _selectedMagicInfo.PowerBase)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.PowerBonus =  (ushort)(temp - _selectedMagicInfo.PowerBase);
            UpdateMagicForm();
        }

        private void TxtDmgBonusMin_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ushort temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.MPowerBase = temp;
            UpdateMagicForm();
        }

        private void TxtDmgBonusMax_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ushort temp = 0;
            if (!IsValid(ref temp)) return;
            if (temp < _selectedMagicInfo.MPowerBase)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.MPowerBonus = (ushort)(temp - _selectedMagicInfo.MPowerBase);
            UpdateMagicForm();
        }

        private void TxtDelayBase_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            uint temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.DelayBase = temp;
        }

        private void TxtDelayReduction_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            uint temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.DelayReduction = temp;
        }

        private void TxtRange_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            byte temp = 0;
            if (!IsValid(ref temp)) return;
            
            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.Range = temp;
        }

        private void TxtDmgMultBase_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            float temp = 0;
            if (!IsValid(ref temp)) return;


            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.MultiplierBase = temp;
            UpdateMagicForm(1);
        }

        private void TxtDmgMultBoost_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            float temp = 0;
            if (!IsValid(ref temp)) return;

            ActiveControl.BackColor = SystemColors.Window;
            _selectedMagicInfo.MultiplierBonus = temp;
            UpdateMagicForm(2);
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            _selectedMagicInfo.Name = ActiveControl.Text;
            UpdateMagicForm();
            if (ActiveControl.Text == "")
            {
                ActiveControl.BackColor = Color.Red;
            }
            else {
                ActiveControl.BackColor = SystemColors.Window;              
            }            
        }

        private void TbxSearchMagic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var searchName = tbxSearchMagic.Text.Trim().ToLower();
                var itemList = Envir.MagicInfoList.FindAll(x => x.Name.ToLower().Contains(searchName)).ToArray();

                //itemList.Sort((x, y) => x.Name.CompareTo(y.Name));
                MagiclistBox.Items.Clear();
                MagiclistBox.Items.AddRange(itemList);
            }
        }

        private void btnDeleteSkill_Click(object sender, EventArgs e)
        {
            if (MagiclistBox.SelectedIndex==-1)
            {
                MessageBox.Show("请选中一个技能再删除");
            }
            else
            {
                var magicInfo = MagiclistBox.SelectedItem as MagicInfo;
                Envir.MagicInfoList.Remove(magicInfo);
                MagiclistBox.Items.RemoveAt(MagiclistBox.SelectedIndex);
                Envir.SaveDB();
                InitMagicListBox();
            }

        }
    }
}
