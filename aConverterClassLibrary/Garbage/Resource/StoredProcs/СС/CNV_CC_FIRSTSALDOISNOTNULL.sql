SET TERM ^ ;

create or alter procedure CNV$CC_FIRSTSALDOISNOTNULL (
    ACTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    MONTH_ integer,
    YEAR_ integer,
    SERVICECD integer,
    BDEBET decimal(18,4))
as
BEGIN
  
  IF (actiontype = 0) THEN BEGIN
     FOR SELECT FIRST 1 lshet, servicecd, year_, month_,  bdebet
         FROM cnv$nachopl n1
         WHERE year_*100 + month_ = (SELECT MIN(year_*100+month_) FROM cnv$nachopl n2 WHERE n1.lshet = n2.lshet AND n1.servicecd = n2.servicecd) and
               bdebet <> 0
     INTO :lshet, :servicecd, :year_, :month_, :bdebet
     DO BEGIN
         SUSPEND;
     END
  END
  
  ELSE IF (actiontype = 1) THEN BEGIN
     FOR SELECT lshet, servicecd, year_, month_,  bdebet
         FROM cnv$nachopl n1
         WHERE year_*100 + month_ = (SELECT MIN(year_*100+month_) FROM cnv$nachopl n2 WHERE n1.lshet = n2.lshet AND n1.servicecd = n2.servicecd) and
               bdebet <> 0
     INTO :lshet, :servicecd, :year_, :month_, :bdebet
     DO BEGIN
         SUSPEND;
     END
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT SELECT ON CNV$NACHOPL TO PROCEDURE CNV$CC_FIRSTSALDOISNOTNULL;

GRANT EXECUTE ON PROCEDURE CNV$CC_FIRSTSALDOISNOTNULL TO SYSDBA;