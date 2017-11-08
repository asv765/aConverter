SET TERM ^ ;

create or alter procedure CNV$CNV_01050_GROUPCOUNTERS (
    NEEDDELETE smallint = 0,
    GENERATECD smallint = 1,
	GENCHANGEDOC smallint = 1)
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
begin

  if (NEEDDELETE = 1) then
  begin
    delete from COUNTERINDICATION CI
    where KOD in (select EQUIPMENTID
                  from PARENTEQUIPMENT PE
                  where PE.IMPORTTAG in (select COUNTERID
                                         from CNV$COUNTERS
                                         where COUNTER_LEVEL = 1));
    delete from PARENTEQUIPMENT PE
    where PE.IMPORTTAG in (select COUNTERID
                           from CNV$COUNTERS
                           where COUNTER_LEVEL = 1);
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
  for select distinct C.COUNTERID
      from CNV$COUNTERS C
      where C.COUNTER_LEVEL = 1
      order by C.LSHET, C.COUNTERID
      into :COUNTERID
  do
  begin
    for select first 1 C.CNTTYPE, C.SERIALNUM, C.SETUPDATE, C.DEACTDATE, C.SETUPPLACE, C.PLOMBDATE, C.PLOMBNAME,
                       C.LASTPOV, C.NEXTPOV, C.PRIM_, C.NAME, C.STATUSID, C.STATUSDATE, C.COUNTER_LEVEL,
                       C.TARGETBALANCE_KOD, C.DISTRIBUTINGMETHOD, C.TARGETNEGATIVEBALANCE_KOD, C.GROUPCOUNTERMODULEID
        from CNV$COUNTERS C
        where C.COUNTERID = :COUNTERID and C.COUNTER_LEVEL = 1
        order by C.LSHET, C.COUNTERID
        into :CNTTYPE, :SERIALNUM, :SETUPDATE, :DEACTDATE, :SETUPPLACE, :PLOMBDATE, :PLOMBNAME, :LASTPOV, :NEXTPOV,
             :PRIM_, :NAME, :STATUSID, :STATUSDATE, :COUNTER_LEVEL, :TARGETBALANCE_KOD, :DISTRIBUTINGMETHOD,
             :TARGETNEGATIVEBALANCE_KOD, :GROUPCOUNTERMODULEID
    do
    begin
      if (GENERATECD = 1) then
        EQUIPMENTID = gen_id(PARENTEQUI_GEN, 1);
      else
        EQUIPMENTID = :COUNTERID;
      insert into PARENTEQUIPMENT (EQUIPMENTID, SERIALNUMBER, IMPORTTAG, NOTE, UNITINGID)
      values (:EQUIPMENTID, :SERIALNUM, :COUNTERID, :PRIM_, :EQUIPMENTID);
      insert into RESOURCECOUNTERS (KOD, KODCOUNTERSTYPES, SETUPDATE, COUNTER_LEVEL, COUNTERPLACE, DATEPPR, LASTPPRDATE,
                                    NAME, TARGETBALANCE_KOD, DISTRIBUTINGMETHOD, TARGETNEGATIVEBALANCE_KOD,
                                    GROUPCOUNTERMODULEID)
      values (:EQUIPMENTID, :CNTTYPE, :SETUPDATE, :COUNTER_LEVEL, :SETUPPLACE, :NEXTPOV, :LASTPOV, :NAME,
              :TARGETBALANCE_KOD, :DISTRIBUTINGMETHOD, :TARGETNEGATIVEBALANCE_KOD, :GROUPCOUNTERMODULEID);
      if (SETUPDATE is null) then
        SETUPDATE = DEFAULTINSTALLDATE;
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

      for select distinct LSHET
          from CNV$COUNTERS C
          where C.COUNTERID = :COUNTERID and C.COUNTER_LEVEL = 1
          order by C.LSHET, C.COUNTERID
          into :LSHET
      do
      begin
        insert into ABONENTSEQUIPMENT (LSHET, EQUIPMENTID, INSTALLDATE, REMOVEDATE)
        values (:LSHET, :EQUIPMENTID, :SETUPDATE, :DEACTDATE);
      end

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
  for select distinct PE.EQUIPMENTID, CI.OLDIND, CI.OB_EM, CI.INDICATION, CI.INDDATE, CI.DOCUMENTCD, CI.INDTYPE
      from CNV$CNTRSIND CI
      inner join PARENTEQUIPMENT PE on CI.COUNTERID = PE.IMPORTTAG
      inner join RESOURCECOUNTERS RC on RC.KOD = PE.EQUIPMENTID
      where RC.COUNTER_LEVEL = 1
      into :EQUIPMENTID, :OLDIND, :OB_EM, :INDICATION, :INDDATE, :DOCUMENTCD, :INDTYPE
  do
  begin
  if (GENCHANGEDOC = 1) then
  begin
	select DOCUMENTCD
    from CNV$CNV_DOCUMENTNUMERATOR(:DOCUMENTCD, 'Импорт показаний', :INDDATE, :INDDATE, :BASEORG)
    into :NEWDOCUMENTCD;
  end
  else NEWDOCUMENTCD = null;

    insert into COUNTERGROUPVOLUMS (KOD, NYEAR, NMONTH, DOCUMENTCD, NDAY, VOLUME, K_RECOUNT, PREVIOUSINDICATION,
                                    INDICATION, RECORDSTATUS, INDICATIONTYPE, UCHETDATE, FACTVOLUMEMONTH,
                                    FACTVOLUMEYEAR, IGNOREABONENTVOLUMES, ISDIRECTINPUTABONENTSVOLUME)
    values (:EQUIPMENTID, extract(year from :INDDATE), extract(month from :INDDATE), :NEWDOCUMENTCD,
            extract(day from :INDDATE), :OB_EM, 1, :OLDIND, :INDICATION, 1, :INDTYPE, :INDDATE,
            extract(month from :INDDATE), extract(year from :INDDATE), 0, 0);
  end
end^

SET TERM ; ^

