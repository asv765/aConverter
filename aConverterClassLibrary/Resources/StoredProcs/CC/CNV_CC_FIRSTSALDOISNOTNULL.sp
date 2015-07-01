create procedure CNV$CC_FIRSTSALDOISNOTNULL (
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
  IF (actiontype = 0) THEN BEGIN
     FOR SELECT FIRST 1 lshet, servicecd, year_, month_,  bdebet
         FROM cnv$nachopl n1
         WHERE year_*100 + month_ = (SELECT MIN(year_*100+month_) FROM cnv$nachopl n2 WHERE n1.lshet = n2.lshet AND n1.servicecd = n2.servicecd)
     INTO :lshet, :servicecd, :year_, :month_, :bdebet
     DO BEGIN
         SUSPEND;
     END
  END
  /* Исправление */
  /* saldoCorrectionType:
        0 - Не корректировать сальдо;
        1 - Пересчитать сальдо назад с конца истории;
        2 - (Не реализовано) Скорректировать суммой изменений сальдо на конец предыдущего месяца,
        3 - (Не реализовано) Скорректировать сальдо на начало текущего месяца с суммой изменений в текущем месяце,
        4 - (Не реализовано) Пересчитать сальдо вперед с начала истории,
 */
  ELSE IF (actiontype = 2) THEN BEGIN
     IF (saldocorrectiontype = 1) THEN BEGIN
        FOR SELECT DISTINCT lshet, servicecd FROM cnv$cc_FIRSTSALDOISNOTNULL(1)
            INTO :lshet, :servicecd
        DO BEGIN
           isfirstrow = 1;
           FOR SELECT year_, month_, fnath, prochl, oplata, edebet
               FROM cnv$nachopl
               WHERE lshet = :lshet AND servicecd = :servicecd
               ORDER BY year_ DESCENDING, month_ DESCENDING
               INTO :year_, :month_, :fnath, :prochl, :oplata, :edebet DO BEGIN
               IF (isfirstrow = 1) THEN BEGIN
                  isfirstrow = 0;
                  counteddebet = edebet;
               END
               ELSE BEGIN
                  UPDATE CNV$NACHOPL SET edebet = :counteddebet
                  WHERE lshet = :lshet AND servicecd = :servicecd AND year_ = :year_ AND month_ = :month_;
               END
               UPDATE CNV$NACHOPL SET bdebet = edebet - fnath - prochl + oplata
               WHERE lshet = :lshet AND servicecd = :servicecd AND year_ = :year_ AND month_ = :month_;
               counteddebet = counteddebet - fnath - prochl + oplata;
           END
        END
     END
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
END
