/* CNV_DOCUMENTNUMERATOR */
SET TERM ^ ;

create or alter procedure CNV$CNV_DOCUMENTNUMERATOR (
    IMPORTKEY varchar(20),
    DOCNAME varchar(150),
    DOCDATE date,
    INPUTDATE date,
    BASEORG integer)
returns (
    DOCUMENTCD integer)
as
BEGIN
  if (importkey = '') then
  begin
        select gen_id(documents_gen, 1) from RDB$DATABASE into :documentcd;
        INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCTYPEID, DOCNAME, DOCDATE, INPUTDATE)
        VALUES (:documentcd, :baseorg, 1, 1, 9, :docname, :docdate, :inputdate);
  end
  else
  begin
      select documentcd from cnv$documentnumeratortable t where t.importkey=:importkey into :documentcd;
      if (documentcd IS NULL) then
      begin
        select gen_id(documents_gen, 1) from RDB$DATABASE into :documentcd;
        INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCTYPEID, DOCNAME, DOCDATE, INPUTDATE)
        VALUES (:documentcd, :baseorg, 1, 1, 9, :docname, :docdate, :inputdate);
        insert into CNV$DOCUMENTNUMERATORTABLE(DOCUMENTCD, IMPORTKEY) values (:documentcd, :importkey);
      end
  end
  SUSPEND;
END^

SET TERM ; ^

GRANT INSERT ON DOCUMENTS TO PROCEDURE CNV$CNV_DOCUMENTNUMERATOR;

GRANT SELECT,INSERT ON CNV$DOCUMENTNUMERATORTABLE TO PROCEDURE CNV$CNV_DOCUMENTNUMERATOR;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01000_COUNTERS;
GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO SYSDBA;


/* CNV_00100_REGIONDISTRICTS */
SET TERM ^ ;

create or alter procedure CNV$CNV_00100_REGIONDISTRICTS
as
declare variable RAYONKOD integer;
declare variable RAYONNAME varchar(50);
declare variable CNT integer;
BEGIN
  FOR SELECT rayonkod, MAX(rayonname) AS rayonname, COUNT(*) AS cnt
        FROM cnv$abonent
        GROUP BY rayonkod
        INTO :rayonkod, :rayonname, :cnt
  DO BEGIN
     UPDATE OR INSERT INTO regiondistricts (regiondistrictcd, regiondistrictnm)
        VALUES (:rayonkod, :rayonname) MATCHING (regiondistrictcd);
  END
  EXECUTE STATEMENT 'ALTER SEQUENCE regiondistricts_g RESTART WITH 0';
  SELECT FIRST 1 GEN_ID(regiondistricts_g,
  (SELECT MAX(regiondistrictcd) + 1 FROM regiondistricts) )
  FROM rdb$generators WHERE rdb$generators.rdb$generator_name = 'REGIONDISTRICTS_G' INTO :cnt;
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00100_REGIONDISTRICTS;

GRANT SELECT,INSERT,UPDATE ON REGIONDISTRICTS TO PROCEDURE CNV$CNV_00100_REGIONDISTRICTS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00100_REGIONDISTRICTS TO SYSDBA;


/* CNV_00200_PUNKT */
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


/* CNV_00300_STREET */
SET TERM ^ ;

create or alter procedure CNV$CNV_00300_STREET
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
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00300_STREET;

GRANT SELECT,INSERT,UPDATE ON STREET TO PROCEDURE CNV$CNV_00300_STREET;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00300_STREET TO SYSDBA;


/* CNV_00400_DISTRICT */
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


/* CNV_00500_INFORMATIONOWNERS */
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


/* CNV_00600_HOUSES */
SET TERM ^ ;

create or alter procedure CNV$CNV_00600_HOUSES
as
declare variable HOUSECD integer;
declare variable DISTKOD integer;
declare variable ULICAKOD integer;
declare variable TOWNSKOD integer;
declare variable HOUSENO integer;
declare variable HOUSEPOSTFIX varchar(30);
declare variable KORPUSNO integer;
declare variable KORPUSPOSTFIX varchar(30);
declare variable CNT integer;
declare variable POSTINDEX varchar(10);
BEGIN
    FOR SELECT housecd, distkod, ulicakod, townskod, houseno, housepostfix, korpusno, korpuspostfix, MAX(postindex) AS postindex, COUNT(*) AS cnt
    FROM cnv$abonent
    GROUP BY housecd, distkod, ulicakod, townskod, houseno, housepostfix, korpusno, korpuspostfix
    INTO :housecd, :distkod, :ulicakod, :townskod, :houseno, :housepostfix, :korpusno, :korpuspostfix, :postindex, :cnt
    DO BEGIN
        UPDATE OR INSERT INTO houses ( housecd,  distcd,   streetcd,  punktcd,   houseno,  housepostfix,  postindex,  korpusno,  korpuspostfix)
                              VALUES (:housecd, :distkod, :ulicakod, :townskod, :houseno, :housepostfix, :postindex, :korpusno, :korpuspostfix)
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


