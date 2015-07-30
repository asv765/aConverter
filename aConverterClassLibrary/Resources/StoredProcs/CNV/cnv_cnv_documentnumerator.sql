SET TERM ^ ;

create or alter procedure CNV$CNV_DOCUMENTNUMERATOR (
    IMPORTKEY varchar(20),
    DOCNAME varchar(150),
    DOCDATE date,
    INPUTDATE date,
    BASEORG integer)
returns (
    DOCUMENTCD integer)
as
BEGIN
  if (importkey = '') then
  begin
        select gen_id(documents_gen, 1) from RDB$DATABASE into :documentcd;
        INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCTYPEID, DOCNAME, DOCDATE, INPUTDATE)
        VALUES (:documentcd, :baseorg, 1, 1, 9, :docname, :docdate, :inputdate);
  end
  else
  begin
      select documentcd from cnv$documentnumeratortable t where t.importkey=:importkey into :documentcd;
      if (documentcd IS NULL) then
      begin
        select gen_id(documents_gen, 1) from RDB$DATABASE into :documentcd;
        INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCTYPEID, DOCNAME, DOCDATE, INPUTDATE)
        VALUES (:documentcd, :baseorg, 1, 1, 9, :docname, :docdate, :inputdate);
        insert into CNV$DOCUMENTNUMERATORTABLE(DOCUMENTCD, IMPORTKEY) values (:documentcd, :importkey);
      end
  end
  SUSPEND;
END^

SET TERM ; ^

GRANT INSERT ON DOCUMENTS TO PROCEDURE CNV$CNV_DOCUMENTNUMERATOR;

GRANT SELECT,INSERT ON CNV$DOCUMENTNUMERATORTABLE TO PROCEDURE CNV$CNV_DOCUMENTNUMERATOR;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01000_COUNTERS;
GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO SYSDBA;
