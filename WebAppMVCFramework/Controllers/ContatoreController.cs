using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVCFramework.DTO;
using WebAppMVCFramework.Models;

namespace WebAppMVCFramework.Controllers
{
    public class ContatoreController : Controller
    {
        public Contatore contatore = new Contatore();

        [Route("Contatore")]
        public ActionResult Index()
        {
            return View(contatore);
        }


        public ActionResult Increment(int Conteggio)
        {
            //NOTA: per modificare il valore in un textboxfor creato con l' html helper è necessario pulire lo stato del model, questo perchè l' helper include le funzionalità di validazione nel input field generato, le quali mantengono il valore fisso tra chiamate al fine di permettere la visualizzazione di messaggi d' errore
            //ModelState.Clear();

            Conteggio++;
            contatore.Conteggio = Conteggio;
            return View("Index", contatore);
        }

        [HttpPost]
        public ActionResult IncrementPost(Contatore conto)
        {
            conto.Conteggio++;
            contatore.Conteggio = conto.Conteggio;
            return View("Index", contatore);
        }

        [HttpPost]
        public ActionResult IncrementPostAjax(Contatore conto)
        {
            conto.Conteggio++;
            contatore.Conteggio = conto.Conteggio;
            return Json(contatore);
        }
    }
}