-- Query: Get the list of not discontinued products including category name ordered by product name.
select c.CategoryName,p.* 
from Products p 
inner join Categories c on c.CategoryID = p.CategoryID
where p.Discontinued = 0 
order by ProductName;