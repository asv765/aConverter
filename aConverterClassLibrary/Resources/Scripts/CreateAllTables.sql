/* DOCUMENTNUMERATORTABLE */
CREATE TABLE CNV$DOCUMENTNUMERATORTABLE  (
    DOCUMENTCD  INTEGER NOT NULL,
    IMPORTKEY   VARCHAR(20) NOT NULL
);
ALTER TABLE CNV$DOCUMENTNUMERATORTABLE ADD PRIMARY KEY (IMPORTKEY);
CREATE UNIQUE INDEX CNV$DOCUMENTNUMERATORTABLE_IDX1 ON CNV$DOCUMENTNUMERATORTABLE (DOCUMENTCD); 

/* AADDCHAR */
CREATE TABLE CNV$AADDCHAR (
    ID INTEGER NOT NULL,
    LSHET VARCHAR(10),
    ADDCHARCD INTEGER,
    "VALUE" VARCHAR(30),
    primary key (ID));
CREATE INDEX CNV$AADDCHAR_IDX1 ON CNV$AADDCHAR (LSHET);
CREATE SEQUENCE GEN_CNV$AADDCHAR_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$AADDCHAR_BI FOR CNV$AADDCHAR
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$AADDCHAR_ID,1);
END
^
SET TERM ; ^


/* ABONENT */
CREATE GENERATOR GEN_CNV$ABONENT_ID;

CREATE TABLE CNV$ABONENT (
    ID             INTEGER NOT NULL,
    LSHET          VARCHAR(10),
    HOUSECD        INTEGER,
    DISTKOD        INTEGER,
    DISTNAME       VARCHAR(40),
    RAYONKOD       INTEGER,
    RAYONNAME      VARCHAR(40),
    TOWNSKOD       INTEGER,
    TOWNSNAME      VARCHAR(40),
    ULICAKOD       INTEGER,
    ULICANAME      VARCHAR(40),
    F              VARCHAR(100),
    I              VARCHAR(50),
    O              VARCHAR(50),
    PRIM_          VARCHAR(250),
    EXTLSHET       VARCHAR(20),
    EXTLSHET2      VARCHAR(20),
    PHONENUM       VARCHAR(100),
    POSTINDEX      VARCHAR(6),
    DUCD           INTEGER,
    DUNAME         VARCHAR(50),
    ISDELETED      INTEGER NOT NULL,
    HOUSENO        VARCHAR(10),
    HOUSEPOSTFIX   VARCHAR(10),
    KORPUSNO       INTEGER,
    KORPUSPOSTFIX  VARCHAR(10),
    FLATNO         INTEGER,
    FLATPOSTFIX    VARCHAR(10),
    ROOMNO         SMALLINT,
	ROOMPOSTFIX	   VARCHAR(10),
	CLOSEDATE		TIMESTAMP
);

ALTER TABLE CNV$ABONENT ADD PRIMARY KEY (ID);

CREATE INDEX CNV$ABONENT_IDX1 ON CNV$ABONENT (LSHET);
CREATE INDEX CNV$ABONENT_IDX2 ON CNV$ABONENT (HOUSECD);

SET TERM ^ ;

CREATE OR ALTER TRIGGER CNV$ABONENT_BI FOR CNV$ABONENT
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$ABONENT_ID,1);
END
^

SET TERM ; ^

/* CHARLST */
CREATE TABLE CNV$CHARLST (
    ID INTEGER NOT NULL,
    ADDCHARCD    INTEGER,
    ADDCHARNAME  VARCHAR(50),
    ADDCHTYPE    SMALLINT,
    ADDCHARMODE  SMALLINT,
    SHORTNAME    VARCHAR(50)
);
CREATE INDEX CNV$CHARLST_IDX1 ON CNV$CHARLST (ADDCHARCD);
CREATE SEQUENCE GEN_CNV$CHARLST_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$CHARLST_BI FOR CNV$CHARLST
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$CHARLST_ID,1);
END
^
SET TERM ; ^


/* CHARS */
CREATE TABLE CNV$CHARS (
  ID INTEGER NOT NULL,
  LSHET VARCHAR(10),
  CHARCD INTEGER,
  CHARNAME VARCHAR(50),
  VALUE_ NUMERIC(11,4),
  DATE_ TIMESTAMP,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$CHARS_IDX1 ON CNV$CHARS (LSHET);
CREATE SEQUENCE GEN_CNV$CHARS_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$CHARS_BI FOR CNV$CHARS
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$CHARS_ID,1);
END
^
SET TERM ; ^


/* CHARVALS */
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


