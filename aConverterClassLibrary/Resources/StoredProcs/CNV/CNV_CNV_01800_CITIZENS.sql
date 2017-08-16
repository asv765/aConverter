SET TERM ^ ;

create or alter procedure CNV$CNV_01800_CITIZENS (
	GENERATEID smallint = 1)
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
    update or insert into EXTORGSPR (EXTORGCD, EXTORGROLEGISZKHID, BANK, BIK, CANGIVELGOT, CANISSUEPASSP, CHARSIMPORT,
                           DEPARTMENTCD, DIRECTOR, EMAIL, EQUIPMENTMAKE, EQUIPMENTSALE, EXPORTTOPAYSYSTEM, EXTORGNM,
                           HASOWNADRESSCD, INN, ISACCOUNT, ISALTERNATIVEACCOUNT, ISBASEORGANIZATION, ISDEFAULTORG,
                           ISEXTERNALCALC, ISMANAGMENTCOMPANY, ISMILITARYCOMISSION, ISPROVIDER, ISREGISTRATIONAUTHORITY,
                           ISTAX, JURIDICALADDRESS, KORACCOUNT, KPP, LSHETFORMAT, MAINACCOUNTANT, NOTE, OGRN, OKPO,
                           PAYIMPORT, PHONE, POSTADDRESS, RS, SECTORADDRESS, SECTORPHONE, SECTORWORKMODE)
    values (:DORGCD, null, null, null, '0', '1', '0', null, null, null, '0', '0', null, :DORGNAME, '0', null, '0', null,
            '0', '0', '0', null, '0', '0', null, '0', null, null, null, null, null, null, null, null, '0', null, null,
            null, null, null, null);
    DORGCD = (select max(EXTORGCD) from EXTORGSPR);
    execute statement 'SET GENERATOR extorgspr_g TO ' || :DORGCD;
  end
  for select C.LSHET, C.ISMAINCITYZEN, C.NOMER, C.SERIA, C.DATDN, C.DORGNAME, C.DORGCD, C.DOCTYPEID, C.F, C.I, C.O,
             C.STARTDATE, C.ENDDATE, C.COMMENT_, C.BIRTHDATE, C.CITIZENID, C.SEX, C.HIDDEN, C.OWNERSHIPNUMERATOR,
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
	if (:GENERATEID = 1) then CITIZENID = (select gen_id(CITYZENS_GEN, 1) from RDB$DATABASE);
	else CITIZENID = :UNIQUECITYZENID;
    insert into CITYZENS (CITYZEN_ID, LSHET, ISMAINCITYZEN, PASPORTNO, PASPORTSR, PASSPORTDATE, PASSPORTNOTE, CTZFIO,
                          CTZNAME, CTZPARENTNAME, STARTDATE, ENDDATE, NOTE, BIRTHDAY, UNIQUECITYZENID, SEX, HIDDEN,
                          OWNERSHIPNUMERATOR, OWNERSHIPDENOMINATOR, OWNERSHIPTYPE, BIRTHCOUNTRY, BIRTHDISTRICT,
                          BIRTHREGION, BIRTHCITY, BIRTHVILLAGE, CITIZENSTATEID, CITIZENSHIPID, LEAVECASEID, ARRIVEDATE,
                          REGISTRATIONTYPE, LEAVEDATE, DEATHDATE, NUMBEREGRP, DATEEGRP)
    values (:CITIZENID, :LSHET, :ISMAINCITYZEN, :PASPORTNO, :PASPORTSR, :PASSPORTDATE, :DORGNAME, :F, :I, :O,
            :STARTDATE, :ENDDATE, :NOTE, :BIRTHDAY, :UNIQUECITYZENID, :SEX, :HIDDEN, :OWNERSHIPNUMERATOR,
            :OWNERSHIPDENOMINATOR, :OWNERSHIPTYPE, :BIRTHCOUNTRY, :BIRTHDISTRICT, :BIRTHREGION, :BIRTHCITY,
            :BIRTHVILLAGE, :CITIZENSTATEID, :CITIZENSHIPID, :LEAVECASEID, :ARRIVEDATE, :REGISTRATIONTYPE, :LEAVEDATE,
            :DEATHDATE, :numberegrp, :dateegrp);
	if (:STATUSDATE is not null) then
		INSERT INTO CITIZENSTATUSES (CITYZEN_ID, CITIZENSTATEID, STATUSDATE, DOCUMENTCD) 
		VALUES (:CITIZENID, :CITIZENSTATEID, :STATUSDATE, NULL);
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
            /* Если регистрация мпж, то прибытие пмж, в остальных случаях пмп */
            if (regtype = 1) then migrtype = 1;
            else migrtype = 2;
            UPDATE OR INSERT INTO CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE)
            VALUES (:documentcd, :citizenid, :startdate, :migrtype, 1, NULL)
            MATCHING (CITYZEN_ID, MIGRATIONTYPE);
        end

        if (enddate is not null) then
        begin
            UPDATE OR INSERT INTO CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE)
            VALUES (:documentcd, :citizenid, :enddate, 0, 2, NULL)
            MATCHING (CITYZEN_ID, MIGRATIONTYPE);
        end
    end
end^

SET TERM ; ^