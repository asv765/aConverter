CREATE TABLE CNV$COUNTERTYPEADDCHAR (
  ID INTEGER NOT NULL,
  COUNTERTYPEID INTEGER,
  ADDCHARCD INTEGER,
  VALUE_ varchar(2000),
  PRIMARY KEY (ID)
);
CREATE SEQUENCE GEN_CNV$COUNTERTYPEADDCHAR_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$COUNTERTYPEADDCHAR_BI FOR CNV$COUNTERTYPEADDCHAR
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$COUNTERTYPEADDCHAR_ID,1);
END
^
SET TERM ; ^
