using aConverterClassLibrary;
namespace aConverter.Forms
{
    partial class FormCheckCasesList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NeedAnalize = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.checkCaseNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkCaseClassNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.проверитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkCaseClassBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxCheckCaseClass = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkCaseClassBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NeedAnalize,
            this.checkCaseNameDataGridViewTextBoxColumn,
            this.checkCaseClassNameDataGridViewTextBoxColumn,
            this.resultDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.checkCaseClassBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(546, 356);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            // 
            // NeedAnalize
            // 
            this.NeedAnalize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NeedAnalize.DataPropertyName = "NeedAnalize";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.NullValue = false;
            this.NeedAnalize.DefaultCellStyle = dataGridViewCellStyle1;
            this.NeedAnalize.FalseValue = "";
            this.NeedAnalize.HeaderText = "";
            this.NeedAnalize.Name = "NeedAnalize";
            this.NeedAnalize.Width = 20;
            // 
            // checkCaseNameDataGridViewTextBoxColumn
            // 
            this.checkCaseNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.checkCaseNameDataGridViewTextBoxColumn.DataPropertyName = "CheckCaseName";
            this.checkCaseNameDataGridViewTextBoxColumn.HeaderText = "Наименование теста";
            this.checkCaseNameDataGridViewTextBoxColumn.Name = "checkCaseNameDataGridViewTextBoxColumn";
            this.checkCaseNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // checkCaseClassNameDataGridViewTextBoxColumn
            // 
            this.checkCaseClassNameDataGridViewTextBoxColumn.DataPropertyName = "CheckCaseClassName";
            this.checkCaseClassNameDataGridViewTextBoxColumn.HeaderText = "Класс теста";
            this.checkCaseClassNameDataGridViewTextBoxColumn.Name = "checkCaseClassNameDataGridViewTextBoxColumn";
            this.checkCaseClassNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.checkCaseClassNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // resultDataGridViewTextBoxColumn
            // 
            this.resultDataGridViewTextBoxColumn.DataPropertyName = "ResultString";
            this.resultDataGridViewTextBoxColumn.FillWeight = 200F;
            this.resultDataGridViewTextBoxColumn.HeaderText = "Результат";
            this.resultDataGridViewTextBoxColumn.Name = "resultDataGridViewTextBoxColumn";
            this.resultDataGridViewTextBoxColumn.ReadOnly = true;
            this.resultDataGridViewTextBoxColumn.Width = 200;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.проверитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 26);
            // 
            // проверитьToolStripMenuItem
            // 
            this.проверитьToolStripMenuItem.Name = "проверитьToolStripMenuItem";
            this.проверитьToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.проверитьToolStripMenuItem.Text = "Проверить";
            this.проверитьToolStripMenuItem.Click += new System.EventHandler(this.проверитьToolStripMenuItem_Click);
            // 
            // checkCaseClassBindingSource
            // 
            this.checkCaseClassBindingSource.DataSource = typeof(aConverterClassLibrary.CheckCase);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripComboBoxCheckCaseClass});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(546, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::aConverter.Properties.Resources.bullet_go;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Запустить анализ";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::aConverter.Properties.Resources.tick;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Остановить анализ";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripComboBoxCheckCaseClass
            // 
            this.toolStripComboBoxCheckCaseClass.DropDownWidth = 300;
            this.toolStripComboBoxCheckCaseClass.Name = "toolStripComboBoxCheckCaseClass";
            this.toolStripComboBoxCheckCaseClass.Size = new System.Drawing.Size(300, 25);
            this.toolStripComboBoxCheckCaseClass.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxCheckCaseClass_SelectedIndexChanged);
            // 
            // FormCheckCasesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 382);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormCheckCasesList";
            this.Text = "Варианты проверки";
            this.Load += new System.EventHandler(this.FormCheckCasesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkCaseClassBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.BindingSource checkCaseClassBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem проверитьToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NeedAnalize;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkCaseNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkCaseClassNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCheckCaseClass;
    }
}