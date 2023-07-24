-- Query: Get all Nancy Davolio’s customers.
select distinct c.* 
from Employees e 
inner join Orders o on e.EmployeeID = o.EmployeeID
inner join Customers c on o.CustomerID = c.CustomerID
where e.FirstName='Nancy' and e.LastName='Davolio'; 