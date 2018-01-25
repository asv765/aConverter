SET TERM ^ ;

create or alter procedure CNV$CNV_01110_COUNTERADDCHAR
as
begin
	merge into equipmentadditionalchars ad
	using cnv$counteraddchar ca on ca.counterid = ad.equipmentid and ca.addcharcd = ad.additionalcharcd
	when matched then
		update set ad.significance = ca.value_
	when not matched then
		insert(additionalcharcd, equipmentid, significance)
			values(ca.addcharcd, ca.counterid, ca.value_);
end^

SET TERM ; ^