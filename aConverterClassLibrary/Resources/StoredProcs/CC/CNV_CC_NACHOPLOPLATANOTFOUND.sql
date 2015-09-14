SET TERM ^ ;

CREATE OR ALTER procedure CNV$CC_NACHOPLOPLATANOTFOUND (
    ACTIONTYPE smallint)
returns (
    LSHET varchar(10),
    SERVICECD integer,
    YEAR_ smallint,
    MONTH_ smallint,
    OPLATA numeric(18,4))
as
BEGIN
  IF (actiontype = 0) THEN BEGIN
    FOR SELECT FIRST 1 lshet, servicecd, year_, month_,  oplata
    FROM CNV$CC_NACHOPLOPLATANOTFOUND(1)
     INTO :lshet, :servicecd, :year_, :month_, :oplata
     DO BEGIN
         SUSPEND;
     END
  END
  ELSE IF (actiontype = 1) THEN BEGIN
    FOR SELECT lshet, servicecd, year_, month_,  oplata
    FROM cnv$nachopl n
    WHERE n.oplata <> 0 AND NOT EXISTS
          (SELECT lshet FROM cnv$oplata o
           WHERE n.lshet = o.lshet AND
                 n.servicecd = o.servicecd AND
                 n.year_ = EXTRACT(YEAR FROM o.date_vv) AND
                 n.month_ = EXTRACT(MONTH FROM o.date_vv))
     INTO :lshet, :servicecd, :year_, :month_, :oplata
         DO BEGIN
             SUSPEND;
         END
  END
  ELSE IF (actiontype = 2) THEN BEGIN
    UPDATE cnv$nachopl n SET oplata = 0
    WHERE n.oplata <> 0 AND NOT EXISTS
          (SELECT lshet, servicecd, year_, month_,  oplata
    FROM cnv$nachopl n
    WHERE n.oplata <> 0 AND NOT EXISTS
          (SELECT lshet FROM cnv$oplata o
           WHERE n.lshet = o.lshet AND
                 n.servicecd = o.servicecd AND
                 n.year_ = EXTRACT(YEAR FROM o.date_vv) AND
                 n.month_ = EXTRACT(MONTH FROM o.date_vv)));
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END
^

SET TERM ; ^