/* CITIZENMIGRATION */
CREATE TABLE CNV$CITIZENMIGRATION (
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


/* CITIZENS */
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


/* CITYZENLGOTA */
CREATE TABLE CNV$CITYZENLGOTA (
  ID INTEGER NOT NULL,
  CITYZENID INTEGER NOT NULL,
  LGOTACD INTEGER NOT NULL,
  LGOTANAME VARCHAR(100),
  STARTDATE TIMESTAMP,
  ENDDATE TIMESTAMP,
  PRIORITY INTEGER,
  ISDELETED INTEGER, 
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$CITYZENLGOTA_IDX1 ON CNV$CITYZENLGOTA (CITYZENID);
CREATE SEQUENCE GEN_CNV$CITYZENLGOTA_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$CITYZENLGOTA_BI FOR CNV$CITYZENLGOTA
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL or NEW.ID = 0) THEN
    NEW.ID = GEN_ID(GEN_CNV$CITYZENLGOTA_ID,1);
END
^
SET TERM ; ^


/* LGOTSUMMA */
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


/* CNTRSIND */
CREATE TABLE CNV$CNTRSIND (
  ID INTEGER NOT NULL,
  COUNTERID varchar(20),
  DOCUMENTCD varchar(20),
  OLDIND NUMERIC(16,4),
  OB_EM NUMERIC(16,4),
  INDICATION NUMERIC(16,4),
  INDDATE TIMESTAMP,
  INDTYPE INTEGER,
  PRIMARY KEY (ID)
);
CREATE SEQUENCE GEN_CNV$CNTRSIND_ID;
CREATE INDEX CNV$CNTRSIND_IDX1 ON CNV$CNTRSIND (COUNTERID);

SET TERM ^ ;
CREATE TRIGGER CNV$CNTRSIND_BI FOR CNV$CNTRSIND
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$CNTRSIND_ID,1);
END^

SET TERM ; ^

/* COUNTERS */

CREATE GENERATOR GEN_CNV$COUNTERS_ID;

CREATE TABLE CNV$COUNTERS (
    ID          INTEGER NOT NULL,
    COUNTERID   VARCHAR(20),
    LSHET       VARCHAR(10),
    CNTTYPE     INTEGER,
    CNTNAME     VARCHAR(50),
    SETUPDATE   TIMESTAMP,
    SERIALNUM   VARCHAR(30),
    SETUPPLACE  INTEGER,
    PLOMBDATE   TIMESTAMP,
    PLOMBNAME   VARCHAR(40),
    LASTPOV     DATE,
    NEXTPOV     TIMESTAMP,
    PRIM_       VARCHAR(100),
    DEACTDATE   TIMESTAMP,
    TAG         VARCHAR(30),
    NAME        VARCHAR(50)
);

ALTER TABLE CNV$COUNTERS ADD PRIMARY KEY (ID);

CREATE INDEX CNV$COUNTERS_IDX1 ON CNV$COUNTERS (LSHET);
CREATE INDEX CNV$COUNTERS_IDX2 ON CNV$COUNTERS (COUNTERID);

SET TERM ^ ;

CREATE OR ALTER TRIGGER CNV$COUNTERS_BI FOR CNV$COUNTERS
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$COUNTERS_ID,1);
END
^


SET TERM ; ^


/* DOGOVOR */
CREATE TABLE CNV$DOGOVOR (
  ID INTEGER NOT NULL,
  LSHET varchar(10),
  DOGTYPEKOD INTEGER,
  DOGTYPENAM varchar(50),
  DESCRIPTIO varchar(100),
  STARTDATE timestamp,
  ENDDATE timestamp,
  SERIA varchar(12),
  NOMER varchar(12),
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$DOGOVOR_IDX1 ON CNV$DOGOVOR (LSHET);
CREATE SEQUENCE GEN_CNV$DOGOVOR_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$DOGOVOR_BI FOR CNV$DOGOVOR
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$DOGOVOR_ID,1);
END
^
SET TERM ; ^


/* EQUIPMENT */
CREATE TABLE CNV$EQUIPMENT (
  ID INTEGER NOT NULL,
  LSHET varchar(10),
  GROUPKOD INTEGER,
  GROUPNAME varchar(50),
  MARKKOD INTEGER,
  MARKNAME varchar(50),
  SETUPDATE timestamp,
  SERIALNUM varchar(30),
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$EQUIPMENT_IDX1 ON CNV$EQUIPMENT (LSHET);
CREATE SEQUENCE GEN_CNV$EQUIPMENT_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$EQUIPMENT_BI FOR CNV$EQUIPMENT
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$EQUIPMENT_ID,1);
END
^
SET TERM ; ^


