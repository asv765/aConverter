SET TERM ^ ;

create or alter procedure CNV$CNV_03000_CITIZENS_TVER (
    NEEDDELETE smallint)
as
declare variable ISOWNER varchar(1) = null;
declare variable ISLIVING varchar(1) = null;
declare variable ISREGISTRED varchar(1) = null;
declare variable EMAIL varchar(100) = null;
declare variable PHONE varchar(100) = null;
declare variable ENDDATE timestamp = null;
declare variable STARTDATE timestamp = null;
declare variable UNIQUECITYZENID varchar(45) = null;
declare variable F varchar(50) = null;
declare variable O varchar(50) = null;
declare variable I varchar(50) = null;
declare variable CITIZENID integer = null;
declare variable C_HIDDEN smallint = 0;
declare variable C_OSHIPNUM integer = null;
declare variable C_OSHIPDEN integer = null;
declare variable C_STATE integer = 3;
declare variable C_STARTDATE timestamp = null;
declare variable C_REGTYPE integer = null;
declare variable C_ENDDATE timestamp = null;
declare variable C_ARRIVEDATE timestamp = null;
declare variable C_LEAVEDATE timestamp = null;
declare variable C_OSHIPTYPE integer = null;
declare variable LSHET varchar(10) = null;
begin
  if (NEEDDELETE = 1) then
  begin
    delete from CITYZENADDITIONCHARS
    where CITYZEN_ID in (select CITYZEN_ID
                         from CITYZENS
                         where LSHET in (select LSHET
                                         from CNV$CITIZENS));
    delete from CITYZENS
    where LSHET in (select LSHET
                    from CNV$CITIZENS);
  end
  for select LSHET, CITIZENID, F, I, O, STARTDATE, ENDDATE, DOCUMENTNAME, DORGNAME, VREMREG, PRIBYT, COMMENT_, NOMER
      from CNV$CITIZENS
      order by LSHET, CITIZENID
      into :LSHET, :CITIZENID, :F, :I, :O, :STARTDATE, :ENDDATE, :PHONE, :EMAIL, :ISREGISTRED, :ISLIVING, :ISOWNER, :UNIQUECITYZENID
  do
  begin
    C_STARTDATE = null;
    C_ENDDATE = null;
    C_REGTYPE = null;
    C_ARRIVEDATE = null;
    C_LEAVEDATE = null;
    C_OSHIPNUM = null;
    C_OSHIPDEN = null;
    C_OSHIPTYPE = null;
    C_STATE = 3;
    C_HIDDEN = 0;

    if (:ISREGISTRED = '1') then
    begin
      C_STARTDATE = :STARTDATE;
      C_ENDDATE = :ENDDATE;
      C_REGTYPE = 1;
    end
    if (:ISLIVING = '1') then
    begin
      C_ARRIVEDATE = :STARTDATE;
      C_LEAVEDATE = :ENDDATE;
    end
    if (:ISOWNER = '1') then
    begin
      C_OSHIPNUM = 1;
      C_OSHIPDEN = 1;
      C_OSHIPTYPE = 2;
      C_STATE = 1;
    end
    if (:ENDDATE is not null and
        :ENDDATE < current_timestamp) then
    begin
      C_HIDDEN = 1;
      C_OSHIPNUM = null;
      C_OSHIPDEN = null;
      C_OSHIPTYPE = null;
    end

    insert into CITYZENS (CITYZEN_ID, LSHET, ISMAINCITYZEN, CTZFIO, CTZNAME, CTZPARENTNAME, HIDDEN, OWNERSHIPNUMERATOR,
                          OWNERSHIPDENOMINATOR, OWNERSHIPTYPE, CITIZENSTATEID, STARTDATE, ENDDATE, REGISTRATIONTYPE,
                          ARRIVEDATE, LEAVEDATE, UNIQUECITYZENID)
    values (:CITIZENID, :LSHET, 0, :F, :I, :O, :C_HIDDEN, :C_OSHIPNUM, :C_OSHIPDEN, :C_OSHIPTYPE, :C_STATE,
            :C_STARTDATE, :C_ENDDATE, :C_REGTYPE, :C_ARRIVEDATE, :C_LEAVEDATE, :UNIQUECITYZENID);

    if (:EMAIL is not null) then
      insert into CITYZENADDITIONCHARS (CITYZEN_ID, ADDITIONALCHARCD, SIGNIFICANCE)
      values (:CITIZENID, 11002, :EMAIL);
    if (:PHONE is not null) then
      insert into CITYZENADDITIONCHARS (CITYZEN_ID, ADDITIONALCHARCD, SIGNIFICANCE)
      values (:CITIZENID, 11001, :PHONE);
  end
  update CITYZENS
  set ISMAINCITYZEN = 1
  where HIDDEN = 0 and
        CITIZENSTATEID = 1;
  for select C.LSHET, CA.SIGNIFICANCE
      from CITYZENS C
      inner join CITYZENADDITIONCHARS CA on C.CITYZEN_ID = CA.CITYZEN_ID and
            CA.ADDITIONALCHARCD = 11001
      where C.ISMAINCITYZEN = 1
      order by C.LSHET
      into :LSHET, :PHONE
  do
  begin
    insert into ABONENTPHONES (LSHET, PHONETYPEID, PHONENUMBER)
    values (:LSHET, 0, :PHONE);
  end
end^