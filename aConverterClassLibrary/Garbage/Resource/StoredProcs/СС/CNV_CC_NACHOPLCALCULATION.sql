SET TERM ^ ;

create or alter procedure CNV$CC_NACHOPLCALCULATION (
    ACTIONTYPE smallint)
returns (
    LSHET varchar(10),
    MONTH_ integer,
    YEAR_ integer,
    MONTH2 integer,
    YEAR2 integer,
    BDEBET numeric(18,4),
    FNATH numeric(18,4),
    PROCHL numeric(18,4),
    OPLATA numeric(18,4),
    EDEBET numeric(18,4),
    SERVICECD integer,
    SERVICENAME varchar(50))
as
begin
  /* Тестирование, Диагностика */
  if (ACTIONTYPE = 0) then begin
     FOR SELECT FIRST 1 lshet, month_, year_, month2, year2, bdebet, fnath, prochl, oplata, edebet, servicecd, servicename
     FROM CNV$NACHOPL
     WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)
     INTO :lshet, :month_, :year_, :month2, :year2, :bdebet, :fnath, :prochl, :oplata, :edebet, :servicecd, :servicename
     do begin
        suspend;
     end
  end
  else if (ACTIONTYPE = 1) then begin
     FOR SELECT lshet, month_, year_, month2, year2, bdebet, fnath, prochl, oplata, edebet, servicecd, servicename
     FROM CNV$NACHOPL
     WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)
     INTO :lshet, :month_, :year_, :month2, :year2, :bdebet, :fnath, :prochl, :oplata, :edebet, :servicecd, :servicename
     do begin
        suspend;
     end
  end
  else
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
end
^

SET TERM ; ^

GRANT SELECT ON CNV$NACHOPL TO PROCEDURE CNV$CC_NACHOPLCALCULATION;

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLCALCULATION TO SYSDBA;
