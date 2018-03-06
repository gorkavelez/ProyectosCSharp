namespace winAvalaraTest
{
    partial class Main
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
            this.cmdGetDocNo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResp = new System.Windows.Forms.TextBox();
            this.cmdVoid = new System.Windows.Forms.Button();
            this.cmdCOMMIT = new System.Windows.Forms.Button();
            this.cmdADJUST = new System.Windows.Forms.Button();
            this.cmdDELETE = new System.Windows.Forms.Button();
            this.cbTransactions = new System.Windows.Forms.ComboBox();
            this.lblTotalTrans = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.transaction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNewDocCode = new System.Windows.Forms.TextBox();
            this.cmdNEWCODE = new System.Windows.Forms.Button();
            this.cmdUNCOMMIT = new System.Windows.Forms.Button();
            this.btnHORA = new System.Windows.Forms.Button();
            this.chkPRO = new System.Windows.Forms.CheckBox();
            this.txtDoc = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdGetDocNo
            // 
            this.cmdGetDocNo.Location = new System.Drawing.Point(364, 57);
            this.cmdGetDocNo.Name = "cmdGetDocNo";
            this.cmdGetDocNo.Size = new System.Drawing.Size(75, 23);
            this.cmdGetDocNo.TabIndex = 0;
            this.cmdGetDocNo.Text = "GetByCode";
            this.cmdGetDocNo.UseVisualStyleBackColor = true;
            this.cmdGetDocNo.Click += new System.EventHandler(this.cmdGetDocNo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Document No.";
            // 
            // txtResp
            // 
            this.txtResp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResp.Location = new System.Drawing.Point(16, 111);
            this.txtResp.Multiline = true;
            this.txtResp.Name = "txtResp";
            this.txtResp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResp.Size = new System.Drawing.Size(956, 292);
            this.txtResp.TabIndex = 3;
            // 
            // cmdVoid
            // 
            this.cmdVoid.Location = new System.Drawing.Point(498, 8);
            this.cmdVoid.Name = "cmdVoid";
            this.cmdVoid.Size = new System.Drawing.Size(75, 23);
            this.cmdVoid.TabIndex = 4;
            this.cmdVoid.Text = "VOID";
            this.cmdVoid.UseVisualStyleBackColor = true;
            this.cmdVoid.Click += new System.EventHandler(this.cmdVoid_Click);
            // 
            // cmdCOMMIT
            // 
            this.cmdCOMMIT.Location = new System.Drawing.Point(665, 8);
            this.cmdCOMMIT.Name = "cmdCOMMIT";
            this.cmdCOMMIT.Size = new System.Drawing.Size(75, 23);
            this.cmdCOMMIT.TabIndex = 5;
            this.cmdCOMMIT.Text = "COMMIT";
            this.cmdCOMMIT.UseVisualStyleBackColor = true;
            this.cmdCOMMIT.Click += new System.EventHandler(this.cmdCOMMIT_Click);
            // 
            // cmdADJUST
            // 
            this.cmdADJUST.Location = new System.Drawing.Point(746, 8);
            this.cmdADJUST.Name = "cmdADJUST";
            this.cmdADJUST.Size = new System.Drawing.Size(75, 23);
            this.cmdADJUST.TabIndex = 6;
            this.cmdADJUST.Text = "ADJUST";
            this.cmdADJUST.UseVisualStyleBackColor = true;
            this.cmdADJUST.Click += new System.EventHandler(this.cmdADJUST_Click);
            // 
            // cmdDELETE
            // 
            this.cmdDELETE.Location = new System.Drawing.Point(579, 8);
            this.cmdDELETE.Name = "cmdDELETE";
            this.cmdDELETE.Size = new System.Drawing.Size(75, 23);
            this.cmdDELETE.TabIndex = 7;
            this.cmdDELETE.Text = "DELETE";
            this.cmdDELETE.UseVisualStyleBackColor = true;
            this.cmdDELETE.Click += new System.EventHandler(this.cmdDELETE_Click);
            // 
            // cbTransactions
            // 
            this.cbTransactions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTransactions.FormattingEnabled = true;
            this.cbTransactions.Location = new System.Drawing.Point(95, 8);
            this.cbTransactions.Name = "cbTransactions";
            this.cbTransactions.Size = new System.Drawing.Size(260, 21);
            this.cbTransactions.TabIndex = 8;
            // 
            // lblTotalTrans
            // 
            this.lblTotalTrans.AutoSize = true;
            this.lblTotalTrans.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTrans.ForeColor = System.Drawing.Color.Green;
            this.lblTotalTrans.Location = new System.Drawing.Point(361, 11);
            this.lblTotalTrans.Name = "lblTotalTrans";
            this.lblTotalTrans.Size = new System.Drawing.Size(23, 17);
            this.lblTotalTrans.TabIndex = 9;
            this.lblTotalTrans.Text = "...";
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.transaction});
            this.dgData.Location = new System.Drawing.Point(16, 418);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.Size = new System.Drawing.Size(953, 320);
            this.dgData.TabIndex = 15;
            // 
            // transaction
            // 
            this.transaction.HeaderText = "Transaction";
            this.transaction.Name = "transaction";
            this.transaction.ReadOnly = true;
            this.transaction.Width = 700;
            // 
            // txtNewDocCode
            // 
            this.txtNewDocCode.Location = new System.Drawing.Point(95, 34);
            this.txtNewDocCode.Name = "txtNewDocCode";
            this.txtNewDocCode.Size = new System.Drawing.Size(260, 20);
            this.txtNewDocCode.TabIndex = 16;
            // 
            // cmdNEWCODE
            // 
            this.cmdNEWCODE.Location = new System.Drawing.Point(498, 34);
            this.cmdNEWCODE.Name = "cmdNEWCODE";
            this.cmdNEWCODE.Size = new System.Drawing.Size(156, 23);
            this.cmdNEWCODE.TabIndex = 17;
            this.cmdNEWCODE.Text = "CHANGE DOC CODE";
            this.cmdNEWCODE.UseVisualStyleBackColor = true;
            this.cmdNEWCODE.Click += new System.EventHandler(this.cmdNEWCODE_Click);
            // 
            // cmdUNCOMMIT
            // 
            this.cmdUNCOMMIT.Location = new System.Drawing.Point(665, 34);
            this.cmdUNCOMMIT.Name = "cmdUNCOMMIT";
            this.cmdUNCOMMIT.Size = new System.Drawing.Size(75, 23);
            this.cmdUNCOMMIT.TabIndex = 18;
            this.cmdUNCOMMIT.Text = "UNCOMMIT";
            this.cmdUNCOMMIT.UseVisualStyleBackColor = true;
            this.cmdUNCOMMIT.Click += new System.EventHandler(this.cmdUNCOMMIT_Click);
            // 
            // btnHORA
            // 
            this.btnHORA.Location = new System.Drawing.Point(856, 11);
            this.btnHORA.Name = "btnHORA";
            this.btnHORA.Size = new System.Drawing.Size(75, 43);
            this.btnHORA.TabIndex = 19;
            this.btnHORA.Text = "HORA TEST";
            this.btnHORA.UseVisualStyleBackColor = true;
            this.btnHORA.Click += new System.EventHandler(this.btnHORA_Click);
            // 
            // chkPRO
            // 
            this.chkPRO.AutoSize = true;
            this.chkPRO.Location = new System.Drawing.Point(16, 34);
            this.chkPRO.Name = "chkPRO";
            this.chkPRO.Size = new System.Drawing.Size(49, 17);
            this.chkPRO.TabIndex = 20;
            this.chkPRO.Text = "PRO";
            this.chkPRO.UseVisualStyleBackColor = true;
            // 
            // txtDoc
            // 
            this.txtDoc.Location = new System.Drawing.Point(95, 60);
            this.txtDoc.Name = "txtDoc";
            this.txtDoc.Size = new System.Drawing.Size(260, 20);
            this.txtDoc.TabIndex = 21;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(856, 60);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 45);
            this.btnLoad.TabIndex = 22;
            this.btnLoad.Text = "LOAD";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 750);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtDoc);
            this.Controls.Add(this.chkPRO);
            this.Controls.Add(this.btnHORA);
            this.Controls.Add(this.cmdUNCOMMIT);
            this.Controls.Add(this.cmdNEWCODE);
            this.Controls.Add(this.txtNewDocCode);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.lblTotalTrans);
            this.Controls.Add(this.cbTransactions);
            this.Controls.Add(this.cmdDELETE);
            this.Controls.Add(this.cmdADJUST);
            this.Controls.Add(this.cmdCOMMIT);
            this.Controls.Add(this.cmdVoid);
            this.Controls.Add(this.txtResp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdGetDocNo);
            this.Name = "Main";
            this.Text = "TAXES";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdGetDocNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResp;
        private System.Windows.Forms.Button cmdVoid;
        private System.Windows.Forms.Button cmdCOMMIT;
        private System.Windows.Forms.Button cmdADJUST;
        private System.Windows.Forms.Button cmdDELETE;
        private System.Windows.Forms.ComboBox cbTransactions;
        private System.Windows.Forms.Label lblTotalTrans;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.DataGridViewTextBoxColumn transaction;
        private System.Windows.Forms.TextBox txtNewDocCode;
        private System.Windows.Forms.Button cmdNEWCODE;
        private System.Windows.Forms.Button cmdUNCOMMIT;
        private System.Windows.Forms.Button btnHORA;
        private System.Windows.Forms.CheckBox chkPRO;
        private System.Windows.Forms.TextBox txtDoc;
        private System.Windows.Forms.Button btnLoad;
    }
}

