using exam2.DAL;
using exam2.Helper;
using exam2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exam2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SectionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SectionController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
           _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<ClientsSection> clientsections = await _context.ClientSections.ToListAsync();
            return View(clientsections);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClientsSection clientsSection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (clientsSection.formFile == null)
            {
                ModelState.AddModelError("formFile", "Sekil daxil edin");
                return View();
            }
            if (!clientsSection.formFile.CheckFileType("image/"))
            {
                ModelState.AddModelError("formFile", "Sekli duzgun daxil edin");
                return View();
            }
            if (clientsSection.formFile.CheckFileLength(200))
            {
                ModelState.AddModelError("formFile", "Sekln olcusu 200 mb cox ola bilmez");
                return View();
            }
            clientsSection.ImageUrl = clientsSection.formFile.CreateFile(_env.WebRootPath,"Upload/Section");
            await _context.ClientSections.AddAsync(clientsSection);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return BadRequest();
            ClientsSection clients =await _context.ClientSections.FirstOrDefaultAsync(c => c.Id == id);
            if (clients == null) return NotFound();
            return View(clients);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, ClientsSection clientsSection)
        {
            if (id == null) return BadRequest();
            ClientsSection clients = await _context.ClientSections.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (clientsSection.formFile == null)
            {
                ModelState.AddModelError("formFile", "Sekil daxil edin");
                if (clientsSection.formFile.CheckFileType("image/"))
                {
                  ModelState.AddModelError("formFile", "Sekli duzgun daxil edin");
                  return View();
                }
                if (clientsSection.formFile.CheckFileLength(200))
                {
                   ModelState.AddModelError("formFile", "Sekln olcusu 200 mb cox ola bilmez");
                    return View();
                 }

                clients.ImageUrl.DeleteFile(_env.WebRootPath, "Upload/Section");
                clients.ImageUrl = clientsSection.formFile.CreateFile(_env.WebRootPath, "Upload/Section");
            }

            clients.Description = clientsSection.Description;
            clients.Title = clientsSection.Title;
            clients.ImageUrl = clientsSection.ImageUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
           
           
        }
        public async Task<IActionResult> Delete(int id)
        {
            ClientsSection clientsSection=await _context.ClientSections.FirstOrDefaultAsync(x=>x.Id == id);
            if (clientsSection == null) return BadRequest();
            clientsSection.ImageUrl.DeleteFile(_env.WebRootPath, "Upload/Section");
            _context.ClientSections.Remove(clientsSection);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
