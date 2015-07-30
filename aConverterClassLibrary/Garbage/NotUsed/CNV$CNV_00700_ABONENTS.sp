create procedure CNV$CNV_00700_ABONENTS
as
declare variable LSHET varchar(10);
declare variable DUCD integer;
declare variable HOUSECD integer;
declare variable FLATNO smallint;
declare variable FLATPOSTFIX varchar(10);
declare variable ROOMNO integer;
declare variable F varchar(100);
declare variable I varchar(50);
declare variable O varchar(50);
declare variable PHONENUM varchar(100);
declare variable PRIM_ varchar(250);
declare variable ISDELETED smallint;
BEGIN
    FOR SELECT lshet, ducd, housecd, flatno, flatpostfix, roomno, f, i, o, phonenum, prim_, isdeleted
    FROM cnv$abonent
    ORDER BY lshet
    INTO :lshet, :ducd, :housecd, :flatno, :flatpostfix, :roomno, :f, :i, :o, :phonenum, :prim_, :isdeleted
    DO BEGIN
        UPDATE OR INSERT INTO abonents (lshet, ownerid, housecd, flatno, flatpostfix, roomno, fio, name, second_name, tel, note, deleted)
        VALUES (:lshet, :ducd, :housecd, :flatno, :flatpostfix, :roomno, :f, :i, :o, :phonenum, :prim_, :isdeleted)
        MATCHING (lshet);
    END
END
