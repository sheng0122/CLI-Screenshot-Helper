@echo off
echo Compiling CLI Screenshot Helper (Bilingual Version)...
echo ==================================================
echo.

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /target:winexe /out:CLIScreenshotHelper.exe /reference:System.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll /reference:System.Xml.dll CLIScreenshotHelper.cs

if %errorlevel% equ 0 (
    echo.
    echo Compiled successfully!
    echo Output: CLIScreenshotHelper.exe
    echo.
    echo === FEATURES ===
    echo.
    echo 1. BILINGUAL SUPPORT:
    echo    - Traditional Chinese (default)
    echo    - English
    echo    - Switch in Settings/Language tab
    echo.
    echo 2. TWO MODES:
    echo    - Auto-detect: Monitor ALL screenshots
    echo    - Hotkey: Custom trigger (Win+Shift+D)
    echo.
    echo 3. NO MANUAL MODE:
    echo    - Removed for better user experience
    echo    - More intuitive operation
    echo.
    echo 4. RENAMED:
    echo    - Now called "CLI Screenshot Helper"
    echo    - Better describes its purpose
    echo.
) else (
    echo.
    echo Compilation failed!
)
pause