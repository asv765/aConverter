SET TERM ^ ;

create or alter procedure CNV$CNV_01120_COUNTERTYPEADDCHAR
as
begin
	merge into countermarkchars ad
	using cnv$countertypeaddchar ca on ca.countertypeid = ad.markaid and ca.addcharcd = ad.additionalcharcd
	when matched then
		update set ad.significance = ca.value_
	when not matched then
		insert(additionalcharcd, markaid, significance)
			values(ca.addcharcd, ca.countertypeid, ca.value_);
end^

SET TERM ; ^