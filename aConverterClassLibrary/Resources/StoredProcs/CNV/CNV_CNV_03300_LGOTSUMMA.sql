SET TERM ^ ;
create or alter procedure CNV$CNV_03300_LGOTSUMMA
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable YEAR2 integer;
declare variable MONTH2 integer;
declare variable LSHET varchar(10);
declare variable SUMMA numeric(18,4);
declare variable REGIMCD integer;
declare variable SERVICECD integer;
declare variable DATE_ date;
declare variable FYEAR integer;
declare variable FMONTH integer;
declare variable FDAY integer;
declare variable DOCUMENTCD varchar(20);
declare variable TYPE_ integer;
declare variable OLDYEAR integer;
declare variable OLDMONTH integer;
declare variable OLDLSHET varchar(10);
declare variable OLDDOCUMENTCD varchar(20);
declare variable NCASEID integer;
declare variable BASEORG integer;
declare variable cityzenid integer;
declare variable lgotacd integer;
begin
  oldyear = -1;
  oldmonth = -1;
  oldlshet = '-1';
  olddocumentcd = '-1';
  SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
  FOR SELECT YEAR_, MONTH_, YEAR2, MONTH2, LSHET, SUMMA, REGIMCD, SERVICECD, DATE_VV AS DATE_,
    EXTRACT(YEAR FROM DATE_VV) AS FYEAR, EXTRACT(MONTH FROM DATE_VV) AS FMONTH, EXTRACT(DAY FROM DATE_VV) AS FDAY, DOCUMENTCD, TYPE_, cityzenid, lgotacd
    FROM CNV$LGOTSUMMA
    WHERE SUMMA <> 0
    order by year_,  month_, lshet, documentcd
    INTO :YEAR_, :MONTH_, :YEAR2, :MONTH2, :lshet,  :summa,  :regimcd,  :servicecd, :date_,
      :fyear,  :fmonth, :fday, :documentcd, :type_, :cityzenid, :lgotacd
  DO BEGIN
    begin
		if ((select count(*)
		     from CNV$CITYZENLGOTA
		     where CITYZENID = :CITYZENID and
		           LGOTACD = :LGOTACD) > 0) then
		begin
		  if ((:OLDYEAR <> :YEAR_) or (:OLDMONTH <> :MONTH_) or (:OLDLSHET <> :LSHET) or (:OLDDOCUMENTCD <> :DOCUMENTCD)) then
		  begin
		    select DOCUMENTCD
		    from CNV$CNV_DOCUMENTNUMERATOR(:DOCUMENTCD, 'lgotsumma import', :DATE_, :DATE_, :BASEORG)
		    into :NCASEID;
		    update or insert into PERERASHETCASE (CASEID, LSHET, NACHISLCASEID, AUTOUSE, IZMEN, FYEAR, FMONTH, FDAY,
		                                          ISMONTH, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY)
		    values (:NCASEID, :LSHET, :NCASEID, 1, 0, :FYEAR, :FMONTH, :FDAY, 0, :YEAR2, :MONTH2, 1, :YEAR2, :MONTH2, 1);
		    OLDYEAR = :YEAR_;
		    OLDMONTH = :MONTH_;
		    OLDLSHET = :LSHET;
		    OLDDOCUMENTCD = :DOCUMENTCD;
		  end
		  insert into LGOTSUMMA (LSHET, CASEID, KODREGIM, BALANCE_KOD, SUMMATYPE, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY,
		                         SUMMA, NORMTYPE, CITYZEN_ID, KOD)
		  values (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :TYPE_, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, :SUMMA, 0,
		          :CITYZENID, :LGOTACD);
		end
	end
  END
end^

SET TERM ; ^