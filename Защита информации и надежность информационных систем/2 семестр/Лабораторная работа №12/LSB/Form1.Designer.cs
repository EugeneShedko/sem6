namespace StegProject
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(316, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Осадить сообщение ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.write_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(262, 199);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(315, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(189, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Извлечь сообщение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.read_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Контейнер с осажденным сообщением:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(305, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 71);
            this.label1.TabIndex = 6;
            this.label1.Text = "1. Выберите пустой контейнер bmp \r\n2. Выберите файл txt с сообщением \r\n3. Назовит" +
    "е bmp-контейнер с осажденным сообщением";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(304, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 60);
            this.label2.TabIndex = 7;
            this.label2.Text = "1. Выберите bmp файл с осажденным сообщением\r\n2. Назовите txt-файл, куда запишетс" +
    "я извлеченное сообщение";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 268);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Метод LSB";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

