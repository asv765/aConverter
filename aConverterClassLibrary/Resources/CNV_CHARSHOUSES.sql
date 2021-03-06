CREATE TABLE CNV$CHARSHOUSES (
  ID INTEGER NOT NULL,
  HOUSECD INTEGER,
  CHARCD INTEGER,
  CHARNAME VARCHAR(50),
  VALUE_ NUMERIC(11,4),
  DATE_ TIMESTAMP,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$CHARSHOUSES_IDX1 ON CNV$CHARSHOUSES (HOUSECD);
CREATE SEQUENCE GEN_CNV$CHARSHOUSES_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$CHARSHOUSES_BI FOR CNV$CHARSHOUSES
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$CHARSHOUSES_ID,1);
END
^
SET TERM ; ^
