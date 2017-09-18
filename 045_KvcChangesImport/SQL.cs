namespace _045_KvcChangesImport
{
    public static class SQL
    {
        #region Информация о гражданах
        public const string KvcChangesCitizensImport =
@"create or alter procedure KVCCHANGESCITIZENSIMPORT (
    IMPORTDATE date)
as
declare variable LSHET varchar(10) = null;
declare variable ISMAINCITYZEN integer = null;
declare variable PASPORTNO varchar(50) = null;
declare variable PASPORTSR varchar(20) = null;
declare variable PASSPORTDATE timestamp = null;
declare variable DORGNAME varchar(1000) = null;
declare variable DORGCD integer = null;
declare variable DOCTYPEID integer = null;
declare variable F varchar(30) = null;
declare variable I varchar(30) = null;
declare variable O varchar(30) = null;
declare variable STARTDATE timestamp = null;
declare variable ENDDATE timestamp = null;
declare variable NOTE varchar(1000) = null;
declare variable BIRTHDAY timestamp = null;
declare variable UNIQUECITYZENID varchar(45) = null;
declare variable SEX integer = null;
declare variable HIDDEN integer = null;
declare variable OWNERSHIPNUMERATOR integer = null;
declare variable OWNERSHIPDENOMINATOR integer = null;
declare variable OWNERSHIPTYPE integer = null;
declare variable BIRTHCOUNTRY varchar(500) = null;
declare variable BIRTHDISTRICT varchar(500) = null;
declare variable BIRTHREGION varchar(500) = null;
declare variable BIRTHCITY varchar(500) = null;
declare variable BIRTHVILLAGE varchar(500) = null;
declare variable CITIZENSTATEID integer = null;
declare variable CITIZENSHIPID integer = null;
declare variable LEAVECASEID integer = null;
declare variable ARRIVEDATE timestamp = null;
declare variable LEAVEDATE timestamp = null;
declare variable DEATHDATE timestamp = null;
declare variable REGISTRATIONTYPE integer = null;
declare variable DOCUMENTCD integer = null;
declare variable CITIZENID integer = null;
declare variable STATUSDATE timestamp = null;
declare variable ISOWNER integer;
declare variable REGTYPE integer;
declare variable MIGRTYPE integer;
declare variable NUMBEREGRP varchar(100);
declare variable DATEEGRP date;
begin
  for select distinct C.DORGCD, C.DORGNAME
      from CNV$CITIZENS C
      where C.DORGCD IS NOT NULL AND C.DORGCD not in (select extorgcd from extorgspr)
      into :DORGCD, :DORGNAME
  do
  begin
    /* Заведение организаций, выдающих документы */
    update or insert into EXTORGSPR (EXTORGCD, EXTORGROLEGISZKHID, BANK, BIK, CANGIVELGOT, CANISSUEPASSP, CHARSIMPORT,
                           DEPARTMENTCD, DIRECTOR, EMAIL, EQUIPMENTMAKE, EQUIPMENTSALE, EXPORTTOPAYSYSTEM, EXTORGNM,
                           HASOWNADRESSCD, INN, ISACCOUNT, ISALTERNATIVEACCOUNT, ISBASEORGANIZATION, ISDEFAULTORG,
                           ISEXTERNALCALC, ISMANAGMENTCOMPANY, ISMILITARYCOMISSION, ISPROVIDER, ISREGISTRATIONAUTHORITY,
                           ISTAX, JURIDICALADDRESS, KORACCOUNT, KPP, LSHETFORMAT, MAINACCOUNTANT, NOTE, OGRN, OKPO,
                           PAYIMPORT, PHONE, POSTADDRESS, RS, SECTORADDRESS, SECTORPHONE, SECTORWORKMODE)
    values (:DORGCD, null, null, null, '0', '1', '0', null, null, null, '0', '0', null, :DORGNAME, '0', null, '0', null,
            '0', '0', '0', null, '0', '0', null, '0', null, null, null, null, null, null, null, null, '0', null, null,
            null, null, null, null)
    matching(extorgcd);
    DORGCD = (select max(EXTORGCD) from EXTORGSPR);
    execute statement 'SET GENERATOR extorgspr_g TO ' || :DORGCD;
  end

  /* Обновление информации о гражданах */
  for select C.LSHET, C.ISMAINCITYZEN, C.NOMER, C.SERIA, C.DATDN, C.DORGNAME, C.DORGCD, C.DOCTYPEID, C.F, C.I, C.O,
             C.STARTDATE, C.ENDDATE, C.COMMENT_, C.BIRTHDATE, iif(C.UNIQUECITIZENID is null, C.CITIZENID, C.UNIQUECITIZENID), C.SEX, C.HIDDEN, C.OWNERSHIPNUMERATOR,
             C.OWNERSHIPDENOMINATOR, C.OWNERSHIPTYPE, C.BIRTHCOUNTRY, C.BIRTHDISTRICT, C.BIRTHREGION, C.BIRTHCITY,
             C.BIRTHVILLAGE, C.STATUSID, C.CITIZENSHIP, C.LEAVECASEID, C.ARRIVEDATE, C.LEAVEDATE, C.DEATHDATE,
             C.REGISTRATIONTYPE, C.STATUSDATE, C.egrpnumber, c.egrpdate
      from CNV$CITIZENS C
      into :LSHET, :ISMAINCITYZEN, :PASPORTNO, :PASPORTSR, :PASSPORTDATE, :DORGNAME, :DORGCD, :DOCTYPEID, :F, :I, :O,
           :STARTDATE, :ENDDATE, :NOTE, :BIRTHDAY, :UNIQUECITYZENID, :SEX, :HIDDEN, :OWNERSHIPNUMERATOR,
           :OWNERSHIPDENOMINATOR, :OWNERSHIPTYPE, :BIRTHCOUNTRY, :BIRTHDISTRICT, :BIRTHREGION, :BIRTHCITY,
           :BIRTHVILLAGE, :CITIZENSTATEID, :CITIZENSHIPID, :LEAVECASEID, :ARRIVEDATE, :LEAVEDATE, :DEATHDATE,
           :REGISTRATIONTYPE, :STATUSDATE, :numberegrp, :dateegrp
  do
  begin
    CITIZENID = (select ci.cityzen_id from cityzens ci where ci.uniquecityzenid = :UNIQUECITYZENID);
    if (:citizenid is null) then CITIZENID = (select gen_id(CITYZENS_GEN, 1) from RDB$DATABASE);
    update or insert into CITYZENS (CITYZEN_ID, LSHET, ISMAINCITYZEN, PASPORTNO, PASPORTSR, PASSPORTDATE, PASSPORTNOTE, CTZFIO,
                          CTZNAME, CTZPARENTNAME, STARTDATE, ENDDATE, NOTE, BIRTHDAY, UNIQUECITYZENID, SEX, HIDDEN,
                          OWNERSHIPNUMERATOR, OWNERSHIPDENOMINATOR, OWNERSHIPTYPE, BIRTHCOUNTRY, BIRTHDISTRICT,
                          BIRTHREGION, BIRTHCITY, BIRTHVILLAGE, CITIZENSTATEID, CITIZENSHIPID, LEAVECASEID, ARRIVEDATE,
                          REGISTRATIONTYPE, LEAVEDATE, DEATHDATE, NUMBEREGRP, DATEEGRP)
    values (:CITIZENID, :LSHET, :ISMAINCITYZEN, :PASPORTNO, :PASPORTSR, :PASSPORTDATE, :DORGNAME, :F, :I, :O,
            :STARTDATE, :ENDDATE, :NOTE, :BIRTHDAY, :UNIQUECITYZENID, :SEX, :HIDDEN, :OWNERSHIPNUMERATOR,
            :OWNERSHIPDENOMINATOR, :OWNERSHIPTYPE, :BIRTHCOUNTRY, :BIRTHDISTRICT, :BIRTHREGION, :BIRTHCITY,
            :BIRTHVILLAGE, :CITIZENSTATEID, :CITIZENSHIPID, :LEAVECASEID, :ARRIVEDATE, :REGISTRATIONTYPE, :LEAVEDATE,
            :DEATHDATE, :numberegrp, :dateegrp)
    matching (CITYZEN_ID);
     if (:STATUSDATE is not null) then
    begin
        UPDATE OR INSERT INTO CITIZENSTATUSES (CITYZEN_ID, CITIZENSTATEID, STATUSDATE, DOCUMENTCD) 
        VALUES (:CITIZENID, :CITIZENSTATEID, :STATUSDATE, NULL)
        MATCHING (CITYZEN_ID, STATUSDATE);
        DELETE FROM CITIZENSTATUSES WHERE CITYZEN_ID = :CITIZENID AND STATUSDATE > :STATUSDATE;
        if (:CITIZENSTATEID <> 1) then 
            update CITYZENS set OWNERSHIPTYPE = null, OWNERSHIPNUMERATOR = null, OWNERSHIPDENOMINATOR = null where CITYZEN_ID = :CITIZENID;
    end
    if (:DOCTYPEID is not null) then
    begin
      DOCUMENTCD = (select gen_id(DOCUMENTS_GEN, 1)
                    from RDB$DATABASE);
      insert into DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCTYPEID, DOCNAME, DOC_NUMBER,
                             DOC_SER, DOCDATE, INPUTDATE, OUTPUTDATE, FACTDOCUMENTDATE, DOCUMENTIDENTIFER,
                             INTERNALUSEONLY, EMPLOYEE, FROMSUPERIOR, STATUS, REASONID, DEPARTMENTCODE)
      values (:DOCUMENTCD, :DORGCD, 1, 1, :DOCTYPEID, 'Импорт документа гражданина', :PASPORTNO, :PASPORTSR, :PASSPORTDATE, :PASSPORTDATE, null,
              current_date, null, null, null, null, 0, null, null);
      insert into CITYZENDOCUMENTS (CITYZEN_ID, DOCUMENTCD, ISMAINDOC)
      values (:CITIZENID, :DOCUMENTCD, 1);
    end
  end

  /* Удаление граждан, отсутствующих в импорте */
  documentcd = (select * from createdocument('Удаление граждан при импорте из РМБ ' || :importdate));
    for select distinct c.cityzen_id, c.startdate, c.registrationtype, c.enddate,
            iif ((select first 1 cs.citizenstateid from citizenstatuses cs
            where cs.cityzen_id = c.cityzen_id and cs.statusdate <= :importdate
            order by cs.statusdate desc) in (1,4,6), 1, 0)
        from cityzens c
        left join cnv$citizens cc on iif(cc.uniquecitizenid is null, cast(cc.citizenid as varchar(45)), cc.uniquecitizenid) = c.uniquecityzenid
        where c.lshet in (select cc2.lshet from cnv$citizens cc2) and cc.citizenid is null
    into :citizenid, :startdate, :regtype, :enddate, :isowner
    do begin
        if (enddate is null or enddate > :importdate) then
        begin
            if (startdate is null) then enddate = null;
            else enddate = :importdate;
        end
    
        update cityzens c
        set c.enddate = :enddate,
            c.hidden = 1,
            c.ownershiptype = null,
            c.ownershipnumerator = null,
            c.ownershipdenominator = null
        where c.cityzen_id = :citizenid;

        if (startdate is not null) then
        begin
            /* Если регистрация мпж, то прибытие пмж, в остальных случаях пмп */
            if (regtype = 1) then migrtype = 1;
            else migrtype = 2;
            UPDATE OR INSERT INTO CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE)
            VALUES (:documentcd, :citizenid, :startdate, :migrtype, 1, NULL)
            MATCHING (CITYZEN_ID, MIGRATIONDATE);
        end

        if (enddate is not null) then
        begin
            UPDATE OR INSERT INTO CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE)
            VALUES (:documentcd, :citizenid, :enddate, 0, 2, NULL)
            MATCHING (CITYZEN_ID, MIGRATIONDATE);
        end
    
        if (isowner = 1) then
        begin
            update or insert into citizenstatuses (CITYZEN_ID, CITIZENSTATEID, STATUSDATE, documentcd)
            values (:citizenid, 3, :importdate, :documentcd)
            matching (CITYZEN_ID, STATUSDATE);
        end

        delete from citizenstatuses cs
        where cs.cityzen_id = :citizenid and cs.statusdate > :importdate;
        delete from cityzenmigration cm
        where cm.cityzen_id = :citizenid and cm.migrationdate > :importdate;
    end

   /* Добавление миграции гражданам для корректного расчета */
    documentcd = (select documentcd from createdocument('Установка миграции на основе сконвертированной регистрации'));
    for select c.cityzen_id, c.startdate, c.enddate, c.registrationtype
        from cityzens c
        inner join cnv$citizens cc on iif(cc.uniquecitizenid is null, cast(cc.citizenid as varchar(45)), cc.uniquecitizenid) = c.uniquecityzenid
        where c.startdate is not null or c.enddate is not null
    into :citizenid, :startdate, :enddate, :regtype
    do begin
        if (startdate is not null) then
        begin
            /* Если нет существующей миграции на дату прибытия */
            /*if ((select first 1 0 from cityzenmigration cm
                where cm.cityzen_id = :citizenid and cm.migrationdate = :startdate)
                    is null) then
            begin*/
                /* Если регистрация пмж, то прибытие пмж, в остальных случаях пмп */
                if (regtype = 1) then migrtype = 1;
                else migrtype = 2;
                UPDATE OR INSERT INTO CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE)
                VALUES (:documentcd, :citizenid, :startdate, :migrtype, 1, NULL)
                MATCHING (CITYZEN_ID, MIGRATIONDATE);
            /*end*/
        end

        if (enddate is not null) then
        begin
            /* Если нет существующей миграции на дату выбытия */
            /*if ((select first 1 0 from cityzenmigration cm
                where cm.cityzen_id = :citizenid and cm.migrationdate = :enddate)
                    is null) then
            begin*/
                UPDATE OR INSERT INTO CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE)
                VALUES (:documentcd, :citizenid, :enddate, 0, 2, NULL)
                MATCHING (CITYZEN_ID, MIGRATIONDATE);
            /*end*/
        end
    end
