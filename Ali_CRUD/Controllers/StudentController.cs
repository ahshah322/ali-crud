using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ali_CRUD.Context;


namespace Ali_CRUD.Controllers
{
    public class StudentController : Controller
    {
        //We create a database Object (DbObj) in this controler to access database i.e Ali_CRUD = Ali_CRUD.Context.DbObj
        Ali_CRUDEntities DbObj = new Ali_CRUDEntities();

        // GET: Student
        public ActionResult Student(tbl_Student Obj)
        {
            if(Obj != null)
            {
                return View(Obj);
            }
            else
            {
                return View();
            }

            
        }

        /*
         * Action result name AddStudent which have an argument that contains the information about the
         * the student to be add. In this action we access Student table from database and save it in an Object (Obj).
         * Here model contains the data comming from the Form data and assing value to newly created Ojbect in database.
         * After that first we check if the form validation are applied or not i.e (ModelState.IsValid), Also if the data
         * comming from the Form having Id==0 we only then add the Student otherwise we will Edit the record by
         * else part of if condition.
         */
        [HttpPost]
        public ActionResult AddStudent(tbl_Student model)
        {
            tbl_Student Obj = new tbl_Student();

            if (ModelState.IsValid)
            {
                Obj.Id = model.Id;
                Obj.Name = model.Name;
                Obj.Fname = model.Fname;
                Obj.Email = model.Email;
                Obj.Mobile = model.Mobile;
                Obj.Description = model.Description;

                if (model.Id == 0)
                {
                    DbObj.tbl_Student.Add(Obj);
                    DbObj.SaveChanges();
                }
                else
                {
                    DbObj.Entry(Obj).State = EntityState.Modified;
                    DbObj.SaveChanges();
                }
            }

            ModelState.Clear();

            return View("Student");
        }

        // StudentList action result will show all the records in Student table in a list format and shows reuslt on Studentlist page. 
        public ActionResult StudentList()
        {
            var res = DbObj.tbl_Student.ToList();
            return View(res);
        }


        // Delete action result will take an arugment Id that is the ID to be deleted and check throug Where clause that
        // if Id's are same then delete that record from database and savecanges are made to update the database.
        // And also a new list of database records sent to the StudentList page which will shows the updated databse record.
        public ActionResult Delete(int id)
        {
            var res = DbObj.tbl_Student.Where(x => x.Id == id).First();
            DbObj.tbl_Student.Remove(res);
            DbObj.SaveChanges();

            var list = DbObj.tbl_Student.ToList();

            return View("StudentList", list);
        }
    }
}