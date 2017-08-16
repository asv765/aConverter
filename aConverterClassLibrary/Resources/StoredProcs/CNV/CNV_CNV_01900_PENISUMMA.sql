SET TERM ^ ;

create or alter procedure CNV$CNV_01900_PENISUMMA
as
declare variable SERVICECD integer;
declare variable LSHET varchar(10);
declare variable FDATE timestamp;
declare variable FDAY integer;
declare variable FMONTH integer;
declare variable FYEAR integer;
declare variable ABONENTSALDO numeric(18,4);
declare variable PENINACHISLSUMMA numeric(18,4);
declare variable ISCONTROLPOINT integer;
declare variable NDATE timestamp;
declare variable IZMEN integer;
declare variable BASEORG integer;
declare variable DOCCD integer;
declare variable OLDYEAR integer;
declare variable OLDMONTH integer;
begin
  oldyear = -1;
  oldmonth = -1;
  select first 1 EXTORGCD
  from EXTORGSPR EOS
  where EOS.ISBASEORGANIZATION = 1
  into :BASEORG;
  for select SERVICECD, LSHET, FDATE, FDAY, FMONTH, FYEAR, ABONENTSALDO, PENINACHISLSUMMA, ISCONTROLPOINT, NDATE, IZMEN
      from CNV$PENISUMMA
      where PENINACHISLSUMMA <> 0
      order by FYEAR, FMONTH, LSHET
      into :SERVICECD, :LSHET, :FDATE, :FDAY, :FMONTH, :FYEAR, :ABONENTSALDO, :PENINACHISLSUMMA, :ISCONTROLPOINT, :NDATE, :IZMEN
  do
  begin
  if ((:oldyear <> :FYEAR) or (:oldmonth <> :FMONTH) ) then 
    select DOCUMENTCD from CNV$CNV_DOCUMENTNUMERATOR('', 'Импорт данных о пене', :FDATE, :FDATE, :BASEORG) into :DOCCD;

    insert into PENISUMMA (BALANCE_KOD, DOCUMENTCD, LSHET, FDATE, FDAY, FMONTH, FYEAR, ABONENTSALDO, PENINACHISLSUMMA,
                           ISCONTROLPOINT, NDATE, IZMEN)
    values (:SERVICECD, :DOCCD, :LSHET, :FDATE, :FDAY, :FMONTH, :FYEAR, :ABONENTSALDO, :PENINACHISLSUMMA,
            :ISCONTROLPOINT, :NDATE, :IZMEN);
	oldyear = :FYEAR;
	oldmonth = :FMONTH;
  end
end^

SET TERM ; ^