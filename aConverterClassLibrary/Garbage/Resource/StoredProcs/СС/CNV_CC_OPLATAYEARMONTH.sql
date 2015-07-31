SET TERM ^ ;

create or alter procedure CNV$CC_OPLATAYEARMONTH (
    ACTIONTYPE smallint)
returns (
    LSHET        VARCHAR(10),
    DOCUMENTCD   VARCHAR(20),
    MONTH_       INTEGER,
    YEAR_        INTEGER,
    SUMMA        NUMERIC(18,2),
    DATE_        TIMESTAMP,
    DATE_VV      TIMESTAMP,
    SOURCECD     INTEGER,
    SOURCENAME   VARCHAR(50),
    SERVICECD    INTEGER,
    SERVICENAME  VARCHAR(50))
as
begin
  /* Тестирование, Диагностика */
  if (ACTIONTYPE = 0) then begin
     FOR SELECT FIRST 1 lshet, documentcd, month_, year_, summa, date_, date_vv, sourcecd, sourcename, servicecd, servicename
     FROM CNV$CC_OPLATAYEARMONTH(1)
     INTO :lshet, :documentcd, :month_, :year_, :summa, :date_, :date_vv, :sourcecd, :sourcename, :servicecd, :servicename
     do begin
        suspend;
     end
  end
  else if (ACTIONTYPE = 1) then begin
     FOR SELECT lshet, documentcd, month_, year_, summa, date_, date_vv, sourcecd, sourcename, servicecd, servicename
     FROM CNV$OPLATA
     WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))
     INTO :lshet, :documentcd, :month_, :year_, :summa, :date_, :date_vv, :sourcecd, :sourcename, :servicecd, :servicename
     do begin
        suspend;
     end
  end
  else
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
end
^

SET TERM ; ^

GRANT SELECT ON CNV$OPLATA TO PROCEDURE CNV$CC_OPLATAYEARMONTH;

GRANT EXECUTE ON PROCEDURE CNV$CC_OPLATAYEARMONTH TO SYSDBA;