/* CNV_00700_ABONENTS */
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
declare variable DOCUMENTCD integer;
BEGIN
    FOR SELECT lshet, ducd, housecd, flatno, flatpostfix, roomno, roompostfix, f, i, o, phonenum, prim_, isdeleted, closedate
    FROM cnv$abonent
    ORDER BY lshet
    INTO :lshet, :ducd, :housecd, :flatno, :flatpostfix, :roomno, :roompostfix, :f, :i, :o, :phonenum, :prim_, :isdeleted, :closedate
    DO BEGIN
        UPDATE OR INSERT INTO abonents (lshet, ownerid, housecd, flatno, flatpostfix, roomno, roompostfix, fio, name, second_name, note, deleted,DELETE_DATE)
        VALUES (:lshet, :ducd, :housecd, :flatno, :flatpostfix, :roomno, :roompostfix, :f, :i, :o, :prim_, :isdeleted, :closedate)
        MATCHING (lshet);
        IF (phonenum IS NOT NULL) THEN BEGIN
           DELETE FROM abonentphones WHERE LSHET = :lshet;
           INSERT INTO ABONENTPHONES (LSHET, PHONETYPEID, PHONENUMBER) VALUES (:lshet, 0, :phonenum);
        END
		IF (isdeleted = 1) THEN BEGIN
			DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
			INSERT INTO DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
				VALUES (:DOCUMENTCD, 1, 1, 'Convert deleted abonents', :CLOSEDATE);
			UPDATE ABONENTS SET ISDELETED = :DOCUMENTCD WHERE LSHET = :LSHET; 
		END
    END
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_00700_ABONENTS;

GRANT SELECT,INSERT,UPDATE ON ABONENTS TO PROCEDURE CNV$CNV_00700_ABONENTS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00700_ABONENTS TO SYSDBA;



/* CNV_00800_CHARS */
SET TERM ^ ;

create or alter procedure CNV$CNV_00800_CHARS (
    NEEDDELETE smallint)
as
declare variable LSHET varchar(10);
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE FROM ccharsdoublevalues WHERE lshet IN (SELECT lshet FROM cnv$chars);
        DELETE FROM documents WHERE documentcd IN (SELECT ccal.documentcd
                    FROM ccharsabonentlist ccal INNER JOIN cnv$chars cc ON ccal.lshet = cc.lshet);
    END
    /*
    EXECUTE STATEMENT 'ALTER SEQUENCE DOCUMENTS_GEN RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(documents_gen, (SELECT MAX(documentcd) + 1 FROM documents) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DOCUMENTS_GEN'
    INTO :cnt;
    */
    FOR SELECT lshet, charcd, value_, date_
        FROM cnv$chars
        ORDER BY lshet, charcd, date_
        INTO :lshet, :charcd, :value_, :date_
    DO BEGIN
        documentcd = GEN_ID(DOCUMENTS_GEN, 1);
        INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
            VALUES (:documentcd, 1, 1, 'Import cchars', :date_);
        INSERT INTO ccharsabonentlist (lshet, kodccharslist, abonentcchardate, documentcd, significance)
            VALUES (:lshet, :charcd, :date_, :documentcd, :value_);
    END

END^

SET TERM ; ^

GRANT SELECT,DELETE ON CCHARSDOUBLEVALUES TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT ON CNV$CHARS TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT,INSERT,DELETE ON DOCUMENTS TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT SELECT,INSERT ON CCHARSABONENTLIST TO PROCEDURE CNV$CNV_00800_CHARS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_00800_CHARS TO SYSDBA;


/* CNV_00850_CHARSHOUSES */
SET TERM ^ ;

create or alter procedure CNV$CNV_00850_CHARSHOUSES (
    NEEDDELETE smallint)
as
declare variable HOUSECD integer;
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE FROM CCHARSHOUSELIST WHERE housecd IN (SELECT housecd FROM cnv$charshouses);
        DELETE FROM documents WHERE documentcd IN (SELECT ccal.documentcd
                    FROM CCHARSHOUSELIST ccal INNER JOIN cnv$charshouses cc ON ccal.housecd = cc.housecd);
    END
    /*
    EXECUTE STATEMENT 'ALTER SEQUENCE DOCUMENTS_GEN RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(documents_gen, (SELECT MAX(documentcd) + 1 FROM documents) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DOCUMENTS_GEN'
    INTO :cnt;
    */
    FOR SELECT housecd, charcd, value_, date_
        FROM cnv$charshouses
        ORDER BY housecd, charcd, date_
        INTO :housecd, :charcd, :value_, :date_
    DO BEGIN
        documentcd = GEN_ID(DOCUMENTS_GEN, 1);
        INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
            VALUES (:documentcd, 1, 1, 'Import houses cchars', :date_);
        INSERT INTO CCHARSHOUSELIST (housecd, KOD, HOUSECCHARDATE, documentcd, significance, ISACTUAL)
            VALUES (:housecd, :charcd, :date_, :documentcd, :value_, 1);
    END

END^

SET TERM ; ^


/* CNV_00900_LCHARS */
SET TERM ^ ;

create or alter procedure CNV$CNV_00900_LCHARS (
    NEEDDELETE smallint)
