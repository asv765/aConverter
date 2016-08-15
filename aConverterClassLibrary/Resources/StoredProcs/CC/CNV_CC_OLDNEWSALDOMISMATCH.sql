SET TERM ^ ;

create or alter procedure CNV$CC_OLDNEWSALDOMISMATCH (
    ACTIONTYPE smallint = 0,
    SALDOCORRECTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    MONTH_ integer,
    YEAR_ integer,
    SERVICECD integer,
    OLDEDEBET decimal(18,4),
    BDEBET decimal(18,4))
as
declare variable OLDSERVICECD integer = 0;
declare variable OLDLSHET varchar(10);
declare variable EDEBET decimal(18,4);
declare variable ISFIRSTROW smallint;
declare variable FNATH decimal(18,4);
declare variable PROCHL decimal(18,4);
declare variable OPLATA decimal(18,4);
declare variable COUNTEDDEBET decimal(18,4);
BEGIN
  /* Тестирование, Диагностика */
  IF (actiontype = 0 OR actiontype = 1 ) THEN BEGIN
     FOR SELECT lshet, servicecd, year_, month_, bdebet, edebet
         FROM cnv$nachopl
         ORDER BY lshet, servicecd, year_, month_
     INTO :lshet, :servicecd, :year_, :month_, :bdebet, :edebet
     DO BEGIN
         IF (oldlshet = lshet AND oldservicecd = servicecd) THEN BEGIN
            IF (bdebet <> oldedebet) THEN BEGIN
                SUSPEND;
                /* Если Диагностика, то при первом найденном пропуске выходим из процедуры */
                IF (actiontype = 0) THEN LEAVE;
            END
         END
         ELSE BEGIN
            oldlshet = lshet;
            oldservicecd = servicecd;
         END
         oldedebet = edebet;
     END
  END
  /* Исправление */
  /* saldoCorrectionType:
        0 - Не корректировать сальдо;
        1 - Пересчитать сальдо назад с конца истории;
        2 - (Не реализовано) Скорректировать суммой изменений сальдо на конец предыдущего месяца,
        3 - (Не реализовано) Скорректировать сальдо на начало текущего месяца с суммой изменений в текущем месяце,
        4 - Пересчитать сальдо вперед с начала истории,
 */
  ELSE IF (actiontype = 2) THEN BEGIN
     IF (saldocorrectiontype = 1) THEN BEGIN
        FOR SELECT DISTINCT lshet, servicecd FROM cnv$cc_oldnewsaldomismatch(1)
            INTO :lshet, :servicecd
        DO BEGIN
           isfirstrow = 1;
           FOR SELECT year_, month_, fnath, prochl, oplata, edebet
               FROM cnv$nachopl
               WHERE lshet = :lshet AND servicecd = :servicecd
               ORDER BY year_ DESCENDING, month_ DESCENDING
               INTO :year_, :month_, :fnath, :prochl, :oplata, :edebet DO BEGIN
               if (isfirstrow = 1) then BEGIN
                  isfirstrow = 0;
                  counteddebet = edebet;
               END
               ELSE BEGIN
                  UPDATE CNV$NACHOPL SET edebet = :counteddebet
                  WHERE lshet = :lshet AND servicecd = :servicecd and year_ = :year_ and month_ = :month_;
               END
               UPDATE CNV$NACHOPL SET bdebet = edebet - fnath - prochl + oplata
               WHERE lshet = :lshet AND servicecd = :servicecd and year_ = :year_ and month_ = :month_;
               counteddebet = counteddebet - fnath - prochl + oplata;
           END
        END
     END
	 IF (saldocorrectiontype = 4) THEN BEGIN
        FOR SELECT DISTINCT lshet, servicecd FROM cnv$cc_oldnewsaldomismatch(1)
            INTO :lshet, :servicecd
        DO BEGIN
           isfirstrow = 1;
           FOR SELECT year_, month_, fnath, prochl, oplata, bdebet
               FROM cnv$nachopl
               WHERE lshet = :lshet AND servicecd = :servicecd
               ORDER BY year_ , month_ 
               INTO :year_, :month_, :fnath, :prochl, :oplata, :bdebet DO BEGIN
               if (isfirstrow = 1) then BEGIN
                  isfirstrow = 0;
                  counteddebet = bdebet;
               END
               ELSE BEGIN
                  UPDATE CNV$NACHOPL SET bdebet = :counteddebet
                  WHERE lshet = :lshet AND servicecd = :servicecd and year_ = :year_ and month_ = :month_;
               END
               UPDATE CNV$NACHOPL SET edebet = bdebet + fnath + prochl - oplata
               WHERE lshet = :lshet AND servicecd = :servicecd and year_ = :year_ and month_ = :month_;
               counteddebet = counteddebet + fnath + prochl - oplata;
           END
        END
     END
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT SELECT,UPDATE ON CNV$NACHOPL TO PROCEDURE CNV$CC_OLDNEWSALDOMISMATCH;

GRANT EXECUTE ON PROCEDURE CNV$CC_OLDNEWSALDOMISMATCH TO PROCEDURE CNV$CC_OLDNEWSALDOMISMATCH;

GRANT EXECUTE ON PROCEDURE CNV$CC_OLDNEWSALDOMISMATCH TO SYSDBA;