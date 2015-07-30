create procedure CNV$CNV_00100_REGIONDISTRICTS
as
declare variable RAYONKOD integer;
declare variable RAYONNAME varchar(50);
declare variable CNT integer;
BEGIN
  FOR SELECT rayonkod, MAX(rayonname) AS rayonname, COUNT(*) AS cnt
        FROM cnv$abonent
        GROUP BY rayonkod
        INTO :rayonkod, :rayonname, :cnt
  DO BEGIN
     UPDATE OR INSERT INTO regiondistricts (regiondistrictcd, regiondistrictnm)
        VALUES (:rayonkod, :rayonname) MATCHING (regiondistrictcd);
  END
  EXECUTE STATEMENT 'ALTER SEQUENCE regiondistricts_g RESTART WITH 0';
  SELECT FIRST 1 GEN_ID(regiondistricts_g,
  (SELECT MAX(regiondistrictcd) + 1 FROM regiondistricts) )
  FROM rdb$generators WHERE rdb$generators.rdb$generator_name = 'REGIONDISTRICTS_G' INTO :cnt;
END
