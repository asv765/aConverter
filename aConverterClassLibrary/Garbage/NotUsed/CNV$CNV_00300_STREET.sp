create procedure CNV$CNV_00300_STREET
as
declare variable ULICAKOD integer;
declare variable TOWNSKOD integer;
declare variable ULICANAME varchar(100);
declare variable CNT integer;
BEGIN
    FOR SELECT ulicakod, townskod, MAX(ulicaname) AS ulicaname, COUNT(*)
        FROM cnv$abonent
        GROUP BY ulicakod, townskod
        INTO :ulicakod, :townskod, :ulicaname, :cnt
    DO BEGIN
        UPDATE OR INSERT INTO street (streetcd, punktcd, streetnm, note)
            VALUES (:ulicakod, :townskod, :ulicaname, NULL) MATCHING (streetcd, punktcd);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE STREET_G RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(street_g, (SELECT MAX(streetcd) + 1 FROM street) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'STREET_G'
    INTO :cnt;
END
