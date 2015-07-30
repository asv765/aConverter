select n.lshet, n.servicecd, n.servicenam, n.month_, n.year_, n.oplata, SUM(o.summa) as summa
from cnv$nachopl n inner join cnv$oplata o
    on n.lshet = o.lshet and
    n.servicecd = o.servicecd and
    n.year_ = EXTRACT(YEAR FROM o.date_vv) and
    n.month_ = EXTRACT(MONTH FROM o.date_vv)
group BY n.lshet, n.servicecd, n.servicenam, n.month_, n.year_, n.oplata
HAVING n.oplata <> SUM(o.summa)