create procedure CNV$CNV_00400_DISTRICT
as
declare variable TOWNSKOD integer;
declare variable DISTKOD integer;
declare variable DISTNAME varchar(50);
declare variable CNT integer;
BEGIN
    FOR SELECT townskod, distkod, MAX(distname) AS distname, COUNT(*) AS cnt
      FROM cnv$abonent
      GROUP BY townskod, distkod
      INTO :townskod, :distkod, :distname, :cnt
      DO BEGIN
        UPDATE OR INSERT INTO DISTRICT (DISTCD, DISTNM, PUNKTCD)
            VALUES (:DISTKOD, :DISTNAME, :TOWNSKOD) MATCHING (DISTCD);
      END
    EXECUTE STATEMENT 'ALTER SEQUENCE DISTRICT_G RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(DISTRICT_G, (SELECT MAX(DISTCD) + 1 FROM DISTRICT) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DISTRICT_G'
    INTO :cnt;
END
