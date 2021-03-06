SET TERM ^ ;

create or alter procedure CNV$CNV_00400_DISTRICT
as
declare variable TOWNSKOD integer;
declare variable DISTKOD integer;
declare variable DISTNAME varchar(50);
declare variable CNT integer;
BEGIN
    FOR SELECT townskod, distkod, MAX(distname) AS distname, COUNT(*) AS cnt
      FROM cnv$abonent
	  where distkod is not null
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
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00400_DISTRICT;

GRANT SELECT,INSERT,UPDATE ON DISTRICT TO PROCEDURE CNV$CNV_00400_DISTRICT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00400_DISTRICT TO SYSDBA;
