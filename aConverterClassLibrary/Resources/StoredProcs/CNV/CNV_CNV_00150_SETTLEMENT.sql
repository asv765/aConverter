SET TERM ^ ;

create or alter procedure CNV$CNV_00150_SETTLEMENT
as
declare variable SETTLEMENTKOD integer;
declare variable SETTLEMENTNAME varchar(200);
declare variable cnt integer;
BEGIN
    FOR SELECT DISTINCT SETTLEMENTKOD, SETTLEMENTNAME
        FROM cnv$abonent
	where SETTLEMENTNAME is not null
        ORDER BY SETTLEMENTKOD
        INTO :SETTLEMENTKOD, :SETTLEMENTNAME
    DO BEGIN
       UPDATE OR INSERT INTO SETTLEMENTS (SETTLEMENTID, SETTLEMENTNAME)
            VALUES (:SETTLEMENTKOD, :SETTLEMENTNAME) MATCHING (SETTLEMENTID);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE SETTLEMENTS_G RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(SETTLEMENTS_G, (SELECT MAX(SETTLEMENTID) + 1 FROM SETTLEMENTS) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'SETTLEMENTS_G'
    into :cnt;
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00150_SETTLEMENT;

GRANT SELECT,INSERT,UPDATE ON SETTLEMENTS TO PROCEDURE CNV$CNV_00150_SETTLEMENT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00150_SETTLEMENT TO SYSDBA;
