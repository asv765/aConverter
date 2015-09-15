if exist %1.fbk del %1.fbk
call backup.bat %1 %1.fbk
if exist %1.bak delete %1.bak
ren %1 %1.bak
call restore.bat %1.fbk %1