/* GCOUNTER */
CREATE TABLE CNV$GCOUNTER (
  ID INTEGER NOT NULL,
  COUNTERID varchar(20),
  LSHET varchar(10),
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$GCOUNTER_IDX1 ON CNV$GCOUNTER (LSHET);
CREATE SEQUENCE GEN_CNV$GCOUNTER_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$GCOUNTER_BI FOR CNV$GCOUNTER
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$GCOUNTER_ID,1);
END
^
SET TERM ; ^


/* CHARSHOUSES */
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


/* HADDCHAR */
CREATE TABLE CNV$HADDCHAR (
  ID INTEGER NOT NULL,
  HOUSECD INTEGER,
  ADDCHARCD INTEGER,
  VALUE_ varchar(30),
  DATE_ timestamp,
  PRIMARY KEY (ID)
);
CREATE SEQUENCE GEN_CNV$HADDCHAR_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$HADDCHAR_BI FOR CNV$HADDCHAR
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$HADDCHAR_ID,1);
END
^
SET TERM ; ^


/* LCHARS */
CREATE TABLE CNV$LCHARS (
  ID INTEGER NOT NULL,
  LSHET varchar(10),
  LCHARCD INTEGER,
  LCHARNAME varchar(100),
  VALUE_ INTEGER,
  VALUEDESC varchar(100),
  DATE_ TIMESTAMP,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$LCHARS_IDX1 ON CNV$LCHARS (LSHET);
CREATE SEQUENCE GEN_CNV$LCHARS_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$LCHARS_BI FOR CNV$LCHARS
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$LCHARS_ID,1);
END
^
SET TERM ; ^


/* NACH */
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
  PROCHLVOLUME NUMERIC(18,4) NOT NULL,
  VOLUME NUMERIC(18,4) NOT NULL,
  REGIMCD INTEGER NOT NULL,
  REGIMNAME varchar(50) NOT NULL,
  SERVICECD INTEGER NOT NULL,
  SERVICENAME varchar(50) NOT NULL,
  DATE_VV timestamp NOT NULL,
  TYPE_ INTEGER NOT NULL,
  VTYPE_ INTEGER,
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


/* NACHOPL */
CREATE TABLE CNV$NACHOPL (
  ID INTEGER NOT NULL,
  LSHET varchar(10) NOT NULL,
  MONTH_ INTEGER NOT NULL,
  YEAR_ INTEGER NOT NULL,
  MONTH2 INTEGER NOT NULL,
  YEAR2 INTEGER NOT NULL,
  BDEBET NUMERIC(18,4) NOT NULL,
  FNATH NUMERIC(18,4) NOT NULL,
  PROCHL NUMERIC(18,4) NOT NULL,
  OPLATA NUMERIC(18,2) NOT NULL,
  EDEBET NUMERIC(18,4) NOT NULL,
  SERVICECD INTEGER NOT NULL,
  SERVICENAME varchar(50) NOT NULL,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$NACHOPL_IDX1 ON CNV$NACHOPL (LSHET);
CREATE SEQUENCE GEN_CNV$NACHOPL_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$NACHOPL_BI FOR CNV$NACHOPL
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$NACHOPL_ID,1);
END
^
SET TERM ; ^


/* OPLATA */
CREATE TABLE CNV$OPLATA (
  ID INTEGER NOT NULL,
  LSHET varchar(10) NOT NULL,
  DOCUMENTCD varchar(20) NOT NULL,
  MONTH_ INTEGER NOT NULL,
  YEAR_ INTEGER NOT NULL,
  SUMMA NUMERIC(18,2) NOT NULL,
  DATE_ timestamp NOT NULL,
  DATE_VV timestamp NOT NULL,
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


/* PENI */
CREATE TABLE CNV$PENI (
  ID INTEGER NOT NULL,
  LSHET VARCHAR(10) NOT NULL,
  SERVICECD INTEGER NOT NULL,
  DOLGDATE TIMESTAMP NOT NULL,
  DOLG NUMERIC(18,4) NOT NULL,
  PRIMARY KEY (ID)
);
CREATE INDEX CNV$PENI_IDX1 ON CNV$PENI (LSHET);
CREATE SEQUENCE GEN_CNV$PENI_ID;

SET TERM ^ ;
CREATE TRIGGER CNV$PENI_BI FOR CNV$PENI
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$PENI_ID,1);
END
^
SET TERM ; ^


/* SUPPLNET */
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
