SET TERM ^ ;

create or alter procedure CNV$CNV_00800_CHARS (
    NEEDDELETE smallint)
as
declare variable LSHET varchar(10);
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable CNT integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE FROM ccharsdoublevalues WHERE lshet IN (SELECT lshet FROM cnv$chars);
        DELETE FROM documents WHERE documentcd IN (SELECT ccal.documentcd
                    FROM ccharsabonentlist ccal INNER JOIN cnv$chars cc ON ccal.lshet = cc.lshet);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE DOCUMENTS_GEN RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(documents_gen, (SELECT MAX(documentcd) + 1 FROM documents) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DOCUMENTS_GEN'
    INTO :cnt;

    FOR SELECT lshet, charcd, value_, date_
        FROM cnv$chars
        ORDER BY lshet, charcd, date_
        INTO :lshet, :charcd, :value_, :date_
    DO BEGIN
        INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
            VALUES (GEN_ID(documents_gen, 1), 1, 1, '������ �������������� �������������', :date_);
        INSERT INTO ccharsabonentlist (lshet, kodccharslist, abonentcchardate, documentcd, significance)
            VALUES (:lshet, :charcd, :date_, GEN_ID(documents_gen,0), :value_);
    END

END^

SET TERM ; ^

GRANT SELECT,DELETE ON CCHARSDOUBLEVALUES TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT ON CNV$CHARS TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT,INSERT,DELETE ON DOCUMENTS TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT,INSERT ON CCHARSABONENTLIST TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00800_CHARS TO SYSDBA;