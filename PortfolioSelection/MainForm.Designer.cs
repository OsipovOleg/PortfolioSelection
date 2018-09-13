namespace PortfolioSelection
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.ФайлItem = new System.Windows.Forms.ToolStripMenuItem();
            this.получитьДанныеItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализItem = new System.Windows.Forms.ToolStripMenuItem();
            this.марковицItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шарпItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainCont = new System.Windows.Forms.SplitContainer();
            this.GridIncome = new System.Windows.Forms.DataGridView();
            this.GridX = new System.Windows.Forms.DataGridView();
            this.ИмяКомпании = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Процент = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DpText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mpText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainCont)).BeginInit();
            this.MainCont.Panel1.SuspendLayout();
            this.MainCont.Panel2.SuspendLayout();
            this.MainCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridIncome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridX)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ФайлItem,
            this.анализItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(894, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // ФайлItem
            // 
            this.ФайлItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.получитьДанныеItem});
            this.ФайлItem.Name = "ФайлItem";
            this.ФайлItem.Size = new System.Drawing.Size(48, 20);
            this.ФайлItem.Text = "Файл";
            // 
            // получитьДанныеItem
            // 
            this.получитьДанныеItem.Name = "получитьДанныеItem";
            this.получитьДанныеItem.Size = new System.Drawing.Size(172, 22);
            this.получитьДанныеItem.Text = "Получить данные";
            this.получитьДанныеItem.Click += new System.EventHandler(this.получитьДанныеItem_Click);
            // 
            // анализItem
            // 
            this.анализItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.марковицItem,
            this.шарпItem});
            this.анализItem.Name = "анализItem";
            this.анализItem.Size = new System.Drawing.Size(59, 20);
            this.анализItem.Text = "Анализ";
            // 
            // марковицItem
            // 
            this.марковицItem.Name = "марковицItem";
            this.марковицItem.Size = new System.Drawing.Size(152, 22);
            this.марковицItem.Text = "Марковиц";
            this.марковицItem.Click += new System.EventHandler(this.марковицItem_Click);
            // 
            // шарпItem
            // 
            this.шарпItem.Name = "шарпItem";
            this.шарпItem.Size = new System.Drawing.Size(152, 22);
            this.шарпItem.Text = "Шарп";
            this.шарпItem.Click += new System.EventHandler(this.шарпItem_Click);
            // 
            // MainCont
            // 
            this.MainCont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainCont.Location = new System.Drawing.Point(0, 24);
            this.MainCont.Name = "MainCont";
            // 
            // MainCont.Panel1
            // 
            this.MainCont.Panel1.Controls.Add(this.GridIncome);
            // 
            // MainCont.Panel2
            // 
            this.MainCont.Panel2.Controls.Add(this.GridX);
            this.MainCont.Panel2.Controls.Add(this.DpText);
            this.MainCont.Panel2.Controls.Add(this.label2);
            this.MainCont.Panel2.Controls.Add(this.mpText);
            this.MainCont.Panel2.Controls.Add(this.label1);
            this.MainCont.Size = new System.Drawing.Size(894, 387);
            this.MainCont.SplitterDistance = 458;
            this.MainCont.TabIndex = 2;
            // 
            // GridIncome
            // 
            this.GridIncome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridIncome.Location = new System.Drawing.Point(0, 0);
            this.GridIncome.Name = "GridIncome";
            this.GridIncome.ReadOnly = true;
            this.GridIncome.Size = new System.Drawing.Size(458, 387);
            this.GridIncome.TabIndex = 2;
            // 
            // GridX
            // 
            this.GridX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridX.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridX.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ИмяКомпании,
            this.Процент});
            this.GridX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GridX.Location = new System.Drawing.Point(0, 192);
            this.GridX.Name = "GridX";
            this.GridX.ReadOnly = true;
            this.GridX.Size = new System.Drawing.Size(432, 195);
            this.GridX.TabIndex = 4;
            // 
            // ИмяКомпании
            // 
            this.ИмяКомпании.HeaderText = "Компания";
            this.ИмяКомпании.Name = "ИмяКомпании";
            this.ИмяКомпании.ReadOnly = true;
            // 
            // Процент
            // 
            this.Процент.HeaderText = "Содержание в портфеле";
            this.Процент.Name = "Процент";
            this.Процент.ReadOnly = true;
            // 
            // DpText
            // 
            this.DpText.Location = new System.Drawing.Point(214, 74);
            this.DpText.Name = "DpText";
            this.DpText.ReadOnly = true;
            this.DpText.Size = new System.Drawing.Size(100, 20);
            this.DpText.TabIndex = 3;
            this.DpText.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дисперсия портфеля, %";
            // 
            // mpText
            // 
            this.mpText.Location = new System.Drawing.Point(214, 23);
            this.mpText.Name = "mpText";
            this.mpText.Size = new System.Drawing.Size(100, 20);
            this.mpText.TabIndex = 1;
            this.mpText.Text = "0,39797";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Нижняя граница для доходности %\r\n";
            // 
            // openFile
            // 
            this.openFile.FileName = "Price.csv";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 411);
            this.Controls.Add(this.MainCont);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Portfolio Selection";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainCont.Panel1.ResumeLayout(false);
            this.MainCont.Panel2.ResumeLayout(false);
            this.MainCont.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainCont)).EndInit();
            this.MainCont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridIncome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem ФайлItem;
        private System.Windows.Forms.ToolStripMenuItem получитьДанныеItem;
        private System.Windows.Forms.SplitContainer MainCont;
        private System.Windows.Forms.DataGridView GridIncome;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.ToolStripMenuItem анализItem;
        private System.Windows.Forms.ToolStripMenuItem марковицItem;
        private System.Windows.Forms.ToolStripMenuItem шарпItem;
        private System.Windows.Forms.TextBox mpText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GridX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ИмяКомпании;
        private System.Windows.Forms.DataGridViewTextBoxColumn Процент;
        private System.Windows.Forms.TextBox DpText;
        private System.Windows.Forms.Label label2;
    }
}

