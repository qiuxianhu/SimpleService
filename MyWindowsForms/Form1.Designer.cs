namespace MyWindowsForms
{
    partial class Form1
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
            this.btnServiceStart = new System.Windows.Forms.Button();
            this.btnServiceStop = new System.Windows.Forms.Button();
            this.btnTestService = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnServiceStart
            // 
            this.btnServiceStart.Location = new System.Drawing.Point(99, 56);
            this.btnServiceStart.Name = "btnServiceStart";
            this.btnServiceStart.Size = new System.Drawing.Size(170, 60);
            this.btnServiceStart.TabIndex = 0;
            this.btnServiceStart.Text = "服务开始";
            this.btnServiceStart.UseVisualStyleBackColor = true;
            // 
            // btnServiceStop
            // 
            this.btnServiceStop.Location = new System.Drawing.Point(99, 185);
            this.btnServiceStop.Name = "btnServiceStop";
            this.btnServiceStop.Size = new System.Drawing.Size(170, 60);
            this.btnServiceStop.TabIndex = 1;
            this.btnServiceStop.Text = "服务停止";
            this.btnServiceStop.UseVisualStyleBackColor = true;
            // 
            // btnTestService
            // 
            this.btnTestService.Location = new System.Drawing.Point(99, 314);
            this.btnTestService.Name = "btnTestService";
            this.btnTestService.Size = new System.Drawing.Size(170, 60);
            this.btnTestService.TabIndex = 2;
            this.btnTestService.Text = "测试服务";
            this.btnTestService.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 450);
            this.Controls.Add(this.btnTestService);
            this.Controls.Add(this.btnServiceStop);
            this.Controls.Add(this.btnServiceStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnServiceStart;
        private System.Windows.Forms.Button btnServiceStop;
        private System.Windows.Forms.Button btnTestService;
    }
}

