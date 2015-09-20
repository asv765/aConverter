SET TERM ^ ;

create or alter procedure CNV$CNV_00950_COUNTERSTYPES
as
declare variable CNTTYPE integer;
declare variable CNTNAME varchar(50);
declare variable CNT integer;
BEGIN
    FOR SELECT cnttype, MAX(cntname) AS cntname, COUNT(*)
        FROM cnv$counters
        GROUP BY cnttype
        INTO :cnttype, :cntname, :cnt
    DO BEGIN
        UPDATE OR INSERT INTO counterstypes (kod, equipmenttypeid, periodkod, name, coefficient, digitcount, equipmentgroupid, extorgcd, dimensiontype, minconsumption, maxconsumption, mintemperature, maxtemperature, countermarkmoduleid, servicelifeid)
            VALUES (:cnttype, 12, 4, :cntname, 1, 8, 32, NULL, NULL, 0, 0, 0, 0, NULL, NULL);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE COUNTERSTYPES_G RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(counterstypes_g, (SELECT MAX(kod) + 1 FROM counterstypes) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'COUNTERSTYPES_G'
    INTO :cnt;
END^

SET TERM ; ^

GRANT SELECT ON CNV$COUNTERS TO PROCEDURE CNV$CNV_00950_COUNTERSTYPES;

GRANT SELECT,INSERT,UPDATE ON COUNTERSTYPES TO PROCEDURE CNV$CNV_00950_COUNTERSTYPES;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00950_COUNTERSTYPES TO SYSDBA;
