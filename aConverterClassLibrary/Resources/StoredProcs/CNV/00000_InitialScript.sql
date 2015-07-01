CREATE TABLE TEMPIMPORTTABLE (
    DOCUMENTCD  INTEGER NOT NULL,
    IMPORTKEY   VARCHAR(20) NOT NULL
);


ALTER TABLE TEMPIMPORTTABLE ADD PRIMARY KEY (IMPORTKEY);
CREATE UNIQUE INDEX TEMPIMPORTTABLE_IDX2 ON TEMPIMPORTTABLE (DOCUMENTCD); 

COMMIT WORK;

SET TERM ^ ;

CREATE OR ALTER PROCEDURE tempimportproc (
    importkey VARCHAR(20),
    docname VARCHAR(150),
    docdate DATE,
    inputdate DATE)
returns (documentcd integer)
AS
BEGIN
  if (importkey = '') then
  begin
        select gen_id(documents_gen, 1) from RDB$DATABASE into :documentcd;
        INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCTYPEID, DOCNAME, DOCDATE, INPUTDATE)
        VALUES (:documentcd, 1, 1, 1, 9, :docname, :docdate, :inputdate);    
  end
  else
  begin
      select documentcd from tempimporttable t where t.importkey=:importkey into :documentcd;
      if (documentcd IS NULL) then
      begin
        select gen_id(documents_gen, 1) from RDB$DATABASE into :documentcd;
        INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCTYPEID, DOCNAME, DOCDATE, INPUTDATE)
        VALUES (:documentcd, 1, 1, 1, 9, :docname, :docdate, :inputdate);
        insert into tempimporttable(DOCUMENTCD, IMPORTKEY) values (:documentcd, :importkey);
      end
  end
  SUSPEND;
END^

SET TERM ; ^

GRANT SELECT,INSERT ON TEMPIMPORTTABLE TO PROCEDURE TEMPIMPORTPROC;

GRANT INSERT ON DOCUMENTS TO PROCEDURE TEMPIMPORTPROC;

GRANT EXECUTE ON PROCEDURE TEMPIMPORTPROC TO SYSDBA;

COMMIT WORK;


