@echo off

set gbak="C:\Program Files\Firebird\Firebird_2_5\bin\gbak.exe"
set host=localhost
set SYSDBA_PWD=masterkey

set fbk_file=%~f2
set database_file=%~f1

if "%fbk_file%"=="" goto bad_params
if "%database_file%"=="" goto bad_params

if "%fbk_file%"=="%database_file%" (
@echo STOP. ����� ��室���� � 楫����� 䠩��� ᮢ������.
exit /b 1
)

if exist %fbk_file% (
@echo STOP. ������� 䠩� FBK 㦥 �������: %fbk_file%
exit /b 1
)

if not exist %database_file% (
@echo STOP. ��室�� 䠩� �� �� �������: %database_file%
exit /b 1
)


for /f "tokens=1-3 delims=:,./- " %%I in ("%DATE%") do set BKUP_DATE=%%K-%%J-%%I
for /f "tokens=1-2 delims=:,./- " %%I in ("%TIME%") do set BKUP_TIME=%%I-%%J
set DT=%BKUP_DATE%_%BKUP_TIME%

set log_file=%fbk_file%[backup_log_%DT%].txt
if exist %log_file% del %log_file%

@echo.
@echo ����� backup:
@echo     %host%:%database_file%
@echo     %fbk_file%
@echo ��� ������ � 䠩�:                                                    
@echo     %log_file%
@echo ������ Ctrl+C, �⮡� ��ࢠ�� �����.

%gbak% -b -v -z -y %log_file% -user sysdba -password %SYSDBA_PWD% %host%:%database_file% %fbk_file%

date /t >> %log_file%
time /t >> %log_file%
goto end


:bad_params
@echo ����୮ ������ �室�� ��ࠬ����. �ਬ��:
@echo     backup.bat database.fdb database.fbk
goto end

:end