end";
        #endregion

        #region Количественные характеристики домов

        public const string KvcChangesHouseCchImport =
            @"create or alter procedure KvcChangesHouseCchImport
as
declare variable HOUSECD integer;
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
begin
    DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
    insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
		values (:DOCUMENTCD, 1, 1, 'Импорт количественных характеристик домов', :DATE_);

  for select HOUSECD, CHARCD, VALUE_, DATE_
      from CNV$CHARSHOUSES
      order by HOUSECD, CHARCD, DATE_
      into :HOUSECD, :CHARCD, :VALUE_, :DATE_
  do
  begin
    if (:CHARCD > 0) then
    begin
		update or insert into CCHARSHOUSELIST (HOUSECD, KOD, HOUSECCHARDATE, DOCUMENTCD, SIGNIFICANCE, ISACTUAL)
			values (:HOUSECD, :CHARCD, :DATE_, :DOCUMENTCD, :VALUE_, 1)
			matching (HOUSECD, KOD, HOUSECCHARDATE);
    end
    else
    begin
      if (:CHARCD = -1) then
          if (:value_ = 0) then
            update HOUSES H
            set H.BUILDINGYEAR = null
            where H.HOUSECD = :HOUSECD;
          else
            update HOUSES H
            set H.BUILDINGYEAR = :value_
            where H.HOUSECD = :HOUSECD;
      else
      if (:CHARCD = -2) then
        update HOUSES H
        set H.FLOORCOUNT = :VALUE_
        where H.HOUSECD = :HOUSECD;
      else
      if (:CHARCD = -3) then
        update HOUSES H
        set H.ENTRANCCOUNT = :VALUE_
        where H.HOUSECD = :HOUSECD;
	  if (:CHARCD = -4) then
        update HOUSES H
        set H.ISMANYKVART = :VALUE_
        where H.HOUSECD = :HOUSECD;
    end
  end

