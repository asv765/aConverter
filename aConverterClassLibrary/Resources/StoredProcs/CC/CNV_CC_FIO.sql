SET TERM ^ ;

create or alter procedure CNV$CC_FIO (
    ACTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    F varchar(50),
    I varchar(50),
    O varchar(50))
as
declare variable OCOUNT integer = 0;
declare variable ICOUNT integer = 0;
declare variable FCOUNT integer = 0;
BEGIN
  
  IF (actiontype = 0) THEN BEGIN
     SELECT COUNT(*) FROM cnv$abonent WHERE (f IS NOT NULL) AND (f <> '') INTO :fcount;
     SELECT COUNT(*) FROM cnv$abonent WHERE (i IS NOT NULL) AND (i <> '') INTO :icount;
     SELECT COUNT(*) FROM cnv$abonent WHERE (o IS NOT NULL) AND (o <> '') INTO :ocount;
     IF (fcount <> 0 AND icount = 0 AND ocount = 0) THEN BEGIN
        SELECT FIRST 1 lshet, f, i, o FROM cnv$abonent INTO :lshet, :f, :i, :o;
        SUSPEND;
     END
  END
  
  ELSE IF (actiontype = 1) THEN BEGIN
     FOR SELECT lshet, f, i, o
        FROM cnv$abonent
        INTO :lshet, :f, :i, :o DO
       SUSPEND;
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
  
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CC_FIO;

GRANT EXECUTE ON PROCEDURE CNV$CC_FIO TO SYSDBA;