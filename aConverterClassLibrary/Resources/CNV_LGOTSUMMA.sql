CREATE TABLE CNV$LGOTSUMMA (
  ID INTEGER NOT NULL,
  LSHET varchar(10) NOT NULL,
  CITYZENID INTEGER NOT NULL,
  LGOTACD INTEGER NOT NULL,
  LGOTANAME VARCHAR(100),
  REGIMCD INTEGER NOT NULL,
  REGIMNAME varchar(50) NOT NULL,
  SERVICECD INTEGER NOT NULL,
  SERVICENAME varchar(50) NOT NULL,
  SUMMA NUMERIC(18,4) NOT NULL,
  MONTH_ INTEGER NOT NULL,
  YEAR_ INTEGER NOT NULL,
  MONTH2 INTEGER NOT NULL,
  YEAR2 INTEGER NOT NULL,
  DATE_VV timestamp NOT NULL,
  TYPE_ INTEGER NOT NULL,
  COUNTERCD INTEGER,
  SUPPLIERID INTEGER,
  DOCUMENTCD varchar(20) NOT NULL,
  DOCNAME varchar(150),
  DOCNUMBER varchar(10),
  DOCDATE timestamp,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$LGOTSUMMA_IDX1 ON CNV$LGOTSUMMA (LSHET);
CREATE SEQUENCE GEN_CNV$LGOTSUMMA_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$LGOTSUMMA_BI FOR CNV$LGOTSUMMA
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL OR NEW.ID = 0) THEN
    NEW.ID = GEN_ID(GEN_CNV$LGOTSUMMA_ID,1);
END
^
SET TERM ; ^
