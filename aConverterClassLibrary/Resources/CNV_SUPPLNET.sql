CREATE TABLE CNV$SUPPLNET (
  ID INTEGER NOT NULL,
  LSHET varchar(10) DEFAULT NULL,
  SERVICECD INTEGER DEFAULT NULL,
  KNOTL1CD INTEGER DEFAULT NULL,
  KNOTL1NAME varchar(100) DEFAULT NULL,
  KNOTL2CD INTEGER DEFAULT NULL,
  KNOTL2NAME varchar(100) DEFAULT NULL,
  SUPPDATE timestamp DEFAULT NULL,
  CONNECTED SMALLINT DEFAULT NULL,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$SUPPLNET_IDX1 ON CNV$SUPPLNET (LSHET);
CREATE SEQUENCE GEN_CNV$SUPPLNET_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$SUPPLNET_BI FOR CNV$SUPPLNET
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$SUPPLNET_ID,1);
END
^
SET TERM ; ^