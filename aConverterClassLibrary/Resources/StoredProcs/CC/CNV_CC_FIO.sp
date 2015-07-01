create procedure CNV$CC_FIO (
    ACTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    F varchar(50),
    I varchar(50),
    O varchar(50))
as
declare variable OCOUNT integer = 0;
declare variable ICOUNT integer = 0;
declare variable FCOUNT integer = 0;
begin
  /* Тестирование */
  if (ACTIONTYPE = 0) then begin
     SELECT COUNT(*) FROM cnv$abonent WHERE (F IS NOT NULL) AND (F <> '') INTO :FCOUNT;
     SELECT COUNT(*) FROM cnv$abonent WHERE (I IS NOT NULL) AND (I <> '') INTO :ICOUNT;
     SELECT COUNT(*) FROM cnv$abonent WHERE (O IS NOT NULL) AND (O <> '') INTO :OCOUNT;
     if (FCOUNT <> 0 AND ICOUNT = 0 AND OCOUNT = 0) then
        SELECT FIRST 1 LSHET, F, I, O FROM cnv$abonent INTO :lshet, :F, :I, :O;
     suspend;
  end
  /* Диагностика */
  else if (ACTIONTYPE = 1) then begin
     FOR SELECT FIRST 1 LSHET, F, I, O FROM cnv$abonent INTO :lshet, :F, :I, :O do
       suspend;
  end
  else
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
  /* Исправление */
end
