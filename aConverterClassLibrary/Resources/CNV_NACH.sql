CREATE TABLE CNV$NACH (
  ID INTEGER NOT NULL,
  LSHET varchar(10) NOT NULL,
  DOCUMENTCD varchar(20) NOT NULL,
  MONTH_ INTEGER NOT NULL,
  YEAR_ INTEGER NOT NULL,
  MONTH2 INTEGER NOT NULL,
  YEAR2 INTEGER NOT NULL,
  FNATH NUMERIC(18,4) NOT NULL,
  PROCHL NUMERIC(18,4) NOT NULL,
  VOLUME NUMERIC(18,4) NOT NULL,
  REGIMCD INTEGER NOT NULL,
  REGIMNAME varchar(50) NOT NULL,
  SERVICECD INTEGER NOT NULL,
  SERVICENAM varchar(50) NOT NULL,
  DATE_VV timestamp NOT NULL,
  TYPE_ INTEGER NOT NULL,
  DOCNAME varchar(150),
  DOCNUMBER varchar(10),
  DOCDATE timestamp,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$NACH_IDX1 ON CNV$NACH (LSHET);
CREATE SEQUENCE GEN_CNV$NACH_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$NACH_BI FOR CNV$NACH
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$NACH_ID,1);
END
^
SET TERM ; ^
