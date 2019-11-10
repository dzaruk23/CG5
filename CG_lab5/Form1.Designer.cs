namespace CG_lab5
{
    partial class Form1
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
            this.button_L_sytems = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_L_sytems
            // 
            this.button_L_sytems.Location = new System.Drawing.Point(12, 13);
            this.button_L_sytems.Name = "button_L_sytems";
            this.button_L_sytems.Size = new System.Drawing.Size(203, 80);
            this.button_L_sytems.TabIndex = 0;
            this.button_L_sytems.Text = "L-системы";
            this.button_L_sytems.UseVisualStyleBackColor = true;
            this.button_L_sytems.Click += new System.EventHandler(this.button_L_sytems_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 108);
            this.Controls.Add(this.button_L_sytems);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_L_sytems;

    }
}