end";

        #endregion

        #region Качественные характеристики домов

        public const string KvcChangesHouseLchImport =
@"create or alter procedure KvcChangesHouseLchImport 
as
declare variable HOUSECD integer;
declare variable CHARCD integer;
declare variable VALUE_ integer;
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
begin
	DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
   insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
		values (:DOCUMENTCD, 1, 1, 'Импорт качественных характеристик домов', :DATE_);
		
  for select HOUSECD, LCHARCD, VALUE_, DATE_
      from cnv$LCHARHOUSES
      order by HOUSECD, LCHARCD, DATE_
      into :HOUSECD, :CHARCD, :VALUE_, :DATE_
  do
  begin
      update or insert into lcharshouselist (HOUSECD, KOD, HOUSELCHARDATE, DOCUMENTCD, SIGNIFICANCE, ISACTIVE)
			values (:HOUSECD, :CHARCD, :DATE_, :DOCUMENTCD, :VALUE_, 1)
			matching (HOUSECD, KOD, HOUSELCHARDATE);
  end

end";

        #endregion

        #region Информация о групповых счетчиках

        public const string KvcChangesHouseOdnCounters =
@"create or alter procedure KvcChangesHouseOdnCounters
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
declare variable TAG varchar(30);
begin
	for select C.COUNTERID, C.CNTTYPE, C.SERIALNUM, C.SETUPDATE, C.DEACTDATE, C.SETUPPLACE, C.PLOMBDATE, C.PLOMBNAME,
                       C.LASTPOV, C.NEXTPOV, C.PRIM_, C.NAME, C.STATUSID, C.STATUSDATE, C.COUNTER_LEVEL,
                       C.TARGETBALANCE_KOD, C.DISTRIBUTINGMETHOD, C.TARGETNEGATIVEBALANCE_KOD, C.GROUPCOUNTERMODULEID, TAG
        from CNV$COUNTERS C
        where C.COUNTER_LEVEL = 1 and C.COUNTERID IS NOT NULL
        into :COUNTERID, :CNTTYPE, :SERIALNUM, :SETUPDATE, :DEACTDATE, :SETUPPLACE, :PLOMBDATE, :PLOMBNAME, :LASTPOV, :NEXTPOV,
             :PRIM_, :NAME, :STATUSID, :STATUSDATE, :COUNTER_LEVEL, :TARGETBALANCE_KOD, :DISTRIBUTINGMETHOD,
             :TARGETNEGATIVEBALANCE_KOD, :GROUPCOUNTERMODULEID, :TAG
	do
	begin
		update or insert into PARENTEQUIPMENT (EQUIPMENTID, SERIALNUMBER, IMPORTTAG, NOTE, UNITINGID)
			values (:COUNTERID, :SERIALNUM, :TAG, :PRIM_, :EQUIPMENTID)
			matching (EQUIPMENTID);
      update or insert into RESOURCECOUNTERS (KOD, KODCOUNTERSTYPES, SETUPDATE, COUNTER_LEVEL, COUNTERPLACE, DATEPPR, LASTPPRDATE,
                                    NAME, TARGETBALANCE_KOD, DISTRIBUTINGMETHOD, TARGETNEGATIVEBALANCE_KOD,
                                    GROUPCOUNTERMODULEID)
			values (:COUNTERID, :CNTTYPE, :SETUPDATE, :COUNTER_LEVEL, :SETUPPLACE, :NEXTPOV, :LASTPOV, :NAME,
              :TARGETBALANCE_KOD, :DISTRIBUTINGMETHOD, :TARGETNEGATIVEBALANCE_KOD, :GROUPCOUNTERMODULEID)
			matching (KOD);
		update or insert into EQSTATUSES (EQUIPMENTID, STATUSDATE, STATUSCD, DOCUMENTCD)
        values (:COUNTERID, :STATUSDATE, :STATUSID, null)
		  matching (EQUIPMENTID, STATUSDATE);
	end
	
  for select distinct C.TAG
      from CNV$COUNTERS C
      where C.COUNTER_LEVEL = 1 and C.COUNTERID IS NULL
      order by C.LSHET, C.TAG
      into :TAG
  do
  begin
    for select first 1 C.CNTTYPE, C.SERIALNUM, C.SETUPDATE, C.DEACTDATE, C.SETUPPLACE, C.PLOMBDATE, C.PLOMBNAME,
                       C.LASTPOV, C.NEXTPOV, C.PRIM_, C.NAME, C.STATUSID, C.STATUSDATE, C.COUNTER_LEVEL,
                       C.TARGETBALANCE_KOD, C.DISTRIBUTINGMETHOD, C.TARGETNEGATIVEBALANCE_KOD, C.GROUPCOUNTERMODULEID
        from CNV$COUNTERS C
        where C.TAG = :TAG
        order by C.LSHET, C.TAG
        into :CNTTYPE, :SERIALNUM, :SETUPDATE, :DEACTDATE, :SETUPPLACE, :PLOMBDATE, :PLOMBNAME, :LASTPOV, :NEXTPOV,
             :PRIM_, :NAME, :STATUSID, :STATUSDATE, :COUNTER_LEVEL, :TARGETBALANCE_KOD, :DISTRIBUTINGMETHOD,
             :TARGETNEGATIVEBALANCE_KOD, :GROUPCOUNTERMODULEID
    do
    begin
		/*Проверка на существование счетчика для повторного импорта*/
		COUNTERID = (select first 1 pq.equipmentid from parentequipment pq where pq.importtag = :TAG);
		if (COUNTERID IS NOT NULL) then
		begin
			update or insert into PARENTEQUIPMENT (EQUIPMENTID, SERIALNUMBER, IMPORTTAG, NOTE, UNITINGID)
				values (:COUNTERID, :SERIALNUM, :TAG, :PRIM_, :EQUIPMENTID)
				matching (EQUIPMENTID);
			update or insert into RESOURCECOUNTERS (KOD, KODCOUNTERSTYPES, SETUPDATE, COUNTER_LEVEL, COUNTERPLACE, DATEPPR, LASTPPRDATE,
                                    NAME, TARGETBALANCE_KOD, DISTRIBUTINGMETHOD, TARGETNEGATIVEBALANCE_KOD,
                                    GROUPCOUNTERMODULEID)
				values (:COUNTERID, :CNTTYPE, :SETUPDATE, :COUNTER_LEVEL, :SETUPPLACE, :NEXTPOV, :LASTPOV, :NAME,
              :TARGETBALANCE_KOD, :DISTRIBUTINGMETHOD, :TARGETNEGATIVEBALANCE_KOD, :GROUPCOUNTERMODULEID)
				matching (KOD);
			update or insert into EQSTATUSES (EQUIPMENTID, STATUSDATE, STATUSCD, DOCUMENTCD)
				values (:COUNTERID, :STATUSDATE, :STATUSID, null)
				matching (EQUIPMENTID, STATUSDATE);
		end
		else /*Если счетчик новый*/
			begin
			EQUIPMENTID = gen_id(PARENTEQUI_GEN, 1);
			insert into PARENTEQUIPMENT (EQUIPMENTID, SERIALNUMBER, IMPORTTAG, NOTE, UNITINGID)
			values (:EQUIPMENTID, :SERIALNUM, :TAG, :PRIM_, :EQUIPMENTID);
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
				where C.TAG = :TAG
				order by C.LSHET, C.TAG
				into :LSHET
			do
			begin
			insert into ABONENTSEQUIPMENT (LSHET, EQUIPMENTID, INSTALLDATE, REMOVEDATE)
			values (:LSHET, :EQUIPMENTID, :STATUSDATE, :DEACTDATE);
			end
		end
    end
  end

  select max(EQUIPMENTID)
  from PARENTEQUIPMENT
  into :MAXEQID;
  if (:MAXEQID IS NULL) then MAXEQID = 1;
  execute statement 'SET GENERATOR ParentEquipment_Uniting TO ' || :MAXEQID;
end";

        #endregion

        #region Отношения граждан

        public const string KvcChangesCitizenRelations =
@"create or alter procedure KvcChangesCitizenRelations
as
declare variable CITIZENIDFROM integer;
declare variable CITIZENIDTO integer;
declare variable RELATIONID integer = null;
begin
    for select c1.cityzen_id, c2.cityzen_id, cr.relationid
    from cnv$citizenrelations cr
    inner join cityzens c1 on c1.uniquecityzenid = cast(cr.citizenidfrom as varchar(45))
    inner join cityzens c2 on c2.uniquecityzenid = cast(cr.citizenidto as varchar(45))
    order by cr.citizenidfrom
    into :citizenidfrom, :citizenidto, :relationid
    do begin
        update or insert into relatives(RELATIVE1, RELATIVE2, RELATIONID)
        values (:citizenidfrom, :citizenidto, :relationid)
          matching (RELATIVE1, RELATIVE2);
    end
end";

        #endregion
    }
}
