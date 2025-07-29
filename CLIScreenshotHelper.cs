using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CLIScreenshotHelper
{
    // Language strings
    public static class Lang
    {
        public static string Language = "zh-TW"; // Default to Chinese
        
        // Application
        public static string AppName { get { return Language == "zh-TW" ? "CLI 截圖助手" : "CLI Screenshot Helper"; } }
        public static string AppRunning { get { return Language == "zh-TW" ? "程式已在執行中" : "Application is already running"; } }
        public static string Tip { get { return Language == "zh-TW" ? "提示" : "Tip"; } }
        
        // Tray menu
        public static string OpenFolder { get { return Language == "zh-TW" ? "開啟截圖資料夾" : "Open Screenshots Folder"; } }
        public static string Settings { get { return Language == "zh-TW" ? "設定" : "Settings"; } }
        public static string Exit { get { return Language == "zh-TW" ? "結束" : "Exit"; } }
        
        // Settings window
        public static string SettingsTitle { get { return Language == "zh-TW" ? "CLI 截圖助手 設定" : "CLI Screenshot Helper Settings"; } }
        public static string GeneralTab { get { return Language == "zh-TW" ? "一般" : "General"; } }
        public static string HotkeyTab { get { return Language == "zh-TW" ? "快捷鍵" : "Hotkey"; } }
        public static string LanguageTab { get { return Language == "zh-TW" ? "語言" : "Language"; } }
        
        // General settings
        public static string SavePath { get { return Language == "zh-TW" ? "儲存路徑:" : "Save Path:"; } }
        public static string Browse { get { return Language == "zh-TW" ? "瀏覽..." : "Browse..."; } }
        public static string FileNameFormat { get { return Language == "zh-TW" ? "檔名格式:" : "Filename Format:"; } }
        public static string Preview { get { return Language == "zh-TW" ? "預覽:" : "Preview:"; } }
        public static string FormatHelp { get { return Language == "zh-TW" ? "格式說明: {0:yyyyMMdd} = 日期, {0:HHmmss} = 時間" : "Format: {0:yyyyMMdd} = Date, {0:HHmmss} = Time"; } }
        public static string ShowNotifications { get { return Language == "zh-TW" ? "顯示通知" : "Show Notifications"; } }
        public static string PlaySound { get { return Language == "zh-TW" ? "播放音效" : "Play Sound"; } }
        public static string AutoStart { get { return Language == "zh-TW" ? "開機時自動啟動" : "Start with Windows"; } }
        public static string AutoDetectMode { get { return Language == "zh-TW" ? "自動偵測所有截圖 (偵測剪貼簿變化)" : "Auto-detect all screenshots (monitor clipboard)"; } }
        public static string UseHotkey { get { return Language == "zh-TW" ? "使用快捷鍵觸發" : "Use hotkey trigger"; } }
        
        // Hotkey settings
        public static string SetHotkey { get { return Language == "zh-TW" ? "設定截圖快捷鍵:" : "Set Screenshot Hotkey:"; } }
        public static string ModifierKeys { get { return Language == "zh-TW" ? "修飾鍵:" : "Modifier Keys:"; } }
        public static string Key { get { return Language == "zh-TW" ? "按鍵:" : "Key:"; } }
        public static string CurrentHotkey { get { return Language == "zh-TW" ? "目前快捷鍵:" : "Current Hotkey:"; } }
        public static string HotkeyNote { get { return Language == "zh-TW" ? 
            "注意: Win+Shift+S 仍然是 Windows 預設截圖\n此設定是觸發截圖並複製路徑的快捷鍵" : 
            "Note: Win+Shift+S is still Windows default screenshot\nThis setting triggers screenshot and copies path"; } }
        
        // Language settings
        public static string SelectLanguage { get { return Language == "zh-TW" ? "選擇語言:" : "Select Language:"; } }
        public static string Chinese { get { return Language == "zh-TW" ? "繁體中文" : "Traditional Chinese"; } }
        public static string English { get { return Language == "zh-TW" ? "英文" : "English"; } }
        
        // Buttons
        public static string OK { get { return Language == "zh-TW" ? "確定" : "OK"; } }
        public static string Cancel { get { return Language == "zh-TW" ? "取消" : "Cancel"; } }
        
        // Error messages
        public static string FormatError { get { return Language == "zh-TW" ? "格式錯誤" : "Format Error"; } }
        public static string SelectModifier { get { return Language == "zh-TW" ? "請至少選擇一個修飾鍵" : "Please select at least one modifier key"; } }
        public static string SelectKey { get { return Language == "zh-TW" ? "請選擇一個按鍵" : "Please select a key"; } }
        public static string Error { get { return Language == "zh-TW" ? "錯誤" : "Error"; } }
        public static string CannotCreateFolder { get { return Language == "zh-TW" ? "無法建立資料夾" : "Cannot create folder"; } }
        public static string SelectFolder { get { return Language == "zh-TW" ? "選擇截圖儲存資料夾" : "Select screenshot save folder"; } }
        
        // Hotkey registration
        public static string HotkeyConflict { get { return Language == "zh-TW" ? 
            "無法註冊快捷鍵 {0}\n可能已被其他程式使用" : 
            "Cannot register hotkey {0}\nIt may be used by another program"; } }
        
        // Notifications
        public static string Started { get { return Language == "zh-TW" ? "已啟動！" : "Started!"; } }
        public static string HotkeyInfo { get { return Language == "zh-TW" ? "{0} 截圖並複製路徑" : "{0} to screenshot and copy path"; } }
        public static string SettingsUpdated { get { return Language == "zh-TW" ? "設定已更新" : "Settings Updated"; } }
        public static string NewHotkey { get { return Language == "zh-TW" ? "新快捷鍵: {0}" : "New Hotkey: {0}"; } }
        public static string ScreenshotSaved { get { return Language == "zh-TW" ? "截圖已儲存" : "Screenshot Saved"; } }
        public static string PathCopied { get { return Language == "zh-TW" ? "路徑已複製:\n{0}" : "Path copied:\n{0}"; } }
        public static string ProcessingError { get { return Language == "zh-TW" ? "處理截圖時發生錯誤" : "Error processing screenshot"; } }
        public static string WaitingForScreenshot { get { return Language == "zh-TW" ? "等待截圖中..." : "Waiting for screenshot..."; } }
        public static string AutoDetectEnabled { get { return Language == "zh-TW" ? "自動偵測已啟用" : "Auto-detect enabled"; } }
        public static string Monitoring { get { return Language == "zh-TW" ? "監控剪貼簿變化中" : "Monitoring clipboard changes"; } }
        
        // Tray tooltip
        public static string TrayTooltip { get { return Language == "zh-TW" ? 
            "CLI 截圖助手\n{0}" : 
            "CLI Screenshot Helper\n{0}"; } }
        public static string AutoDetectTooltip { get { return Language == "zh-TW" ? 
            "自動偵測: 任何截圖都會儲存並複製路徑" : 
            "Auto-detect: Any screenshot will be saved and path copied"; } }
    }
    
    // Settings class
    public class Settings
    {
        public string SavePath { get; set; }
        public string FileNameFormat { get; set; }
        public bool ShowNotifications { get; set; }
        public bool PlaySound { get; set; }
        public bool AutoStart { get; set; }
        public bool AutoDetectMode { get; set; }
        public bool UseHotKey { get; set; }
        public bool UseCtrl { get; set; }
        public bool UseAlt { get; set; }
        public bool UseShift { get; set; }
        public bool UseWin { get; set; }
        public string HotKey { get; set; }
        public string Language { get; set; }
        
        public Settings()
        {
            // Default values
            SavePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                "Screenshots"
            );
            FileNameFormat = "Screenshot_{0:yyyyMMdd_HHmmss}";
            ShowNotifications = true;
            PlaySound = false;
            AutoStart = false;
            AutoDetectMode = true;
            UseHotKey = true;
            UseWin = true;
            UseShift = true;
            UseCtrl = false;
            UseAlt = false;
            HotKey = "D";
            Language = "zh-TW";
        }
        
        public string GetHotKeyDescription()
        {
            string desc = "";
            if (UseWin) desc += "Win+";
            if (UseCtrl) desc += "Ctrl+";
            if (UseAlt) desc += "Alt+";
            if (UseShift) desc += "Shift+";
            desc += HotKey;
            return desc;
        }
        
        public uint GetModifiers()
        {
            uint mod = 0;
            if (UseWin) mod |= 0x0008;
            if (UseCtrl) mod |= 0x0002;
            if (UseAlt) mod |= 0x0001;
            if (UseShift) mod |= 0x0004;
            return mod;
        }
        
        public uint GetVirtualKey()
        {
            switch (HotKey.ToUpper())
            {
                case "A": return 0x41;
                case "B": return 0x42;
                case "C": return 0x43;
                case "D": return 0x44;
                case "E": return 0x45;
                case "F": return 0x46;
                case "G": return 0x47;
                case "H": return 0x48;
                case "I": return 0x49;
                case "J": return 0x4A;
                case "K": return 0x4B;
                case "L": return 0x4C;
                case "M": return 0x4D;
                case "N": return 0x4E;
                case "O": return 0x4F;
                case "P": return 0x50;
                case "Q": return 0x51;
                case "R": return 0x52;
                case "S": return 0x53;
                case "T": return 0x54;
                case "U": return 0x55;
                case "V": return 0x56;
                case "W": return 0x57;
                case "X": return 0x58;
                case "Y": return 0x59;
                case "Z": return 0x5A;
                case "1": return 0x31;
                case "2": return 0x32;
                case "3": return 0x33;
                case "4": return 0x34;
                case "5": return 0x35;
                case "6": return 0x36;
                case "7": return 0x37;
                case "8": return 0x38;
                case "9": return 0x39;
                case "0": return 0x30;
                case "F1": return 0x70;
                case "F2": return 0x71;
                case "F3": return 0x72;
                case "F4": return 0x73;
                case "F5": return 0x74;
                case "F6": return 0x75;
                case "F7": return 0x76;
                case "F8": return 0x77;
                case "F9": return 0x78;
                case "F10": return 0x79;
                case "F11": return 0x7A;
                case "F12": return 0x7B;
                default: return 0x44; // Default D
            }
        }
        
        public void Save()
        {
            try
            {
                string configPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "CLIScreenshotHelper"
                );
                Directory.CreateDirectory(configPath);
                
                string configFile = Path.Combine(configPath, "settings.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (StreamWriter writer = new StreamWriter(configFile))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch { }
        }
        
        public static Settings Load()
        {
            try
            {
                string configFile = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "CLIScreenshotHelper",
                    "settings.xml"
                );
                
                if (File.Exists(configFile))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    using (StreamReader reader = new StreamReader(configFile))
                    {
                        Settings settings = (Settings)serializer.Deserialize(reader);
                        Lang.Language = settings.Language;
                        return settings;
                    }
                }
            }
            catch { }
            
            return new Settings();
        }
    }
    
    // Settings Form
    public class SettingsForm : Form
    {
        private Settings settings;
        private TextBox pathTextBox;
        private TextBox formatTextBox;
        private CheckBox notificationCheckBox;
        private CheckBox soundCheckBox;
        private CheckBox autoStartCheckBox;
        private CheckBox autoDetectCheckBox;
        private CheckBox useHotKeyCheckBox;
        private Label previewLabel;
        private CheckBox winCheckBox;
        private CheckBox ctrlCheckBox;
        private CheckBox altCheckBox;
        private CheckBox shiftCheckBox;
        private ComboBox keyComboBox;
        private Label hotKeyPreviewLabel;
        private RadioButton chineseRadio;
        private RadioButton englishRadio;
        private bool isLoadingSettings = false;
        
        public SettingsForm(Settings currentSettings)
        {
            settings = currentSettings;
            InitializeUI();
            LoadSettings();
        }
        
        void InitializeUI()
        {
            Text = Lang.SettingsTitle;
            Size = new Size(520, 600);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            
            TabControl tabControl = new TabControl();
            tabControl.Location = new Point(10, 10);
            tabControl.Size = new Size(490, 500);
            
            // General Tab
            TabPage generalTab = new TabPage(Lang.GeneralTab);
            
            // Save path
            Label pathLabel = new Label();
            pathLabel.Text = Lang.SavePath;
            pathLabel.Location = new Point(20, 20);
            pathLabel.Size = new Size(100, 20);
            generalTab.Controls.Add(pathLabel);
            
            pathTextBox = new TextBox();
            pathTextBox.Location = new Point(20, 45);
            pathTextBox.Size = new Size(320, 25);
            pathTextBox.Text = settings.SavePath;
            generalTab.Controls.Add(pathTextBox);
            
            Button browseButton = new Button();
            browseButton.Text = Lang.Browse;
            browseButton.Location = new Point(350, 43);
            browseButton.Size = new Size(80, 25);
            browseButton.Click += BrowseButton_Click;
            generalTab.Controls.Add(browseButton);
            
            // Filename format
            Label formatLabel = new Label();
            formatLabel.Text = Lang.FileNameFormat;
            formatLabel.Location = new Point(20, 80);
            formatLabel.Size = new Size(100, 20);
            generalTab.Controls.Add(formatLabel);
            
            formatTextBox = new TextBox();
            formatTextBox.Location = new Point(20, 105);
            formatTextBox.Size = new Size(410, 25);
            formatTextBox.Text = settings.FileNameFormat;
            formatTextBox.TextChanged += FormatTextBox_TextChanged;
            generalTab.Controls.Add(formatTextBox);
            
            // Preview
            Label previewTitleLabel = new Label();
            previewTitleLabel.Text = Lang.Preview;
            previewTitleLabel.Location = new Point(20, 140);
            previewTitleLabel.Size = new Size(50, 20);
            generalTab.Controls.Add(previewTitleLabel);
            
            previewLabel = new Label();
            previewLabel.Location = new Point(70, 140);
            previewLabel.Size = new Size(360, 20);
            previewLabel.ForeColor = Color.Blue;
            generalTab.Controls.Add(previewLabel);
            
            // Format help
            Label helpLabel = new Label();
            helpLabel.Text = Lang.FormatHelp;
            helpLabel.Location = new Point(20, 170);
            helpLabel.Size = new Size(410, 20);
            helpLabel.ForeColor = Color.Gray;
            generalTab.Controls.Add(helpLabel);
            
            // Options
            notificationCheckBox = new CheckBox();
            notificationCheckBox.Text = Lang.ShowNotifications;
            notificationCheckBox.Location = new Point(20, 210);
            notificationCheckBox.Size = new Size(200, 25);
            notificationCheckBox.Checked = settings.ShowNotifications;
            generalTab.Controls.Add(notificationCheckBox);
            
            soundCheckBox = new CheckBox();
            soundCheckBox.Text = Lang.PlaySound;
            soundCheckBox.Location = new Point(20, 240);
            soundCheckBox.Size = new Size(200, 25);
            soundCheckBox.Checked = settings.PlaySound;
            generalTab.Controls.Add(soundCheckBox);
            
            autoStartCheckBox = new CheckBox();
            autoStartCheckBox.Text = Lang.AutoStart;
            autoStartCheckBox.Location = new Point(20, 270);
            autoStartCheckBox.Size = new Size(200, 25);
            autoStartCheckBox.Checked = settings.AutoStart;
            generalTab.Controls.Add(autoStartCheckBox);
            
            // Monitoring mode
            autoDetectCheckBox = new CheckBox();
            autoDetectCheckBox.Text = Lang.AutoDetectMode;
            autoDetectCheckBox.Location = new Point(20, 310);
            autoDetectCheckBox.Size = new Size(410, 25);
            autoDetectCheckBox.Checked = settings.AutoDetectMode;
            generalTab.Controls.Add(autoDetectCheckBox);
            
            useHotKeyCheckBox = new CheckBox();
            useHotKeyCheckBox.Text = Lang.UseHotkey;
            useHotKeyCheckBox.Location = new Point(20, 340);
            useHotKeyCheckBox.Size = new Size(410, 25);
            useHotKeyCheckBox.Checked = settings.UseHotKey;
            generalTab.Controls.Add(useHotKeyCheckBox);
            
            // Hotkey Tab
            TabPage hotKeyTab = new TabPage(Lang.HotkeyTab);
            
            Label hotKeyLabel = new Label();
            hotKeyLabel.Text = Lang.SetHotkey;
            hotKeyLabel.Location = new Point(20, 20);
            hotKeyLabel.Size = new Size(200, 20);
            hotKeyLabel.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            hotKeyTab.Controls.Add(hotKeyLabel);
            
            // Modifier keys
            Label modifierLabel = new Label();
            modifierLabel.Text = Lang.ModifierKeys;
            modifierLabel.Location = new Point(20, 60);
            modifierLabel.Size = new Size(60, 20);
            hotKeyTab.Controls.Add(modifierLabel);
            
            winCheckBox = new CheckBox();
            winCheckBox.Text = "Win";
            winCheckBox.Location = new Point(90, 60);
            winCheckBox.Size = new Size(60, 20);
            winCheckBox.CheckedChanged += HotKeyChanged;
            hotKeyTab.Controls.Add(winCheckBox);
            
            ctrlCheckBox = new CheckBox();
            ctrlCheckBox.Text = "Ctrl";
            ctrlCheckBox.Location = new Point(160, 60);
            ctrlCheckBox.Size = new Size(60, 20);
            ctrlCheckBox.CheckedChanged += HotKeyChanged;
            hotKeyTab.Controls.Add(ctrlCheckBox);
            
            altCheckBox = new CheckBox();
            altCheckBox.Text = "Alt";
            altCheckBox.Location = new Point(230, 60);
            altCheckBox.Size = new Size(60, 20);
            altCheckBox.CheckedChanged += HotKeyChanged;
            hotKeyTab.Controls.Add(altCheckBox);
            
            shiftCheckBox = new CheckBox();
            shiftCheckBox.Text = "Shift";
            shiftCheckBox.Location = new Point(300, 60);
            shiftCheckBox.Size = new Size(60, 20);
            shiftCheckBox.CheckedChanged += HotKeyChanged;
            hotKeyTab.Controls.Add(shiftCheckBox);
            
            // Key
            Label keyLabel = new Label();
            keyLabel.Text = Lang.Key;
            keyLabel.Location = new Point(20, 100);
            keyLabel.Size = new Size(60, 20);
            hotKeyTab.Controls.Add(keyLabel);
            
            keyComboBox = new ComboBox();
            keyComboBox.Location = new Point(90, 100);
            keyComboBox.Size = new Size(150, 25);
            keyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            keyComboBox.Items.AddRange(new string[] {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
                "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"
            });
            keyComboBox.SelectedIndexChanged += HotKeyChanged;
            hotKeyTab.Controls.Add(keyComboBox);
            
            // Hotkey preview
            Label hotKeyPreviewTitle = new Label();
            hotKeyPreviewTitle.Text = Lang.CurrentHotkey;
            hotKeyPreviewTitle.Location = new Point(20, 150);
            hotKeyPreviewTitle.Size = new Size(80, 20);
            hotKeyTab.Controls.Add(hotKeyPreviewTitle);
            
            hotKeyPreviewLabel = new Label();
            hotKeyPreviewLabel.Location = new Point(100, 150);
            hotKeyPreviewLabel.Size = new Size(300, 20);
            hotKeyPreviewLabel.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            hotKeyPreviewLabel.ForeColor = Color.Blue;
            hotKeyTab.Controls.Add(hotKeyPreviewLabel);
            
            Label hotKeyNote = new Label();
            hotKeyNote.Text = Lang.HotkeyNote;
            hotKeyNote.Location = new Point(20, 200);
            hotKeyNote.Size = new Size(400, 40);
            hotKeyNote.ForeColor = Color.Gray;
            hotKeyTab.Controls.Add(hotKeyNote);
            
            // Language Tab
            TabPage languageTab = new TabPage(Lang.LanguageTab);
            
            Label languageLabel = new Label();
            languageLabel.Text = Lang.SelectLanguage;
            languageLabel.Location = new Point(20, 20);
            languageLabel.Size = new Size(200, 20);
            languageLabel.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            languageTab.Controls.Add(languageLabel);
            
            chineseRadio = new RadioButton();
            chineseRadio.Text = Lang.Chinese;
            chineseRadio.Location = new Point(20, 60);
            chineseRadio.Size = new Size(200, 25);
            chineseRadio.CheckedChanged += LanguageChanged;
            languageTab.Controls.Add(chineseRadio);
            
            englishRadio = new RadioButton();
            englishRadio.Text = Lang.English;
            englishRadio.Location = new Point(20, 90);
            englishRadio.Size = new Size(200, 25);
            englishRadio.CheckedChanged += LanguageChanged;
            languageTab.Controls.Add(englishRadio);
            
            // Add tabs
            tabControl.TabPages.Add(generalTab);
            tabControl.TabPages.Add(hotKeyTab);
            tabControl.TabPages.Add(languageTab);
            Controls.Add(tabControl);
            
            // Buttons
            Button okButton = new Button();
            okButton.Text = Lang.OK;
            okButton.Location = new Point(325, 520);
            okButton.Size = new Size(85, 30);
            okButton.Click += OkButton_Click;
            Controls.Add(okButton);
            
            Button cancelButton = new Button();
            cancelButton.Text = Lang.Cancel;
            cancelButton.Location = new Point(415, 520);
            cancelButton.Size = new Size(85, 30);
            cancelButton.DialogResult = DialogResult.Cancel;
            Controls.Add(cancelButton);
            
            UpdatePreview();
            UpdateHotKeyPreview();
        }
        
        void LoadSettings()
        {
            isLoadingSettings = true;
            
            pathTextBox.Text = settings.SavePath;
            formatTextBox.Text = settings.FileNameFormat;
            notificationCheckBox.Checked = settings.ShowNotifications;
            soundCheckBox.Checked = settings.PlaySound;
            autoStartCheckBox.Checked = settings.AutoStart;
            autoDetectCheckBox.Checked = settings.AutoDetectMode;
            useHotKeyCheckBox.Checked = settings.UseHotKey;
            winCheckBox.Checked = settings.UseWin;
            ctrlCheckBox.Checked = settings.UseCtrl;
            altCheckBox.Checked = settings.UseAlt;
            shiftCheckBox.Checked = settings.UseShift;
            keyComboBox.Text = settings.HotKey;
            
            if (settings.Language == "zh-TW")
                chineseRadio.Checked = true;
            else
                englishRadio.Checked = true;
                
            isLoadingSettings = false;
        }
        
        void LanguageChanged(object sender, EventArgs e)
        {
            if (isLoadingSettings) return;
            
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null || !radioButton.Checked) return;
            
            if (chineseRadio.Checked)
                Lang.Language = "zh-TW";
            else
                Lang.Language = "en-US";
            
            MessageBox.Show(
                Lang.Language == "zh-TW" ? 
                "語言設定將在重新啟動程式後生效" : 
                "Language settings will take effect after restarting the application",
                "CLI Screenshot Helper",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        
        void HotKeyChanged(object sender, EventArgs e)
        {
            UpdateHotKeyPreview();
        }
        
        void UpdateHotKeyPreview()
        {
            string preview = "";
            if (winCheckBox.Checked) preview += "Win+";
            if (ctrlCheckBox.Checked) preview += "Ctrl+";
            if (altCheckBox.Checked) preview += "Alt+";
            if (shiftCheckBox.Checked) preview += "Shift+";
            if (!string.IsNullOrEmpty(keyComboBox.Text))
                preview += keyComboBox.Text;
            else
                preview += "?";
            
            hotKeyPreviewLabel.Text = preview;
        }
        
        void BrowseButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = pathTextBox.Text;
                dialog.Description = Lang.SelectFolder;
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pathTextBox.Text = dialog.SelectedPath;
                }
            }
        }
        
        void FormatTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }
        
        void UpdatePreview()
        {
            try
            {
                string preview = string.Format(formatTextBox.Text, DateTime.Now) + ".png";
                previewLabel.Text = preview;
                previewLabel.ForeColor = Color.Blue;
            }
            catch
            {
                previewLabel.Text = Lang.FormatError;
                previewLabel.ForeColor = Color.Red;
            }
        }
        
        void OkButton_Click(object sender, EventArgs e)
        {
            // Validate hotkey if enabled
            if (useHotKeyCheckBox.Checked)
            {
                if (!winCheckBox.Checked && !ctrlCheckBox.Checked && !altCheckBox.Checked && !shiftCheckBox.Checked)
                {
                    MessageBox.Show(Lang.SelectModifier, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (string.IsNullOrEmpty(keyComboBox.Text))
                {
                    MessageBox.Show(Lang.SelectKey, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
            // Validate path
            if (!Directory.Exists(pathTextBox.Text))
            {
                try
                {
                    Directory.CreateDirectory(pathTextBox.Text);
                }
                catch
                {
                    MessageBox.Show(Lang.CannotCreateFolder, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            // Save settings
            settings.SavePath = pathTextBox.Text;
            settings.FileNameFormat = formatTextBox.Text;
            settings.ShowNotifications = notificationCheckBox.Checked;
            settings.PlaySound = soundCheckBox.Checked;
            settings.AutoStart = autoStartCheckBox.Checked;
            settings.AutoDetectMode = autoDetectCheckBox.Checked;
            settings.UseHotKey = useHotKeyCheckBox.Checked;
            settings.UseWin = winCheckBox.Checked;
            settings.UseCtrl = ctrlCheckBox.Checked;
            settings.UseAlt = altCheckBox.Checked;
            settings.UseShift = shiftCheckBox.Checked;
            settings.HotKey = keyComboBox.Text;
            settings.Language = Lang.Language;
            
            settings.Save();
            
            // Handle auto-start
            SetAutoStart(settings.AutoStart);
            
            DialogResult = DialogResult.OK;
        }
        
        void SetAutoStart(bool enable)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                
                if (enable)
                {
                    key.SetValue("CLIScreenshotHelper", Application.ExecutablePath);
                }
                else
                {
                    key.DeleteValue("CLIScreenshotHelper", false);
                }
                
                key.Close();
            }
            catch { }
        }
    }
    
    // Main Program
    class Program
    {
        // Windows API
        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        
        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        
        [DllImport("user32.dll")]
        static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        
        [DllImport("user32.dll")]
        static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        
        // Constants
        const int WM_HOTKEY = 0x0312;
        const int WM_DRAWCLIPBOARD = 0x0308;
        const int WM_CHANGECBCHAIN = 0x030D;
        const uint VK_LWIN = 0x5B;
        const uint VK_SHIFT = 0x10;
        const uint VK_S = 0x53;
        const uint KEYEVENTF_KEYUP = 0x0002;
        
        static NotifyIcon trayIcon;
        static System.Windows.Forms.Timer timer;
        static Settings settings;
        static bool isRunning = true;
        static bool expectingScreenshot = false;
        static DateTime lastScreenshotTime = DateTime.MinValue;
        static MessageWindow msgWindow;
        static IntPtr nextClipboardViewer;
        static string lastImageHash = "";
        static bool isHotKeyTriggered = false;
        
        // Hidden window
        class MessageWindow : Form
        {
            public MessageWindow()
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Visible = false;
            }
            
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    TriggerScreenshot();
                }
                else if (m.Msg == WM_DRAWCLIPBOARD)
                {
                    if (settings.AutoDetectMode)
                    {
                        ProcessClipboardChange();
                    }
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                }
                else if (m.Msg == WM_CHANGECBCHAIN)
                {
                    if (m.WParam == nextClipboardViewer)
                    {
                        nextClipboardViewer = m.LParam;
                    }
                    else
                    {
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    }
                }
                base.WndProc(ref m);
            }
            
            void TriggerScreenshot()
            {
                isHotKeyTriggered = true;
                expectingScreenshot = true;
                lastScreenshotTime = DateTime.Now;
                
                UpdateTrayTooltip(Lang.WaitingForScreenshot);
                
                keybd_event((byte)VK_LWIN, 0, 0, 0);
                keybd_event((byte)VK_SHIFT, 0, 0, 0);
                keybd_event((byte)VK_S, 0, 0, 0);
                
                Thread.Sleep(50);
                
                keybd_event((byte)VK_S, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((byte)VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((byte)VK_LWIN, 0, KEYEVENTF_KEYUP, 0);
            }
            
            void ProcessClipboardChange()
            {
                if (!settings.AutoDetectMode) return;
                
                try
                {
                    if (Clipboard.ContainsImage())
                    {
                        Image image = Clipboard.GetImage();
                        if (image != null)
                        {
                            string currentHash = GetImageHash(image);
                            
                            if (currentHash != lastImageHash)
                            {
                                lastImageHash = currentHash;
                                
                                // Save and process
                                string filename = string.Format(settings.FileNameFormat, DateTime.Now) + ".png";
                                string filepath = Path.Combine(settings.SavePath, filename);
                                
                                image.Save(filepath, ImageFormat.Png);
                                
                                // Copy path to clipboard
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new Action(() => {
                                        Clipboard.Clear();
                                        Thread.Sleep(100);
                                        Clipboard.SetText(filepath);
                                    }));
                                }
                                else
                                {
                                    Clipboard.Clear();
                                    Thread.Sleep(100);
                                    Clipboard.SetText(filepath);
                                }
                                
                                // Show notification
                                if (settings.ShowNotifications)
                                {
                                    trayIcon.ShowBalloonTip(
                                        3000,
                                        Lang.ScreenshotSaved,
                                        string.Format(Lang.PathCopied, filepath),
                                        ToolTipIcon.Info
                                    );
                                }
                                
                                if (settings.PlaySound)
                                {
                                    System.Media.SystemSounds.Asterisk.Play();
                                }
                            }
                            
                            image.Dispose();
                        }
                    }
                }
                catch { }
            }
        }

        [STAThread]
        static void Main()
        {
            bool createdNew;
            using (var mutex = new Mutex(true, "CLIScreenshotHelperMutex", out createdNew))
            {
                if (!createdNew)
                {
                    MessageBox.Show(Lang.AppRunning, Lang.Tip);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Initialize();
                Application.Run(msgWindow);
                Cleanup();
            }
        }

        static void Initialize()
        {
            // Load settings
            settings = Settings.Load();
            Lang.Language = settings.Language;
            Directory.CreateDirectory(settings.SavePath);

            // Create message window
            msgWindow = new MessageWindow();
            
            // Register clipboard viewer
            if (settings.AutoDetectMode)
            {
                nextClipboardViewer = SetClipboardViewer(msgWindow.Handle);
            }
            
            // Register hotkey
            if (settings.UseHotKey)
            {
                RegisterCurrentHotKey();
            }

            // System tray
            trayIcon = new NotifyIcon();
            trayIcon.Icon = SystemIcons.Application;
            UpdateTrayTooltip();
            trayIcon.Visible = true;

            // Context menu
            UpdateContextMenu();
            
            // Double click to open settings
            trayIcon.DoubleClick += (s, e) => ShowSettings();

            // Timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();

            // Startup notification
            if (settings.ShowNotifications)
            {
                string message = Lang.Started + "\n";
                
                if (settings.AutoDetectMode)
                {
                    message += Lang.AutoDetectEnabled + "\n";
                }
                
                if (settings.UseHotKey)
                {
                    message += string.Format(Lang.HotkeyInfo, settings.GetHotKeyDescription());
                }
                
                trayIcon.ShowBalloonTip(
                    3000,
                    Lang.AppName,
                    message,
                    ToolTipIcon.Info
                );
            }
        }
        
        static void RegisterCurrentHotKey()
        {
            // Unregister old
            UnregisterHotKey(msgWindow.Handle, 1);
            
            // Register new
            if (!RegisterHotKey(msgWindow.Handle, 1, settings.GetModifiers(), settings.GetVirtualKey()))
            {
                MessageBox.Show(
                    string.Format(Lang.HotkeyConflict, settings.GetHotKeyDescription()),
                    Lang.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
        
        static void UpdateTrayTooltip(string extraInfo = null)
        {
            string tooltip = Lang.AppName;
            
            if (!string.IsNullOrEmpty(extraInfo))
            {
                tooltip = extraInfo;
            }
            else if (settings.AutoDetectMode && settings.UseHotKey)
            {
                tooltip = Lang.AppName;
            }
            else if (settings.AutoDetectMode)
            {
                tooltip = Lang.Language == "zh-TW" ? "CLI 截圖助手 - 自動偵測" : "CLI Screenshot Helper - Auto";
            }
            else if (settings.UseHotKey)
            {
                tooltip = Lang.AppName + " - " + settings.GetHotKeyDescription();
            }
            
            // Ensure tooltip is within 64 character limit
            if (tooltip.Length > 63)
            {
                tooltip = tooltip.Substring(0, 60) + "...";
            }
            
            trayIcon.Text = tooltip;
        }
        
        static void ShowSettings()
        {
            using (SettingsForm form = new SettingsForm(settings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Reload settings
                    settings = Settings.Load();
                    
                    // Re-register hotkey
                    if (settings.UseHotKey)
                    {
                        RegisterCurrentHotKey();
                    }
                    else
                    {
                        UnregisterHotKey(msgWindow.Handle, 1);
                    }
                    
                    // Update clipboard monitoring
                    if (settings.AutoDetectMode && nextClipboardViewer == IntPtr.Zero)
                    {
                        nextClipboardViewer = SetClipboardViewer(msgWindow.Handle);
                    }
                    else if (!settings.AutoDetectMode && nextClipboardViewer != IntPtr.Zero)
                    {
                        ChangeClipboardChain(msgWindow.Handle, nextClipboardViewer);
                        nextClipboardViewer = IntPtr.Zero;
                    }
                    
                    // Update tray tooltip
                    UpdateTrayTooltip();
                    
                    // Update context menu with new language
                    UpdateContextMenu();
                    
                    if (settings.ShowNotifications)
                    {
                        trayIcon.ShowBalloonTip(
                            2000,
                            Lang.SettingsUpdated,
                            settings.UseHotKey ? string.Format(Lang.NewHotkey, settings.GetHotKeyDescription()) : "",
                            ToolTipIcon.Info
                        );
                    }
                }
            }
        }

        static void Timer_Tick(object sender, EventArgs e)
        {
            if (!isRunning || !expectingScreenshot || !isHotKeyTriggered) return;

            // For hotkey mode, wait at least 1.5 seconds
            if ((DateTime.Now - lastScreenshotTime).TotalSeconds < 1.5) return;

            // Timeout after 60 seconds
            if ((DateTime.Now - lastScreenshotTime).TotalSeconds > 60)
            {
                expectingScreenshot = false;
                isHotKeyTriggered = false;
                UpdateTrayTooltip();
                return;
            }

            try
            {
                if (Clipboard.ContainsImage())
                {
                    Image image = Clipboard.GetImage();
                    if (image != null)
                    {
                        expectingScreenshot = false;
                        isHotKeyTriggered = false;

                        // Generate filename and path
                        string filename = string.Format(settings.FileNameFormat, DateTime.Now) + ".png";
                        string filepath = Path.Combine(settings.SavePath, filename);

                        // Save image
                        image.Save(filepath, ImageFormat.Png);
                        
                        // Update hash
                        lastImageHash = GetImageHash(image);
                        image.Dispose();

                        // Ensure file is written
                        Thread.Sleep(300);

                        // Copy path to clipboard
                        if (msgWindow.InvokeRequired)
                        {
                            msgWindow.Invoke(new Action(() => {
                                Clipboard.Clear();
                                Clipboard.SetText(filepath);
                            }));
                        }
                        else
                        {
                            Clipboard.Clear();
                            Clipboard.SetText(filepath);
                        }

                        // Show notification
                        if (settings.ShowNotifications)
                        {
                            trayIcon.ShowBalloonTip(
                                3000,
                                Lang.ScreenshotSaved,
                                string.Format(Lang.PathCopied, filepath),
                                ToolTipIcon.Info
                            );
                        }
                        
                        if (settings.PlaySound)
                        {
                            System.Media.SystemSounds.Asterisk.Play();
                        }
                        
                        UpdateTrayTooltip();
                    }
                }
            }
            catch (Exception ex)
            {
                if (settings.ShowNotifications)
                {
                    trayIcon.ShowBalloonTip(
                        3000,
                        Lang.Error,
                        Lang.ProcessingError,
                        ToolTipIcon.Error
                    );
                }
            }
        }
        
        static string GetImageHash(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(imageBytes);
                    return Convert.ToBase64String(hash);
                }
            }
        }

        static void UpdateContextMenu()
        {
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(Lang.OpenFolder, (s, e) => {
                System.Diagnostics.Process.Start(settings.SavePath);
            });
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add(Lang.Settings, (s, e) => {
                ShowSettings();
            });
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add(Lang.Exit, (s, e) => {
                isRunning = false;
                msgWindow.Close();
                Application.Exit();
            });
            trayIcon.ContextMenu = contextMenu;
        }
        
        static void Cleanup()
        {
            if (msgWindow != null && msgWindow.Handle != IntPtr.Zero)
            {
                UnregisterHotKey(msgWindow.Handle, 1);
                
                if (nextClipboardViewer != IntPtr.Zero)
                {
                    ChangeClipboardChain(msgWindow.Handle, nextClipboardViewer);
                }
            }

            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
            }
        }
    }
}