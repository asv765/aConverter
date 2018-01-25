SET TERM ^ ;

create or alter procedure CNV$CNV_00910_ADDCHARS (
    NEEDDELETE smallint = 0)
as
BEGIN
    IF (needdelete = 1) THEN 
		DELETE FROM abonentadditionalchars AD where AD.lshet IN (SELECT LSHET FROM cnv$aaddchar);

	merge into abonentadditionalchars ad
	using cnv$aaddchar ca on ca.lshet = ad.lshet and ca.addcharcd = ad.additionalcharcd
	when matched then
		update set ad.significance = ca."VALUE"
	when not matched then
		insert(additionalcharcd, lshet, significance)
			values(ca.addcharcd, ca.lshet, ca."VALUE");
END^

SET TERM ; ^

/* Following GRANT statetements are generated automatically */

GRANT SELECT,DELETE ON LCHARSDOUBLEVALUES TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT ON CNV$LCHARS TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT,INSERT,DELETE ON DOCUMENTS TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT,INSERT ON LCHARSABONENTLIST TO PROCEDURE CNV$CNV_00900_LCHARS;

/* Existing privileges on this procedure */

GRANT EXECUTE ON PROCEDURE CNV$CNV_00900_LCHARS TO SYSDBA;
