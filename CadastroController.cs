using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoImg.Context;
using ProjetoImg.Models;


namespace CadastroAluno.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppCont _appCont;

        public CadastroController(AppCont appCont)
        {
            _appCont = appCont;
        }
        public IActionResult Index()
        {
            var allTasks = _appCont.Alunos.ToList();
            return View(allTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Aluno = await _appCont.Aluno
                .FirstOrDefaultAsync(x => x.ID == id);

            if (Aluno == null)
            {
                return BadRequest();
            }

            return View(Aluno);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aluno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cad_Aluno Aluno, IFormFile foto)
        {


            if (foto != null && foto.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                foto.CopyTo(memoryStream);
                Aluno.Foto = memoryStream.ToArray(); // Converte imagem para byte[]
            }

            _appCont.Alunos.Add(Aluno); // Adiciona o Aluno ao banco
            _appCont.SaveChanges(); // Salva as alterações no banco
            return RedirectToAction(nameof(Index)); // Redireciona para a listagem


            return View(Aluno); // Retorna a view com erros de validação
        }



        // GET: Aluno/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || !_appCont.Alunos.Any(c => c.ID == id))
            {
                return NotFound();
            }

            var Aluno = _appCont.Alunos.Find(id);
            return View(Aluno);
        }

        // POST: Aluno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cad_Aluno Aluno, IFormFile foto)
        {
            if (id != Aluno.ID)
            {
                return NotFound();
            }


            try
            {
                if (foto != null && foto.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    foto.CopyTo(memoryStream);
                    Aluno.Foto = memoryStream.ToArray();
                }
                else
                {
                    // Mantém a foto existente se nenhuma nova for enviada
                    Aluno.Foto = _appCont.Alunos.AsNoTracking()
                        .FirstOrDefault(c => c.ID == Aluno.ID)?.Foto;
                }

                _appCont.Update(Aluno);
                _appCont.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appCont.Alunos.Any(e => e.ID == Aluno.ID))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));

            return View(Aluno);
        }

        // GET: Aluno/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Aluno = _appCont.Alunos
                .FirstOrDefault(m => m.ID == id);

            if (Aluno == null)
            {
                return NotFound();
            }

            return View(Aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var Aluno = _appCont.Alunos.Find(id);

            if (Aluno == null)
            {
                return NotFound();
            }

            _appCont.Alunos.Remove(Aluno);
            _appCont.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
