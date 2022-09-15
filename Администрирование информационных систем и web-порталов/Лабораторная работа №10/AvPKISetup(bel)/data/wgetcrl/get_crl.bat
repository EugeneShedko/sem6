@echo off

set CURDIR=%~dp0
rem set PX_USER=--proxy-user=
rem set PX_PASS=--proxy-password=
rem set http_proxy=http://10.1.0.50:3128
rem set https_proxy=http://10.1.0.50:3128

del %TEMP%\*.crl >NUL: 2>&1

"%CURDIR%wget" --no-check-certificate -q -T 30 -O %TEMP%\kuc.crl %PX_USER% %PX_PASS% https://nces.by/wp-content/uploads/certificates/pki/kuc.crl
if ERRORLEVEL 1 goto label_1
"%CURDIR%AvCmUt4.exe" -C %TEMP%\kuc.crl >NUL

:label_1
"%CURDIR%wget" --no-check-certificate -q -T 30 -O %TEMP%\ruc.crl %PX_USER% %PX_PASS% https://nces.by/wp-content/uploads/certificates/pki/ruc.crl
if ERRORLEVEL 1 goto label_2
"%CURDIR%AvCmUt4.exe" -C %TEMP%\ruc.crl >NUL

:label_2
"%CURDIR%wget" --no-check-certificate -q -T 30 -O %TEMP%\cas_ruc.crl %PX_USER% %PX_PASS% https://nces.by/wp-content/uploads/certificates/pki/cas_ruc.crl
if ERRORLEVEL 1 goto end
"%CURDIR%AvCmUt4.exe" -C %TEMP%\cas_ruc.crl >NUL

:end

del c:%HOMEPATH%\Desktop\AvCmUt4.log

rem AvCmUt4.exe -C %TEMP%\kuc.crl >NUL
rem AvCmUt4.exe -C %TEMP%\ruc.crl >NUL
rem AvCmUt4.exe -C %TEMP%\mns_ca.crl >NUL
rem AvCmUt4.exe -C %TEMP%\mns-ra.crl >NUL
rem AvCmUt4.exe -C %TEMP%\rup.crl >NUL

rem "c:\Program Files\Avest\AvPCM_NCES\MngCert.exe" kuc.crl /importcrl /silentrun
rem "c:\Program Files\Avest\AvPCM_NCES\MngCert.exe" ruc.crl /importcrl /silentrun
rem "c:\Program Files\Avest\AvPCM_MNS\MngCert.exe" mns_ca.crl /importcrl /silentrun
rem "c:\Program Files\Avest\AvPCM_MNS\MngCert.exe" mns-ra.crl /importcrl /silentrun
rem "c:\Program Files\Avest\AvPCM_MNS\MngCert.exe" rup.crl /importcrl /silentrun