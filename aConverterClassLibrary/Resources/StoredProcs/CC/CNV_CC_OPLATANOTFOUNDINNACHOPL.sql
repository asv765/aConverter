SET TERM ^ ;

create or alter procedure CNV$CC_OPLATANOTFOUNDINNACHOPL (
    ACTIONTYPE smallint)
returns (
    LSHET varchar(10),
    DOCUMENTCD varchar(20),
    MONTH_ smallint,
    YEAR_ smallint,
    SUMMA numeric(18,4),
    DATE_ timestamp,
    DATE_VV timestamp,
    SOURCECD integer,
    SOURCENAME varchar(50),
    SERVICECD integer,
    SERVICENAME varchar(50))
as
BEGIN
  IF (actiontype = 0) THEN BEGIN
    FOR SELECT FIRST 1 lshet, documentcd, month_, year_, summa, date_, date_vv, sourcecd, sourcename, servicecd, servicename
    FROM cnv$cc_oplatanotfoundinnachopl(1)
    INTO :lshet, :documentcd, :month_, :year_, :summa, :date_, :date_vv, :sourcecd, :sourcename, :servicecd, :servicename
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 1) THEN BEGIN
    FOR SELECT lshet, documentcd, month_, year_, summa, date_, date_vv, sourcecd, sourcename, servicecd, servicename
        FROM cnv$oplata o
        WHERE o.summa <> 0 AND NOT EXISTS
           (SELECT lshet FROM cnv$nachopl n
            WHERE n.lshet = o.lshet AND
               n.servicecd = o.servicecd AND
               n.year_ = EXTRACT(YEAR FROM o.Date_Vv) AND
               n.month_ = EXTRACT(MONTH FROM o.Date_Vv))
         INTO :lshet, :documentcd, :month_, :year_, :summa, :date_, :date_vv, :sourcecd, :sourcename, :servicecd, :servicename
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 2) THEN BEGIN
    DELETE FROM cnv$oplata o
    WHERE o.summa <> 0 AND NOT EXISTS
          (SELECT lshet FROM cnv$nachopl n
           WHERE n.lshet = o.lshet AND
                 n.servicecd = o.servicecd AND
                 n.year_ = EXTRACT(YEAR FROM o.Date_Vv) AND
                 n.month_ = EXTRACT(MONTH FROM o.Date_Vv));
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT EXECUTE ON PROCEDURE CNV$CC_OPLATANOTFOUNDINNACHOPL TO PROCEDURE CNV$CC_OPLATANOTFOUNDINNACHOPL;

GRANT SELECT,DELETE ON CNV$OPLATA TO PROCEDURE CNV$CC_OPLATANOTFOUNDINNACHOPL;

GRANT SELECT ON CNV$NACHOPL TO PROCEDURE CNV$CC_OPLATANOTFOUNDINNACHOPL;

GRANT EXECUTE ON PROCEDURE CNV$CC_OPLATANOTFOUNDINNACHOPL TO SYSDBA;
