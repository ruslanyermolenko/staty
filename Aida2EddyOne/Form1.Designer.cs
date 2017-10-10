namespace Aida2EddyOne
{
    partial class AidaToAddyOne
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AidaToAddyOne));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listEddyOneFile = new System.Windows.Forms.ListView();
            this.ListDEP_File = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SELECTAIDAFILE = new System.Windows.Forms.Button();
            this.progress_Bar = new System.Windows.Forms.ProgressBar();
            this.SETCALEDDYONE = new System.Windows.Forms.Button();
            this.SelectDirAidafiles = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listEddyOneFile);
            this.panel1.Controls.Add(this.ListDEP_File);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1237, 317);
            this.panel1.TabIndex = 0;
            // 
            // listEddyOneFile
            // 
            this.listEddyOneFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listEddyOneFile.Location = new System.Drawing.Point(615, 0);
            this.listEddyOneFile.Name = "listEddyOneFile";
            this.listEddyOneFile.Size = new System.Drawing.Size(622, 317);
            this.listEddyOneFile.TabIndex = 1;
            this.listEddyOneFile.UseCompatibleStateImageBehavior = false;
            this.listEddyOneFile.View = System.Windows.Forms.View.List;
            // 
            // ListDEP_File
            // 
            this.ListDEP_File.Dock = System.Windows.Forms.DockStyle.Left;
            this.ListDEP_File.GridLines = true;
            this.ListDEP_File.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ListDEP_File.Location = new System.Drawing.Point(0, 0);
            this.ListDEP_File.Name = "ListDEP_File";
            this.ListDEP_File.Size = new System.Drawing.Size(615, 317);
            this.ListDEP_File.TabIndex = 0;
            this.ListDEP_File.UseCompatibleStateImageBehavior = false;
            this.ListDEP_File.View = System.Windows.Forms.View.List;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.SELECTAIDAFILE);
            this.panel2.Controls.Add(this.progress_Bar);
            this.panel2.Controls.Add(this.SETCALEDDYONE);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 317);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1237, 82);
            this.panel2.TabIndex = 2;
            // 
            // SELECTAIDAFILE
            // 
            this.SELECTAIDAFILE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SELECTAIDAFILE.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.SELECTAIDAFILE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SELECTAIDAFILE.Location = new System.Drawing.Point(0, 29);
            this.SELECTAIDAFILE.Name = "SELECTAIDAFILE";
            this.SELECTAIDAFILE.Size = new System.Drawing.Size(1237, 33);
            this.SELECTAIDAFILE.TabIndex = 7;
            this.SELECTAIDAFILE.Text = "Вкажіть кореневу директорію \"КОНТРОЛЮ\" в форматі AIDA";
            this.SELECTAIDAFILE.UseVisualStyleBackColor = true;
            this.SELECTAIDAFILE.Click += new System.EventHandler(this.SELECTAIDAFILE_Click_2);
            // 
            // progress_Bar
            // 
            this.progress_Bar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progress_Bar.Location = new System.Drawing.Point(0, 62);
            this.progress_Bar.Name = "progress_Bar";
            this.progress_Bar.Size = new System.Drawing.Size(1237, 20);
            this.progress_Bar.TabIndex = 6;
            // 
            // SETCALEDDYONE
            // 
            this.SETCALEDDYONE.Dock = System.Windows.Forms.DockStyle.Top;
            this.SETCALEDDYONE.Location = new System.Drawing.Point(0, 0);
            this.SETCALEDDYONE.Name = "SETCALEDDYONE";
            this.SETCALEDDYONE.Size = new System.Drawing.Size(1237, 29);
            this.SETCALEDDYONE.TabIndex = 3;
            this.SETCALEDDYONE.Text = "Вкажіть, будь-ласка, кореневу директорію, де буде створено сконвертований \"КОНТРО" +
    "ЛЬ\"";
            this.SETCALEDDYONE.UseVisualStyleBackColor = true;
            this.SETCALEDDYONE.Click += new System.EventHandler(this.SETCALEDDYONE_Click);
            // 
            // AidaToAddyOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 399);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AidaToAddyOne";
            this.Text = "AIDA to EddyOne ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AidaToAddyOne_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView ListDEP_File;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FolderBrowserDialog SelectDirAidafiles;
        private System.Windows.Forms.ListView listEddyOneFile;
        private System.Windows.Forms.Button SETCALEDDYONE;
        private System.Windows.Forms.Button SELECTAIDAFILE;
        private System.Windows.Forms.ProgressBar progress_Bar;
    }
}

