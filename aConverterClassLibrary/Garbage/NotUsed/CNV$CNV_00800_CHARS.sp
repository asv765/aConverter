create procedure CNV$CNV_00800_CHARS (
    NEEDDELETE smallint)
as
declare variable LSHET varchar(10);
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable CNT integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE from ccharsdoublevalues where lshet in (select lshet from cnv$chars);
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
            VALUES (GEN_ID(documents_gen, 1), 1, 1, 'Импорт количественных характеристик', :date_);
        INSERT INTO ccharsabonentlist (lshet, kodccharslist, abonentcchardate, documentcd, significance)
            VALUES (:lshet, :charcd, :date_, GEN_ID(documents_gen,0), :value_);
    END

END
