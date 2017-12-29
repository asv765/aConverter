CREATE TABLE CNV$COUNTERADDCHAR (
  ID INTEGER NOT NULL,
  COUNTERID INTEGER,
  ADDCHARCD INTEGER,
  VALUE_ varchar(2000),
  PRIMARY KEY (ID)
);
CREATE SEQUENCE GEN_CNV$COUNTERADDCHAR_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$COUNTERADDCHAR_BI FOR CNV$COUNTERADDCHAR
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$COUNTERADDCHAR_ID,1);
END
^
SET TERM ; ^
