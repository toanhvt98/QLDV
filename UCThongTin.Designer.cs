
namespace QLDV
{
    partial class UCThongTin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.qLDVDataSet = new QLDV.QLDVDataSet();
            this.dangvienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dangvienTableAdapter = new QLDV.QLDVDataSetTableAdapters.dangvienTableAdapter();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anhdangvienDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.solylichDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sotheDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tendangvienDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDVDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dangvienBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.anhdangvienDataGridViewImageColumn,
            this.solylichDataGridViewTextBoxColumn,
            this.sotheDataGridViewTextBoxColumn,
            this.tendangvienDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dangvienBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(46, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(742, 209);
            this.dataGridView1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(281, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(246, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Thông tin Đảng viên";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(250, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 22);
            this.textBox1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(167, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tìm kiếm:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(417, 60);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 21);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Số lý lịch";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(519, 60);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(70, 21);
            this.radioButton2.TabIndex = 10;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Số thẻ";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(606, 60);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(54, 21);
            this.radioButton3.TabIndex = 10;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Tên";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(694, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 50);
            this.button1.TabIndex = 11;
            this.button1.Text = "Xem";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // qLDVDataSet
            // 
            this.qLDVDataSet.DataSetName = "QLDVDataSet";
            this.qLDVDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dangvienBindingSource
            // 
            this.dangvienBindingSource.DataMember = "dangvien";
            this.dangvienBindingSource.DataSource = this.qLDVDataSet;
            // 
            // dangvienTableAdapter
            // 
            this.dangvienTableAdapter.ClearBeforeFill = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Width = 125;
            // 
            // anhdangvienDataGridViewImageColumn
            // 
            this.anhdangvienDataGridViewImageColumn.DataPropertyName = "anhdangvien";
            this.anhdangvienDataGridViewImageColumn.HeaderText = "anhdangvien";
            this.anhdangvienDataGridViewImageColumn.MinimumWidth = 6;
            this.anhdangvienDataGridViewImageColumn.Name = "anhdangvienDataGridViewImageColumn";
            this.anhdangvienDataGridViewImageColumn.ReadOnly = true;
            this.anhdangvienDataGridViewImageColumn.Width = 200;
            // 
            // solylichDataGridViewTextBoxColumn
            // 
            this.solylichDataGridViewTextBoxColumn.DataPropertyName = "solylich";
            this.solylichDataGridViewTextBoxColumn.HeaderText = "solylich";
            this.solylichDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.solylichDataGridViewTextBoxColumn.Name = "solylichDataGridViewTextBoxColumn";
            this.solylichDataGridViewTextBoxColumn.ReadOnly = true;
            this.solylichDataGridViewTextBoxColumn.Width = 125;
            // 
            // sotheDataGridViewTextBoxColumn
            // 
            this.sotheDataGridViewTextBoxColumn.DataPropertyName = "sothe";
            this.sotheDataGridViewTextBoxColumn.HeaderText = "sothe";
            this.sotheDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.sotheDataGridViewTextBoxColumn.Name = "sotheDataGridViewTextBoxColumn";
            this.sotheDataGridViewTextBoxColumn.ReadOnly = true;
            this.sotheDataGridViewTextBoxColumn.Width = 125;
            // 
            // tendangvienDataGridViewTextBoxColumn
            // 
            this.tendangvienDataGridViewTextBoxColumn.DataPropertyName = "tendangvien";
            this.tendangvienDataGridViewTextBoxColumn.HeaderText = "tendangvien";
            this.tendangvienDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tendangvienDataGridViewTextBoxColumn.Name = "tendangvienDataGridViewTextBoxColumn";
            this.tendangvienDataGridViewTextBoxColumn.ReadOnly = true;
            this.tendangvienDataGridViewTextBoxColumn.Width = 125;
            // 
            // UCThongTin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Name = "UCThongTin";
            this.Size = new System.Drawing.Size(825, 409);
            this.Load += new System.EventHandler(this.UCThongTin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDVDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dangvienBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource dangvienBindingSource;
        private QLDVDataSet qLDVDataSet;
        private QLDVDataSetTableAdapters.dangvienTableAdapter dangvienTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn anhdangvienDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn solylichDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sotheDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tendangvienDataGridViewTextBoxColumn;
    }
}
