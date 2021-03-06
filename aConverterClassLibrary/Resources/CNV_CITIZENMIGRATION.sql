﻿CREATE TABLE CNV$CITIZENMIGRATION (
  ID INTEGER NOT NULL,
  CITIZENID INTEGER,
  DATE_ TIMESTAMP,
  MIGRATIONTYPE INTEGER,
  DIRECTION INTEGER,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$CITIZENMIGRATION_IDX1 ON CNV$CITIZENMIGRATION (CITIZENID);
CREATE SEQUENCE GEN_CNV$CITIZENMIGRATION_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$CITIZENMIGRATION_BI FOR CNV$CITIZENMIGRATION
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$CITIZENMIGRATION_ID,1);
END
^
SET TERM ; ^