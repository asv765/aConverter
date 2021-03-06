/******************************************************************************/
/***               Generated by IBExpert 01.07.2015 18:30:24                ***/
/******************************************************************************/

/******************************************************************************/
/***      Following SET SQL DIALECT is just for the Database Comparer       ***/
/******************************************************************************/
/* SET SQL DIALECT 3; */



/******************************************************************************/
/***                                 Tables                                 ***/
/******************************************************************************/


CREATE GENERATOR GEN_CNV$ABONENT_ID;

CREATE TABLE CNV$ABONENT (
    ID             INTEGER NOT NULL,
    LSHET          VARCHAR(10),
    HOUSECD        INTEGER,
    DISTKOD        INTEGER,
    DISTNAME       VARCHAR(40),
    RAYONKOD       INTEGER,
    RAYONNAME      VARCHAR(40),
	SETTLEMENTKOD  INTEGER,
	SETTLEMENTNAME VARCHAR(200),
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
	HOUSENOTE      VARCHAR(4000),
    KORPUSNO       INTEGER,
    KORPUSPOSTFIX  VARCHAR(10),
    FLATNO         INTEGER,
    FLATPOSTFIX    VARCHAR(10),
    ROOMNO         SMALLINT,
	ROOMPOSTFIX	   VARCHAR(10),
	CLOSEDATE	   TIMESTAMP,
	EMAIL		   VARCHAR(500),
	STARTDATE	   TIMESTAMP
);




/******************************************************************************/
/***                              Primary Keys                              ***/
/******************************************************************************/

ALTER TABLE CNV$ABONENT ADD PRIMARY KEY (ID);


/******************************************************************************/
/***                                Indices                                 ***/
/******************************************************************************/

CREATE INDEX CNV$ABONENT_IDX1 ON CNV$ABONENT (LSHET);
CREATE INDEX CNV$ABONENT_IDX2 ON CNV$ABONENT (HOUSECD);


/******************************************************************************/
/***                                Triggers                                ***/
/******************************************************************************/


SET TERM ^ ;



/******************************************************************************/
/***                          Triggers for tables                           ***/
/******************************************************************************/



/* Trigger: CNV$ABONENT_BI */
CREATE OR ALTER TRIGGER CNV$ABONENT_BI FOR CNV$ABONENT
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$ABONENT_ID,1);
END
^


SET TERM ; ^



/******************************************************************************/
/***                               Privileges                               ***/
/******************************************************************************/
