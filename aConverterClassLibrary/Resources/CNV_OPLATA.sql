CREATE TABLE CNV$OPLATA (
  ID INTEGER NOT NULL,
  LSHET varchar(10) NOT NULL,
  DOCUMENTCD varchar(20) NOT NULL,
  MONTH_ INTEGER NOT NULL,
  YEAR_ INTEGER NOT NULL,
  SUMMA NUMERIC(18,2) NOT NULL,
  DATE_ timestamp NOT NULL,
  DATE_VV timestamp NOT NULL,
  DATETIND timestamp DEFAULT NULL,
  SOURCECD INTEGER NOT NULL,
  SOURCENAME varchar(50) NOT NULL,
  SERVICECD INTEGER NOT NULL,
  SERVICENAME varchar(50) NOT NULL,
  PRIM_ varchar(100) DEFAULT NULL,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$OPLATA_IDX1 ON CNV$OPLATA (LSHET);
CREATE SEQUENCE GEN_CNV$OPLATA_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$OPLATA_BI FOR CNV$OPLATA
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$OPLATA_ID,1);
END
^
SET TERM ; ^
