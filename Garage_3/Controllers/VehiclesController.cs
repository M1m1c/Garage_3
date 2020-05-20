using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_3.Data;
using Garage_3.Models;
using Garage_3.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Garage_3.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage_3Context _context;

        public VehiclesController(Garage_3Context context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index(string regNum)
        {
            var vehicles = VehicleSearch(regNum,_context.Vehicle);
            var temp = vehicles.Select(v => v).Include(v => v.Color).Include(v => v.VehicleType).Include(v => v.Owner);
            return View(await temp.ToListAsync());
        }

        private IQueryable<Vehicle> VehicleSearch(string regNum, DbSet<Vehicle> vehicles)
        {
            return string.IsNullOrWhiteSpace(regNum) ? 
                vehicles :
                vehicles.Where(v => v.RegNum.ToUpper().StartsWith(regNum.ToUpper()));
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.Include(v => v.Color).Include(v => v.VehicleType).Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.RegNum == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            
            return View(vehicle);
        }
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.MemberNumber == id);
            if (owner == null)
            {
                return NotFound();
            }
            var model = new Profile
            {
                MemberNumber = owner.MemberNumber,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                UserName = owner.UserName,
                Telephone = owner.Telephone,
                Email = owner.Email,
                Vehicles = await _context.Vehicle.Where(o=> o.MemberNumber==owner.MemberNumber).ToListAsync()
            };

            return View(model);
        }
        public IActionResult Return(string user)
        {
            var owner = _context.Owners.FirstOrDefault(o => o.UserName == user);
            if (owner!=null)
            {
               return RedirectToAction(nameof(Profile), new { id = owner.MemberNumber });
            }
            return NotFound();
        }
        // GET: Vehicles/Create
        public IActionResult AddVehicle(int? id)
        {
            if (_context.Owners.Any(o => o.MemberNumber == id))
            {
                return View();
            }
            return NotFound();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(int? id, [Bind("RegNum,Wheels,Model,Brand,ColorName,VehicleType")] AddVehicleViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var vehicle = new Vehicle {
                    RegNum = viewModel.RegNum.ToUpper(),
                    Wheels = viewModel.Wheels,
                    Model = viewModel.Model,
                    Brand = viewModel.Brand
                };

                
                int tempColorId = ColorSetup(viewModel.ColorName);

                vehicle.ColorId = tempColorId;

                vehicle.Color = _context.Colors.Find(tempColorId);


                int tempTypeId = VehicleTypeSetup(viewModel.VehicleType);

                vehicle.TypeID = tempTypeId;

                vehicle.VehicleType = _context.VehicleTypes.Find(tempTypeId);

                vehicle.MemberNumber = (int)id;
                vehicle.Owner = _context.Owners.Find((int)id);

                if (vehicle.Color != null && vehicle.VehicleType != null && vehicle.Owner != null)
                {
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(viewModel);
        }

        private int ColorSetup(string colorName)
        {
            if (_context.Colors.Any(c => c.ColorName.ToLower() == colorName.ToLower()) == false)
            {
                _context.Colors.Add(new Color
                {
                    ColorName = colorName.ToUpper()
                });

                _context.SaveChanges();
            }

            var tempColorId = _context.Colors.FirstOrDefault(c => c.ColorName.ToLower() == colorName.ToLower()).Id;
            return tempColorId;
        }

        private int VehicleTypeSetup(string VehicleTypeName)
        {
            if (_context.VehicleTypes.Any(c => c.VehicleTypeName.ToLower() == VehicleTypeName.ToLower()) == false)
            {
                _context.VehicleTypes.Add(new VehicleType
                {
                    VehicleTypeName = VehicleTypeName.ToUpper()
                });

                _context.SaveChanges();
            }

            var tempColorId = _context.VehicleTypes.FirstOrDefault(c => c.VehicleTypeName.ToLower() == VehicleTypeName.ToLower()).Id;
            return tempColorId;
        }

        [HttpPost]
        public JsonResult RegNumExists(string RegNum)
        {
            return Json(VehicleExists(RegNum) == false);
        }
        //[HttpPost]
        public IActionResult Park(string regNum)
        {
            var vehicle = _context.Vehicle.Find(regNum);

            if (vehicle != null)
            {
                if (vehicle.ParkedFlag == true)
                {
                    vehicle.ParkedFlag = false;
                    vehicle.DepartureTime = DateTime.Now;
                }
                else
                {
                    vehicle.ParkedFlag = true;
                    vehicle.ArrivalTime = DateTime.Now;
                }
                _context.SaveChanges();
            return RedirectToAction(nameof(Profile), new { id= vehicle.MemberNumber});
            }
            return NotFound();

        }
        public ReceiptViewModel ToReceiptViewModel(Vehicle vehicle)
        {
            var owner = _context.Owners.FirstOrDefault(o=>o.MemberNumber == vehicle.MemberNumber);
            
            var model = new ReceiptViewModel
            {
                RegNum = vehicle.RegNum,
                VehicleType = vehicle.VehicleType,
                UserName = owner.UserName,
                ArrivalTime = vehicle.ArrivalTime,
                DepartureTime = vehicle.DepartureTime
                
            };
            model.TotalParkedTime = model.DepartureTime - model.ArrivalTime;

            model.Price = model.TotalParkedTime.Hours * 100;
            model.Price += model.TotalParkedTime.Days * 24 * 100;
            return model;
        }
        public async Task<IActionResult> ShowReceipt(string regNum)
        {
            var vehicle = await _context.Vehicle.FindAsync(regNum);
            var model = ToReceiptViewModel(vehicle);
            return View(model);
        }
        public IActionResult AddOwner()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOwner([Bind("UserName,FirstName, LastName, Email, Telephone")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(OwnerIndex));
            }
            return View(owner);
        }

        [HttpPost]
        public JsonResult UserNameExists(string UserName)
        {
            return Json(_context.Owners.Any(o => o.UserName == UserName) == false);
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

        //TODO make this username check work
        [HttpPost]
        public JsonResult DoesOwnerExists(string Owner)
        {
            return Json(_context.Owners.Any(o => o.UserName == Owner));
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

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var model = ToEditViewModel(vehicle);
            return View(model);
        }
        
        private EditVehicleViewModel ToEditViewModel(Vehicle vehicle)
        {
            return new EditVehicleViewModel
            {
                RegNum = vehicle.RegNum,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Wheels = (int)vehicle.Wheels,
                Owner = _context.Owners.Find(vehicle.MemberNumber).UserName,
                ColorName = _context.Colors.Find(vehicle.ColorId).ColorName,
                VehicleType = _context.VehicleTypes.Find(vehicle.TypeID).VehicleTypeName
            };
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RegNum,Wheels,Model,Brand,ColorName,VehicleType,Owner")] EditVehicleViewModel editVehicle)
        {
            var vehicle = _context.Vehicle.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                vehicle.Wheels = editVehicle.Wheels;
                vehicle.Model = editVehicle.Model;
                vehicle.Brand = editVehicle.Brand;
                vehicle.Color = _context.Colors.FirstOrDefault(c=> c.ColorName== editVehicle.ColorName.ToUpper());
                vehicle.Owner = _context.Owners.FirstOrDefault(o => o.UserName == editVehicle.Owner);
                vehicle.VehicleType = _context.VehicleTypes.FirstOrDefault(vt => vt.VehicleTypeName == editVehicle.VehicleType.ToUpper());
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.RegNum))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.RegNum == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

            private bool VehicleExists(string id)
        {
            return _context.Vehicle.Any(v => v.RegNum.ToUpper() == id.ToUpper());
        }

        public async Task<IActionResult> OwnerIndex(string input)
        {
            var owners = string.IsNullOrWhiteSpace(input) ? 
                _context.Owners : 
                _context.Owners.Where(v => v.UserName.ToUpper().StartsWith(input.ToUpper()));

            var list = await owners.ToListAsync();

            var model = list.Select(o => ToOwnerIndex(o));

            return View(model);
        }

        public OwnerIndexViewModel ToOwnerIndex(Owner owner)
        {
            return new OwnerIndexViewModel {
                MemberNumber = owner.MemberNumber,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                UserName = owner.UserName,
                VehicleCount = _context.Vehicle.Select(v => v.MemberNumber == owner.MemberNumber).Count()          
            };
        }
    }
}
