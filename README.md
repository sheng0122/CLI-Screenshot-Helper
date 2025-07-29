# CLI Screenshot Helper

[English](#english) | [中文](#中文)

## English

A Windows tool that automatically saves screenshots and copies file paths to clipboard - bringing Mac-like screenshot workflow to Windows.

Originally created to solve the frustration of sharing screenshots with Claude Code - Windows users couldn't directly paste file paths after taking screenshots like Mac users could.

### Problem
- Windows: Screenshot → Copy image only → Need to manually save → Find file → Copy path
- Mac: Screenshot → Auto-save → Path in clipboard → Done

### Features
- Auto-detect any screenshot (Win+Shift+S, Print Screen, etc.)
- Auto-save to custom folder
- Auto-copy file path to clipboard
- Custom hotkey support (default: Win+Shift+D)
- Bilingual interface (Chinese/English)
- System tray application

### Installation
1. Download `CLIScreenshotHelper.exe`
2. Run the executable
3. Configure settings via system tray icon

### Usage
1. Take a screenshot using any method
2. File automatically saved
3. Path already in clipboard - just paste!

### Requirements
- Windows 10/11
- .NET Framework 4.0+ (pre-installed on most Windows)

### Build from Source
```cmd
compile-cli-helper.cmd
```

### License
MIT

---

## 中文

自動儲存截圖並複製檔案路徑的 Windows 工具 - 讓 Windows 擁有 Mac 般的截圖體驗。

最初是為了解決使用 Claude Code 時的困擾而開發 - Windows 無法像 Mac 用戶一樣在截圖後直接貼上檔案路徑。

### 問題
- Windows：截圖 → 只複製圖片 → 手動儲存 → 找檔案 → 複製路徑
- Mac：截圖 → 自動儲存 → 路徑在剪貼簿 → 完成

### 功能
- 自動偵測任何截圖（Win+Shift+S、Print Screen 等）
- 自動儲存到指定資料夾
- 自動複製檔案路徑到剪貼簿
- 自訂快捷鍵（預設：Win+Shift+D）
- 雙語介面（中文/英文）
- 系統托盤程式

### 安裝
1. 下載 `CLIScreenshotHelper.exe`
2. 執行程式
3. 透過系統托盤圖示設定

### 使用
1. 使用任何方式截圖
2. 檔案自動儲存
3. 路徑已在剪貼簿 - 直接貼上！

### 系統需求
- Windows 10/11
- .NET Framework 4.0+（大部分 Windows 已預裝）

### 從原始碼建置
```cmd
compile-cli-helper.cmd
```

### 授權
MIT