as
declare variable LSHET varchar(10);
declare variable LCHARCD integer;
declare variable VALUE_ integer;
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
BEGIN
    IF (needdelete = 1) THEN BEGIN
        DELETE FROM lcharsdoublevalues WHERE lshet IN (SELECT lshet FROM cnv$lchars);
        DELETE FROM documents WHERE documentcd IN (SELECT ccal.documentcd
                    FROM lcharsabonentlist ccal INNER JOIN cnv$lchars cc ON ccal.lshet = cc.lshet);
    END
    /*
    EXECUTE STATEMENT 'ALTER SEQUENCE DOCUMENTS_GEN RESTART WITH 0';
    SELECT FIRST 1 GEN_ID(documents_gen, (SELECT MAX(documentcd) + 1 FROM documents) )
    FROM rdb$generators
    WHERE rdb$generators.rdb$generator_name = 'DOCUMENTS_GEN'
    INTO :cnt;*/
    FOR SELECT lshet, lcharcd, value_, date_
        FROM cnv$lchars
        ORDER BY lshet, lcharcd, date_
        INTO :lshet, :lcharcd, :value_, :date_
    DO BEGIN
        documentcd = GEN_ID(DOCUMENTS_GEN, 1);
        INSERT INTO documents (documentcd, registerusercd, otvetstvusercd, docname, factdocumentdate)
            VALUES (:documentcd, 1, 1, 'Import lchars', :date_);
        INSERT INTO lcharsabonentlist (lshet, kodlcharslist, abonentlchardate, documentcd, significance)
            VALUES (:lshet, :lcharcd, :date_, :documentcd, :value_);
    END
END^

SET TERM ; ^

/* Following GRANT statetements are generated automatically */

GRANT SELECT,DELETE ON LCHARSDOUBLEVALUES TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT ON CNV$LCHARS TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT,INSERT,DELETE ON DOCUMENTS TO PROCEDURE CNV$CNV_00900_LCHARS;
GRANT SELECT,INSERT ON LCHARSABONENTLIST TO PROCEDURE CNV$CNV_00900_LCHARS;

/* Existing privileges on this procedure */

GRANT EXECUTE ON PROCEDURE CNV$CNV_00900_LCHARS TO SYSDBA;


/* CNV_00950_COUNTERSTYPES */
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


/* CNV_01000_COUNTERS */
SET TERM ^ ;

CREATE OR ALTER procedure CNV$CNV_01000_COUNTERS (
    NEEDDELETE smallint,
	GENERATECD smallint = 1)
