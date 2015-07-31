SET TERM ^ ;

create or alter procedure CNV$CC_LSHETLENGTH (
    ACTIONTYPE smallint = 0)
returns (
    LSHETLENGTH integer,
    COUNT_ integer)
as
begin
  /* Тестирование, Диагностика */
  if (ACTIONTYPE = 0 or ACTIONTYPE = 1 ) then begin
     for SELECT CHAR_LENGTH(LSHET) AS LSHETLENGTH, COUNT(*) AS COUNT_
        FROM CNV$ABONENT
        GROUP BY CHAR_LENGTH(LSHET)
        ORDER BY CHAR_LENGTH(LSHET)
        into :LSHETLENGTH, :COUNT_
     do begin
        suspend;
     end
  end
  else
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
end^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CC_LSHETLENGTH;

GRANT EXECUTE ON PROCEDURE CNV$CC_LSHETLENGTH TO SYSDBA;