using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContextFile;
using SunPiontOfSaleFinalProject.Entiteis.Models;
using SunPiontOfSaleFinalProject.Repositories.Interfaces;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    public class ProductsController : Controller
    {
       private IBaseRepository<Product> _productRepository;
       private IBaseRepository<Category> _categoryRepository;
        private IUploadFile _uploadFile;
        public ProductsController(IBaseRepository<Product> productRepository, IBaseRepository<Category> categoryRepository, IUploadFile uploadFile)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _uploadFile = uploadFile;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var products = await _productRepository.GetAll(null, new[] { "category" });
            return View(products);
        }

        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _productRepository.GetById(id));
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            //ViewBag.categories = await _categoryRepository.GetAll();
            var category = await _categoryRepository.GetAll();
            var product = new Product() { categoryList = category.ToList() };
            return View(product);
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product item)
        {
            try
            {
                if(item.ImageFile != null)
                {
                    string FileName = await _uploadFile.UploadFileAsync("\\Images\\ProductsImages\\", item.ImageFile);
                    item.ProductImage = FileName;
                }
                await _productRepository.AddItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetAll();
            Product product = await _productRepository.GetById(id);
            product.categoryList = category.ToList();
            return View("EditProduct", product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product item)
        {
            try
            {
                await _productRepository.UpdateItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditProduct");
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
