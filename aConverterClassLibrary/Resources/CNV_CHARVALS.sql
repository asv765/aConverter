CREATE TABLE CNV$CHARVALS (
  ID INTEGER NOT NULL,
  ADDCHARCD INTEGER,
  ADDCVALUE INTEGER,
  ADDCNAME varchar(50),
  PRIMARY KEY (ID)
);
CREATE SEQUENCE GEN_CNV$CHARVALS_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$CHARVALS_BI FOR CNV$CHARVALS
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$CHARVALS_ID,1);
END
^
SET TERM ; ^
