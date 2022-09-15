@echo on
chcp 1251

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"
echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\Desktop\Скачать СОС ГосСУОК.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "c:\Program Files\Avest\AvPCM_nces\get_crl.bat" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%
cscript /nologo %SCRIPT%
del %SCRIPT% 

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"
echo sLinkFile = "%USERPROFILE%\Рабочий стол\Скачать СОС ГосСУОК.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "c:\Program Files\Avest\AvPCM_nces\get_crl.bat" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%
cscript /nologo %SCRIPT%
del %SCRIPT% 

goto end

:end

del %USERPROFILE%\Desktop\1.txt
del "%USERPROFILE%\Рабочий стол\1.txt"

rem wmic.exe process where name="explorer.exe" call TERMINATE