SET TERM ^ ;

create or alter procedure CNV$CC_NACHOPLNACHMISMATCH (
    ACTIONTYPE smallint)
returns (
    LSHET varchar(10),
    SERVICECD smallint,
    YEAR_ smallint,
    MONTH_ smallint,
    NACHOPL_FNATH numeric(18,4),
    NACH_FNATH numeric(18,4),
    NACHOPL_PROCHL numeric(18,4),
    NACH_PROCHL numeric(18,4))
as
BEGIN
  IF (actiontype = 0) THEN BEGIN
    FOR SELECT FIRST 1 lshet, servicecd, year_, month_, nachopl_fnath, nach_fnath, nachopl_prochl, nach_prochl
    FROM cnv$cc_nachoplnachmismatch(1)
    INTO :lshet, :servicecd, :year_, :month_, :nachopl_fnath, :nach_fnath, :nachopl_prochl, :nach_prochl
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 1) THEN BEGIN
    FOR SELECT n.lshet, n.servicecd, n.year_, n.month_, n.fnath, SUM(nc.fnath) as nach_fnath, n.prochl, SUM(nc.prochl) as nach_prochl
    FROM cnv$nachopl n INNER JOIN cnv$nach nc
        ON n.lshet = nc.lshet AND
        n.servicecd = nc.servicecd AND
        n.year_ = EXTRACT(YEAR FROM nc.date_vv) AND
        n.month_ = EXTRACT(MONTH FROM nc.date_vv)
    GROUP BY n.lshet, n.servicecd, n.servicename, n.month_, n.year_, n.fnath, n.prochl
    HAVING n.fnath <> SUM(nc.fnath) OR n.prochl <> SUM(nc.prochl)
    INTO :lshet, :servicecd, :year_, :month_, :nachopl_fnath, :nach_fnath, :nachopl_prochl, :nach_prochl
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 2) THEN BEGIN
    UPDATE cnv$nachopl n SET n.fnath = (SELECT SUM(nc1.fnath)
    FROM cnv$nach nc1
    WHERE n.lshet = nc1.lshet AND
         n.servicecd = nc1.servicecd AND
         n.year_ = EXTRACT(YEAR FROM nc1.date_vv) AND
         n.month_ = EXTRACT(MONTH FROM nc1.date_vv))
    WHERE EXISTS (SELECT lshet FROM cnv$NACH nc2
           WHERE n.lshet = nc2.lshet AND
                 n.servicecd = nc2.servicecd AND
                 n.year_ = EXTRACT(YEAR FROM nc2.date_vv) AND
                 n.month_ = EXTRACT(MONTH FROM nc2.date_vv));

    UPDATE cnv$nachopl n SET n.prochl = (SELECT SUM(nc1.prochl)
    FROM cnv$nach nc1
    WHERE n.lshet = nc1.lshet AND
         n.servicecd = nc1.servicecd AND
         n.year_ = EXTRACT(YEAR FROM nc1.date_vv) AND
         n.month_ = EXTRACT(MONTH FROM nc1.date_vv))
    WHERE EXISTS (SELECT lshet FROM cnv$NACH nc2
           WHERE n.lshet = nc2.lshet AND
                 n.servicecd = nc2.servicecd AND
                 n.year_ = EXTRACT(YEAR FROM nc2.date_vv) AND
                 n.month_ = EXTRACT(MONTH FROM nc2.date_vv));
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLNACHMISMATCH TO PROCEDURE CNV$CC_NACHOPLNACHMISMATCH;

GRANT SELECT,UPDATE ON CNV$NACHOPL TO PROCEDURE CNV$CC_NACHOPLNACHMISMATCH;

GRANT SELECT ON CNV$NACH TO PROCEDURE CNV$CC_NACHOPLNACHMISMATCH;

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLNACHMISMATCH TO PROCEDURE CNV$CC_NACHOPLNACHMISMATCH;
GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLNACHMISMATCH TO SYSDBA;