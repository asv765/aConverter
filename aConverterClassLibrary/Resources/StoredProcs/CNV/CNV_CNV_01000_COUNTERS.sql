SET TERM ^ ;

create or alter procedure CNV$CNV_01000_COUNTERS (
    NEEDDELETE smallint)
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
BEGIN
    /*
    needdelete = 1 - удал€ютс€ счетчики, коды которых присутствовуют в таблице cnv$counters;
    needdelete = 2 - удал€ютс€ счетчики по всем импортируемым абонентам
    */
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
        equipmentid = GEN_ID(parentequi_gen, 1);
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
    EXECUTE STATEMENT 'SET GENERATOR ParentEquipment_Uniting TO 0';

    SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
    FOR SELECT pe.equipmentid, ci.oldind, ci.ob_em, ci.indication, ci.inddate, ci.documentcd, ci.indtype
    FROM cnv$cntrsind ci INNER JOIN parentequipment pe ON ci.counterid = pe.importtag
    WHERE (ci.oldind <> 0 OR ci.ob_em<>0 OR ci.indication<>0)
        INTO :equipmentid, :oldind, :ob_em, :indication, :inddate, :documentcd, :indtype
    DO BEGIN
        SELECT documentcd
            FROM cnv$cnv_documentnumerator(:documentcd, '»мпорт данных', :inddate, :inddate, :baseorg)
            INTO :newdocumentcd;
        INSERT INTO counterindication (counterindicationfactid, kod, indicationdate, documentcd, indicationvalue, previousindication, volume, indicationtype, dependfromcntindicationfactid)
        VALUES (NULL, :equipmentid, :inddate, :newdocumentcd, :indication, :oldind, :ob_em, :indtype, NULL);
    END
END^

SET TERM ; ^

GRANT SELECT,INSERT,DELETE ON COUNTERINDICATION TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT,INSERT,DELETE ON PARENTEQUIPMENT TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT ON CNV$COUNTERS TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT,INSERT ON ABONENTSEQUIPMENT TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT ON SYSTEMVARIABLES TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT INSERT ON RESOURCECOUNTERS TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT,INSERT,UPDATE ON EQSTATUSES TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT INSERT ON EQUIPMENTPLOMBS TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT ON EXTORGSPR TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT SELECT ON CNV$CNTRSIND TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01000_COUNTERS;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01000_COUNTERS TO SYSDBA;