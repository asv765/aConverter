using aConverterClassLibrary;
namespace aConverter.Forms
{
    partial class FormStatistics
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.сформироватьВсеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сформироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьСтатистикуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьСтатистикуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSelectType1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxSelectType2 = new System.Windows.Forms.ToolStripComboBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statisticNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statisticTypeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сформироватьВсеToolStripMenuItem,
            this.сформироватьToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.добавитьСтатистикуToolStripMenuItem,
            this.удалитьСтатистикуToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(217, 114);
            // 
            // сформироватьВсеToolStripMenuItem
            // 
            this.сформироватьВсеToolStripMenuItem.Image = global::aConverter.Properties.Resources.bullet_go;
            this.сформироватьВсеToolStripMenuItem.Name = "сформироватьВсеToolStripMenuItem";
            this.сформироватьВсеToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.сформироватьВсеToolStripMenuItem.Text = "Сформировать все";
            this.сформироватьВсеToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // сформироватьToolStripMenuItem
            // 
            this.сформироватьToolStripMenuItem.Image = global::aConverter.Properties.Resources.application_go;
            this.сформироватьToolStripMenuItem.Name = "сформироватьToolStripMenuItem";
            this.сформироватьToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.сформироватьToolStripMenuItem.Text = "Сформировать";
            this.сформироватьToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Image = global::aConverter.Properties.Resources.application_edit;
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать статистику";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // добавитьСтатистикуToolStripMenuItem
            // 
            this.добавитьСтатистикуToolStripMenuItem.Image = global::aConverter.Properties.Resources.add;
            this.добавитьСтатистикуToolStripMenuItem.Name = "добавитьСтатистикуToolStripMenuItem";
            this.добавитьСтатистикуToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.добавитьСтатистикуToolStripMenuItem.Text = "Добавить статистику";
            this.добавитьСтатистикуToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // удалитьСтатистикуToolStripMenuItem
            // 
            this.удалитьСтатистикуToolStripMenuItem.Image = global::aConverter.Properties.Resources.delete;
            this.удалитьСтатистикуToolStripMenuItem.Name = "удалитьСтатистикуToolStripMenuItem";
            this.удалитьСтатистикуToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.удалитьСтатистикуToolStripMenuItem.Text = "Удалить статистику";
            this.удалитьСтатистикуToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripButton1,
            this.toolStripButton4,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripComboBoxSelectType1,
            this.toolStripComboBoxSelectType2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(650, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::aConverter.Properties.Resources.bullet_go;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Выполнить все статистики";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::aConverter.Properties.Resources.application_go;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Выполнить статистику";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::aConverter.Properties.Resources.application_edit;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Редактировать статистику";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::aConverter.Properties.Resources.add;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Добавить статистику";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::aConverter.Properties.Resources.delete;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Удалить статистику";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripComboBoxSelectType1
            // 
            this.toolStripComboBoxSelectType1.Items.AddRange(new object[] {
            "Все",
            "По исходным данным (DBF)",
            "По исходным данным (MySQL)",
            "По целевой базе (FDB)"});
            this.toolStripComboBoxSelectType1.Name = "toolStripComboBoxSelectType1";
            this.toolStripComboBoxSelectType1.Size = new System.Drawing.Size(160, 25);
            this.toolStripComboBoxSelectType1.ToolTipText = "По классу статистики";
            this.toolStripComboBoxSelectType1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSelectType1_SelectedIndexChanged);
            // 
            // toolStripComboBoxSelectType2
            // 
            this.toolStripComboBoxSelectType2.Items.AddRange(new object[] {
            "Все",
            "Таблица",
            "Одиночное значение",
            "Без значений"});
            this.toolStripComboBoxSelectType2.Name = "toolStripComboBoxSelectType2";
            this.toolStripComboBoxSelectType2.Size = new System.Drawing.Size(160, 25);
            this.toolStripComboBoxSelectType2.ToolTipText = "По типу значения";
            this.toolStripComboBoxSelectType2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSelectType2_SelectedIndexChanged);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.Location = new System.Drawing.Point(563, 384);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Закрыть";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.statisticNameDataGridViewTextBoxColumn,
            this.statisticTypeNameDataGridViewTextBoxColumn,
            this.Value});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.bindingSource1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(650, 339);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // statisticNameDataGridViewTextBoxColumn
            // 
            this.statisticNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.statisticNameDataGridViewTextBoxColumn.DataPropertyName = "StatisticName";
            this.statisticNameDataGridViewTextBoxColumn.HeaderText = "Статистика";
            this.statisticNameDataGridViewTextBoxColumn.Name = "statisticNameDataGridViewTextBoxColumn";
            this.statisticNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statisticTypeNameDataGridViewTextBoxColumn
            // 
            this.statisticTypeNameDataGridViewTextBoxColumn.DataPropertyName = "StatisticTypeName";
            this.statisticTypeNameDataGridViewTextBoxColumn.HeaderText = "Тип статистики";
            this.statisticTypeNameDataGridViewTextBoxColumn.Name = "statisticTypeNameDataGridViewTextBoxColumn";
            this.statisticTypeNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.statisticTypeNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Значение";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(aConverterClassLibrary.Statistic);
            // 
            // FormStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 419);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonExit);
            this.Name = "FormStatistics";
            this.Text = "Список статистик";
            this.Load += new System.EventHandler(this.FormStatisticSets_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem сформироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSelectType1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSelectType2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripMenuItem сформироватьВсеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьСтатистикуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьСтатистикуToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn statisticNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statisticTypeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}