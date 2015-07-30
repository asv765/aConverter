SET TERM ^ ;

create or alter procedure CNV$CNV_00500_INFORMATIONOWNERS
as
declare variable DUCD integer;
declare variable DUNAME varchar(100);
declare variable CNT integer;
BEGIN
    FOR SELECT ducd, MAX(duname) AS duname, COUNT(*)
    FROM cnv$abonent
    GROUP BY ducd
    INTO :ducd, :duname, :cnt
    DO BEGIN
        UPDATE OR INSERT INTO informationowners (ownerid, ownername)
            VALUES (:ducd, :duname) MATCHING (ownerid);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE INFORMATIONOWNERS_G RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(informationowners_g, (SELECT MAX(ownerid) + 1 FROM informationowners) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'INFORMATIONOWNERS_G'
    INTO :cnt;
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00500_INFORMATIONOWNERS;

GRANT SELECT,INSERT,UPDATE ON INFORMATIONOWNERS TO PROCEDURE CNV$CNV_00500_INFORMATIONOWNERS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00500_INFORMATIONOWNERS TO SYSDBA;
