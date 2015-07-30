SET TERM ^ ;

create or alter procedure CNV$CNV_00200_PUNKT
as
declare variable TOWNSKOD integer;
declare variable RAYONKOD integer;
declare variable TOWNSNAME varchar(50);
declare variable CNT integer;
BEGIN
    FOR SELECT townskod, MAX(rayonkod) AS rayonkod, MAX(townsname) AS townsname, COUNT(*) AS cnt
        FROM cnv$abonent
        GROUP BY townskod
        ORDER BY townskod
        INTO :townskod, :rayonkod, :townsname, :cnt
    DO BEGIN
       UPDATE OR INSERT INTO punkt (punktcd, punktnm, regiondistrictcd, settlementid)
            VALUES (:townskod, :townsname, :rayonkod, NULL) MATCHING (punktcd);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE PUNKT_G RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(PUNKT_G, (SELECT MAX(PUNKTCD) + 1 FROM PUNKT) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'PUNKT_G'
    INTO :cnt;
  /* Procedure Text */
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00200_PUNKT;

GRANT SELECT,INSERT,UPDATE ON PUNKT TO PROCEDURE CNV$CNV_00200_PUNKT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00200_PUNKT TO SYSDBA;
