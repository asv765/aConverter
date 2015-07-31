SET TERM ^ ;

create or alter procedure CNV$CC_SALDOHISTORYGAP (
    ACTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    MONTH_ integer,
    YEAR_ integer,
    SERVICECD integer)
as
declare variable OLDSERVICECD integer;
declare variable OLDLSHET varchar(10);
declare variable CURRMONTH integer;
declare variable CURRYEAR integer;
declare variable QUERYMONTH integer;
declare variable QUERYYEAR integer;
declare variable EDEBET decimal(18,4);
declare variable SERVICENAME varchar(50);
BEGIN
  /* Тестирование, Диагностика */
  IF (actiontype = 0 OR actiontype = 1 ) THEN BEGIN
     FOR SELECT lshet, year_, month_, servicecd
         FROM cnv$nachopl
         ORDER BY lshet, servicecd, year_, month_
     INTO :lshet, :queryyear, :querymonth, :servicecd
     DO BEGIN
         IF (oldlshet = lshet AND oldservicecd = servicecd) THEN BEGIN
            IF (currmonth = 12) THEN BEGIN
                curryear = curryear + 1;
                currmonth = 1;
            END
            ELSE
                currmonth = currmonth + 1;
            WHILE (currmonth <> querymonth OR curryear <> queryyear) DO BEGIN
                month_ = currmonth;
                year_ = curryear;
                SUSPEND;
                /* Если Диагностика, то при первом найденном пропуске выходим из процедуры */
                IF (actiontype = 0) THEN LEAVE;
                IF (currmonth = 12) THEN BEGIN
                    curryear = curryear + 1;
                    currmonth = 1;
                END
                ELSE
                    currmonth = currmonth + 1;
            END
         END
         ELSE BEGIN
            currmonth = querymonth;
            curryear = queryyear;
            oldlshet = lshet;
            oldservicecd = servicecd;
         END
     END
  END
  /* Исправление */
  ELSE IF (actiontype = 2) THEN begin
     FOR SELECT lshet, year_, month_, servicecd FROM
         cnv$cc_saldohistorygap(1)
         INTO :lshet,  :queryyear, :querymonth, :servicecd
     DO BEGIN
         /* Получаем недостающие данные для восстановления записи в месяце из предыдущего месяца */
        IF (querymonth = 1) THEN BEGIN
            curryear = queryyear - 1;
            currmonth = 12;
        END
        ELSE BEGIN
            currmonth = querymonth - 1;
            curryear = queryyear;
        END
        SELECT FIRST 1 n.EDEBET, n.SERVICENAME
        FROM cnv$nachopl n
        WHERE n.LSHET = :lshet AND MONTH_ = :currmonth and YEAR_ = :curryear AND servicecd = :servicecd
        INTO :edebet, :servicename;
        INSERT INTO CNV$NACHOPL (LSHET, MONTH_, YEAR_, MONTH2, YEAR2, BDEBET, FNATH, PROCHL, OPLATA, EDEBET, SERVICECD, SERVICENAME) VALUES (:lshet, :querymonth, :queryyear, :querymonth, :queryyear, :edebet, 0, 0, 0, :edebet, :servicecd, :servicename);
     END
  END
  ELSE
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0 или 1 не поддерживается процедурой';
END^

SET TERM ; ^

GRANT SELECT,INSERT ON CNV$NACHOPL TO PROCEDURE CNV$CC_SALDOHISTORYGAP;

GRANT EXECUTE ON PROCEDURE CNV$CC_SALDOHISTORYGAP TO PROCEDURE CNV$CC_SALDOHISTORYGAP;

GRANT EXECUTE ON PROCEDURE CNV$CC_SALDOHISTORYGAP TO SYSDBA;