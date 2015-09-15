@echo off

set gbak="C:\Program Files\Firebird\Firebird_2_5\bin\gbak.exe"
set host=localhost
set SYSDBA_PWD=masterkey

set fbk_file=%~f1
set database_file=%~f2

if "%fbk_file%"=="" goto bad_params
if "%database_file%"=="" goto bad_params

if "%fbk_file%"=="%database_file%" (
@echo STOP. Имена исходного и целевого файлов совпадают.
exit /b 1
)

if not exist %fbk_file% (
@echo STOP. Исходный файл FBK не существует: %fbk_file%
exit /b 1
)

if exist %database_file% (
@echo STOP. Восстанавливаемая БД уже существует: %database_file%
exit /b 1
)


for /f "tokens=1-3 delims=:,./- " %%I in ("%DATE%") do set BKUP_DATE=%%K-%%J-%%I
for /f "tokens=1-2 delims=:,./- " %%I in ("%TIME%") do set BKUP_TIME=%%I-%%J
set DT=%BKUP_DATE%_%BKUP_TIME%

set log_file=%database_file%[restore_log_%DT%].txt
if exist %log_file% del %log_file%

@echo.
@echo Запуск restore:
@echo     %fbk_file%
@echo     %host%:%database_file%
@echo Лог пишется в файл:
@echo     %log_file%
@echo Нажмите Ctrl+C, чтобы прервать процесс.

%gbak% -c -v -z -y %log_file% -page_size 16384 -user sysdba -password %SYSDBA_PWD% %fbk_file% %host%:%database_file%

date /t >> %log_file%
time /t >> %log_file%
goto end


:bad_params
@echo Неверно заданы входные параметры. Пример:
@echo     restore.bat database.fbk database.fdb
goto end

:end
