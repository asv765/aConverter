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