as
declare variable LSHET varchar(10);
declare variable COUNTERID varchar(20);
declare variable CNTTYPE integer;
declare variable SERIALNUM varchar(30);
declare variable SETUPDATE timestamp;
declare variable DEACTDATE timestamp;
declare variable SETUPPLACE integer;
declare variable PLOMBNAME varchar(40);
declare variable PLOMBDATE timestamp;
declare variable LASTPOV date;
declare variable NEXTPOV timestamp;
declare variable PRIM_ varchar(100);
declare variable EQUIPMENTID integer;
declare variable NAME varchar(50);
declare variable DEFAULTINSTALLDATE timestamp;
declare variable OLDIND numeric(16,4);
declare variable OB_EM numeric(16,4);
declare variable INDICATION numeric(16,4);
declare variable INDDATE timestamp;
declare variable DOCUMENTCD varchar(20);
declare variable INDTYPE integer;
declare variable NEWDOCUMENTCD integer;
declare variable BASEORG integer;
declare variable MAXEQID integer;
BEGIN
    
    IF (needdelete = 1) THEN BEGIN
       DELETE FROM counterindication ci
            WHERE kod IN (SELECT equipmentid FROM parentequipment pe WHERE pe.importtag IN (SELECT counterid FROM cnv$counters));
       DELETE FROM parentequipment pe WHERE pe.importtag IN (SELECT counterid FROM cnv$counters);
    END
    IF (needdelete = 2) THEN BEGIN
       DELETE FROM counterindication ci
            WHERE kod IN (SELECT equipmentid FROM parentequipment pe WHERE pe.equipmentid IN
            (SELECT equipmentid
             FROM abonentsequipment ae
             WHERE ae.lshet IN (SELECT lshet FROM cnv$abonent)));
       DELETE FROM parentequipment pe WHERE pe.equipmentid IN
            (SELECT equipmentid
             FROM abonentsequipment ae
             WHERE ae.lshet IN (SELECT lshet FROM cnv$abonent));
    END
    SELECT CAST(variablevalue AS TIMESTAMP)
        FROM systemvariables sv
        WHERE sv.variablename = 'SYSTEMSTARTDATE'
    INTO :defaultinstalldate;
    FOR SELECT c.lshet, c.counterid, c.cnttype, c.serialnum, c.setupdate, c.deactdate,
        c.setupplace, c.plombdate, c.plombname, c.lastpov, c.nextpov, c.prim_, c.name
       FROM cnv$counters c
       ORDER BY c.lshet, c.counterid
       INTO :lshet, :counterid, :cnttype, :serialnum, :setupdate, :deactdate,
        :setupplace, :plombdate, :plombname, :lastpov, :nextpov, :prim_, :name
    DO BEGIN
		IF (generatecd = 1 ) THEN equipmentid = GEN_ID(parentequi_gen, 1);
		ELSE equipmentid = :counterid;
        INSERT INTO parentequipment (equipmentid, serialnumber, importtag, note, unitingid)
            VALUES (:equipmentid, :serialnum, :counterid, :prim_, :equipmentid);
        INSERT INTO resourcecounters (kod, kodcounterstypes, setupdate, counter_level, counterplace, dateppr, lastpprdate, name)
            VALUES (:equipmentid, :cnttype, :setupdate, 0, :setupplace, :nextpov, :lastpov, :name);
        IF (setupdate IS NULL) THEN
            setupdate = defaultinstalldate;
        INSERT INTO abonentsequipment (lshet, equipmentid, installdate, removedate)
            VALUES (:lshet, :equipmentid, :setupdate, :deactdate);
        IF (setupdate IS NOT NULL) THEN BEGIN
            INSERT INTO eqstatuses (equipmentid, statusdate, statuscd, documentcd)
                VALUES (:equipmentid, :setupdate, 1, NULL);
        END
        IF (deactdate IS NOT NULL) THEN BEGIN
            UPDATE OR INSERT INTO eqstatuses (equipmentid, statusdate, statuscd, documentcd)
                VALUES (:equipmentid, :deactdate, 0, NULL);
        END
        IF (plombname IS NOT NULL) THEN BEGIN
            IF (CHAR_LENGTH(plombname) > 0) THEN BEGIN
                INSERT INTO equipmentplombs (plombid, equipmentid, plombnumber, plombtypeid, plombdate)
                    VALUES (GEN_ID(equipmentplombs_g,1), :equipmentid, :plombname, 1, :plombdate);
            END
        END

    END
	select max(equipmentid) from parentequipment into :maxeqid;
    EXECUTE STATEMENT 'SET GENERATOR ParentEquipment_Uniting TO ' || :maxeqid;

    SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
    FOR SELECT pe.equipmentid, ci.oldind, ci.ob_em, ci.indication, ci.inddate, ci.documentcd, ci.indtype
    FROM cnv$cntrsind ci INNER JOIN parentequipment pe ON ci.counterid = pe.importtag
        INTO :equipmentid, :oldind, :ob_em, :indication, :inddate, :documentcd, :indtype
    DO BEGIN
        SELECT documentcd
            FROM cnv$cnv_documentnumerator(:documentcd, 'Data import', :inddate, :inddate, :baseorg)
            INTO :newdocumentcd;
        INSERT INTO counterindication (counterindicationfactid, kod, indicationdate, documentcd, indicationvalue, previousindication, volume, indicationtype, dependfromcntindicationfactid)
        VALUES (NULL, :equipmentid, :inddate, :newdocumentcd, :indication, :oldind, :ob_em, :indtype, NULL);
    END
END
^

SET TERM ; ^


/* CNV_01300_SOURCEDOC */
SET TERM ^ ;

create or alter procedure CNV$CNV_01300_SOURCEDOC
as
declare variable CNT integer;
declare variable SOURCENAME varchar(50);
declare variable SOURCECD integer;
begin
  FOR SELECT sourcecd, MAX(sourcename) as sourcename
    FROM cnv$oplata
    GROUP BY sourcecd
    INTO :sourcecd, :sourcename
  DO BEGIN
    UPDATE OR INSERT INTO SOURCEDOC (SourceDocCD, PayMethodCD, SourceDocNM, ExtOrgCD, ISSubs, SHOWASSUBSIDY )
        VALUES (:SOURCECD, 1, :SOURCENAME, 1, 0, 0) MATCHING (SourceDocCD);
  END
  EXECUTE STATEMENT 'ALTER SEQUENCE SOURCEDOC_G RESTART WITH 0';
  SELECT FIRST 1 GEN_ID(SOURCEDOC_G, (SELECT MAX(SourceDoc.SourceDocCD) + 1 FROM SourceDoc) )
  FROM rdb$generators
  WHERE rdb$generators.rdb$generator_name = 'SOURCEDOC_G'
  into :cnt;
end^

SET TERM ; ^

GRANT SELECT ON CNV$OPLATA TO PROCEDURE CNV$CNV_01300_SOURCEDOC;

GRANT SELECT,INSERT,UPDATE ON SOURCEDOC TO PROCEDURE CNV$CNV_01300_SOURCEDOC;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01300_SOURCEDOC TO SYSDBA;


/* CNV_01400_OPLATA */
SET TERM ^ ;

