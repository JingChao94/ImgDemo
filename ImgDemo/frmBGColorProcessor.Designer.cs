namespace ImgDemo
{
    partial class frmBgColorProcessor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBgColorProcessor));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelectImg = new System.Windows.Forms.Button();
            this.btnQuerybgColor = new System.Windows.Forms.Button();
            this.btnSelectReplacementColor = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTolerance = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbShowPanel = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelectImg);
            this.flowLayoutPanel1.Controls.Add(this.btnQuerybgColor);
            this.flowLayoutPanel1.Controls.Add(this.btnSelectReplacementColor);
            this.flowLayoutPanel1.Controls.Add(this.btnReplace);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.tbTolerance);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnSelectImg
            // 
            this.btnSelectImg.Location = new System.Drawing.Point(3, 3);
            this.btnSelectImg.Name = "btnSelectImg";
            this.btnSelectImg.Size = new System.Drawing.Size(106, 52);
            this.btnSelectImg.TabIndex = 4;
            this.btnSelectImg.Text = "选择图片";
            this.btnSelectImg.UseVisualStyleBackColor = true;
            this.btnSelectImg.Click += new System.EventHandler(this.btnSelectImg_Click);
            // 
            // btnQuerybgColor
            // 
            this.btnQuerybgColor.Enabled = false;
            this.btnQuerybgColor.Location = new System.Drawing.Point(115, 3);
            this.btnQuerybgColor.Name = "btnQuerybgColor";
            this.btnQuerybgColor.Size = new System.Drawing.Size(123, 52);
            this.btnQuerybgColor.TabIndex = 2;
            this.btnQuerybgColor.Text = "查找背景色";
            this.btnQuerybgColor.UseVisualStyleBackColor = true;
            this.btnQuerybgColor.Click += new System.EventHandler(this.btnQuerybgColor_Click);
            // 
            // btnSelectReplacementColor
            // 
            this.btnSelectReplacementColor.Enabled = false;
            this.btnSelectReplacementColor.Location = new System.Drawing.Point(244, 3);
            this.btnSelectReplacementColor.Name = "btnSelectReplacementColor";
            this.btnSelectReplacementColor.Size = new System.Drawing.Size(123, 52);
            this.btnSelectReplacementColor.TabIndex = 3;
            this.btnSelectReplacementColor.Text = "选择替换色";
            this.btnSelectReplacementColor.UseVisualStyleBackColor = true;
            this.btnSelectReplacementColor.Click += new System.EventHandler(this.btnSelectReplacementColor_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Enabled = false;
            this.btnReplace.Location = new System.Drawing.Point(373, 3);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(139, 52);
            this.btnReplace.TabIndex = 0;
            this.btnReplace.Text = "替换";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(518, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 40, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "容差:";
            // 
            // tbTolerance
            // 
            this.tbTolerance.Location = new System.Drawing.Point(559, 35);
            this.tbTolerance.Margin = new System.Windows.Forms.Padding(3, 35, 3, 3);
            this.tbTolerance.Name = "tbTolerance";
            this.tbTolerance.Size = new System.Drawing.Size(33, 21);
            this.tbTolerance.TabIndex = 9;
            this.tbTolerance.Text = "100";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(598, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 52);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel1.Controls.Add(this.pbShowPanel);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(3, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 389);
            this.panel1.TabIndex = 8;
            // 
            // pbShowPanel
            // 
            this.pbShowPanel.BackColor = System.Drawing.Color.LavenderBlush;
            this.pbShowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbShowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbShowPanel.ErrorImage = global::ImgDemo.Properties.Resources.微信图片_20191012100523;
            this.pbShowPanel.Image = global::ImgDemo.Properties.Resources._637399356059340000;
            this.pbShowPanel.Location = new System.Drawing.Point(0, 0);
            this.pbShowPanel.Name = "pbShowPanel";
            this.pbShowPanel.Size = new System.Drawing.Size(797, 389);
            this.pbShowPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbShowPanel.TabIndex = 2;
            this.pbShowPanel.TabStop = false;
            this.pbShowPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbShowPanel_MouseClick);
            // 
            // frmBgColorProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(816, 488);
            this.MinimumSize = new System.Drawing.Size(816, 488);
            this.Name = "frmBgColorProcessor";
            this.Text = "背景色替换器";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnQuerybgColor;
        private System.Windows.Forms.Button btnSelectReplacementColor;
        private System.Windows.Forms.Button btnSelectImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbShowPanel;
        private System.Windows.Forms.TextBox tbTolerance;
        private System.Windows.Forms.Button btnSave;
    }
}

