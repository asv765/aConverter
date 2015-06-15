CREATE TABLE CNV$GCOUNTER (
  ID INTEGER NOT NULL,
  COUNTERID varchar(20),
  LSHET varchar(10),
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$GCOUNTER_IDX1 ON CNV$GCOUNTER (LSHET);
CREATE SEQUENCE GEN_CNV$GCOUNTER_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$GCOUNTER_BI FOR CNV$GCOUNTER
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$GCOUNTER_ID,1);
END
^
SET TERM ; ^
