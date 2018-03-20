SET TERM ^ ;

create or alter procedure CNV$CNV_00700_ABONENTS
as
declare variable LSHET varchar(10);
declare variable DUCD integer;
declare variable HOUSECD integer;
declare variable FLATNO integer;
declare variable FLATPOSTFIX varchar(10);
declare variable ROOMNO integer;
declare variable ROOMPOSTFIX varchar(10);
declare variable F varchar(100);
declare variable I varchar(50);
declare variable O varchar(50);
declare variable PHONENUM varchar(100);
declare variable PRIM_ varchar(250);
declare variable ISDELETED smallint;
declare variable CLOSEDATE timestamp;
declare variable ADDDOCUMENTCD integer;
declare variable DELDOCUMENTCD integer;
declare variable SPLITPHONE varchar(100);
declare variable email varchar(500);
declare variable STARTDATE timestamp;
BEGIN
    FOR SELECT lshet, ducd, housecd, flatno, flatpostfix, roomno, roompostfix, f, i, o, phonenum, prim_, isdeleted, closedate, email, startdate
    FROM cnv$abonent
    ORDER BY lshet
    INTO :lshet, :ducd, :housecd, :flatno, :flatpostfix, :roomno, :roompostfix, :f, :i, :o, :phonenum, :prim_, :isdeleted, :closedate, email, :startdate
    DO BEGIN
		if (STARTDATE IS NOT NULL) THEN BEGIN
			ADDDOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
			INSERT INTO DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
				VALUES (:ADDDOCUMENTCD, 1, 1, 'Конвертация новых абонентов', :STARTDATE);
		END
		IF (isdeleted = 1 AND CLOSEDATE IS NOT NULL) THEN BEGIN
			DELDOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
			INSERT INTO DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
				VALUES (:DELDOCUMENTCD, 1, 1, 'Конвертация удаленных абонентов', :CLOSEDATE);
		END
        UPDATE OR INSERT INTO abonents (lshet, ownerid, housecd, flatno, flatpostfix, roomno, roompostfix, fio, name, second_name, note, deleted,DELETE_DATE, email, isdeleted, adddocumentcd)
        VALUES (:lshet, :ducd, :housecd, :flatno, :flatpostfix, :roomno, :roompostfix, :f, :i, :o, :prim_, :isdeleted, :closedate, :email, :deldocumentcd, :adddocumentcd)
        MATCHING (lshet);
        IF (phonenum IS NOT NULL) THEN BEGIN
           DELETE FROM abonentphones WHERE LSHET = :lshet;
		   for select SPLITVALUE from SPLIT(:phonenum, ';') into :SPLITPHONE
		   do begin
				INSERT INTO ABONENTPHONES (LSHET, PHONETYPEID, PHONENUMBER) VALUES (:lshet, 0, TRIM(:SPLITPHONE));
		   end
        END
    END
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00700_ABONENTS;

GRANT SELECT,INSERT,UPDATE ON ABONENTS TO PROCEDURE CNV$CNV_00700_ABONENTS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00700_ABONENTS TO SYSDBA;
