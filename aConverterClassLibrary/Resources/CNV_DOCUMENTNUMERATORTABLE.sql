CREATE TABLE CNV$DOCUMENTNUMERATORTABLE  (
    DOCUMENTCD  INTEGER NOT NULL,
    IMPORTKEY   VARCHAR(20) NOT NULL
);
ALTER TABLE CNV$DOCUMENTNUMERATORTABLE ADD PRIMARY KEY (IMPORTKEY);
CREATE UNIQUE INDEX CNV$DOCUMENTNUMERATORTABLE_IDX1 ON CNV$DOCUMENTNUMERATORTABLE (DOCUMENTCD); 
