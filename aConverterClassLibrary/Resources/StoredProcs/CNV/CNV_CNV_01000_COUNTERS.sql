SET TERM ^ ;

create or alter procedure CNV$CNV_01000_COUNTERS (
    NEEDDELETE smallint = 0,
    GENERATECD smallint = 1,
	GENCHANGEDOC smallint = 1,
	WITHPERERASHETCASE smallint = 1)
as
declare variable LSHET varchar(10);
declare variable COUNTERID varchar(20);
declare variable CNTTYPE integer;
declare variable SERIALNUM varchar(30);
declare variable SETUPDATE timestamp;
declare variable DEACTDATE timestamp;
declare variable SETUPPLACE integer;
declare variable PLOMBNAME varchar(40);
declare variable PLOMBDATE timestamp;
declare variable LASTPOV date;
declare variable NEXTPOV timestamp;
declare variable PRIM_ varchar(100);
declare variable EQUIPMENTID integer;
declare variable NAME varchar(150);
declare variable DEFAULTINSTALLDATE timestamp;
declare variable OLDIND numeric(16,4);
declare variable OB_EM numeric(16,4);
declare variable INDICATION numeric(16,4);
declare variable INDDATE timestamp;
declare variable DOCUMENTCD varchar(20);
declare variable INDTYPE integer;
declare variable NEWDOCUMENTCD integer;
declare variable BASEORG integer;
declare variable MAXEQID integer;
declare variable STATUSID integer;
declare variable STATUSDATE timestamp;
declare variable COUNTER_LEVEL integer;
declare variable TARGETBALANCE_KOD integer;
declare variable DISTRIBUTINGMETHOD integer;
declare variable TARGETNEGATIVEBALANCE_KOD integer;
declare variable GROUPCOUNTERMODULEID integer;
declare variable KODREGIM integer;
declare variable NOCALCCHILDBALANCES integer;
declare variable year_ integer;
declare variable month_ integer;
declare variable day_ integer;
declare variable casetype integer;
declare variable untingid varchar(20);
begin

  if (NEEDDELETE = 1) then
  begin
    delete from COUNTERINDICATION CI
    where KOD in (select EQUIPMENTID
                  from PARENTEQUIPMENT PE
                  where PE.IMPORTTAG in (select COUNTERID
                                         from CNV$COUNTERS
										 where COUNTER_LEVEL <> 1));
    delete from PARENTEQUIPMENT PE
    where PE.IMPORTTAG in (select COUNTERID
                           from CNV$COUNTERS
						   where COUNTER_LEVEL <> 1);
  end
  if (NEEDDELETE = 2) then
  begin
    delete from COUNTERINDICATION CI
    where KOD in (select EQUIPMENTID
                  from PARENTEQUIPMENT PE
                  where PE.EQUIPMENTID in (select EQUIPMENTID
                                           from ABONENTSEQUIPMENT AE
                                           where AE.LSHET in (select LSHET
                                                              from CNV$ABONENT)));
    delete from PARENTEQUIPMENT PE
    where PE.EQUIPMENTID in (select EQUIPMENTID
                             from ABONENTSEQUIPMENT AE
                             where AE.LSHET in (select LSHET
                                                from CNV$ABONENT));
  end
  select cast(VARIABLEVALUE as timestamp)
  from SYSTEMVARIABLES SV
  where SV.VARIABLENAME = 'SYSTEMSTARTDATE'
  into :DEFAULTINSTALLDATE;
  for select C.LSHET, C.COUNTERID, C.CNTTYPE, C.SERIALNUM, C.SETUPDATE, C.DEACTDATE, C.SETUPPLACE, C.PLOMBDATE,
             C.PLOMBNAME, C.LASTPOV, C.NEXTPOV, C.PRIM_, C.NAME, C.STATUSID, C.STATUSDATE, C.COUNTER_LEVEL,
             C.TARGETBALANCE_KOD, C.DISTRIBUTINGMETHOD, C.TARGETNEGATIVEBALANCE_KOD, C.GROUPCOUNTERMODULEID, C.KODREGIM, C.NOCALCCHILDBALANCES, C.UNTINGID
      from CNV$COUNTERS C
	  where C.COUNTER_LEVEL <> 1
      order by C.LSHET, C.COUNTERID
      into :LSHET, :COUNTERID, :CNTTYPE, :SERIALNUM, :SETUPDATE, :DEACTDATE, :SETUPPLACE, :PLOMBDATE, :PLOMBNAME,
           :LASTPOV, :NEXTPOV, :PRIM_, :NAME, :STATUSID, :STATUSDATE, :COUNTER_LEVEL, :TARGETBALANCE_KOD,
           :DISTRIBUTINGMETHOD, :TARGETNEGATIVEBALANCE_KOD, :GROUPCOUNTERMODULEID, :KODREGIM, :NOCALCCHILDBALANCES, :UNTINGID
  do
  begin
    if (GENERATECD = 1) then
      EQUIPMENTID = gen_id(PARENTEQUI_GEN, 1);
    else
      EQUIPMENTID = :COUNTERID;
	if (:UNTINGID is null) then UNTINGID = :EQUIPMENTID;
	else
    begin
        UNTINGID = (select first 1 untingid from cnv$counters where counterid = :counterid);
        if (:UNTINGID = :COUNTERID) then
        begin
            UPDATE CNV$COUNTERS 
                SET UNTINGID = :EQUIPMENTID
                WHERE UNTINGID = :COUNTERID;
            UNTINGID = :EQUIPMENTID;
        end
    end
    insert into PARENTEQUIPMENT (EQUIPMENTID, SERIALNUMBER, IMPORTTAG, NOTE, UNITINGID)
    values (:EQUIPMENTID, :SERIALNUM, :COUNTERID, :PRIM_, :UNTINGID);
    insert into RESOURCECOUNTERS (KOD, KODCOUNTERSTYPES, SETUPDATE, COUNTER_LEVEL, COUNTERPLACE, DATEPPR, LASTPPRDATE,
                                  NAME, TARGETBALANCE_KOD, DISTRIBUTINGMETHOD, TARGETNEGATIVEBALANCE_KOD, NOCALCCHILDBALANCES)
    values (:EQUIPMENTID, :CNTTYPE, :SETUPDATE, :COUNTER_LEVEL, :SETUPPLACE, :NEXTPOV, :LASTPOV, :NAME,
            :TARGETBALANCE_KOD, :DISTRIBUTINGMETHOD, :TARGETNEGATIVEBALANCE_KOD, :NOCALCCHILDBALANCES);
    if (SETUPDATE is null) then
      SETUPDATE = DEFAULTINSTALLDATE;
    insert into ABONENTSEQUIPMENT (LSHET, EQUIPMENTID, INSTALLDATE, REMOVEDATE)
    values (:LSHET, :EQUIPMENTID, :SETUPDATE, :DEACTDATE);
    if (SETUPDATE is not null and
        STATUSDATE is null) then
    begin
      insert into EQSTATUSES (EQUIPMENTID, STATUSDATE, STATUSCD, DOCUMENTCD)
      values (:EQUIPMENTID, :SETUPDATE, 1, null);
    end
    if (DEACTDATE is not null) then
    begin
      update or insert into EQSTATUSES (EQUIPMENTID, STATUSDATE, STATUSCD, DOCUMENTCD)
      values (:EQUIPMENTID, :DEACTDATE, 0, null);
    end
    if (PLOMBNAME is not null) then
    begin
      if (char_length(PLOMBNAME) > 0) then
      begin
        insert into EQUIPMENTPLOMBS (PLOMBID, EQUIPMENTID, PLOMBNUMBER, PLOMBTYPEID, PLOMBDATE)
        values (gen_id(EQUIPMENTPLOMBS_G, 1), :EQUIPMENTID, :PLOMBNAME, 1, :PLOMBDATE);
      end
    end
    if (STATUSDATE is not null) then
    begin
      insert into EQSTATUSES (EQUIPMENTID, STATUSDATE, STATUSCD, DOCUMENTCD)
      values (:EQUIPMENTID, :STATUSDATE, :STATUSID, null);
    end
	if (KODREGIM is not null) then
    begin
      insert into EQUIPMENT_REGIMCONSUM (EQUIPMENTID, KODREGIM)
      values (:EQUIPMENTID, :KODREGIM);
    end

  end
  select max(EQUIPMENTID)
  from PARENTEQUIPMENT
  into :MAXEQID;
  if (:MAXEQID IS NULL) then MAXEQID = 1;
  execute statement 'SET GENERATOR ParentEquipment_Uniting TO ' || :MAXEQID;

  select first 1 EXTORGCD
  from EXTORGSPR EOS
  where EOS.ISBASEORGANIZATION = 1
  into :BASEORG;
  for select PE.EQUIPMENTID, CI.OLDIND, CI.OB_EM, CI.INDICATION, CI.INDDATE, CI.DOCUMENTCD, CI.INDTYPE, CI.CASETYPE, CO.LSHET
      from CNV$CNTRSIND CI
      inner join PARENTEQUIPMENT PE on CI.COUNTERID = PE.IMPORTTAG
	  inner join RESOURCECOUNTERS RC on RC.KOD = PE.EQUIPMENTID
	  inner join CNV$COUNTERS CO on CO.COUNTERID = CI.COUNTERID
	  where RC.COUNTER_LEVEL <> 1
      into :EQUIPMENTID, :OLDIND, :OB_EM, :INDICATION, :INDDATE, :DOCUMENTCD, :INDTYPE, :CASETYPE, :LSHET
  do
  begin
  if (DOCUMENTCD is not null) then
  begin
	select DOCUMENTCD
    from CNV$CNV_DOCUMENTNUMERATOR(:DOCUMENTCD, 'Импорт показаний', :INDDATE, :INDDATE, :BASEORG)
    into :NEWDOCUMENTCD;
  end
  else NEWDOCUMENTCD = null;
    
    insert into COUNTERINDICATION (COUNTERINDICATIONFACTID, KOD, INDICATIONDATE, DOCUMENTCD, INDICATIONVALUE,
                                   PREVIOUSINDICATION, VOLUME, INDICATIONTYPE, DEPENDFROMCNTINDICATIONFACTID)
    values (null, :EQUIPMENTID, :INDDATE, :NEWDOCUMENTCD, :INDICATION, :OLDIND, :OB_EM, :INDTYPE, null);

	if (WITHPERERASHETCASE = 1) then
	begin
		if (not exists(select 0 from PERERASHETCASE where CASEID = :NEWDOCUMENTCD and LSHET = :LSHET)) then
		begin
			year_ = extract(year from :INDDATE);
			month_ = extract(month from :INDDATE);
			day_ = extract(day from :INDDATE);
			INSERT INTO PERERASHETCASE (CASEID, LSHET, NACHISLCASEID, AUTOUSE, IZMEN, FYEAR, FMONTH, FDAY, ISMONTH, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, CASETYPE)
			VALUES (:NEWDOCUMENTCD, :LSHET, :NEWDOCUMENTCD, 0, 0, :year_, :month_, :day_, 0, :year_, :month_, :day_, :year_, :month_, :day_, :CASETYPE);
		end
	end
  end
end^

SET TERM ; ^

