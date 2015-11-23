SET TERM ^ ;

create or alter procedure CNV$CNV_02100_EXTLSHETS (
    EXTORGCD4EXTLSHET integer,
    EXTORGCD4EXTLSHET2 integer)
as
declare variable LSHET varchar(10);
declare variable EXTLSHET varchar(20);
BEGIN
  FOR SELECT lshet, extlshet
    FROM cnv$abonent
    WHERE (extlshet IS NOT NULL)
    INTO :lshet, :extlshet DO BEGIN
    UPDATE OR INSERT INTO extorgaccounts (extorgcd, lshet, extlshet) VALUES (:extorgcd4extlshet, :lshet, :extlshet);
  END
  FOR SELECT lshet, extlshet2
    FROM cnv$abonent
    WHERE (extlshet2 IS NOT NULL)
    INTO :lshet, :extlshet DO BEGIN
    UPDATE OR INSERT INTO extorgaccounts (extorgcd, lshet, extlshet) VALUES (:extorgcd4extlshet2, :lshet, :extlshet);
  END
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_02100_EXTLSHETS;

GRANT SELECT,INSERT,UPDATE ON EXTORGACCOUNTS TO PROCEDURE CNV$CNV_02100_EXTLSHETS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_02100_EXTLSHETS TO SYSDBA;