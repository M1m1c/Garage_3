using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage_3.Data;
using Microsoft.AspNetCore.Mvc;

namespace Garage_3.Controllers
{
    public class RemoteController : Controller
    {
        private readonly Garage_3Context _context;

        public RemoteController(Garage_3Context context)
        {
            _context = context;
        }

        //------------------------------------REMOTES------------------------------------------------------------------

        [HttpPost]
        public JsonResult RegNumExists(string RegNum)
        {
            return Json((_context.Vehicle.Any(v => v.RegNum.ToUpper() == RegNum.ToUpper())) == false);
        }

        [HttpPost]
        public JsonResult UserNameExists(string UserName)
        {
            return Json(DoesUserNameMatch(UserName) == false);
        }

        [HttpPost]
        public JsonResult EmailExists(string Email)
        {
            return Json(_context.Owners.Any(o => o.Email == Email) == false);
        }

        [HttpPost]
        public JsonResult PhoneExists(string Telephone)
        {
            return Json(_context.Owners.Any(o => o.Telephone == Telephone) == false);
        }

        [HttpPost]
        public JsonResult DoesOwnerExists(string Owner)
        {
            return Json(DoesUserNameMatch(Owner));
        }

        [HttpPost]
        public JsonResult DoesVehicleTypeExist(string VehicleType)
        {
            return Json(_context.VehicleTypes.Any(vt => vt.VehicleTypeName == VehicleType.ToUpper()));
        }

        [HttpPost]
        public JsonResult DoesColorTypeExist(string ColorName)
        {
            return Json(_context.Colors.Any(c => c.ColorName == ColorName.ToUpper()));
        }

        [HttpPost]
        public JsonResult ColorNameSameOrUnique(string ColorName, int id)
        {
            return Json(_context.Colors.Where(c => c.Id != id ).Any(c => c.ColorName == ColorName) == false);
        }

        [HttpPost]
        public JsonResult VehicleTypeSameOrUnique(string VehicleTypeName, int id)
        {
            return Json(_context.VehicleTypes.Where(t => t.Id != id).Any(t => t.VehicleTypeName == VehicleTypeName) == false);
        }

        [HttpPost]
        public JsonResult UserNameSameOrUnique(string UserName, int MemberNumber)
        {
            //tar ut alla som inte har samma id, letar efter samma username, får inte vara true
            return Json(_context.Owners.Where(o => o.MemberNumber != MemberNumber)
                .Any(o => o.UserName == UserName) == false);
        }

        [HttpPost]
        public JsonResult EmailSameOrUnique(string Email, int MemberNumber)
        {
            return Json(_context.Owners.Where(o => o.MemberNumber != MemberNumber)
                .Any(o => o.Email == Email) == false);
        }

        [HttpPost]
        public JsonResult PhoneSameOrUnique(string Telephone, int MemberNumber)
        {
            return Json(_context.Owners.Where(o => o.MemberNumber != MemberNumber)
                .Any(o => o.Telephone == Telephone) == false);
        }

        private bool DoesUserNameMatch(string userName)
        {
            return _context.Owners.Any(o => o.UserName == userName);
        }
    }
}