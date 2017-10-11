SET TERM ^ ;

create or alter procedure CNV$CNV_00800_CHARS (
    NEEDDELETE smallint = 0,
	GENCHANGEDOC smallint = 1)
as
declare variable LSHET varchar(10);
declare variable OLDLSHET varchar(10);
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE FROM ccharsdoublevalues WHERE lshet IN (SELECT lshet FROM cnv$chars);
        DELETE FROM documents WHERE documentcd IN (SELECT ccal.documentcd
                    FROM ccharsabonentlist ccal INNER JOIN cnv$chars cc ON ccal.lshet = cc.lshet);
    END
    /*
    EXECUTE STATEMENT 'ALTER SEQUENCE DOCUMENTS_GEN RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(documents_gen, (SELECT MAX(documentcd) + 1 FROM documents) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DOCUMENTS_GEN'
    INTO :cnt;
    */
	if (GENCHANGEDOC <> 1) then
	begin
		documentcd = GEN_ID(DOCUMENTS_GEN, 1);
		INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
			VALUES (:documentcd, 1, 1, 'Импорт количественных характеристик', :date_);
	end
	oldlshet = '-1';
    FOR SELECT lshet, charcd, value_, date_
        FROM cnv$chars
        ORDER BY lshet, charcd, date_
        INTO :lshet, :charcd, :value_, :date_
    DO BEGIN
		if (GENCHANGEDOC = 1) then
		begin
			if (lshet <> oldlshet) then
			begin
				documentcd = GEN_ID(DOCUMENTS_GEN, 1);
				INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
					VALUES (:documentcd, 1, 1, 'Импорт количественных характеристик', :date_);
			end
		end
		INSERT INTO ccharsabonentlist (lshet, kodccharslist, abonentcchardate, documentcd, significance)
			VALUES (:lshet, :charcd, :date_, :documentcd, :value_);
		oldlshet = :lshet;
    END

END^

SET TERM ; ^

GRANT SELECT,DELETE ON CCHARSDOUBLEVALUES TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT ON CNV$CHARS TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT,INSERT,DELETE ON DOCUMENTS TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT,INSERT ON CCHARSABONENTLIST TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00800_CHARS TO SYSDBA;