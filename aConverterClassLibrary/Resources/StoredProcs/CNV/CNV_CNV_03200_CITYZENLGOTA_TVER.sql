SET TERM ^ ;
create or alter procedure CNV$CNV_03200_CITYZENLGOTA_TVER (
    NEEDDELETE smallint)
as
declare variable CITYZENID integer;
declare variable LGOTACD integer;
declare variable STARTDATE timestamp;
declare variable ENDDATE timestamp;
declare variable ISDELETED integer;
declare variable PRIORITY integer;
declare variable DOCUMENTCD integer;
begin
  if (NEEDDELETE = 1) then
  begin
    delete from CITYZENLGOTA
    where CITYZEN_ID in (select CITYZENID
                         from CNV$CITYZENLGOTA);
  end
  for select CITYZENID, LGOTACD, STARTDATE, ENDDATE, ISDELETED, PRIORITY
      from CNV$CITYZENLGOTA
      order by CITYZENID
      into :CITYZENID, :LGOTACD, :STARTDATE, :ENDDATE, :ISDELETED, :PRIORITY
  do
  begin
    DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
    insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
    values (:DOCUMENTCD, 1, 1, 'Конвертация льгот', :STARTDATE);
    insert into CITYZENLGOTA (CITYZEN_ID, KOD, ADDDOCCD, PRIORITY, LGOTENDDATE, DELETED)
    values (:CITYZENID, :LGOTACD, :DOCUMENTCD, :PRIORITY, :ENDDATE, :ISDELETED);

	DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
    insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
    values (:DOCUMENTCD, 1, 1, 'Дата начала льготы', :STARTDATE);
    insert into cityzenlgotactive (CITYZEN_ID, KOD, CITYZENLGOTDATE, DOCUMENTCD, ISACTIVE)
    values (:CITYZENID, :LGOTACD, :STARTDATE, :DOCUMENTCD, 1);
    if (enddate is not null) then
    begin
        DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
        insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
        values (:DOCUMENTCD, 1, 1, 'Дата окончания льготы', :ENDDATE);
        insert into cityzenlgotactive (CITYZEN_ID, KOD, CITYZENLGOTDATE, DOCUMENTCD, ISACTIVE)
        values (:CITYZENID, :LGOTACD, :ENDDATE, :DOCUMENTCD, 0);
    end
  end
end^

SET TERM ; ^