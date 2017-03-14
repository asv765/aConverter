SET TERM ^ ;

create or alter procedure CNV$CNV_01600_NACHISLIMPORT
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable YEAR2 integer;
declare variable MONTH2 integer;
declare variable LSHET varchar(10);
declare variable FNATH numeric(18,4);
declare variable VOLUME numeric(18,4);
declare variable REGIMCD integer;
declare variable SERVICECD integer;
declare variable DATE_ date;
declare variable FYEAR integer;
declare variable FMONTH integer;
declare variable FDAY integer;
declare variable DOCUMENTCD varchar(20);
declare variable TYPE_ integer;
declare variable VTYPE_ integer;
declare variable OLDYEAR integer;
declare variable OLDMONTH integer;
declare variable OLDLSHET varchar(10);
declare variable OLDDOCUMENTCD varchar(20);
declare variable NCASEID integer;
declare variable BASEORG integer;
begin
  oldyear = -1;
  oldmonth = -1;
  oldlshet = '-1';
  olddocumentcd = '-1';
  SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
  FOR SELECT YEAR_, MONTH_, YEAR2, MONTH2, LSHET, FNATH, REGIMCD, SERVICECD, DATE_VV AS DATE_,
    EXTRACT(YEAR FROM DATE_VV) AS FYEAR, EXTRACT(MONTH FROM DATE_VV) AS FMONTH, EXTRACT(DAY FROM DATE_VV) AS FDAY, DOCUMENTCD, TYPE_, volume, VTYPE_
    FROM CNV$NACH
    WHERE FNATH <> 0
    order by year_,  month_, lshet, documentcd
    INTO :YEAR_, :MONTH_, :YEAR2, :MONTH2, :lshet,  :fnath,  :regimcd,  :servicecd, :date_,
      :fyear,  :fmonth, :fday, :documentcd, :type_, :volume, :VTYPE_
  DO BEGIN
    if ((:oldyear <> :year_) or (:oldmonth <> :month_) or (:oldlshet <> :lshet) or (:olddocumentcd <> :documentcd) ) then begin
       select documentcd from cnv$cnv_documentnumerator(:DOCUMENTCD, 'Импорт данных о начислениях', :DATE_, :DATE_, :baseorg) into :ncaseid;
       UPDATE OR INSERT INTO PERERASHETCASE (CASEID, LSHET, NACHISLCASEID, AUTOUSE, IZMEN, FYEAR, FMONTH, FDAY, ISMONTH, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY)
         VALUES (:ncaseid, :LSHET, :ncaseid, 1, 0, :FYEAR, :FMONTH, :FDAY, 0, :YEAR2, :MONTH2, 1, :YEAR2, :MONTH2, 1);
       oldyear = :year_;
       oldmonth = :month_;
       oldlshet = :lshet;
       olddocumentcd = :documentcd;
    end
    INSERT INTO NACHISLSUMMA (LSHET, CASEID, KODREGIM, BALANCE_KOD, SUMMATYPE, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, SUMMA, NORMTYPE)
    VALUES (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :TYPE_, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, :FNATH, 0);
	if (:volume <> 0) then
	    INSERT INTO NACHISLVOLUMS (LSHET, CASEID, KODREGIM, BALANCE_KOD, VOLUMETYPE, VOLUME, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, NORMTYPE)
		VALUES (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :VTYPE_, :volume, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, 0);
  END
end^

SET TERM ; ^

GRANT SELECT ON EXTORGSPR TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT SELECT ON CNV$NACH TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT SELECT,INSERT,UPDATE ON PERERASHETCASE TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT INSERT ON NACHISLSUMMA TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01600_NACHISLIMPORT TO SYSDBA;
