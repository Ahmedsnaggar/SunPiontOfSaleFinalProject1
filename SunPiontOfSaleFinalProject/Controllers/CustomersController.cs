using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunPiontOfSaleFinalProject.Entiteis.Models;
using SunPiontOfSaleFinalProject.Repositories.Interfaces;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    public class CustomersController : Controller
    {
        private IBaseRepository<Customer> _customerRepository;

        public CustomersController(IBaseRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            var customers = await _customerRepository.GetAll();
            return View("CusromersList", customers);
        }

        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var customer = await _customerRepository.GetById(id);
            return View("CustomerDetails", customer);
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View("CreateCustomer");
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer item)
        {
            try
            {
                await _customerRepository.AddItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                return View("CreateCustomer");
            }
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetById(id);
            return View("EditCustomer", customer);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer item)
        {
            try
            {
                _customerRepository.UpdateItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditCustomer");
            }
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetById(id);
            return View("DeleteCustomer", customer);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
               await  _customerRepository.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("DeleteCustomer");
            }
        }
    }
}
