-- Query: Get the total ordered amount (money) by year of the employee Steven Buchanan.
select YEAR(o.OrderDate) Year,SUM(od.UnitPrice * od.Quantity) TotalOrderedAmount 
from Employees e
inner join Orders o on o.EmployeeID = e.EmployeeID
inner join [Order Details] od on od.OrderID = o.OrderID
where FirstName='Steven' and LastName='Buchanan'
group by YEAR(o.OrderDate);