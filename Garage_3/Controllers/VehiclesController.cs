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
        public async Task<IActionResult> Index()
        {
            // var list = await _context.Vehicle.ToListAsync();

            //var viewModel = _context.Vehicle.Select(v => ToVehicleViewModel(v));
            var temp = _context.Vehicle.Select(v => v).Include(v => v.Color).Include(v => v.VehicleType).Include(v => v.Owner);
            return View(await temp.ToListAsync());
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
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
                Vehicles = owner.Vehicles
            };

            return View(model);
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

            viewModel.RegNum.ToUpper();

            if (ModelState.IsValid)
            {
                var vehicle = new Vehicle {
                    RegNum = viewModel.RegNum,
                    Wheels = viewModel.Wheels,
                    Model = viewModel.Model,
                    Brand = viewModel.Brand
                };

                vehicle.ArrivalTime = DateTime.Now;

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
            return Json(_context.Vehicle.Any(v => v.RegNum.ToUpper() == RegNum.ToUpper()) == false);
        }

        public IActionResult Park()
        {
            return View();
        }

        /* public async Task<IActionResult> Park(int? id)
         { 
         }*/
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
                return RedirectToAction(nameof(Index));
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
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RegNum,Wheels,Model,Brand,ArrivalTime,Color,MemberNumber,TypeID")] Vehicle vehicle)
        {
            if (id != vehicle.RegNum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(vehicle);
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
            return _context.Vehicle.Any(e => e.RegNum == id);
        }
        public async Task<IActionResult> OwnerIndex()
        {
            return View(await _context.Owners.ToListAsync());
        }
    }
}