create or alter procedure CNV$CNV_01400_OPLATA
as
declare variable LSHET varchar(10);
declare variable DOCUMENTCD varchar(20);
declare variable MONTH_ integer;
declare variable YEAR_ integer;
declare variable SUMMA numeric(18,2);
declare variable DATE_ timestamp;
declare variable DATE_VV timestamp;
declare variable SOURCECD integer;
declare variable SERVICECD integer;
declare variable DVDAY integer;
declare variable DVMONTH integer;
declare variable DVYEAR integer;
declare variable DDAY integer;
declare variable DMONTH integer;
declare variable DYEAR integer;
declare variable OLDDATE_VV timestamp;
declare variable OLDSOURCECD integer;
declare variable OLDLSHET varchar(10);
declare variable PDCD integer;
declare variable PDKEY varchar(50);
declare variable PSCASEID integer;
declare variable BASEORG integer;
begin
    olddate_vv = date '1-Jan-1990';
    oldsourcecd = -1;
    oldlshet = '-1';
    SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
    for select lshet, documentcd, month_, year_, summa, date_, date_vv, sourcecd, servicecd,
            extract(day from date_vv) as dvday, extract(month from date_vv) as dvmonth, extract(year from date_vv) as dvyear,
            extract(day from date_) as dday, extract(month from date_) as dmonth, extract(year from date_) as dyear
        from cnv$oplata
        order by date_vv, sourcecd, lshet, servicecd
        into :lshet, :documentcd, :month_, :year_, :summa, :date_, :date_vv, :sourcecd, :servicecd, 
			:dvday, :dvmonth, :dvyear,
            :dday, :dmonth, :dyear
    do begin
       if ((:olddate_vv <> :date_vv) or (:oldsourcecd <> :sourcecd)) then begin
          pdkey = 'PD_' || cast(:date_vv as DATE) || '_' || :sourcecd;
          select documentcd
              from cnv$cnv_documentnumerator(:pdkey, 'Import oplata', :date_vv, :date_vv, :baseorg)
              into :pdcd;
          update or insert into primarydoc
               (primarydoccd, sourcedoccd, discountdateday, discountdatemonth, discountdateyear, savedtodb, savedwithwarnings)
             values
               (:pdcd, :sourcecd, :dvday, :dvmonth, :dvyear, 1, 0);
          olddate_vv = :date_vv;
          oldsourcecd = :sourcecd;
          oldlshet = '-1';
       end
       if (:oldlshet <> :lshet) then begin
          insert into PAYTICKET (TICKETCD, PRIMARYDOCCD, USERCD, ABNLSHET, PAYDATE, DISCOUNTDATE, IMPORTDATE, LASTEDITDATE)
             values (gen_id(PAYTICKET_G,1), :pdcd, 1, :lshet, :date_, :date_vv, current_timestamp, current_timestamp);
          oldlshet = :lshet;
       end
       insert into PAYFACT (PAYFACTCD, TICKETCD, GAZSERVICECD, PAYPENI, PAYYEAR, PAYMONTH, PAYSUM, DISCOUNTRESOURCECD)
          values (gen_id(PAYFACT_G,1), gen_id(PAYTICKET_G,0), :servicecd, 0, :YEAR_, :MONTH_, :SUMMA, -1);
       select documentcd from cnv$cnv_documentnumerator(:DOCUMENTCD, 'Import oplata', :DATE_, :DATE_VV, :baseorg) into :PSCASEID;
       insert into PaySumma (PayFactCD, TicketCD, PrimaryDocCD, Balance_Kod, PayType, LShet, PaySum, PayMonth, PayYear, PayDateDay, PayDateMonth, PayDateYear, UserCD, Delta, Caseid)
          values (gen_id(PAYFACT_G,0), gen_id(PAYTICKET_G,0), :pdcd, :SERVICECD, 0, :LSHET, :SUMMA, :MONTH_, :YEAR_, :DDAY, :DMONTH, :DYEAR, 1, 0, :PSCASEID);
    end
    update primarydoc set paytotal = (select sum(pf.paysum) from payticket pt inner join payfact pf on pt.ticketcd = pf.ticketcd where pt.primarydoccd = primarydoc.primarydoccd);
    update primarydoc set ticketscount = (select count(pt.ticketcd) from payticket pt inner join payfact pf on pt.ticketcd = pf.ticketcd where pt.primarydoccd = primarydoc.primarydoccd);
    update payfact set DISCOUNTRESOURCECD = (select balanceslist.kod from balanceslist where balanceslist.balance_kod = payfact.gazservicecd) where (payfact.gazservicecd is not null) and (payfact.gazservicecd <> 102);

end^

SET TERM ; ^

GRANT SELECT ON EXTORGSPR TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT ON CNV$OPLATA TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT,INSERT,UPDATE ON PRIMARYDOC TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT,INSERT ON PAYTICKET TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT,INSERT,UPDATE ON PAYFACT TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT INSERT ON PAYSUMMA TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT ON BALANCESLIST TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01400_OPLATA TO SYSDBA;


/* CNV_01500_SALDO */
SET TERM ^ ;

