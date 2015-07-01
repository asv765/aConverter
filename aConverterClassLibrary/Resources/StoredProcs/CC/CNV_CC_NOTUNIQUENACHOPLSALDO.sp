create procedure CNV$CC_NOTUNIQUENACHOPLSALDO (
    ACTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    MONTH_ integer,
    YEAR_ integer,
    MONTH2 integer,
    YEAR2 integer,
    BDEBET numeric(18,4),
    FNATH numeric(18,4),
    PROCHL numeric(18,4),
    OPLATA numeric(18,2),
    EDEBET numeric(18,4),
    SERVICECD integer,
    SERVICENAM varchar(50))
as
begin
  /* Тестирование, Диагностика */
  if (ACTIONTYPE = 0 or ACTIONTYPE = 1 ) then begin
     for select no1.LSHET, MONTH_, YEAR_, MONTH2, YEAR2, BDEBET, FNATH,
          PROCHL, OPLATA, EDEBET, SERVICECD, SERVICENAM
        from cnv$nachopl no1
        where exists
        (select lshet from cnv$nachopl no2
            where  no1.lshet = no2.lshet and
            no1.month_ = no2.month_ and
            no1.year_ = no2.year_ and
            no1.servicecd = no2.servicecd and
            no1.rdb$db_key != no2.rdb$db_key)
     into :LSHET, :MONTH_, :YEAR_, :MONTH2, :YEAR2, :BDEBET, :FNATH,
          :PROCHL, :OPLATA, :EDEBET, :SERVICECD, :SERVICENAM
     do begin
         suspend;
     end
  end
  /* Исправление */
  else if (actiontype = 2) then
    delete
        from cnv$nachopl nachopl1
        where exists
        (select lshet from cnv$nachopl nachopl2
        where  nachopl1.lshet = nachopl2.lshet and
        nachopl1.month_ = nachopl2.month_ and
        nachopl1.year_ = nachopl2.year_ and
        nachopl1.servicecd = nachopl2.servicecd and
        nachopl1.rdb$db_key > nachopl2.rdb$db_key);
  else
     EXCEPTION cnv$wrong_paramater_value 'Значение ACTIONTYPE отличное от 0,1 или 2 не поддерживается процедурой';
end
