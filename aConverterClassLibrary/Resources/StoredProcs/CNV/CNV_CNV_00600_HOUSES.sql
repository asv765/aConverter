SET TERM ^ ;

create or alter procedure CNV$CNV_00600_HOUSES
as
declare variable HOUSECD integer;
declare variable DISTKOD integer;
declare variable ULICAKOD integer;
declare variable TOWNSKOD integer;
declare variable HOUSENO varchar(10);
declare variable HOUSEPOSTFIX varchar(30);
declare variable KORPUSNO integer;
declare variable KORPUSPOSTFIX varchar(30);
declare variable CNT integer;
declare variable POSTINDEX varchar(10);
declare variable HOUSENOTE varchar(4000);
BEGIN
    FOR SELECT housecd, distkod, ulicakod, townskod, houseno, housepostfix, korpusno, korpuspostfix, MAX(postindex) AS postindex, COUNT(*) AS cnt, housenote
    FROM cnv$abonent
    GROUP BY housecd, distkod, ulicakod, townskod, houseno, housepostfix, korpusno, korpuspostfix, housenote
    INTO :housecd, :distkod, :ulicakod, :townskod, :houseno, :housepostfix, :korpusno, :korpuspostfix, :postindex, :cnt, :housenote
    DO BEGIN
        UPDATE OR INSERT INTO houses ( housecd,  distcd,   streetcd,  punktcd,   houseno,  housepostfix,  postindex,  korpusno,  korpuspostfix, note)
                              VALUES (:housecd, :distkod, :ulicakod, :townskod, :houseno, :housepostfix, :postindex, :korpusno, :korpuspostfix, :housenote)
        MATCHING (housecd);
    END
    EXECUTE STATEMENT 'ALTER SEQUENCE HOUSES_G RESTART WITH 0';
    EXECUTE STATEMENT 'UPDATE HOUSES SET HOUSENO = NULL WHERE HOUSENO = ''''';
    EXECUTE STATEMENT 'update houses set housepostfix=null where housepostfix=''''';
    SELECT FIRST 1 GEN_ID(HOUSES_G, (SELECT MAX(HOUSECD) + 1 FROM HOUSES) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'HOUSES_G'
    INTO :cnt;
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00600_HOUSES;

GRANT SELECT,INSERT,UPDATE ON HOUSES TO PROCEDURE CNV$CNV_00600_HOUSES;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00600_HOUSES TO SYSDBA;