create or alter procedure CNV$CNV_01500_SALDO (
    CURRENTYEAR integer,
    CURRENTMONTH integer)
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable SERVICECD integer;
declare variable LSHET varchar(10);
declare variable BDEBET numeric(18,4);
declare variable EDEBET numeric(18,4);
declare variable DATE_ date;
begin
  EXECUTE STATEMENT 'ALTER trigger saldocheckinsert inactive';
  EXECUTE STATEMENT 'ALTER trigger saldocheckupdate inactive';
  for SELECT YEAR_, MONTH_, SERVICECD, LSHET, BDEBET, EDEBET,
         CAST(('01.' || month_ || '.' || year_) as DATE) as date_
      FROM CNV$NACHOPL
      WHERE YEAR_*12 + MONTH_ < :CURRENTYEAR*12+:CURRENTMONTH
      ORDER BY YEAR_, MONTH_, LSHET, SERVICECD
      INTO :YEAR_, :month_,  :servicecd, :lshet,  :bdebet, :edebet,  :date_
  DO BEGIN
     UPDATE OR INSERT INTO SALDO (NYEAR, NMONTH, BALANCE_KOD, LSHET, DATA, BEGINSUMMA, ENDSUMMA)
        VALUES (:YEAR_, :MONTH_, :SERVICECD, :LSHET, :DATE_, :BDEBET, :EDEBET);
  END
  FOR SELECT YEAR_, MONTH_, SERVICECD
    FROM cnv$NACHOPL
    WHERE YEAR_*12 + MONTH_ < :CurrentYear*12+:CurrentMonth
    GROUP BY YEAR_, MONTH_, SERVICECD
    ORDER BY YEAR_, MONTH_, SERVICECD
      INTO :YEAR_, :month_,  :servicecd
    DO BEGIN
        INSERT INTO saldo (nyear, nmonth, balance_kod, lshet, data, beginsumma, endsumma) SELECT :YEAR_, :MONTH_, :SERVICECD, a.lshet, CURRENT_TIMESTAMP, 0, 0 FROM abonents a
            LEFT JOIN saldo s ON a.lshet = s.lshet AND s.balance_kod = :SERVICECD AND s.nyear = :YEAR_ AND nmonth = :MONTH_ WHERE s.beginsumma IS NULL;

    END
  EXECUTE STATEMENT 'ALTER trigger saldocheckupdate active';
  EXECUTE STATEMENT 'ALTER trigger saldocheckinsert active';
end^

SET TERM ; ^

GRANT SELECT ON CNV$NACHOPL TO PROCEDURE CNV$CNV_01500_SALDO;

GRANT SELECT,INSERT,UPDATE ON SALDO TO PROCEDURE CNV$CNV_01500_SALDO;

GRANT SELECT ON ABONENTS TO PROCEDURE CNV$CNV_01500_SALDO;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01500_SALDO TO SYSDBA;


/* CNV_01600_NACHISLIMPORT */
SET TERM ^ ;

create or alter procedure CNV$CNV_01600_NACHISLIMPORT
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable YEAR2 integer;
declare variable MONTH2 integer;
declare variable LSHET varchar(10);
declare variable FNATH numeric(18,4);
declare variable VOLUME numeric(18,4);
declare variable REGIMCD integer;
declare variable SERVICECD integer;
declare variable DATE_ date;
declare variable FYEAR integer;
declare variable FMONTH integer;
declare variable FDAY integer;
declare variable DOCUMENTCD varchar(20);
declare variable TYPE_ integer;
declare variable VTYPE_ integer;
declare variable OLDYEAR integer;
declare variable OLDMONTH integer;
declare variable OLDLSHET varchar(10);
declare variable OLDDOCUMENTCD varchar(20);
declare variable NCASEID integer;
declare variable BASEORG integer;
begin
  oldyear = -1;
  oldmonth = -1;
  oldlshet = '-1';
  olddocumentcd = '-1';
  SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
  FOR SELECT YEAR_, MONTH_, YEAR2, MONTH2, LSHET, FNATH, REGIMCD, SERVICECD, DATE_VV AS DATE_,
    EXTRACT(YEAR FROM DATE_VV) AS FYEAR, EXTRACT(MONTH FROM DATE_VV) AS FMONTH, EXTRACT(DAY FROM DATE_VV) AS FDAY, DOCUMENTCD, TYPE_, volume, VTYPE_
    FROM CNV$NACH
    WHERE FNATH <> 0
    order by year_,  month_, lshet, documentcd
    INTO :YEAR_, :MONTH_, :YEAR2, :MONTH2, :lshet,  :fnath,  :regimcd,  :servicecd, :date_,
      :fyear,  :fmonth, :fday, :documentcd, :type_, :volume, :VTYPE_
  DO BEGIN
    if ((:oldyear <> :year_) or (:oldmonth <> :month_) or (:oldlshet <> :lshet) or (:olddocumentcd <> :documentcd) ) then begin
       select documentcd from cnv$cnv_documentnumerator(:DOCUMENTCD, 'Import nachisl', :DATE_, :DATE_, :baseorg) into :ncaseid;
       UPDATE OR INSERT INTO PERERASHETCASE (CASEID, LSHET, NACHISLCASEID, AUTOUSE, IZMEN, FYEAR, FMONTH, FDAY, ISMONTH, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY)
         VALUES (:ncaseid, :LSHET, :ncaseid, 1, 0, :FYEAR, :FMONTH, :FDAY, 0, :YEAR2, :MONTH2, 1, :YEAR2, :MONTH2, 1);
       oldyear = :year_;
       oldmonth = :month_;
       oldlshet = :lshet;
       olddocumentcd = :documentcd;
    end
    INSERT INTO NACHISLSUMMA (LSHET, CASEID, KODREGIM, BALANCE_KOD, SUMMATYPE, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, SUMMA, NORMTYPE)
    VALUES (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :TYPE_, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, :FNATH, 0);
	if (:volume <> 0) then
	    INSERT INTO NACHISLVOLUMS (LSHET, CASEID, KODREGIM, BALANCE_KOD, VOLUMETYPE, VOLUME, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, NORMTYPE)
		VALUES (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :VTYPE_, :volume, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, 0);
  END
