SET TERM ^ ;

create or alter procedure CNV$CNV_01020_CNTRSIND
as
declare variable EQUIPMENTID integer;
declare variable OLDIND numeric(16,4);
declare variable OB_EM numeric(16,4);
declare variable INDICATION numeric(16,4);
declare variable INDDATE timestamp;
declare variable DOCUMENTCD varchar(20);
declare variable INDTYPE integer;
declare variable NEWDOCUMENTCD integer;
begin
    for select pe.equipmentid, ci.oldind, ci.ob_em, ci.indication, ci.Inddate, ci.documentcd, ci.indtype
    from cnv$cntrsind ci inner join parentequipment pe on ci.counterid = pe.importtag
    where (ci.oldind <> 0 OR ci.ob_em<>0 OR ci.indication<>0)
        into :equipmentid, :oldind, :ob_em, :indication, :inddate, :documentcd, :indtype
    do begin
        select documentcd
            from cnv$cnv_documentnumerator(:documentcd, 'Импорт данных', :inddate, :inddate)
            into :newdocumentcd;
        INSERT INTO COUNTERINDICATION (COUNTERINDICATIONFACTID, KOD, INDICATIONDATE, DOCUMENTCD, INDICATIONVALUE, PREVIOUSINDICATION, VOLUME, INDICATIONTYPE, DEPENDFROMCNTINDICATIONFACTID)
        values (null, :equipmentid, :inddate, :newdocumentcd, :INDICATION, :OLDIND, :OB_EM, :INDTYPE, NULL);
    end
end^

SET TERM ; ^

GRANT SELECT ON CNV$CNTRSIND TO PROCEDURE CNV$CNV_01020_CNTRSIND;

GRANT SELECT ON PARENTEQUIPMENT TO PROCEDURE CNV$CNV_01020_CNTRSIND;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01020_CNTRSIND;

GRANT INSERT ON COUNTERINDICATION TO PROCEDURE CNV$CNV_01020_CNTRSIND;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01020_CNTRSIND TO SYSDBA;
