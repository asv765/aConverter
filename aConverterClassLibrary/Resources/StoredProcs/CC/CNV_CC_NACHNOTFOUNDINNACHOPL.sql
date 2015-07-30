SET TERM ^ ;

create or alter procedure CNV$CC_NACHNOTFOUNDINNACHOPL (
    ACTIONTYPE smallint)
returns (
    LSHET varchar(10),
    DOCUMENTCD varchar(20),
    MONTH_ smallint,
    YEAR_ smallint,
    FNATH numeric(18,4),
    PROCHL numeric(18,4),
    REGIMCD integer,
    REGIMNAME varchar(50),
    DATE_VV timestamp,
    SERVICECD integer,
    SERVICENAME varchar(50),
    TYPE_ integer)
as
BEGIN
  IF (actiontype = 0) THEN BEGIN
    FOR SELECT FIRST 1 lshet, documentcd, month_, year_, fnath, prochl, date_vv, regimcd, regimname, servicecd, servicename, type_
    FROM cnv$cc_NACHNOTFOUNDINNACHOPL(1)
    INTO :lshet, :documentcd, :month_, :year_, :fnath, :prochl, :date_vv, :regimcd, :regimname, :servicecd, :servicename, :type_
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 1) THEN BEGIN
    FOR SELECT lshet, documentcd, month_, year_, fnath, prochl, date_vv, regimcd, regimname, servicecd, servicename, type_
        FROM cnv$nach nc
        WHERE (nc.fnath <> 0 OR nc.prochl <> 0) AND NOT EXISTS
           (SELECT lshet FROM cnv$nachopl n
            WHERE n.lshet = nc.lshet AND
               n.servicecd = nc.servicecd AND
               n.year_ = nc.year_ AND
               n.month_ = nc.month_)
         INTO :lshet, :documentcd, :month_, :year_, :fnath, :prochl, :date_vv, :regimcd, :regimname, :servicecd, :servicename, :type_
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 2) THEN BEGIN
    DELETE FROM cnv$nach nc
    WHERE (nc.fnath <> 0 OR nc.prochl <> 0) AND NOT EXISTS
          (SELECT lshet FROM cnv$nachopl n
           WHERE n.lshet = nc.lshet AND
                 n.servicecd = nc.servicecd AND
                 n.year_ = nc.year_ AND
                 n.month_ = nc.month_);
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHNOTFOUNDINNACHOPL TO PROCEDURE CNV$CC_NACHNOTFOUNDINNACHOPL;

GRANT SELECT,DELETE ON CNV$NACH TO PROCEDURE CNV$CC_NACHNOTFOUNDINNACHOPL;

GRANT SELECT ON CNV$NACHOPL TO PROCEDURE CNV$CC_NACHNOTFOUNDINNACHOPL;

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHNOTFOUNDINNACHOPL TO SYSDBA;