end^

SET TERM ; ^

GRANT SELECT ON EXTORGSPR TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT SELECT ON CNV$NACH TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT SELECT,INSERT,UPDATE ON PERERASHETCASE TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT INSERT ON NACHISLSUMMA TO PROCEDURE CNV$CNV_01600_NACHISLIMPORT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01600_NACHISLIMPORT TO SYSDBA;


/* CNV_01700_PERERASHETIMPORT */
SET TERM ^ ;

create or alter procedure CNV$CNV_01700_PERERASHETIMPORT
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable YEAR2 integer;
declare variable MONTH2 integer;
declare variable LSHET varchar(10);
declare variable PROCHL numeric(18,4);
declare variable PROCHLVOLUME numeric(18,4);
declare variable REGIMCD integer;
declare variable SERVICECD integer;
declare variable FYEAR integer;
declare variable FMONTH integer;
declare variable FDAY integer;
declare variable DATE_VV timestamp;
declare variable DOCUMENTCD varchar(20);
declare variable TYPE_ integer;
declare variable DOCNAME varchar(150);
declare variable DOCDATE timestamp;
declare variable OLDYEAR integer;
declare variable OLDMONTH integer;
declare variable OLDLSHET varchar(10);
declare variable IDOCNAME varchar(150);
declare variable IDATE timestamp;
declare variable NCASEID integer;
declare variable BASEORG integer;
BEGIN
  oldyear = -1;
  oldmonth = -1;
  oldlshet = '-1';
  SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
  FOR SELECT year_, month_, year2, month2, lshet, prochl, regimcd, servicecd,
       EXTRACT(YEAR FROM date_vv) AS fyear,
       EXTRACT(MONTH FROM date_vv) AS fmonth,
       EXTRACT(DAY FROM date_vv) AS fday,
       date_vv,
       documentcd,
       type_,
       docname,
       docdate,
	   prochlvolume
    FROM cnv$nach
    WHERE prochl <> 0
    ORDER BY year_,month_,lshet
    INTO :year_, :month_, :year2, :month2, :lshet, :prochl, :regimcd, :servicecd,
         :fyear, :fmonth, :fday, :date_vv, :documentcd, :type_, :docname, :docdate, :prochlvolume
  DO BEGIN
    IF (:docname IS NULL) THEN
      idocname = 'Pererachet for ' || + CAST(:MONTH_ AS VARCHAR(10)) || '.' || CAST(:YEAR_ AS VARCHAR(10)) || ' lshet ' || :lshet;
    ELSE
      idocname = :docname;
    IF (:docdate IS NULL) THEN
      idate = :date_vv;
    ELSE
      idate = :docdate;

    IF ((:oldyear <> :year_) OR (:oldmonth <> :month_) OR (:oldlshet <> :lshet)) THEN BEGIN
       ncaseid = GEN_ID(DOCUMENTS_GEN, 1);
       INSERT INTO documents (documentcd, organizationcd, registerusercd, otvetstvusercd, doctypeid, docname, docdate, inputdate)
           VALUES (:ncaseid, :baseorg, 1, 1, 9, :idocname, :idate, :idate);
       INSERT INTO pererashetcase (caseid, lshet, nachislcaseid, autouse, izmen, fyear, fmonth, fday, ismonth, nyear, nmonth, nday, ayear, amonth, aday)
           VALUES (:ncaseid, :lshet, :ncaseid, 0, 1, :fyear, :fmonth, :fday, 0, :year_, :month_, 1, :year2, :month2, 1);
       oldyear = :year_;
       oldmonth = :month_;
       oldlshet = :lshet;
    END
    INSERT INTO nachislsumma (lshet, caseid, kodregim, balance_kod, summatype, nyear, nmonth, nday, ayear, amonth, aday, summa, normtype)
       VALUES (:lshet, :ncaseid, :regimcd, :servicecd, :type_, :year_, :month_, 1, :year2, :month2, 1, :prochl, 0);
	INSERT INTO NACHISLVOLUMS (LSHET, CASEID, KODREGIM, BALANCE_KOD, VOLUME, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, VOLUMETYPE, NORMTYPE)
		VALUES (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :PROCHLVOLUME, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, 1, 0);
  END
END^

SET TERM ; ^

GRANT SELECT ON EXTORGSPR TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT SELECT ON CNV$NACH TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT INSERT ON DOCUMENTS TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT INSERT ON PERERASHETCASE TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT INSERT ON NACHISLSUMMA TO PROCEDURE CNV$CNV_01700_PERERASHETIMPORT;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01700_PERERASHETIMPORT TO SYSDBA;


