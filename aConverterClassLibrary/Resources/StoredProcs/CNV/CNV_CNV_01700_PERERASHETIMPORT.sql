SET TERM ^ ;

create or alter procedure CNV$CNV_01700_PERERASHETIMPORT
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable YEAR2 integer;
declare variable MONTH2 integer;
declare variable LSHET varchar(10);
declare variable PROCHL numeric(18,4);
declare variable PROCHLVOLUME numeric(18,4);
declare variable REGIMCD integer;
declare variable SERVICECD integer;
declare variable FYEAR integer;
declare variable FMONTH integer;
declare variable FDAY integer;
declare variable DATE_VV timestamp;
declare variable DOCUMENTCD varchar(20);
declare variable TYPE_ integer;
declare variable DOCNAME varchar(150);
declare variable DOCDATE timestamp;
declare variable OLDYEAR integer;
declare variable OLDMONTH integer;
declare variable OLDLSHET varchar(10);
declare variable IDOCNAME varchar(150);
declare variable IDATE timestamp;
declare variable NCASEID integer;
declare variable BASEORG integer;
BEGIN
  oldyear = -1;
  oldmonth = -1;
  oldlshet = '-1';
  SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
  FOR SELECT year_, month_, year2, month2, lshet, prochl, regimcd, servicecd,
       EXTRACT(YEAR FROM date_vv) AS fyear,
       EXTRACT(MONTH FROM date_vv) AS fmonth,
       EXTRACT(DAY FROM date_vv) AS fday,
       date_vv,
       documentcd,
       type_,
       docname,
       docdate,
	   prochlvolume
    FROM cnv$nach
    WHERE prochl <> 0
    ORDER BY year_,month_,lshet
    INTO :year_, :month_, :year2, :month2, :lshet, :prochl, :regimcd, :servicecd,
         :fyear, :fmonth, :fday, :date_vv, :documentcd, :type_, :docname, :docdate, :prochlvolume
  DO BEGIN
    IF (:docname IS NULL) THEN
      idocname = 'Перерасчет за ' || + CAST(:MONTH_ AS VARCHAR(10)) || '.' || CAST(:YEAR_ AS VARCHAR(10)) || ' по лицевому счету ' || :lshet;
    ELSE
      idocname = :docname;
    IF (:docdate IS NULL) THEN
      idate = :date_vv;
    ELSE
      idate = :docdate;

    IF ((:oldyear <> :year_) OR (:oldmonth <> :month_) OR (:oldlshet <> :lshet)) THEN BEGIN
/*       SELECT documentcd FROM cnv$cnv_documentnumerator(:documentcd, 'Импорт данных об изменениях', :idate, :idate, :baseorg) INTO :ncaseid; */
       ncaseid = GEN_ID(DOCUMENTS_GEN, 1);
       INSERT INTO documents (documentcd, organizationcd, registerusercd, otvetstvusercd, doctypeid, docname, docdate, inputdate)
           VALUES (:ncaseid, :baseorg, 1, 1, 9, :idocname, :idate, :idate);
       INSERT INTO pererashetcase (caseid, lshet, nachislcaseid, autouse, izmen, fyear, fmonth, fday, ismonth, nyear, nmonth, nday, ayear, amonth, aday)
           VALUES (:ncaseid, :lshet, :ncaseid, 0, 1, :fyear, :fmonth, :fday, 0, :year_, :month_, 1, :year2, :month2, 1);
/*       UPDATE OR INSERT INTO PERERASHETCASE (CASEID, LSHET, NACHISLCASEID, AUTOUSE, IZMEN, FYEAR, FMONTH, FDAY, ISMONTH, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY)
         VALUES (:ncaseid, :LSHET, :ncaseid, 1, 0, :FYEAR, :FMONTH, :FDAY, 0, :YEAR2, :MONTH2, 1, :YEAR2, :MONTH2, 1); */
       oldyear = :year_;
       oldmonth = :month_;
       oldlshet = :lshet;
    END
    INSERT INTO nachislsumma (lshet, caseid, kodregim, balance_kod, summatype, nyear, nmonth, nday, ayear, amonth, aday, summa, normtype)
       VALUES (:lshet, :ncaseid, :regimcd, :servicecd, :type_, :year_, :month_, 1, :year2, :month2, 1, :prochl, 0);
/*    INSERT INTO NACHISLSUMMA (LSHET, CASEID, KODREGIM, BALANCE_KOD, SUMMATYPE, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, SUMMA, NORMTYPE)
    VALUES (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :TYPE_, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, :FNATH, 0); */
		    INSERT INTO NACHISLVOLUMS (LSHET, CASEID, KODREGIM, BALANCE_KOD, VOLUME, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, VOLUMETYPE, NORMTYPE)
		VALUES (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :PROCHLVOLUME, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, 1, 0);
  END
END^

SET TERM ; ^

GRANT SELECT ON EXTORGSPR TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT SELECT ON CNV$NACH TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT INSERT ON DOCUMENTS TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT INSERT ON PERERASHETCASE TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT INSERT ON NACHISLSUMMA TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01700_PERERASHETIMPORT TO SYSDBA;
