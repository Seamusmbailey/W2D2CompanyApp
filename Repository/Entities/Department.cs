namespace CompanyApp.Entities;

public class Department
{
    public int DepartmentID {get;set;}
    public string DepartmentName {get;set;}
    
    public ICollection<Employee> Employees {get;set;}
}