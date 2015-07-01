namespace aConverter.Forms
{
    partial class FormConvert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConvert));
            this.labelConvertPath = new System.Windows.Forms.Label();
            this.textBoxConvertPath = new System.Windows.Forms.TextBox();
            this.textBoxSourceDBFFilePath = new System.Windows.Forms.TextBox();
            this.labelSourceDBFFilePath = new System.Windows.Forms.Label();
            this.dataGridViewConvertCase = new System.Windows.Forms.DataGridView();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorConvertCase = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBarSteps = new System.Windows.Forms.ProgressBar();
            this.labelSteps = new System.Windows.Forms.Label();
            this.labelProcess = new System.Windows.Forms.Label();
            this.progressBarProcess = new System.Windows.Forms.ProgressBar();
            this.bindingSourceConvertCase = new System.Windows.Forms.BindingSource(this.components);
            this.isCheckedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.convertCaseNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConvertCase)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorConvertCase)).BeginInit();
            this.bindingNavigatorConvertCase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceConvertCase)).BeginInit();
            this.SuspendLayout();
            // 
            // labelConvertPath
            // 
            this.labelConvertPath.AutoSize = true;
            this.labelConvertPath.Location = new System.Drawing.Point(12, 9);
            this.labelConvertPath.Name = "labelConvertPath";
            this.labelConvertPath.Size = new System.Drawing.Size(156, 13);
            this.labelConvertPath.TabIndex = 0;
            this.labelConvertPath.Text = "Путь к модулям импорта (dll):";
            // 
            // textBoxConvertPath
            // 
            this.textBoxConvertPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConvertPath.Location = new System.Drawing.Point(12, 25);
            this.textBoxConvertPath.Name = "textBoxConvertPath";
            this.textBoxConvertPath.ReadOnly = true;
            this.textBoxConvertPath.Size = new System.Drawing.Size(787, 20);
            this.textBoxConvertPath.TabIndex = 1;
            // 
            // textBoxSourceDBFFilePath
            // 
            this.textBoxSourceDBFFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSourceDBFFilePath.Location = new System.Drawing.Point(15, 64);
            this.textBoxSourceDBFFilePath.Name = "textBoxSourceDBFFilePath";
            this.textBoxSourceDBFFilePath.ReadOnly = true;
            this.textBoxSourceDBFFilePath.Size = new System.Drawing.Size(784, 20);
            this.textBoxSourceDBFFilePath.TabIndex = 14;
            this.textBoxSourceDBFFilePath.Text = "123";
            // 
            // labelSourceDBFFilePath
            // 
            this.labelSourceDBFFilePath.AutoSize = true;
            this.labelSourceDBFFilePath.Location = new System.Drawing.Point(12, 48);
            this.labelSourceDBFFilePath.Name = "labelSourceDBFFilePath";
            this.labelSourceDBFFilePath.Size = new System.Drawing.Size(207, 13);
            this.labelSourceDBFFilePath.TabIndex = 13;
            this.labelSourceDBFFilePath.Text = "Путь к исходным данным для импорта:";
            // 
            // dataGridViewConvertCase
            // 
            this.dataGridViewConvertCase.AllowUserToAddRows = false;
            this.dataGridViewConvertCase.AllowUserToDeleteRows = false;
            this.dataGridViewConvertCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewConvertCase.AutoGenerateColumns = false;
            this.dataGridViewConvertCase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConvertCase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCheckedDataGridViewCheckBoxColumn,
            this.convertCaseNameDataGridViewTextBoxColumn});
            this.dataGridViewConvertCase.DataSource = this.bindingSourceConvertCase;
            this.dataGridViewConvertCase.Location = new System.Drawing.Point(3, 28);
            this.dataGridViewConvertCase.Name = "dataGridViewConvertCase";
            this.dataGridViewConvertCase.Size = new System.Drawing.Size(784, 324);
            this.dataGridViewConvertCase.TabIndex = 15;
            this.dataGridViewConvertCase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConvertCase_CellDoubleClick);
            this.dataGridViewConvertCase.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewConvertCase_RowPrePaint);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.Location = new System.Drawing.Point(724, 539);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 16;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonConvert
            // 
            this.buttonConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConvert.Location = new System.Drawing.Point(615, 539);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(103, 23);
            this.buttonConvert.TabIndex = 17;
            this.buttonConvert.Text = "Конвертация";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dataGridViewConvertCase);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.bindingNavigatorConvertCase);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(787, 355);
            this.toolStripContainer1.Location = new System.Drawing.Point(12, 90);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(787, 355);
            this.toolStripContainer1.TabIndex = 18;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(212, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // bindingNavigatorConvertCase
            // 
            this.bindingNavigatorConvertCase.AddNewItem = null;
            this.bindingNavigatorConvertCase.BindingSource = this.bindingSourceConvertCase;
            this.bindingNavigatorConvertCase.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigatorConvertCase.DeleteItem = null;
            this.bindingNavigatorConvertCase.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigatorConvertCase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindingNavigatorConvertCase.Location = new System.Drawing.Point(3, 0);
            this.bindingNavigatorConvertCase.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigatorConvertCase.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigatorConvertCase.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigatorConvertCase.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigatorConvertCase.Name = "bindingNavigatorConvertCase";
            this.bindingNavigatorConvertCase.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigatorConvertCase.Size = new System.Drawing.Size(209, 25);
            this.bindingNavigatorConvertCase.TabIndex = 0;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // progressBarSteps
            // 
            this.progressBarSteps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSteps.Location = new System.Drawing.Point(15, 464);
            this.progressBarSteps.Name = "progressBarSteps";
            this.progressBarSteps.Size = new System.Drawing.Size(784, 14);
            this.progressBarSteps.TabIndex = 19;
            // 
            // labelSteps
            // 
            this.labelSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSteps.AutoSize = true;
            this.labelSteps.Location = new System.Drawing.Point(12, 448);
            this.labelSteps.Name = "labelSteps";
            this.labelSteps.Size = new System.Drawing.Size(63, 13);
            this.labelSteps.TabIndex = 20;
            this.labelSteps.Text = "Шаг 0 из 0:";
            // 
            // labelProcess
            // 
            this.labelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(12, 481);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(104, 13);
            this.labelProcess.TabIndex = 21;
            this.labelProcess.Text = "Обработано 0 из 0:";
            // 
            // progressBarProcess
            // 
            this.progressBarProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarProcess.Location = new System.Drawing.Point(15, 497);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(784, 14);
            this.progressBarProcess.TabIndex = 19;
            // 
            // bindingSourceConvertCase
            // 
            this.bindingSourceConvertCase.DataSource = typeof(aConverterClassLibrary.ConvertCase);
            // 
            // isCheckedDataGridViewCheckBoxColumn
            // 
            this.isCheckedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isCheckedDataGridViewCheckBoxColumn.DataPropertyName = "IsChecked";
            this.isCheckedDataGridViewCheckBoxColumn.HeaderText = "";
            this.isCheckedDataGridViewCheckBoxColumn.Name = "isCheckedDataGridViewCheckBoxColumn";
            this.isCheckedDataGridViewCheckBoxColumn.Width = 5;
            // 
            // convertCaseNameDataGridViewTextBoxColumn
            // 
            this.convertCaseNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.convertCaseNameDataGridViewTextBoxColumn.DataPropertyName = "ConvertCaseName";
            this.convertCaseNameDataGridViewTextBoxColumn.HeaderText = "Наименование шага конвертации";
            this.convertCaseNameDataGridViewTextBoxColumn.Name = "convertCaseNameDataGridViewTextBoxColumn";
            this.convertCaseNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormConvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 574);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.labelSteps);
            this.Controls.Add(this.progressBarProcess);
            this.Controls.Add(this.progressBarSteps);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textBoxSourceDBFFilePath);
            this.Controls.Add(this.labelSourceDBFFilePath);
            this.Controls.Add(this.textBoxConvertPath);
            this.Controls.Add(this.labelConvertPath);
            this.Name = "FormConvert";
            this.Text = "Конвертация";
            this.Load += new System.EventHandler(this.FormConvert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConvertCase)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorConvertCase)).EndInit();
            this.bindingNavigatorConvertCase.ResumeLayout(false);
            this.bindingNavigatorConvertCase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceConvertCase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConvertPath;
        private System.Windows.Forms.TextBox textBoxConvertPath;
        private System.Windows.Forms.TextBox textBoxSourceDBFFilePath;
        private System.Windows.Forms.Label labelSourceDBFFilePath;
        private System.Windows.Forms.DataGridView dataGridViewConvertCase;
        private System.Windows.Forms.BindingSource bindingSourceConvertCase;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.BindingNavigator bindingNavigatorConvertCase;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ProgressBar progressBarSteps;
        private System.Windows.Forms.Label labelSteps;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn convertCaseNameDataGridViewTextBoxColumn;
    }
}