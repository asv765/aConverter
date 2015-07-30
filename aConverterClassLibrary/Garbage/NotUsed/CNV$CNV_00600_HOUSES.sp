create procedure CNV$CNV_00600_HOUSES
as
declare variable HOUSECD integer;
declare variable DISTKOD integer;
declare variable ULICAKOD integer;
declare variable TOWNSKOD integer;
declare variable HOUSENO integer;
declare variable HOUSEPOSTFIX integer;
declare variable KORPUSNO integer;
declare variable KORPUSPOSTFIX integer;
declare variable CNT integer;
declare variable POSTINDEX varchar(10);
BEGIN
    FOR SELECT housecd, distkod, ulicakod, townskod, houseno, housepostfix, korpusno, korpuspostfix, MAX(postindex) AS postindex, COUNT(*) AS cnt
    FROM cnv$abonent
    GROUP BY housecd, distkod, ulicakod, townskod, houseno, housepostfix, korpusno, korpuspostfix
    INTO :housecd, :distkod, :ulicakod, :townskod, :houseno, :housepostfix, :korpusno, :korpuspostfix, :postindex, :cnt
    DO BEGIN
        UPDATE OR INSERT INTO houses (housecd, distcd, streetcd, punktcd, houseno, housepostfix, postindex, korpusno, korpuspostfix)
            VALUES (:housecd, :distkod, :ulicakod, :townskod, :houseno, :housepostfix, :postindex, :korpusno, :korpuspostfix)
        MATCHING (housecd);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE HOUSES_G RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(HOUSES_G, (SELECT MAX(HOUSECD) + 1 FROM HOUSES) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'HOUSES_G'
    INTO :cnt;
    EXECUTE STATEMENT 'UPDATE HOUSES SET HOUSENO = NULL WHERE HOUSENO = ''''';
    EXECUTE STATEMENT 'update houses set housepostfix=null where housepostfix=''''';
END
