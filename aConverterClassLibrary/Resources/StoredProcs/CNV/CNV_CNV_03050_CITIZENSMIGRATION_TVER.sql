SET TERM ^ ;
create or alter procedure CNV$CNV_03050_CITIZENSMIGR_TVER (
    NEEDDELETE smallint)
as
declare variable CITYZENID integer = null;
declare variable MIGRATIONDATE timestamp = null;
declare variable MIGRATIONTYPE integer = null;
declare variable DIRECTION integer = null;
declare variable DOCUMENTCD integer = null;
begin
  if (NEEDDELETE = 1) then
  begin
    delete from CITYZENMIGRATION
    where CITYZEN_ID in (select distinct CITIZENID
                         from CNV$CITIZENMIGRATION);
  end
  for select CITIZENID, DATE_, MIGRATIONTYPE, DIRECTION
      from CNV$CITIZENMIGRATION
      order by CITIZENID
      into :CITYZENID, :MIGRATIONDATE, :MIGRATIONTYPE, :DIRECTION
  do
  begin
    DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
    insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
    values (:DOCUMENTCD, 1, 1, 'Конвертация миграции граждан', :MIGRATIONDATE);

    insert into CITYZENMIGRATION (DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION)
    values (:DOCUMENTCD, :CITYZENID, :MIGRATIONDATE, :MIGRATIONTYPE, :DIRECTION);
  end
end^