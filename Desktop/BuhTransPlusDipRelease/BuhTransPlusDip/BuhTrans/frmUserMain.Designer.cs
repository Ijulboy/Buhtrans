
namespace BuhTrans
{
    partial class frmUserMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserMain));
            this.btnLogout = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIExit = new System.Windows.Forms.ToolStripMenuItem();
            this.транспортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.STMIWaybills = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMMileageReport = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIStatistic4TR = new System.Windows.Forms.ToolStripMenuItem();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMICurrencies = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIRateCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMICars = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIFuelNorm = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMPAboutProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLogout.Location = new System.Drawing.Point(533, 453);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(201, 64);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Сменить пользователя";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.транспортToolStripMenuItem,
            this.справочникиToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(756, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIExit});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // TSMIExit
            // 
            this.TSMIExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMIExit.Image = ((System.Drawing.Image)(resources.GetObject("TSMIExit.Image")));
            this.TSMIExit.Name = "TSMIExit";
            this.TSMIExit.Size = new System.Drawing.Size(136, 26);
            this.TSMIExit.Text = "Выход";
            this.TSMIExit.Click += new System.EventHandler(this.TSMIExit_Click);
            // 
            // транспортToolStripMenuItem
            // 
            this.транспортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.журналыToolStripMenuItem,
            this.отчетыToolStripMenuItem});
            this.транспортToolStripMenuItem.Name = "транспортToolStripMenuItem";
            this.транспортToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.транспортToolStripMenuItem.Text = "Транспорт";
            // 
            // журналыToolStripMenuItem
            // 
            this.журналыToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.журналыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.STMIWaybills});
            this.журналыToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("журналыToolStripMenuItem.Image")));
            this.журналыToolStripMenuItem.Name = "журналыToolStripMenuItem";
            this.журналыToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.журналыToolStripMenuItem.Text = "Журналы";
            // 
            // STMIWaybills
            // 
            this.STMIWaybills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.STMIWaybills.Image = ((System.Drawing.Image)(resources.GetObject("STMIWaybills.Image")));
            this.STMIWaybills.Name = "STMIWaybills";
            this.STMIWaybills.Size = new System.Drawing.Size(196, 26);
            this.STMIWaybills.Text = "Путевые листы";
            this.STMIWaybills.Click += new System.EventHandler(this.STMIWaybills_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMMileageReport,
            this.TSMIStatistic4TR});
            this.отчетыToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("отчетыToolStripMenuItem.Image")));
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // TSMMileageReport
            // 
            this.TSMMileageReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMMileageReport.Image = ((System.Drawing.Image)(resources.GetObject("TSMMileageReport.Image")));
            this.TSMMileageReport.Name = "TSMMileageReport";
            this.TSMMileageReport.Size = new System.Drawing.Size(226, 26);
            this.TSMMileageReport.Text = "Отчет по пробегам";
            this.TSMMileageReport.Click += new System.EventHandler(this.TSMMileageReport_Click);
            // 
            // TSMIStatistic4TR
            // 
            this.TSMIStatistic4TR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMIStatistic4TR.Image = ((System.Drawing.Image)(resources.GetObject("TSMIStatistic4TR.Image")));
            this.TSMIStatistic4TR.Name = "TSMIStatistic4TR";
            this.TSMIStatistic4TR.Size = new System.Drawing.Size(226, 26);
            this.TSMIStatistic4TR.Text = "4-ТР";
            this.TSMIStatistic4TR.Click += new System.EventHandler(this.TSMIStatistic4TR_Click);
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMICurrencies,
            this.TSMIRateCurrency,
            this.TSMICars,
            this.TSMIEmployee,
            this.TSMIFuelNorm});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // TSMICurrencies
            // 
            this.TSMICurrencies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMICurrencies.Image = ((System.Drawing.Image)(resources.GetObject("TSMICurrencies.Image")));
            this.TSMICurrencies.Name = "TSMICurrencies";
            this.TSMICurrencies.Size = new System.Drawing.Size(236, 26);
            this.TSMICurrencies.Text = "Валюты";
            this.TSMICurrencies.Click += new System.EventHandler(this.TSMICurrencies_Click);
            // 
            // TSMIRateCurrency
            // 
            this.TSMIRateCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMIRateCurrency.Image = ((System.Drawing.Image)(resources.GetObject("TSMIRateCurrency.Image")));
            this.TSMIRateCurrency.Name = "TSMIRateCurrency";
            this.TSMIRateCurrency.Size = new System.Drawing.Size(236, 26);
            this.TSMIRateCurrency.Text = "Курсы валют";
            this.TSMIRateCurrency.Click += new System.EventHandler(this.TSMIRateCurrency_Click);
            // 
            // TSMICars
            // 
            this.TSMICars.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMICars.Image = ((System.Drawing.Image)(resources.GetObject("TSMICars.Image")));
            this.TSMICars.Name = "TSMICars";
            this.TSMICars.Size = new System.Drawing.Size(236, 26);
            this.TSMICars.Text = "Автомобили";
            this.TSMICars.Click += new System.EventHandler(this.TSMICars_Click);
            // 
            // TSMIEmployee
            // 
            this.TSMIEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMIEmployee.Image = ((System.Drawing.Image)(resources.GetObject("TSMIEmployee.Image")));
            this.TSMIEmployee.Name = "TSMIEmployee";
            this.TSMIEmployee.Size = new System.Drawing.Size(236, 26);
            this.TSMIEmployee.Text = "Сотрудники";
            this.TSMIEmployee.Click += new System.EventHandler(this.TSMIEmployee_Click);
            // 
            // TSMIFuelNorm
            // 
            this.TSMIFuelNorm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMIFuelNorm.Image = ((System.Drawing.Image)(resources.GetObject("TSMIFuelNorm.Image")));
            this.TSMIFuelNorm.Name = "TSMIFuelNorm";
            this.TSMIFuelNorm.Size = new System.Drawing.Size(236, 26);
            this.TSMIFuelNorm.Text = "Нормы расхода ГСМ";
            this.TSMIFuelNorm.Click += new System.EventHandler(this.TSMIFuelNorm_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMPAboutProgram});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // TSMPAboutProgram
            // 
            this.TSMPAboutProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.TSMPAboutProgram.Image = ((System.Drawing.Image)(resources.GetObject("TSMPAboutProgram.Image")));
            this.TSMPAboutProgram.Name = "TSMPAboutProgram";
            this.TSMPAboutProgram.Size = new System.Drawing.Size(187, 26);
            this.TSMPAboutProgram.Text = "О программе";
            this.TSMPAboutProgram.Click += new System.EventHandler(this.TSMPAboutProgram_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(651, 281);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(8, 7);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmUserMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(230)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(756, 529);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmUserMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "БухТранс+";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMIExit;
        private System.Windows.Forms.ToolStripMenuItem транспортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMICurrencies;
        private System.Windows.Forms.ToolStripMenuItem TSMIRateCurrency;
        private System.Windows.Forms.ToolStripMenuItem TSMICars;
        private System.Windows.Forms.ToolStripMenuItem TSMIEmployee;
        private System.Windows.Forms.ToolStripMenuItem STMIWaybills;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMPAboutProgram;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMIFuelNorm;
        private System.Windows.Forms.ToolStripMenuItem TSMMileageReport;
        private System.Windows.Forms.ToolStripMenuItem TSMIStatistic4TR;
    }
}