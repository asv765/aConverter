SET TERM ^ ;

create or alter procedure CNV$CNV_00850_CHARSHOUSES (
    NEEDDELETE smallint)
as
declare variable HOUSECD integer;
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE FROM CCHARSHOUSELIST WHERE housecd IN (SELECT housecd FROM cnv$charshouses);
        DELETE FROM documents WHERE documentcd IN (SELECT ccal.documentcd
                    FROM CCHARSHOUSELIST ccal INNER JOIN cnv$charshouses cc ON ccal.housecd = cc.housecd);
    END
    /*
    EXECUTE STATEMENT 'ALTER SEQUENCE DOCUMENTS_GEN RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(documents_gen, (SELECT MAX(documentcd) + 1 FROM documents) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DOCUMENTS_GEN'
    INTO :cnt;
    */
    FOR SELECT housecd, charcd, value_, date_
        FROM cnv$charshouses
        ORDER BY housecd, charcd, date_
        INTO :housecd, :charcd, :value_, :date_
    DO BEGIN
        documentcd = GEN_ID(DOCUMENTS_GEN, 1);
        INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
            VALUES (:documentcd, 1, 1, 'Импорт количественных характеристик домов', :date_);
        INSERT INTO CCHARSHOUSELIST (housecd, KOD, HOUSECCHARDATE, documentcd, significance, ISACTUAL)
            VALUES (:housecd, :charcd, :date_, :documentcd, :value_, 1);
    END

END^

SET TERM ; ^