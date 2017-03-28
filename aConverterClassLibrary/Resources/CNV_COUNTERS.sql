/******************************************************************************/
/***               Generated by IBExpert 30.06.2015 23:21:29                ***/
/******************************************************************************/

/******************************************************************************/
/***      Following SET SQL DIALECT is just for the Database Comparer       ***/
/******************************************************************************/
/* SET SQL DIALECT 3; */



/******************************************************************************/
/***                                 Tables                                 ***/
/******************************************************************************/


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
    NEXTPOV     DATE,
    PRIM_       VARCHAR(100),
    DEACTDATE   TIMESTAMP,
    TAG         VARCHAR(30),
	GUID_       VARCHAR(50),
    NAME        VARCHAR(50)
);




/******************************************************************************/
/***                              Primary Keys                              ***/
/******************************************************************************/

ALTER TABLE CNV$COUNTERS ADD PRIMARY KEY (ID);


/******************************************************************************/
/***                                Indices                                 ***/
/******************************************************************************/

CREATE INDEX CNV$COUNTERS_IDX1 ON CNV$COUNTERS (LSHET);
CREATE INDEX CNV$COUNTERS_IDX2 ON CNV$COUNTERS (COUNTERID);

/******************************************************************************/
/***                                Triggers                                ***/
/******************************************************************************/


SET TERM ^ ;



/******************************************************************************/
/***                          Triggers for tables                           ***/
/******************************************************************************/



/* Trigger: CNV$COUNTERS_BI */
CREATE OR ALTER TRIGGER CNV$COUNTERS_BI FOR CNV$COUNTERS
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CNV$COUNTERS_ID,1);
END
^


SET TERM ; ^



/******************************************************************************/
/***                               Privileges                               ***/
/******************************************************************************/
