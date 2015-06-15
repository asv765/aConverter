CREATE TABLE CNV$PENI (
  ID INTEGER NOT NULL,
  LSHET VARCHAR(10) NOT NULL,
  SERVICECD INTEGER NOT NULL,
  DOLGDATE TIMESTAMP NOT NULL,
  DOLG NUMERIC(18,4) NOT NULL,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$PENI_IDX1 ON CNV$PENI (LSHET);
CREATE SEQUENCE GEN_CNV$PENI_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$PENI_BI FOR CNV$PENI
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$PENI_ID,1);
END
^
SET TERM ; ^
