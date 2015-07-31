SET TERM ^ ;

create or alter procedure CNV$CC_NACHYEARMONTH (
    ACTIONTYPE smallint)
returns (
    LSHET        VARCHAR(10),
    DOCUMENTCD   VARCHAR(20),
    MONTH_       INTEGER,
    YEAR_        INTEGER,
    MONTH2       INTEGER,
    YEAR2        INTEGER,
    FNATH        NUMERIC(18,4),
    PROCHL       NUMERIC(18,4),
    VOLUME       NUMERIC(18,4),
    REGIMCD      INTEGER,
    REGIMNAME    VARCHAR(50),
    SERVICECD    INTEGER,
    SERVICENAME  VARCHAR(50),
    DATE_VV      TIMESTAMP,
    TYPE_        INTEGER,
    DOCNAME      VARCHAR(150),
    DOCNUMBER    VARCHAR(10),
    DOCDATE      TIMESTAMP)
as
begin
  /* Тестирование, Диагностика */
  if (ACTIONTYPE = 0) then begin
     FOR SELECT FIRST 1 lshet, documentcd, month_, year_, month2, year2, fnath, prochl, volume, regimcd, regimname, date_vv, type_, servicecd, servicename, docname, docnumber, docdate
     FROM CNV$CC_NACHYEARMONTH(1)
     INTO :lshet, :documentcd, :month_, :year_, :month2, :year2, :fnath, :prochl, :volume, :regimcd, :regimname, :date_vv, :type_, :servicecd, :servicename, :docname, :docnumber, :docdate
     do begin
        suspend;
     end
  end
  else if (ACTIONTYPE = 1) then begin
     FOR SELECT lshet, documentcd, month_, year_, month2, year2, fnath, prochl, volume, regimcd, regimname, date_vv, type_, servicecd, servicename, docname, docnumber, docdate
     FROM CNV$NACH
     WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))
     INTO :lshet, :documentcd, :month_, :year_, :month2, :year2, :fnath, :prochl, :volume, :regimcd, :regimname, :date_vv, :type_, :servicecd, :servicename, :docname, :docnumber, :docdate
     do begin
        suspend;
     end
  end
  else
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
end
^

SET TERM ; ^

GRANT SELECT ON CNV$NACH TO PROCEDURE CNV$CC_NACHYEARMONTH;

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHYEARMONTH TO SYSDBA;
