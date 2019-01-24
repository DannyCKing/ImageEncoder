namespace ImageEncoder
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chooseImageFileButton = new System.Windows.Forms.Button();
            this.inputFileTextBox = new System.Windows.Forms.TextBox();
            this.convertImageToTextButton = new System.Windows.Forms.Button();
            this.outputFileTextBox = new System.Windows.Forms.TextBox();
            this.chooseOutputTextFileButton = new System.Windows.Forms.Button();
            this.encryptedCheckBox = new System.Windows.Forms.CheckBox();
            this.chooseOutputImageFilePathButton = new System.Windows.Forms.Button();
            this.outputImageFilePathTextBox = new System.Windows.Forms.TextBox();
            this.inputTextFilePathTextBox = new System.Windows.Forms.TextBox();
            this.chooseInputTextFilePathButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.convertFromTextToImageButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.compressionRateTrackBar = new System.Windows.Forms.TrackBar();
            this.compressionLevelLabel = new System.Windows.Forms.Label();
            this.compressCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ignoreAlphaChannelCheckbox = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressionRateTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Image File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output File";
            // 
            // chooseImageFileButton
            // 
            this.chooseImageFileButton.Location = new System.Drawing.Point(115, 22);
            this.chooseImageFileButton.Name = "chooseImageFileButton";
            this.chooseImageFileButton.Size = new System.Drawing.Size(75, 23);
            this.chooseImageFileButton.TabIndex = 2;
            this.chooseImageFileButton.Text = "Choose File";
            this.chooseImageFileButton.UseVisualStyleBackColor = true;
            this.chooseImageFileButton.Click += new System.EventHandler(this.chooseFileButton_Click);
            // 
            // inputFileTextBox
            // 
            this.inputFileTextBox.Location = new System.Drawing.Point(30, 46);
            this.inputFileTextBox.Name = "inputFileTextBox";
            this.inputFileTextBox.ReadOnly = true;
            this.inputFileTextBox.Size = new System.Drawing.Size(330, 20);
            this.inputFileTextBox.TabIndex = 3;
            // 
            // convertImageToTextButton
            // 
            this.convertImageToTextButton.Location = new System.Drawing.Point(275, 175);
            this.convertImageToTextButton.Name = "convertImageToTextButton";
            this.convertImageToTextButton.Size = new System.Drawing.Size(190, 23);
            this.convertImageToTextButton.TabIndex = 4;
            this.convertImageToTextButton.Text = "Convert From Image To Text File";
            this.convertImageToTextButton.UseVisualStyleBackColor = true;
            this.convertImageToTextButton.Click += new System.EventHandler(this.convertImageToTextButton_Click);
            // 
            // outputFileTextBox
            // 
            this.outputFileTextBox.Location = new System.Drawing.Point(390, 46);
            this.outputFileTextBox.Name = "outputFileTextBox";
            this.outputFileTextBox.ReadOnly = true;
            this.outputFileTextBox.Size = new System.Drawing.Size(330, 20);
            this.outputFileTextBox.TabIndex = 5;
            // 
            // chooseOutputTextFileButton
            // 
            this.chooseOutputTextFileButton.Location = new System.Drawing.Point(473, 22);
            this.chooseOutputTextFileButton.Name = "chooseOutputTextFileButton";
            this.chooseOutputTextFileButton.Size = new System.Drawing.Size(75, 23);
            this.chooseOutputTextFileButton.TabIndex = 6;
            this.chooseOutputTextFileButton.Text = "Choose Location";
            this.chooseOutputTextFileButton.UseVisualStyleBackColor = true;
            // 
            // encryptedCheckBox
            // 
            this.encryptedCheckBox.AutoSize = true;
            this.encryptedCheckBox.Location = new System.Drawing.Point(30, 73);
            this.encryptedCheckBox.Name = "encryptedCheckBox";
            this.encryptedCheckBox.Size = new System.Drawing.Size(62, 17);
            this.encryptedCheckBox.TabIndex = 7;
            this.encryptedCheckBox.Text = "Encrypt";
            this.encryptedCheckBox.UseVisualStyleBackColor = true;
            // 
            // chooseOutputImageFilePathButton
            // 
            this.chooseOutputImageFilePathButton.Location = new System.Drawing.Point(451, 238);
            this.chooseOutputImageFilePathButton.Name = "chooseOutputImageFilePathButton";
            this.chooseOutputImageFilePathButton.Size = new System.Drawing.Size(75, 23);
            this.chooseOutputImageFilePathButton.TabIndex = 13;
            this.chooseOutputImageFilePathButton.Text = "Choose Location";
            this.chooseOutputImageFilePathButton.UseVisualStyleBackColor = true;
            // 
            // outputImageFilePathTextBox
            // 
            this.outputImageFilePathTextBox.Location = new System.Drawing.Point(390, 267);
            this.outputImageFilePathTextBox.Name = "outputImageFilePathTextBox";
            this.outputImageFilePathTextBox.ReadOnly = true;
            this.outputImageFilePathTextBox.Size = new System.Drawing.Size(330, 20);
            this.outputImageFilePathTextBox.TabIndex = 12;
            // 
            // inputTextFilePathTextBox
            // 
            this.inputTextFilePathTextBox.Location = new System.Drawing.Point(30, 267);
            this.inputTextFilePathTextBox.Name = "inputTextFilePathTextBox";
            this.inputTextFilePathTextBox.ReadOnly = true;
            this.inputTextFilePathTextBox.Size = new System.Drawing.Size(330, 20);
            this.inputTextFilePathTextBox.TabIndex = 11;
            // 
            // chooseInputTextFilePathButton
            // 
            this.chooseInputTextFilePathButton.Location = new System.Drawing.Point(108, 238);
            this.chooseInputTextFilePathButton.Name = "chooseInputTextFilePathButton";
            this.chooseInputTextFilePathButton.Size = new System.Drawing.Size(75, 23);
            this.chooseInputTextFilePathButton.TabIndex = 10;
            this.chooseInputTextFilePathButton.Text = "Choose File";
            this.chooseInputTextFilePathButton.UseVisualStyleBackColor = true;
            this.chooseInputTextFilePathButton.Click += new System.EventHandler(this.chooseInputTextFilePathButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Output File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Input Text File";
            // 
            // convertFromTextToImageButton
            // 
            this.convertFromTextToImageButton.Location = new System.Drawing.Point(275, 317);
            this.convertFromTextToImageButton.Name = "convertFromTextToImageButton";
            this.convertFromTextToImageButton.Size = new System.Drawing.Size(190, 23);
            this.convertFromTextToImageButton.TabIndex = 14;
            this.convertFromTextToImageButton.Text = "Convert From Text To Image File";
            this.convertFromTextToImageButton.UseVisualStyleBackColor = true;
            this.convertFromTextToImageButton.Click += new System.EventHandler(this.convertFromTextToImageFileButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 38);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 32);
            this.progressBar.Step = 1;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(57, 390);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(24, 13);
            this.progressLabel.TabIndex = 16;
            this.progressLabel.Text = "0 %";
            // 
            // compressionRateTrackBar
            // 
            this.compressionRateTrackBar.Location = new System.Drawing.Point(153, 95);
            this.compressionRateTrackBar.Maximum = 100;
            this.compressionRateTrackBar.Minimum = 1;
            this.compressionRateTrackBar.Name = "compressionRateTrackBar";
            this.compressionRateTrackBar.Size = new System.Drawing.Size(234, 45);
            this.compressionRateTrackBar.TabIndex = 17;
            this.compressionRateTrackBar.Value = 100;
            this.compressionRateTrackBar.Scroll += new System.EventHandler(this.compressionRateTrackBar_Scroll);
            // 
            // compressionLevelLabel
            // 
            this.compressionLevelLabel.AutoSize = true;
            this.compressionLevelLabel.Location = new System.Drawing.Point(300, 127);
            this.compressionLevelLabel.Name = "compressionLevelLabel";
            this.compressionLevelLabel.Size = new System.Drawing.Size(36, 13);
            this.compressionLevelLabel.TabIndex = 18;
            this.compressionLevelLabel.Text = "100 %";
            // 
            // compressCheckBox
            // 
            this.compressCheckBox.AutoSize = true;
            this.compressCheckBox.Location = new System.Drawing.Point(153, 72);
            this.compressCheckBox.Name = "compressCheckBox";
            this.compressCheckBox.Size = new System.Drawing.Size(104, 17);
            this.compressCheckBox.TabIndex = 19;
            this.compressCheckBox.Text = "Compress Image";
            this.compressCheckBox.UseVisualStyleBackColor = true;
            this.compressCheckBox.CheckedChanged += new System.EventHandler(this.compressCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Compression Rate:";
            // 
            // ignoreAlphaChannelCheckbox
            // 
            this.ignoreAlphaChannelCheckbox.AutoSize = true;
            this.ignoreAlphaChannelCheckbox.Location = new System.Drawing.Point(303, 73);
            this.ignoreAlphaChannelCheckbox.Name = "ignoreAlphaChannelCheckbox";
            this.ignoreAlphaChannelCheckbox.Size = new System.Drawing.Size(86, 17);
            this.ignoreAlphaChannelCheckbox.TabIndex = 21;
            this.ignoreAlphaChannelCheckbox.Text = "Ignore Alpha";
            this.ignoreAlphaChannelCheckbox.UseVisualStyleBackColor = false;
            this.ignoreAlphaChannelCheckbox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ignoreAlphaChannelCheckbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.compressCheckBox);
            this.Controls.Add(this.compressionLevelLabel);
            this.Controls.Add(this.compressionRateTrackBar);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.convertFromTextToImageButton);
            this.Controls.Add(this.chooseOutputImageFilePathButton);
            this.Controls.Add(this.outputImageFilePathTextBox);
            this.Controls.Add(this.inputTextFilePathTextBox);
            this.Controls.Add(this.chooseInputTextFilePathButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.encryptedCheckBox);
            this.Controls.Add(this.chooseOutputTextFileButton);
            this.Controls.Add(this.outputFileTextBox);
            this.Controls.Add(this.convertImageToTextButton);
            this.Controls.Add(this.inputFileTextBox);
            this.Controls.Add(this.chooseImageFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressionRateTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button chooseImageFileButton;
        private System.Windows.Forms.TextBox inputFileTextBox;
        private System.Windows.Forms.Button convertImageToTextButton;
        private System.Windows.Forms.TextBox outputFileTextBox;
        private System.Windows.Forms.Button chooseOutputTextFileButton;
        private System.Windows.Forms.CheckBox encryptedCheckBox;
        private System.Windows.Forms.Button chooseOutputImageFilePathButton;
        private System.Windows.Forms.TextBox outputImageFilePathTextBox;
        private System.Windows.Forms.TextBox inputTextFilePathTextBox;
        private System.Windows.Forms.Button chooseInputTextFilePathButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button convertFromTextToImageButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.TrackBar compressionRateTrackBar;
        private System.Windows.Forms.Label compressionLevelLabel;
        private System.Windows.Forms.CheckBox compressCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ignoreAlphaChannelCheckbox;
    }
}

