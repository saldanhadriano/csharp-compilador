namespace Compilador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxEntrada = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgSimbolos = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgresult = new System.Windows.Forms.DataGridView();
            this.dgC3e = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSimbolos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgresult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgC3e)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxEntrada
            // 
            this.textBoxEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEntrada.Location = new System.Drawing.Point(12, 29);
            this.textBoxEntrada.Multiline = true;
            this.textBoxEntrada.Name = "textBoxEntrada";
            this.textBoxEntrada.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxEntrada.Size = new System.Drawing.Size(295, 430);
            this.textBoxEntrada.TabIndex = 2;
            this.textBoxEntrada.Text = resources.GetString("textBoxEntrada.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Entrada:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.DarkRed;
            this.dataGridView1.Location = new System.Drawing.Point(313, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(257, 615);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dgSimbolos
            // 
            this.dgSimbolos.AllowUserToAddRows = false;
            this.dgSimbolos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSimbolos.GridColor = System.Drawing.Color.DarkRed;
            this.dgSimbolos.Location = new System.Drawing.Point(576, 29);
            this.dgSimbolos.Name = "dgSimbolos";
            this.dgSimbolos.ReadOnly = true;
            this.dgSimbolos.RowHeadersVisible = false;
            this.dgSimbolos.Size = new System.Drawing.Size(300, 615);
            this.dgSimbolos.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(310, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Análise léxica:";
            // 
            // dgresult
            // 
            this.dgresult.AllowUserToAddRows = false;
            this.dgresult.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgresult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgresult.GridColor = System.Drawing.Color.DarkRed;
            this.dgresult.Location = new System.Drawing.Point(12, 479);
            this.dgresult.Name = "dgresult";
            this.dgresult.ReadOnly = true;
            this.dgresult.RowHeadersVisible = false;
            this.dgresult.Size = new System.Drawing.Size(295, 165);
            this.dgresult.TabIndex = 17;
            // 
            // dgC3e
            // 
            this.dgC3e.AllowUserToAddRows = false;
            this.dgC3e.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgC3e.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgC3e.GridColor = System.Drawing.Color.DarkRed;
            this.dgC3e.Location = new System.Drawing.Point(882, 29);
            this.dgC3e.Name = "dgC3e";
            this.dgC3e.ReadOnly = true;
            this.dgC3e.RowHeadersVisible = false;
            this.dgC3e.Size = new System.Drawing.Size(290, 615);
            this.dgC3e.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(879, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Codigo de três endereços:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(9, 463);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Resultado:";
            // 
            // btnAnalisar
            // 
            this.btnAnalisar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnAnalisar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAnalisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalisar.Location = new System.Drawing.Point(12, 650);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(230, 43);
            this.btnAnalisar.TabIndex = 0;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = false;
            this.btnAnalisar.Click += new System.EventHandler(this.BtnAnalisar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Location = new System.Drawing.Point(882, 650);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(263, 43);
            this.btnLimpar.TabIndex = 1;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.BtnLimpar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(573, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Tabela de simbolos:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1186, 701);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgC3e);
            this.Controls.Add(this.dgresult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgSimbolos);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxEntrada);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnAnalisar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compilador";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSimbolos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgresult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgC3e)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void dataGridView1_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
           
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxEntrada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dgSimbolos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgresult;
        private System.Windows.Forms.DataGridView dgC3e;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Label label3;
    }
}

