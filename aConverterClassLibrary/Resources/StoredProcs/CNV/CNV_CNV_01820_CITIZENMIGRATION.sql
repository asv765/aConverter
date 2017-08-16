SET TERM ^ ;

create or alter procedure CNV$CNV_01820_CITIZENMIGRATION (
    USEUNIQUEID smallint = 1)
as
declare variable LASTCITIZENID integer;
declare variable CITIZENID varchar(45);
declare variable DATE_ timestamp;
declare variable DIRECTION integer;
declare variable MIGRATION integer;
begin
  for select CCM.CITIZENID, CCM.DATE_, CCM.DIRECTION, CCM.MIGRATIONTYPE
      from CNV$CITIZENMIGRATION CCM
      into :CITIZENID, :DATE_, :DIRECTION, :MIGRATION
  do
  begin
    if (:USEUNIQUEID = 1) then
      CITIZENID = (select first 1 C.CITYZEN_ID
                   from CITYZENS C
                   where C.UNIQUECITYZENID = :CITIZENID);
    insert into CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE)
        values (null, :CITIZENID, :DATE_, :MIGRATION, :DIRECTION, null);
  end
end^

SET TERM ; ^