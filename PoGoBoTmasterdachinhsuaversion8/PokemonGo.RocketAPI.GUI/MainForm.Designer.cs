namespace PokemonGo.RocketAPI.GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEvolve = new System.Windows.Forms.CheckBox();
            this.cbTransfer = new System.Windows.Forms.CheckBox();
            this.btnUseIncense = new System.Windows.Forms.Button();
            this.btnLuckyEgg = new System.Windows.Forms.Button();
            this.btnStopFarming = new System.Windows.Forms.Button();
            this.btnStartFarming = new System.Windows.Forms.Button();
            this.cbKeepPkToEvolve = new System.Windows.Forms.CheckBox();
            this.btnRecycleItems = new System.Windows.Forms.Button();
            this.btnTransferDuplicates = new System.Windows.Forms.Button();
            this.btnEvolvePokemons = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbPkmnCaptured = new System.Windows.Forms.Label();
            this.lbPkmnHr = new System.Windows.Forms.Label();
            this.lbExpHour = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExtraPlayerInformation = new System.Windows.Forms.Button();
            this.lbIncense = new System.Windows.Forms.Label();
            this.lbLuckyEggs = new System.Windows.Forms.Label();
            this.lbItemsInventory = new System.Windows.Forms.Label();
            this.lbPokemonsInventory = new System.Windows.Forms.Label();
            this.lbExperience = new System.Windows.Forms.Label();
            this.lbLevel = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.dGrid = new System.Windows.Forms.DataGridView();
            this.loggingBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.boxPokestopCount = new System.Windows.Forms.TextBox();
            this.boxPokestopInit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.boxPokestopName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.boxPokemonName = new System.Windows.Forms.TextBox();
            this.boxPokemonCaughtProb = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnMyPokemon = new System.Windows.Forms.Button();
            this.lbcopyright = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnadvoptions = new System.Windows.Forms.Button();
            this.MainMap = new GMap.NET.WindowsForms.GMapControl();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbEvolve);
            this.groupBox1.Controls.Add(this.cbTransfer);
            this.groupBox1.Controls.Add(this.btnUseIncense);
            this.groupBox1.Controls.Add(this.btnLuckyEgg);
            this.groupBox1.Controls.Add(this.btnStopFarming);
            this.groupBox1.Controls.Add(this.btnStartFarming);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(302, 325);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Farming Control";
            // 
            // cbEvolve
            // 
            this.cbEvolve.AutoSize = true;
            this.cbEvolve.Enabled = false;
            this.cbEvolve.Location = new System.Drawing.Point(16, 146);
            this.cbEvolve.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbEvolve.Name = "cbEvolve";
            this.cbEvolve.Size = new System.Drawing.Size(160, 24);
            this.cbEvolve.TabIndex = 10;
            this.cbEvolve.Text = "Evolve Pokemons";
            this.cbEvolve.UseVisualStyleBackColor = true;
            // 
            // cbTransfer
            // 
            this.cbTransfer.AutoSize = true;
            this.cbTransfer.Enabled = false;
            this.cbTransfer.Location = new System.Drawing.Point(16, 118);
            this.cbTransfer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbTransfer.Name = "cbTransfer";
            this.cbTransfer.Size = new System.Drawing.Size(173, 24);
            this.cbTransfer.TabIndex = 9;
            this.cbTransfer.Text = "Transfer Pokemons";
            this.cbTransfer.UseVisualStyleBackColor = true;
            // 
            // btnUseIncense
            // 
            this.btnUseIncense.Enabled = false;
            this.btnUseIncense.Location = new System.Drawing.Point(9, 275);
            this.btnUseIncense.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUseIncense.Name = "btnUseIncense";
            this.btnUseIncense.Size = new System.Drawing.Size(284, 35);
            this.btnUseIncense.TabIndex = 8;
            this.btnUseIncense.Text = "Use Incense";
            this.btnUseIncense.UseVisualStyleBackColor = true;
            this.btnUseIncense.Click += new System.EventHandler(this.btnUseIncense_Click);
            // 
            // btnLuckyEgg
            // 
            this.btnLuckyEgg.Enabled = false;
            this.btnLuckyEgg.Location = new System.Drawing.Point(9, 234);
            this.btnLuckyEgg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLuckyEgg.Name = "btnLuckyEgg";
            this.btnLuckyEgg.Size = new System.Drawing.Size(284, 35);
            this.btnLuckyEgg.TabIndex = 7;
            this.btnLuckyEgg.Text = "Use Lucky egg";
            this.btnLuckyEgg.UseVisualStyleBackColor = true;
            this.btnLuckyEgg.Click += new System.EventHandler(this.btnLuckyEgg_Click);
            // 
            // btnStopFarming
            // 
            this.btnStopFarming.Enabled = false;
            this.btnStopFarming.Location = new System.Drawing.Point(9, 189);
            this.btnStopFarming.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStopFarming.Name = "btnStopFarming";
            this.btnStopFarming.Size = new System.Drawing.Size(284, 35);
            this.btnStopFarming.TabIndex = 1;
            this.btnStopFarming.Text = "Stop Farming";
            this.btnStopFarming.UseVisualStyleBackColor = true;
            this.btnStopFarming.Click += new System.EventHandler(this.btnStopFarming_Click);
            // 
            // btnStartFarming
            // 
            this.btnStartFarming.Enabled = false;
            this.btnStartFarming.Location = new System.Drawing.Point(9, 34);
            this.btnStartFarming.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartFarming.Name = "btnStartFarming";
            this.btnStartFarming.Size = new System.Drawing.Size(284, 75);
            this.btnStartFarming.TabIndex = 0;
            this.btnStartFarming.Text = "Start Farming";
            this.btnStartFarming.UseVisualStyleBackColor = true;
            this.btnStartFarming.Click += new System.EventHandler(this.btnStartFarming_Click);
            // 
            // cbKeepPkToEvolve
            // 
            this.cbKeepPkToEvolve.AutoSize = true;
            this.cbKeepPkToEvolve.Checked = true;
            this.cbKeepPkToEvolve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeepPkToEvolve.Enabled = false;
            this.cbKeepPkToEvolve.Location = new System.Drawing.Point(16, 118);
            this.cbKeepPkToEvolve.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbKeepPkToEvolve.Name = "cbKeepPkToEvolve";
            this.cbKeepPkToEvolve.Size = new System.Drawing.Size(261, 24);
            this.cbKeepPkToEvolve.TabIndex = 5;
            this.cbKeepPkToEvolve.Text = "Keep Pokemons that can evolve";
            this.cbKeepPkToEvolve.UseVisualStyleBackColor = true;
            // 
            // btnRecycleItems
            // 
            this.btnRecycleItems.Enabled = false;
            this.btnRecycleItems.Location = new System.Drawing.Point(9, 154);
            this.btnRecycleItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRecycleItems.Name = "btnRecycleItems";
            this.btnRecycleItems.Size = new System.Drawing.Size(284, 35);
            this.btnRecycleItems.TabIndex = 4;
            this.btnRecycleItems.Text = "Recycle Items";
            this.btnRecycleItems.UseVisualStyleBackColor = true;
            this.btnRecycleItems.Click += new System.EventHandler(this.btnRecycleItems_Click);
            // 
            // btnTransferDuplicates
            // 
            this.btnTransferDuplicates.Enabled = false;
            this.btnTransferDuplicates.Location = new System.Drawing.Point(9, 74);
            this.btnTransferDuplicates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTransferDuplicates.Name = "btnTransferDuplicates";
            this.btnTransferDuplicates.Size = new System.Drawing.Size(284, 35);
            this.btnTransferDuplicates.TabIndex = 3;
            this.btnTransferDuplicates.Text = "Transfer Duplicate Pokemons";
            this.btnTransferDuplicates.UseVisualStyleBackColor = true;
            this.btnTransferDuplicates.Click += new System.EventHandler(this.btnTransferDuplicates_Click);
            // 
            // btnEvolvePokemons
            // 
            this.btnEvolvePokemons.Enabled = false;
            this.btnEvolvePokemons.Location = new System.Drawing.Point(9, 29);
            this.btnEvolvePokemons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEvolvePokemons.Name = "btnEvolvePokemons";
            this.btnEvolvePokemons.Size = new System.Drawing.Size(284, 35);
            this.btnEvolvePokemons.TabIndex = 2;
            this.btnEvolvePokemons.Text = "Evolve Pokemons w/Candy";
            this.btnEvolvePokemons.UseVisualStyleBackColor = true;
            this.btnEvolvePokemons.Click += new System.EventHandler(this.btnEvolvePokemons_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbPkmnCaptured);
            this.groupBox3.Controls.Add(this.lbPkmnHr);
            this.groupBox3.Controls.Add(this.lbExpHour);
            this.groupBox3.Location = new System.Drawing.Point(18, 575);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(302, 114);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stats";
            // 
            // lbPkmnCaptured
            // 
            this.lbPkmnCaptured.AutoSize = true;
            this.lbPkmnCaptured.Location = new System.Drawing.Point(9, 78);
            this.lbPkmnCaptured.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbPkmnCaptured.Name = "lbPkmnCaptured";
            this.lbPkmnCaptured.Size = new System.Drawing.Size(127, 20);
            this.lbPkmnCaptured.TabIndex = 3;
            this.lbPkmnCaptured.Text = "lbPkmnCaptured";
            // 
            // lbPkmnHr
            // 
            this.lbPkmnHr.AutoSize = true;
            this.lbPkmnHr.Location = new System.Drawing.Point(9, 54);
            this.lbPkmnHr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbPkmnHr.Name = "lbPkmnHr";
            this.lbPkmnHr.Size = new System.Drawing.Size(78, 20);
            this.lbPkmnHr.TabIndex = 1;
            this.lbPkmnHr.Text = "lbPkmnHr";
            // 
            // lbExpHour
            // 
            this.lbExpHour.AutoSize = true;
            this.lbExpHour.Location = new System.Drawing.Point(9, 29);
            this.lbExpHour.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbExpHour.Name = "lbExpHour";
            this.lbExpHour.Size = new System.Drawing.Size(83, 20);
            this.lbExpHour.TabIndex = 0;
            this.lbExpHour.Text = "lbExpHour";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExtraPlayerInformation);
            this.groupBox4.Controls.Add(this.lbIncense);
            this.groupBox4.Controls.Add(this.lbLuckyEggs);
            this.groupBox4.Controls.Add(this.lbItemsInventory);
            this.groupBox4.Controls.Add(this.lbPokemonsInventory);
            this.groupBox4.Controls.Add(this.lbExperience);
            this.groupBox4.Controls.Add(this.lbLevel);
            this.groupBox4.Controls.Add(this.lbName);
            this.groupBox4.Location = new System.Drawing.Point(18, 689);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(302, 208);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Information";
            // 
            // btnExtraPlayerInformation
            // 
            this.btnExtraPlayerInformation.Enabled = false;
            this.btnExtraPlayerInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExtraPlayerInformation.Location = new System.Drawing.Point(213, 152);
            this.btnExtraPlayerInformation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExtraPlayerInformation.Name = "btnExtraPlayerInformation";
            this.btnExtraPlayerInformation.Size = new System.Drawing.Size(81, 46);
            this.btnExtraPlayerInformation.TabIndex = 7;
            this.btnExtraPlayerInformation.Text = "Extra";
            this.btnExtraPlayerInformation.UseVisualStyleBackColor = true;
            this.btnExtraPlayerInformation.Click += new System.EventHandler(this.btnExtraPlayerInformation_Click);
            // 
            // lbIncense
            // 
            this.lbIncense.AutoSize = true;
            this.lbIncense.Location = new System.Drawing.Point(9, 178);
            this.lbIncense.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbIncense.Name = "lbIncense";
            this.lbIncense.Size = new System.Drawing.Size(78, 20);
            this.lbIncense.TabIndex = 6;
            this.lbIncense.Text = "lbIncense";
            // 
            // lbLuckyEggs
            // 
            this.lbLuckyEggs.AutoSize = true;
            this.lbLuckyEggs.Location = new System.Drawing.Point(9, 152);
            this.lbLuckyEggs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbLuckyEggs.Name = "lbLuckyEggs";
            this.lbLuckyEggs.Size = new System.Drawing.Size(99, 20);
            this.lbLuckyEggs.TabIndex = 5;
            this.lbLuckyEggs.Text = "lbLuckyEggs";
            // 
            // lbItemsInventory
            // 
            this.lbItemsInventory.AutoSize = true;
            this.lbItemsInventory.Location = new System.Drawing.Point(9, 128);
            this.lbItemsInventory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbItemsInventory.Name = "lbItemsInventory";
            this.lbItemsInventory.Size = new System.Drawing.Size(126, 20);
            this.lbItemsInventory.TabIndex = 4;
            this.lbItemsInventory.Text = "lbItemsInventory";
            // 
            // lbPokemonsInventory
            // 
            this.lbPokemonsInventory.AutoSize = true;
            this.lbPokemonsInventory.Location = new System.Drawing.Point(9, 103);
            this.lbPokemonsInventory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbPokemonsInventory.Name = "lbPokemonsInventory";
            this.lbPokemonsInventory.Size = new System.Drawing.Size(96, 20);
            this.lbPokemonsInventory.TabIndex = 3;
            this.lbPokemonsInventory.Text = "lbPokemons";
            // 
            // lbExperience
            // 
            this.lbExperience.AutoSize = true;
            this.lbExperience.Location = new System.Drawing.Point(9, 78);
            this.lbExperience.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbExperience.Name = "lbExperience";
            this.lbExperience.Size = new System.Drawing.Size(100, 20);
            this.lbExperience.TabIndex = 2;
            this.lbExperience.Text = "lbExperience";
            // 
            // lbLevel
            // 
            this.lbLevel.AutoSize = true;
            this.lbLevel.Location = new System.Drawing.Point(9, 54);
            this.lbLevel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbLevel.Name = "lbLevel";
            this.lbLevel.Size = new System.Drawing.Size(58, 20);
            this.lbLevel.TabIndex = 1;
            this.lbLevel.Text = "lbLevel";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(9, 29);
            this.lbName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(63, 20);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "lbName";
            // 
            // dGrid
            // 
            this.dGrid.AllowUserToAddRows = false;
            this.dGrid.AllowUserToDeleteRows = false;
            this.dGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid.Location = new System.Drawing.Point(328, 391);
            this.dGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dGrid.Name = "dGrid";
            this.dGrid.ReadOnly = true;
            this.dGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGrid.Size = new System.Drawing.Size(620, 400);
            this.dGrid.TabIndex = 0;
            // 
            // loggingBox
            // 
            this.loggingBox.Location = new System.Drawing.Point(328, 798);
            this.loggingBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loggingBox.Multiline = true;
            this.loggingBox.Name = "loggingBox";
            this.loggingBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.loggingBox.Size = new System.Drawing.Size(618, 180);
            this.loggingBox.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.boxPokestopCount);
            this.groupBox2.Controls.Add(this.boxPokestopInit);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.boxPokestopName);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(328, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(320, 112);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Pokestop";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(186, 77);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 20);
            this.label10.TabIndex = 9;
            this.label10.Text = "of";
            // 
            // boxPokestopCount
            // 
            this.boxPokestopCount.Enabled = false;
            this.boxPokestopCount.Location = new System.Drawing.Point(216, 69);
            this.boxPokestopCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxPokestopCount.Name = "boxPokestopCount";
            this.boxPokestopCount.Size = new System.Drawing.Size(92, 26);
            this.boxPokestopCount.TabIndex = 8;
            this.boxPokestopCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // boxPokestopInit
            // 
            this.boxPokestopInit.Enabled = false;
            this.boxPokestopInit.Location = new System.Drawing.Point(86, 69);
            this.boxPokestopInit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxPokestopInit.Name = "boxPokestopInit";
            this.boxPokestopInit.Size = new System.Drawing.Size(92, 26);
            this.boxPokestopInit.TabIndex = 3;
            this.boxPokestopInit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 74);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Count";
            // 
            // boxPokestopName
            // 
            this.boxPokestopName.Enabled = false;
            this.boxPokestopName.Location = new System.Drawing.Point(86, 29);
            this.boxPokestopName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxPokestopName.Name = "boxPokestopName";
            this.boxPokestopName.Size = new System.Drawing.Size(223, 26);
            this.boxPokestopName.TabIndex = 1;
            this.boxPokestopName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Name";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.boxPokemonName);
            this.groupBox5.Controls.Add(this.boxPokemonCaughtProb);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Location = new System.Drawing.Point(657, 18);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(291, 112);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Fighting Pokemon";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 74);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 20);
            this.label11.TabIndex = 19;
            this.label11.Text = "Capture %";
            // 
            // boxPokemonName
            // 
            this.boxPokemonName.Enabled = false;
            this.boxPokemonName.Location = new System.Drawing.Point(100, 29);
            this.boxPokemonName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxPokemonName.Name = "boxPokemonName";
            this.boxPokemonName.Size = new System.Drawing.Size(175, 26);
            this.boxPokemonName.TabIndex = 11;
            this.boxPokemonName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // boxPokemonCaughtProb
            // 
            this.boxPokemonCaughtProb.Enabled = false;
            this.boxPokemonCaughtProb.Location = new System.Drawing.Point(100, 69);
            this.boxPokemonCaughtProb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxPokemonCaughtProb.Name = "boxPokemonCaughtProb";
            this.boxPokemonCaughtProb.Size = new System.Drawing.Size(175, 26);
            this.boxPokemonCaughtProb.TabIndex = 18;
            this.boxPokemonCaughtProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 34);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 20);
            this.label15.TabIndex = 10;
            this.label15.Text = "Name";
            // 
            // btnMyPokemon
            // 
            this.btnMyPokemon.Enabled = false;
            this.btnMyPokemon.Location = new System.Drawing.Point(18, 897);
            this.btnMyPokemon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMyPokemon.Name = "btnMyPokemon";
            this.btnMyPokemon.Size = new System.Drawing.Size(302, 35);
            this.btnMyPokemon.TabIndex = 8;
            this.btnMyPokemon.Text = "My Pokemon";
            this.btnMyPokemon.UseVisualStyleBackColor = true;
            this.btnMyPokemon.Click += new System.EventHandler(this.btnMyPokemon_Click);
            // 
            // lbcopyright
            // 
            this.lbcopyright.AutoSize = true;
            this.lbcopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcopyright.Location = new System.Drawing.Point(780, 1011);
            this.lbcopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbcopyright.Name = "lbcopyright";
            this.lbcopyright.Size = new System.Drawing.Size(185, 13);
            this.lbcopyright.TabIndex = 9;
            this.lbcopyright.Text = "https://github.com/WooAf/PoGoBoT";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnRecycleItems);
            this.groupBox6.Controls.Add(this.btnEvolvePokemons);
            this.groupBox6.Controls.Add(this.cbKeepPkToEvolve);
            this.groupBox6.Controls.Add(this.btnTransferDuplicates);
            this.groupBox6.Location = new System.Drawing.Point(18, 352);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Size = new System.Drawing.Size(302, 214);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Trainer Control";
            // 
            // btnadvoptions
            // 
            this.btnadvoptions.Enabled = false;
            this.btnadvoptions.Location = new System.Drawing.Point(18, 943);
            this.btnadvoptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnadvoptions.Name = "btnadvoptions";
            this.btnadvoptions.Size = new System.Drawing.Size(302, 35);
            this.btnadvoptions.TabIndex = 13;
            this.btnadvoptions.Text = "Advanced Options";
            this.btnadvoptions.UseVisualStyleBackColor = true;
            this.btnadvoptions.Click += new System.EventHandler(this.btnadvoptions_Click);
            // 
            // MainMap
            // 
            this.MainMap.Bearing = 0F;
            this.MainMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainMap.CanDragMap = true;
            this.MainMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.MainMap.Enabled = false;
            this.MainMap.GrayScaleMode = false;
            this.MainMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.MainMap.LevelsKeepInMemmory = 5;
            this.MainMap.Location = new System.Drawing.Point(328, 140);
            this.MainMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainMap.MarkersEnabled = true;
            this.MainMap.MaxZoom = 2;
            this.MainMap.MinZoom = 2;
            this.MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MainMap.Name = "MainMap";
            this.MainMap.NegativeMode = false;
            this.MainMap.PolygonsEnabled = true;
            this.MainMap.RetryLoadTile = 0;
            this.MainMap.RoutesEnabled = true;
            this.MainMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.MainMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.MainMap.ShowTileGridLines = false;
            this.MainMap.Size = new System.Drawing.Size(618, 241);
            this.MainMap.TabIndex = 14;
            this.MainMap.Zoom = 0D;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 992);
            this.Controls.Add(this.MainMap);
            this.Controls.Add(this.btnadvoptions);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.lbcopyright);
            this.Controls.Add(this.btnMyPokemon);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.loggingBox);
            this.Controls.Add(this.dGrid);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(979, 1056);
            this.MinimumSize = new System.Drawing.Size(979, 1022);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PoGoBoT - GUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStopFarming;
        private System.Windows.Forms.Button btnStartFarming;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbPkmnCaptured;
        private System.Windows.Forms.Label lbPkmnHr;
        private System.Windows.Forms.Label lbExpHour;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbLevel;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.DataGridView dGrid;
        private System.Windows.Forms.TextBox loggingBox;
        private System.Windows.Forms.Button btnRecycleItems;
        private System.Windows.Forms.Button btnTransferDuplicates;
        private System.Windows.Forms.Button btnEvolvePokemons;
        private System.Windows.Forms.CheckBox cbKeepPkToEvolve;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox boxPokestopCount;
        private System.Windows.Forms.TextBox boxPokestopInit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox boxPokestopName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox boxPokemonName;
        private System.Windows.Forms.TextBox boxPokemonCaughtProb;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbExperience;
        private System.Windows.Forms.Label lbItemsInventory;
        private System.Windows.Forms.Label lbPokemonsInventory;
        private System.Windows.Forms.Button btnLuckyEgg;
        private System.Windows.Forms.Label lbLuckyEggs;
        private System.Windows.Forms.Label lbIncense;
        private System.Windows.Forms.Button btnUseIncense;
        private System.Windows.Forms.Button btnExtraPlayerInformation;
        private System.Windows.Forms.Button btnMyPokemon;
        private System.Windows.Forms.Label lbcopyright;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnadvoptions;
        private GMap.NET.WindowsForms.GMapControl MainMap;
        private System.Windows.Forms.CheckBox cbEvolve;
        private System.Windows.Forms.CheckBox cbTransfer;
    }
}

