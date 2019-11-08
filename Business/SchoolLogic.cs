using System;
using System.Collections.Generic;
using System.Text;
using Business.Interfaces;
using Data;
using PabedaSchool.Data;

namespace Business
{
    public class SchoolLogic : ISchoolLogic
    {

        public DALSchool _dal;
     
        public SchoolLogic( ApplicationDbContextData context)
        {
          
            _dal = new DALSchool(context);
        }

        public string AddSchool(string Name, string Address, string City, string District)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                if (!_dal.IsExist(Name))
                {
                    _dal.AddSchool(School.CreateStudent(Name, Address, City, District));
                    return "true";
                }
                else
                {
                    return "This School Is Exist Before";
                }
            }
            else
            {
                return "The Name Of School Not Allow To be Empty";

            }
        }

        public string DeleteSchool(int Id)
        {
            if (!_dal.IsExist(Id))
            {
                _dal.DeleteSchool(Id);
                return "true";
            }
            else
            {
                return "This School Is Exist Before";
            }
        }

        public List<School> GetListSchools()
        {
            var ListSchool = _dal.GetSchoolList();

            return ListSchool;
        }

        public string UpdateSchool(int Id, string Address, string City, string District)
        {
            if (!_dal.IsExist(Id))
            {
                var UpdateSchool = _dal.GetSchool(Id);
                UpdateSchool.Address = Address;
                UpdateSchool.City = City;
                UpdateSchool.District = District;
                _dal.UpdateSchool(UpdateSchool);
                return "true";
            }
            else
            {
                return "This School Is Exist Before";
            }
        }
    }
}
