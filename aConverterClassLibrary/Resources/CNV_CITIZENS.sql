CREATE TABLE CNV$CITIZENS (
  ID INTEGER NOT NULL,
  LSHET varchar(10),
  CITIZENID INTEGER,
  F varchar(50),
  I varchar(50),
  O varchar(50),
  BIRTHDATE timestamp,
  STARTDATE timestamp,
  ENDDATE timestamp,
  LGOTA INTEGER,
  LGOTANAME varchar(100),
  DATWP timestamp,
  DATUP timestamp,
  DOCUMENTNAME varchar(50),
  SERIA varchar(20),
  NOMER varchar(50),
  DATDN timestamp,
  DORGNAME varchar(50),
  KOLLG INTEGER,
  HOZ INTEGER,
  BIRTHPLACE varchar(50),
  SOB INTEGER,
  DOLYA NUMERIC(9,2),
  COMMENT_ varchar(100),
  PRIBYT varchar(100),
  VREMREG varchar(100),
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$CITIZENS_IDX1 ON CNV$CITIZENS (LSHET);
CREATE SEQUENCE GEN_CNV$CITIZENS_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$CITIZENS_BI FOR CNV$CITIZENS
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$CITIZENS_ID,1);
END
^
SET TERM ; ^
