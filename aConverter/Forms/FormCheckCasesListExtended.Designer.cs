namespace aConverter.Forms
{
    partial class FormCheckCasesListExtended
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCheckCasesListExtended));
            this.treeViewMain = new System.Windows.Forms.TreeView();
            this.imageListMain = new System.Windows.Forms.ImageList(this.components);
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonGo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFix = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSetGo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.тестироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пометитьКТестированиюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.исключитьИзТестированияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.исправитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain.SuspendLayout();
            this.contextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewMain
            // 
            this.treeViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewMain.ContextMenuStrip = this.contextMenuStripMain;
            this.treeViewMain.ImageIndex = 0;
            this.treeViewMain.ImageList = this.imageListMain;
            this.treeViewMain.Location = new System.Drawing.Point(0, 28);
            this.treeViewMain.Name = "treeViewMain";
            this.treeViewMain.SelectedImageIndex = 0;
            this.treeViewMain.Size = new System.Drawing.Size(742, 294);
            this.treeViewMain.TabIndex = 0;
            this.treeViewMain.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMain_BeforeCollapse);
            this.treeViewMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMain_BeforeExpand);
            this.treeViewMain.DoubleClick += new System.EventHandler(this.treeViewMain_DoubleClick);
            this.treeViewMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewMain_MouseDown);
            // 
            // imageListMain
            // 
            this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
            this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMain.Images.SetKeyName(0, "accept.png");
            this.imageListMain.Images.SetKeyName(1, "cancel.png");
            this.imageListMain.Images.SetKeyName(2, "control_pause.png");
            this.imageListMain.Images.SetKeyName(3, "control_play.png");
            this.imageListMain.Images.SetKeyName(4, "delete.png");
            this.imageListMain.Images.SetKeyName(5, "note.png");
            this.imageListMain.Images.SetKeyName(6, "bell_delete.png");
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(633, 328);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(97, 26);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonGo,
            this.toolStripButtonFix,
            this.toolStripButtonSetGo,
            this.toolStripButtonPause});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(742, 25);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonGo
            // 
            this.toolStripButtonGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGo.Image = global::aConverter.Properties.Resources.bullet_go;
            this.toolStripButtonGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGo.Name = "toolStripButtonGo";
            this.toolStripButtonGo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonGo.Text = "Запустить тестирование";
            this.toolStripButtonGo.Click += new System.EventHandler(this.toolStripButtonGo_Click);
            // 
            // toolStripButtonFix
            // 
            this.toolStripButtonFix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFix.Image = global::aConverter.Properties.Resources.bell_go;
            this.toolStripButtonFix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFix.Name = "toolStripButtonFix";
            this.toolStripButtonFix.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFix.Text = "Исправить ошибки";
            this.toolStripButtonFix.Click += new System.EventHandler(this.toolStripButtonFix_Click);
            // 
            // toolStripButtonSetGo
            // 
            this.toolStripButtonSetGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSetGo.Image = global::aConverter.Properties.Resources.control_play;
            this.toolStripButtonSetGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSetGo.Name = "toolStripButtonSetGo";
            this.toolStripButtonSetGo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSetGo.Text = "Пометить к тестированию";
            this.toolStripButtonSetGo.Click += new System.EventHandler(this.toolStripButtonSetGo_Click);
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPause.Image = global::aConverter.Properties.Resources.control_pause;
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPause.Text = "Пауза";
            this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.тестироватьToolStripMenuItem,
            this.исправитьToolStripMenuItem,
            this.пометитьКТестированиюToolStripMenuItem,
            this.исключитьИзТестированияToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(231, 114);
            // 
            // тестироватьToolStripMenuItem
            // 
            this.тестироватьToolStripMenuItem.Image = global::aConverter.Properties.Resources.bullet_go;
            this.тестироватьToolStripMenuItem.Name = "тестироватьToolStripMenuItem";
            this.тестироватьToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.тестироватьToolStripMenuItem.Text = "Тестировать";
            this.тестироватьToolStripMenuItem.Click += new System.EventHandler(this.тестироватьToolStripMenuItem_Click);
            // 
            // пометитьКТестированиюToolStripMenuItem
            // 
            this.пометитьКТестированиюToolStripMenuItem.Image = global::aConverter.Properties.Resources.control_play;
            this.пометитьКТестированиюToolStripMenuItem.Name = "пометитьКТестированиюToolStripMenuItem";
            this.пометитьКТестированиюToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.пометитьКТестированиюToolStripMenuItem.Text = "Пометить к тестированию";
            this.пометитьКТестированиюToolStripMenuItem.Click += new System.EventHandler(this.пометитьКТестированиюToolStripMenuItem_Click);
            // 
            // исключитьИзТестированияToolStripMenuItem
            // 
            this.исключитьИзТестированияToolStripMenuItem.Image = global::aConverter.Properties.Resources.control_pause;
            this.исключитьИзТестированияToolStripMenuItem.Name = "исключитьИзТестированияToolStripMenuItem";
            this.исключитьИзТестированияToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.исключитьИзТестированияToolStripMenuItem.Text = "Исключить из тестирования";
            this.исключитьИзТестированияToolStripMenuItem.Click += new System.EventHandler(this.исключитьИзТестированияToolStripMenuItem_Click);
            // 
            // исправитьToolStripMenuItem
            // 
            this.исправитьToolStripMenuItem.Image = global::aConverter.Properties.Resources.bell_go;
            this.исправитьToolStripMenuItem.Name = "исправитьToolStripMenuItem";
            this.исправитьToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.исправитьToolStripMenuItem.Text = "Исправить";
            this.исправитьToolStripMenuItem.Click += new System.EventHandler(this.исправитьToolStripMenuItem_Click);
            // 
            // FormCheckCasesListExtended
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 366);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.treeViewMain);
            this.Name = "FormCheckCasesListExtended";
            this.Text = "Проверка целостности данных";
            this.Load += new System.EventHandler(this.FormCheckCasesListExtended_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.contextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewMain;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ImageList imageListMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonGo;
        private System.Windows.Forms.ToolStripButton toolStripButtonSetGo;
        private System.Windows.Forms.ToolStripButton toolStripButtonPause;
        private System.Windows.Forms.ToolStripButton toolStripButtonFix;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem тестироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem исправитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пометитьКТестированиюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem исключитьИзТестированияToolStripMenuItem;
    }
}