SET TERM ^ ;

create or alter procedure CNV$CNV_01500_SALDO (
    CURRENTYEAR integer,
    CURRENTMONTH integer)
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable SERVICECD integer;
declare variable LSHET varchar(10);
declare variable BDEBET numeric(18,4);
declare variable EDEBET numeric(18,4);
declare variable DATE_ date;
begin
  EXECUTE STATEMENT 'ALTER trigger saldocheckinsert inactive';
  EXECUTE STATEMENT 'ALTER trigger saldocheckupdate inactive';
  for SELECT YEAR_, MONTH_, SERVICECD, LSHET, BDEBET, EDEBET,
         CAST(('01.' || month_ || '.' || year_) as DATE) as date_
      FROM CNV$NACHOPL
      WHERE YEAR_*12 + MONTH_ < :CURRENTYEAR*12+:CURRENTMONTH AND (bdebet <> 0 OR edebet <> 0) /*-- в таблице saldo не должно быть записей где beginsumma = 0 и endsumma = 0*/
      ORDER BY YEAR_, MONTH_, LSHET, SERVICECD
      INTO :YEAR_, :month_,  :servicecd, :lshet,  :bdebet, :edebet,  :date_
  DO BEGIN
     UPDATE OR INSERT INTO SALDO (NYEAR, NMONTH, BALANCE_KOD, LSHET, DATA, BEGINSUMMA, ENDSUMMA)
        VALUES (:YEAR_, :MONTH_, :SERVICECD, :LSHET, :DATE_, :BDEBET, :EDEBET);
  END
  /*FOR SELECT YEAR_, MONTH_, SERVICECD  -- в таблице saldo не должно быть записей где beginsumma = 0 и endsumma = 0
    FROM cnv$NACHOPL
    WHERE YEAR_*12 + MONTH_ < :CurrentYear*12+:CurrentMonth AND ()
    GROUP BY YEAR_, MONTH_, SERVICECD
    ORDER BY YEAR_, MONTH_, SERVICECD
      INTO :YEAR_, :month_,  :servicecd
    DO BEGIN
        INSERT INTO saldo (nyear, nmonth, balance_kod, lshet, data, beginsumma, endsumma) SELECT :YEAR_, :MONTH_, :SERVICECD, a.lshet, CURRENT_TIMESTAMP, 0, 0 FROM abonents a
            LEFT JOIN saldo s ON a.lshet = s.lshet AND s.balance_kod = :SERVICECD AND s.nyear = :YEAR_ AND nmonth = :MONTH_ WHERE s.beginsumma IS NULL;
    END*/
  EXECUTE STATEMENT 'ALTER trigger saldocheckupdate active';
  EXECUTE STATEMENT 'ALTER trigger saldocheckinsert active';
end^

SET TERM ; ^

GRANT SELECT ON CNV$NACHOPL TO PROCEDURE CNV$CNV_01500_SALDO;

GRANT SELECT,INSERT,UPDATE ON SALDO TO PROCEDURE CNV$CNV_01500_SALDO;

GRANT SELECT ON ABONENTS TO PROCEDURE CNV$CNV_01500_SALDO;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01500_SALDO TO SYSDBA;