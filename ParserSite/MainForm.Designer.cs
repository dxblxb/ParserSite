namespace ParserSite
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnStart = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TBUrl = new System.Windows.Forms.TextBox();
            this.RTBLog = new System.Windows.Forms.RichTextBox();
            this.webControl1 = new EO.WinForm.WebControl();
            this.Browser = new EO.WebBrowser.WebView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 611);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.webControl1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(110, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(690, 500);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.RTBLog);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(110, 500);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(690, 111);
            this.panel3.TabIndex = 2;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(12, 12);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 0;
            this.BtnStart.Text = "Старт";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TBUrl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(690, 21);
            this.panel4.TabIndex = 0;
            // 
            // TBUrl
            // 
            this.TBUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBUrl.Location = new System.Drawing.Point(0, 0);
            this.TBUrl.Name = "TBUrl";
            this.TBUrl.Size = new System.Drawing.Size(690, 20);
            this.TBUrl.TabIndex = 0;
            this.TBUrl.Text = "https:\\\\foto-lavka.ru";
            // 
            // RTBLog
            // 
            this.RTBLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBLog.Location = new System.Drawing.Point(0, 0);
            this.RTBLog.Name = "RTBLog";
            this.RTBLog.Size = new System.Drawing.Size(690, 111);
            this.RTBLog.TabIndex = 0;
            this.RTBLog.Text = "";
            // 
            // webControl1
            // 
            this.webControl1.BackColor = System.Drawing.Color.White;
            this.webControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webControl1.Location = new System.Drawing.Point(0, 21);
            this.webControl1.Name = "webControl1";
            this.webControl1.Size = new System.Drawing.Size(690, 479);
            this.webControl1.TabIndex = 1;
            this.webControl1.Text = "webControl1";
            this.webControl1.WebView = this.Browser;
            // 
            // Browser
            // 
            this.Browser.InputMsgFilter = null;
            this.Browser.ObjectForScripting = null;
            this.Browser.Title = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "ParserSite";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox TBUrl;
        private System.Windows.Forms.RichTextBox RTBLog;
        private EO.WinForm.WebControl webControl1;
        private EO.WebBrowser.WebView Browser;
    }
}

