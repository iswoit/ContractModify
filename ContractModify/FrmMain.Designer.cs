namespace ContractModify
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExecute = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.bwTask = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(545, 258);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "运行";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // tbLog
            // 
            this.tbLog.BackColor = System.Drawing.Color.White;
            this.tbLog.Location = new System.Drawing.Point(12, 38);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(608, 199);
            this.tbLog.TabIndex = 1;
            // 
            // bwTask
            // 
            this.bwTask.WorkerReportsProgress = true;
            this.bwTask.WorkerSupportsCancellation = true;
            this.bwTask.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwTask_DoWork);
            this.bwTask.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwTask_ProgressChanged);
            this.bwTask.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwTask_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "日志:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(335, 84);
            this.label2.TabIndex = 3;
            this.label2.Text = "功能：\r\n1.将d:\\qsfile\\zqqs\\sh目录下jsmx*_js326.mdd拷贝至d:\\qsxg\r\n2.将d:\\qsfile\\zqqs\\sz\\sjsjg" +
    "mmdd.dbf拷贝至d:\\qsxg\r\n3.执行修改逻辑修改合同前缀号(问照哥)\r\n4.将修改完的4个文件拷回源目录\r\n*临时修改1年，停用时删除自动化节点\r\n" +
    "\r\n";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 340);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnExecute);
            this.Name = "FrmMain";
            this.Text = "修改合同前缀_临时";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbLog;
        private System.ComponentModel.BackgroundWorker bwTask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

