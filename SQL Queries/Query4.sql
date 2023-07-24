-- Query: Get the name of all employees that directly or indirectly report to Andrew Fuller.
with recursiveEmployees as (
select ReportsTo,EmployeeID,(FirstName+' '+LastName) EmployeeName from Employees
where FirstName='Andrew' and LastName='Fuller'

union all

select e.ReportsTo, e.EmployeeID, (e.FirstName+' '+e.LastName) EmployeeName from Employees e
inner join recursiveEmployees root on e.ReportsTo = root.EmployeeID)

select EmployeeName from recursiveEmployees where EmployeeName !='Andrew Fuller';