create procedure CNV$CC_NOTUNIQUELSHET (
    ACTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    COUNT_ integer)
as
begin
  /* Тестирование, Диагностика */
  if (ACTIONTYPE = 0 or ACTIONTYPE = 1 ) then begin
     for select lshet, count(*) as count_ from cnv$abonent group by lshet having count(*) > 1
         into :LSHET, :COUNT_
     do begin
         suspend;
     end
  end
  else
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
end
