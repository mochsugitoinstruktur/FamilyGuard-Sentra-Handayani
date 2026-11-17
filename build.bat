@echo off
echo Mengompilasi FamilyGuard...
set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe

if not exist "%CSC%" (
    set CSC=C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe
)

"%CSC%" /nologo /target:winexe /out:"%~dp0FamilyGuard.exe" /win32manifest:"%~dp0app.manifest" /r:System.dll,System.Windows.Forms.dll,System.Drawing.dll,System.Core.dll "%~dp0Program.cs" "%~dp0MainForm.cs" "%~dp0DnsManager.cs" "%~dp0HostsManager.cs" "%~dp0ConfigManager.cs" "%~dp0PinDialog.cs"

if %ERRORLEVEL% equ 0 (
    echo.
    echo ========================================================
    echo Berhasil mengompilasi FamilyGuard.exe!
    echo Lokasi: %~dp0FamilyGuard.exe
    echo ========================================================
) else (
    echo.
    echo [ERROR] Gagal melakukan kompilasi. Periksa pesan error di atas.
)
