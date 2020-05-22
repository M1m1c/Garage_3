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
    public class OwnerController : Controller
    {
        private readonly Garage_3Context _context;

        public OwnerController(Garage_3Context context)
        {
            _context = context;
        }
        //------------------------------------OWNER------------------------------------------------------------------

        public async Task<IActionResult> OwnerIndex(string input)
        {
            var owners = string.IsNullOrWhiteSpace(input) ?
                _context.Owners :
                _context.Owners.Where(v => v.UserName.ToUpper().StartsWith(input.ToUpper()));

            var list = await owners.ToListAsync();

            var model = list.Select(o => ToOwnerIndex(o));

            return View(model);
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
                Vehicles = await _context.Vehicle.Where(o => o.MemberNumber == owner.MemberNumber).ToListAsync()
            };

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

        public async Task<IActionResult> EditOwner(int? memberNumber)
        {
            if (memberNumber == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(memberNumber);

            if (owner == null)
            {
                return NotFound();
            }
            var model = new EditOwnerViewModel
            {
                MemberNumber = owner.MemberNumber,
                UserName = owner.UserName,
                Email = owner.Email,
                Telephone = owner.Telephone,
                FirstName = owner.FirstName,
                LastName = owner.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOwner(int memberNumber, [Bind("MemberNumber,UserName,FirstName, LastName, Email, Telephone")] EditOwnerViewModel owner)
        {
            var foundOwner = _context.Owners.Find(memberNumber);
            if (foundOwner == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                foundOwner.UserName = owner.UserName;
                foundOwner.Email = owner.Email;
                foundOwner.Telephone = owner.Telephone;
                foundOwner.FirstName = owner.FirstName;
                foundOwner.LastName = owner.LastName;

                try
                {

                    _context.Update(foundOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Owners.Any(o => o.MemberNumber == memberNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(OwnerIndex));
            }
            return View(owner);
        }

        public async Task<IActionResult> DeleteOwner(int? id)
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

            return View(owner);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("DeleteOwner")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            var vehicles = _context.Vehicle.Where(v => v.MemberNumber == owner.MemberNumber);

            foreach (var item in vehicles)
            {
                _context.Vehicle.Remove(item);
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VehiclesController.Index));
        }

        public OwnerIndexViewModel ToOwnerIndex(Owner owner)
        {
            return new OwnerIndexViewModel
            {
                MemberNumber = owner.MemberNumber,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                UserName = owner.UserName,
                VehicleCount = _context.Vehicle.Where(v => v.MemberNumber == owner.MemberNumber).Count()
            };
        }
    }
}