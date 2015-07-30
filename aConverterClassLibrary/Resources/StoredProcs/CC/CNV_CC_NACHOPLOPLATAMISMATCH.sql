SET TERM ^ ;

create or alter procedure CNV$CC_NACHOPLOPLATAMISMATCH (
    ACTIONTYPE smallint)
returns (
    LSHET varchar(10),
    SERVICECD smallint,
    YEAR_ smallint,
    MONTH_ smallint,
    NACHOPL_OPLATA numeric(18,4),
    OPLATA_SUM numeric(18,4))
as
BEGIN
  IF (actiontype = 0) THEN BEGIN
    FOR SELECT FIRST 1 lshet, servicecd, year_, month_, nachopl_oplata, oplata_sum
    FROM cnv$cc_nachoploplatamismatch(1)
    INTO :lshet, :servicecd, :year_, :month_, :nachopl_oplata, :oplata_sum
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 1) THEN BEGIN
    FOR SELECT n.lshet, n.servicecd, n.year_, n.month_, n.oplata, SUM(o.summa)
    FROM cnv$nachopl n INNER JOIN cnv$oplata o
        ON n.lshet = o.lshet AND
        n.servicecd = o.servicecd AND
        n.year_ = EXTRACT(YEAR FROM o.date_vv) AND
        n.month_ = EXTRACT(MONTH FROM o.date_vv)
    GROUP BY n.lshet, n.servicecd, n.servicenam, n.month_, n.year_, n.oplata
    HAVING n.oplata <> SUM(o.summa)
    INTO :lshet, :servicecd, :year_, :month_, :nachopl_oplata, :oplata_sum
    DO BEGIN
        SUSPEND;
    END
  END
  ELSE IF (actiontype = 2) THEN BEGIN
    UPDATE cnv$nachopl n SET n.oplata = (SELECT SUM(summa)
    FROM cnv$oplata o
    WHERE n.lshet = o.lshet AND
         n.servicecd = o.servicecd AND
         n.year_ = EXTRACT(YEAR FROM o.date_vv) AND
         n.month_ = EXTRACT(MONTH FROM o.date_vv))
    WHERE EXISTS (SELECT lshet FROM cnv$oplata o
           WHERE n.lshet = o.lshet AND
                 n.servicecd = o.servicecd AND
                 n.year_ = EXTRACT(YEAR FROM o.date_vv) AND
                 n.month_ = EXTRACT(MONTH FROM o.date_vv));
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH TO PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH;

GRANT SELECT,UPDATE ON CNV$NACHOPL TO PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH;

GRANT SELECT ON CNV$OPLATA TO PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH;

GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH TO PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH;
GRANT EXECUTE ON PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH TO SYSDBA;