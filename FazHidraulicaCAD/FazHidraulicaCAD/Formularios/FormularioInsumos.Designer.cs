namespace FazHidraulicaCAD.Formularios
{
    partial class FormularioInsumos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioInsumos));
            this.BtInserirInsumosH = new System.Windows.Forms.Button();
            this.ListBoxInsumosH = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboBoxInsumosH = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PictureBoxHidraulica = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxHidraulica)).BeginInit();
            this.SuspendLayout();
            // 
            // BtInserirInsumosH
            // 
            this.BtInserirInsumosH.BackColor = System.Drawing.SystemColors.Control;
            this.BtInserirInsumosH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtInserirInsumosH.Location = new System.Drawing.Point(13, 464);
            this.BtInserirInsumosH.Name = "BtInserirInsumosH";
            this.BtInserirInsumosH.Size = new System.Drawing.Size(200, 23);
            this.BtInserirInsumosH.TabIndex = 17;
            this.BtInserirInsumosH.Text = "Inserir";
            this.BtInserirInsumosH.UseVisualStyleBackColor = false;
            this.BtInserirInsumosH.Click += new System.EventHandler(this.BtInserirInsumosH_Click);
            // 
            // ListBoxInsumosH
            // 
            this.ListBoxInsumosH.FormattingEnabled = true;
            this.ListBoxInsumosH.Location = new System.Drawing.Point(13, 259);
            this.ListBoxInsumosH.Name = "ListBoxInsumosH";
            this.ListBoxInsumosH.Size = new System.Drawing.Size(200, 199);
            this.ListBoxInsumosH.TabIndex = 16;
            this.ListBoxInsumosH.SelectedIndexChanged += new System.EventHandler(this.ListBoxInsumosH_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "Itens:";
            // 
            // ComboBoxInsumosH
            // 
            this.ComboBoxInsumosH.FormattingEnabled = true;
            this.ComboBoxInsumosH.Location = new System.Drawing.Point(13, 206);
            this.ComboBoxInsumosH.Name = "ComboBoxInsumosH";
            this.ComboBoxInsumosH.Size = new System.Drawing.Size(200, 21);
            this.ComboBoxInsumosH.TabIndex = 14;
            this.ComboBoxInsumosH.SelectedIndexChanged += new System.EventHandler(this.ComboBoxInsumosH_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Categoria:";
            // 
            // PictureBoxHidraulica
            // 
            this.PictureBoxHidraulica.Location = new System.Drawing.Point(13, 10);
            this.PictureBoxHidraulica.Name = "PictureBoxHidraulica";
            this.PictureBoxHidraulica.Size = new System.Drawing.Size(200, 160);
            this.PictureBoxHidraulica.TabIndex = 12;
            this.PictureBoxHidraulica.TabStop = false;
            // 
            // FormularioInsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 497);
            this.Controls.Add(this.BtInserirInsumosH);
            this.Controls.Add(this.ListBoxInsumosH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBoxInsumosH);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureBoxHidraulica);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(242, 536);
            this.MinimumSize = new System.Drawing.Size(242, 536);
            this.Name = "FormularioInsumos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insumos";
            this.Load += new System.EventHandler(this.FormularioInsumos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxHidraulica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtInserirInsumosH;
        private System.Windows.Forms.ListBox ListBoxInsumosH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboBoxInsumosH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PictureBoxHidraulica;
    }
}