/* CNV_02100_EXTLSHETS */
SET TERM ^ ;

create or alter procedure CNV$CNV_02100_EXTLSHETS (
    EXTORGCD4EXTLSHET integer,
    EXTORGCD4EXTLSHET2 integer)
as
declare variable LSHET varchar(10);
declare variable EXTLSHET varchar(20);
BEGIN
  FOR SELECT lshet, extlshet
    FROM cnv$abonent
    WHERE (extlshet IS NOT NULL)
    INTO :lshet, :extlshet DO BEGIN
    UPDATE OR INSERT INTO extorgaccounts (extorgcd, lshet, extlshet) VALUES (:extorgcd4extlshet, :lshet, :extlshet);
  END
  FOR SELECT lshet, extlshet2
    FROM cnv$abonent
    WHERE (extlshet2 IS NOT NULL)
    INTO :lshet, :extlshet DO BEGIN
    UPDATE OR INSERT INTO extorgaccounts (extorgcd, lshet, extlshet) VALUES (:extorgcd4extlshet2, :lshet, :extlshet);
  END
END^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_02100_EXTLSHETS;

GRANT SELECT,INSERT,UPDATE ON EXTORGACCOUNTS TO PROCEDURE CNV$CNV_02100_EXTLSHETS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_02100_EXTLSHETS TO SYSDBA;


/* CNV_03300_LGOTSUMMA */
SET TERM ^ ;
create or alter procedure CNV$CNV_03300_LGOTSUMMA
as
declare variable YEAR_ integer;
declare variable MONTH_ integer;
declare variable YEAR2 integer;
declare variable MONTH2 integer;
declare variable LSHET varchar(10);
declare variable SUMMA numeric(18,4);
declare variable REGIMCD integer;
declare variable SERVICECD integer;
declare variable DATE_ date;
declare variable FYEAR integer;
declare variable FMONTH integer;
declare variable FDAY integer;
declare variable DOCUMENTCD varchar(20);
declare variable TYPE_ integer;
declare variable OLDYEAR integer;
declare variable OLDMONTH integer;
declare variable OLDLSHET varchar(10);
declare variable OLDDOCUMENTCD varchar(20);
declare variable NCASEID integer;
declare variable BASEORG integer;
declare variable cityzenid integer;
declare variable lgotacd integer;
begin
  oldyear = -1;
  oldmonth = -1;
  oldlshet = '-1';
  olddocumentcd = '-1';
  SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
  FOR SELECT YEAR_, MONTH_, YEAR2, MONTH2, LSHET, SUMMA, REGIMCD, SERVICECD, DATE_VV AS DATE_,
    EXTRACT(YEAR FROM DATE_VV) AS FYEAR, EXTRACT(MONTH FROM DATE_VV) AS FMONTH, EXTRACT(DAY FROM DATE_VV) AS FDAY, DOCUMENTCD, TYPE_, cityzenid, lgotacd
    FROM CNV$LGOTSUMMA
    WHERE SUMMA <> 0
    order by year_,  month_, lshet, documentcd
    INTO :YEAR_, :MONTH_, :YEAR2, :MONTH2, :lshet,  :summa,  :regimcd,  :servicecd, :date_,
      :fyear,  :fmonth, :fday, :documentcd, :type_, :cityzenid, :lgotacd
  DO BEGIN
    begin
		if ((select count(*)
		     from CNV$CITYZENLGOTA
		     where CITYZENID = :CITYZENID and
		           LGOTACD = :LGOTACD) > 0) then
		begin
		  if ((:OLDYEAR <> :YEAR_) or (:OLDMONTH <> :MONTH_) or (:OLDLSHET <> :LSHET) or (:OLDDOCUMENTCD <> :DOCUMENTCD)) then
		  begin
		    select DOCUMENTCD
		    from CNV$CNV_DOCUMENTNUMERATOR(:DOCUMENTCD, 'lgotsumma import', :DATE_, :DATE_, :BASEORG)
		    into :NCASEID;
		    update or insert into PERERASHETCASE (CASEID, LSHET, NACHISLCASEID, AUTOUSE, IZMEN, FYEAR, FMONTH, FDAY,
		                                          ISMONTH, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY)
		    values (:NCASEID, :LSHET, :NCASEID, 1, 0, :FYEAR, :FMONTH, :FDAY, 0, :YEAR2, :MONTH2, 1, :YEAR2, :MONTH2, 1);
		    OLDYEAR = :YEAR_;
		    OLDMONTH = :MONTH_;
		    OLDLSHET = :LSHET;
		    OLDDOCUMENTCD = :DOCUMENTCD;
		  end
		  insert into LGOTSUMMA (LSHET, CASEID, KODREGIM, BALANCE_KOD, SUMMATYPE, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY,
		                         SUMMA, NORMTYPE, CITYZEN_ID, KOD)
		  values (:LSHET, :NCASEID, :REGIMCD, :SERVICECD, :TYPE_, :YEAR_, :MONTH_, 1, :YEAR2, :MONTH2, 1, :SUMMA, 0,
		          :CITYZENID, :LGOTACD);
		end
	end
  END
end^

SET TERM ; ^