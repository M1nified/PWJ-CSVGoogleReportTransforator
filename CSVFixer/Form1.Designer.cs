using DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CSVFixer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_addFiles = new System.Windows.Forms.Button();
            this.button_clearfilelist = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_outputDateTimeFormat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_nameFileWithMonth = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_keepChargedOnly = new System.Windows.Forms.CheckBox();
            this.checkBox_removePaymentDeclined = new System.Windows.Forms.CheckBox();
            this.checkBox_removeCanceled = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListBox_columnsToAdd = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button_doit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(640, 82);
            this.listBox1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Files (drag&&drop)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button_addFiles);
            this.panel1.Controls.Add(this.button_clearfilelist);
            this.panel1.Location = new System.Drawing.Point(6, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 32);
            this.panel1.TabIndex = 1;
            // 
            // button_addFiles
            // 
            this.button_addFiles.Location = new System.Drawing.Point(3, 3);
            this.button_addFiles.Name = "button_addFiles";
            this.button_addFiles.Size = new System.Drawing.Size(75, 23);
            this.button_addFiles.TabIndex = 0;
            this.button_addFiles.Text = "Add files";
            this.button_addFiles.UseVisualStyleBackColor = true;
            // 
            // button_clearfilelist
            // 
            this.button_clearfilelist.Location = new System.Drawing.Point(562, 3);
            this.button_clearfilelist.Name = "button_clearfilelist";
            this.button_clearfilelist.Size = new System.Drawing.Size(75, 23);
            this.button_clearfilelist.TabIndex = 0;
            this.button_clearfilelist.Text = "Clear file list";
            this.button_clearfilelist.UseVisualStyleBackColor = true;
            this.button_clearfilelist.Click += new System.EventHandler(this.button_clearfilelist_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkedListBox_columnsToAdd);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.checkedListBox1);
            this.groupBox2.Controls.Add(this.button_doit);
            this.groupBox2.Location = new System.Drawing.Point(12, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(652, 354);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transformation";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_outputDateTimeFormat);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.checkBox_nameFileWithMonth);
            this.groupBox4.Location = new System.Drawing.Point(275, 147);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(290, 83);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output file";
            // 
            // textBox_outputDateTimeFormat
            // 
            this.textBox_outputDateTimeFormat.Location = new System.Drawing.Point(130, 40);
            this.textBox_outputDateTimeFormat.Name = "textBox_outputDateTimeFormat";
            this.textBox_outputDateTimeFormat.Size = new System.Drawing.Size(154, 20);
            this.textBox_outputDateTimeFormat.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Output datetime format:";
            // 
            // checkBox_nameFileWithMonth
            // 
            this.checkBox_nameFileWithMonth.AutoSize = true;
            this.checkBox_nameFileWithMonth.Location = new System.Drawing.Point(6, 19);
            this.checkBox_nameFileWithMonth.Name = "checkBox_nameFileWithMonth";
            this.checkBox_nameFileWithMonth.Size = new System.Drawing.Size(154, 17);
            this.checkBox_nameFileWithMonth.TabIndex = 9;
            this.checkBox_nameFileWithMonth.Text = "Name file with single month";
            this.checkBox_nameFileWithMonth.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkBox_keepChargedOnly);
            this.groupBox3.Controls.Add(this.checkBox_removePaymentDeclined);
            this.groupBox3.Controls.Add(this.checkBox_removeCanceled);
            this.groupBox3.Location = new System.Drawing.Point(275, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 100);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data filters";
            // 
            // checkBox_keepChargedOnly
            // 
            this.checkBox_keepChargedOnly.AutoSize = true;
            this.checkBox_keepChargedOnly.Checked = true;
            this.checkBox_keepChargedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_keepChargedOnly.Location = new System.Drawing.Point(3, 19);
            this.checkBox_keepChargedOnly.Name = "checkBox_keepChargedOnly";
            this.checkBox_keepChargedOnly.Size = new System.Drawing.Size(206, 17);
            this.checkBox_keepChargedOnly.TabIndex = 6;
            this.checkBox_keepChargedOnly.Text = "Keep CHARGED and only CHARGED";
            this.checkBox_keepChargedOnly.UseVisualStyleBackColor = true;
            // 
            // checkBox_removePaymentDeclined
            // 
            this.checkBox_removePaymentDeclined.AutoSize = true;
            this.checkBox_removePaymentDeclined.Checked = true;
            this.checkBox_removePaymentDeclined.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_removePaymentDeclined.Location = new System.Drawing.Point(3, 42);
            this.checkBox_removePaymentDeclined.Name = "checkBox_removePaymentDeclined";
            this.checkBox_removePaymentDeclined.Size = new System.Drawing.Size(241, 17);
            this.checkBox_removePaymentDeclined.TabIndex = 7;
            this.checkBox_removePaymentDeclined.Text = "Remove PAYMENT_DECLINED transactions";
            this.checkBox_removePaymentDeclined.UseVisualStyleBackColor = true;
            // 
            // checkBox_removeCanceled
            // 
            this.checkBox_removeCanceled.AutoSize = true;
            this.checkBox_removeCanceled.Checked = true;
            this.checkBox_removeCanceled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_removeCanceled.Location = new System.Drawing.Point(3, 65);
            this.checkBox_removeCanceled.Name = "checkBox_removeCanceled";
            this.checkBox_removeCanceled.Size = new System.Drawing.Size(186, 17);
            this.checkBox_removeCanceled.TabIndex = 3;
            this.checkBox_removeCanceled.Text = "Remove CANCELED transactions";
            this.checkBox_removeCanceled.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Columns to add";
            // 
            // checkedListBox_columnsToAdd
            // 
            this.checkedListBox_columnsToAdd.FormattingEnabled = true;
            this.checkedListBox_columnsToAdd.Location = new System.Drawing.Point(275, 32);
            this.checkedListBox_columnsToAdd.Name = "checkedListBox_columnsToAdd";
            this.checkedListBox_columnsToAdd.Size = new System.Drawing.Size(290, 109);
            this.checkedListBox_columnsToAdd.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Columns";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 32);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(263, 304);
            this.checkedListBox1.TabIndex = 1;
            // 
            // button_doit
            // 
            this.button_doit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_doit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button_doit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_doit.Location = new System.Drawing.Point(571, 325);
            this.button_doit.Name = "button_doit";
            this.button_doit.Size = new System.Drawing.Size(75, 23);
            this.button_doit.TabIndex = 0;
            this.button_doit.Text = "DO IT!";
            this.button_doit.UseVisualStyleBackColor = false;
            this.button_doit.Click += new System.EventHandler(this.button_doit_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(676, 528);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "PWJ CSV Manager";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileList_OnDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBox1;
        private GroupBox groupBox1;
        private Panel panel1;
        private Button button_clearfilelist;
        private GroupBox groupBox2;
        private Label label1;
        private CheckedListBox checkedListBox1;
        private Button button_doit;
        private CheckBox checkBox_removeCanceled;
        private Label label2;
        private CheckedListBox checkedListBox_columnsToAdd;
        private CheckBox checkBox_keepChargedOnly;
        private CheckBox checkBox_removePaymentDeclined;
        private Button button_addFiles;
        private GroupBox groupBox4;
        private CheckBox checkBox_nameFileWithMonth;
        private GroupBox groupBox3;
        private TextBox textBox_outputDateTimeFormat;
        private Label label3;
    }
}

