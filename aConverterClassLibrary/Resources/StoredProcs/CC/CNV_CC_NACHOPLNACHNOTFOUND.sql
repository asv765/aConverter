SET TERM ^ ;

create or alter procedure CNV$CC_NACHOPLNACHNOTFOUND (
    ACTIONTYPE smallint)
returns (
    LSHET varchar(10),
    SERVICECD integer,
    YEAR_ smallint,
    MONTH_ smallint,
    FNATH numeric(18,4),
    PROCHL numeric(18,4))
as
BEGIN
  IF (actiontype = 0) THEN BEGIN
    FOR SELECT FIRST 1 lshet, servicecd, year_, month_, fnath, prochl
    FROM CNV$CC_NACHOPLNACHNOTFOUND(1)
     INTO :lshet, :servicecd, :year_, :month_, :fnath, :prochl
     DO BEGIN
         SUSPEND;
     END
  END
  ELSE IF (actiontype = 1) THEN BEGIN
    FOR SELECT lshet, servicecd, year_, month_,  fnath, prochl
    FROM cnv$nachopl n
    WHERE (n.fnath <> 0 OR n.prochl <> 0) AND NOT EXISTS
          (SELECT lshet FROM cnv$nach nc
           WHERE n.lshet = nc.lshet AND
                 n.servicecd = nc.servicecd AND
                 n.year_ = nc.year_ AND
                 n.month_ = nc.month_)
     INTO :lshet, :servicecd, :year_, :month_, :fnath, :prochl
         DO BEGIN
             SUSPEND;
         END
  END
  ELSE IF (actiontype = 2) THEN BEGIN
    UPDATE cnv$nachopl n SET fnath = 0, prochl = 0
    WHERE (n.fnath <> 0 OR n.prochl <> 0) AND NOT EXISTS
          (SELECT lshet FROM cnv$nach nc
           WHERE n.lshet = nc.lshet AND
                 n.servicecd = nc.servicecd AND
                 n.year_ = nc.year_ AND
                 n.month_ = nc.month_);
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLNACHNOTFOUND TO PROCEDURE CNV$CC_NACHOPLNACHNOTFOUND;

GRANT SELECT,UPDATE ON CNV$NACHOPL TO PROCEDURE CNV$CC_NACHOPLNACHNOTFOUND;

GRANT SELECT ON CNV$NACH TO PROCEDURE CNV$CC_NACHOPLNACHNOTFOUND;

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLNACHNOTFOUND TO SYSDBA;