SET TERM ^ ;

create or alter procedure CNV$CNV_00900_LCHARS (
    NEEDDELETE smallint = 0,
	GENCHANGEDOC smallint = 1)
as
declare variable LSHET varchar(10);
declare variable OLDLSHET varchar(10);
declare variable LCHARCD integer;
declare variable VALUE_ integer;
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE FROM lcharsdoublevalues WHERE lshet IN (SELECT lshet FROM cnv$lchars);
        DELETE FROM documents WHERE documentcd IN (SELECT ccal.documentcd
                    FROM lcharsabonentlist ccal INNER JOIN cnv$lchars cc ON ccal.lshet = cc.lshet);
    END
    /*
    EXECUTE STATEMENT 'ALTER SEQUENCE DOCUMENTS_GEN RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(documents_gen, (SELECT MAX(documentcd) + 1 FROM documents) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DOCUMENTS_GEN'
    INTO :cnt;*/
	oldlshet = '-1';
    FOR SELECT lshet, lcharcd, value_, date_
        FROM cnv$lchars
        ORDER BY lshet, lcharcd, date_
        INTO :lshet, :lcharcd, :value_, :date_
    DO BEGIN
	if (lshet <> oldlshet) then
		begin
		if (GENCHANGEDOC = 1) then
		begin
			documentcd = GEN_ID(DOCUMENTS_GEN, 1);
			INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
				VALUES (:documentcd, 1, 1, 'Импорт качественных характеристик', :date_);
		end
		else documentcd = null;
		end
        INSERT INTO lcharsabonentlist (lshet, kodlcharslist, abonentlchardate, documentcd, significance)
            VALUES (:lshet, :lcharcd, :date_, :documentcd, :value_);
		oldlshet = :lshet;
    END
END^

SET TERM ; ^

/* Following GRANT statetements are generated automatically */

GRANT SELECT,DELETE ON LCHARSDOUBLEVALUES TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT ON CNV$LCHARS TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT,INSERT,DELETE ON DOCUMENTS TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT,INSERT ON LCHARSABONENTLIST TO PROCEDURE CNV$CNV_00900_LCHARS;

/* Existing privileges on this procedure */

GRANT EXECUTE ON PROCEDURE CNV$CNV_00900_LCHARS TO SYSDBA;
