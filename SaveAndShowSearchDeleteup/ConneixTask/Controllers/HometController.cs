using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConneixTask.Models;
using System.Data;
using System.Data.Entity;

namespace ConneixTask.Controllers
{
    public class HometController : Controller
    {
        public taskempEntities db = new taskempEntities();
        // GET: Homet
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveAndUpdateProduct(int Emp_Id, string Emp_FirstName, string Emp_LastName, string Emp_Email,
            string Emp_Salary,string Emp_Designation,string Emp_DOB,string Emp_Address,string Gender)
        {
            var result = new jsonMessage();
            try
            {
                //define the model  
                Employee emp = new Employee();
                emp.Emp_Id = Emp_Id;
                emp.Emp_FirstName = Emp_FirstName;
                emp.Emp_LastName = Emp_LastName;
                emp.Emp_Email = Emp_Email;
                emp.Emp_Salary =Convert.ToDecimal(Emp_Salary);
                emp.Emp_Designation = Emp_Designation;
                emp.Emp_DOB =Convert.ToDateTime(Emp_DOB);
                emp.Emp_Address = Emp_Address;
                emp.Gender = Gender;


                //for insert recored..  
                if (emp.Emp_Id == 0)
                {
                    db.Employees.Add(emp);
                    result.Message = "your product has been saved success..";
                    result.Status = true;
                }
                else  //for update recored..  
                {
                    db.Entry(emp).State = EntityState.Modified;
                    result.Message = "your product has been updated successfully..";
                    result.Status = true;
                }
                db.SaveChanges();


            }
            catch (Exception ex)
            {                
                result.Message = "We are unable to process your request at this time. Please try again later.";
                result.Status = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployee()
        {

            List<Employee> _list = new List<Employee>();

            try
            {
                _list = db.Employees.ToList();
                var result = from c in _list
                             select new[]
                             {
                          Convert.ToString( c.Emp_Id ),  // 0     
                          Convert.ToString( c.Emp_FirstName ),  // 1     
                          Convert.ToString( c.Emp_LastName ),  // 2     
                          Convert.ToString( c.Emp_Email ),  // 3     
                          Convert.ToString( c.Emp_Designation ),  // 0     
                          Convert.ToString( c.Emp_Salary ),  // 1     
                          Convert.ToString( c.Emp_DOB ),  // 2     
                          Convert.ToString( c.Emp_Address ),
                          Convert.ToString( c.Gender),
                                                   };

                return Json(new
                {
                    aaData = result
                }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {               
                return Json(new
                {
                    aaData = new List<string[]> { }
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeleteEmployee(int id)
        {
            var result = new jsonMessage();
            try
            {

                var emp = new Employee { Emp_Id = id };
                db.Entry(emp).State = EntityState.Deleted;
                db.SaveChanges();


                result.Message = "your product has been deleted successfully..";
                result.Status = true;

            }
            catch (Exception ex)
            {                
                result.Message = "We are unable to process your request at this time. Please try again later.";
                result.Status = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}