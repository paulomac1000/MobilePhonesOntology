﻿using MobilePhonesOntology.Extensions;
using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VDS.RDF;

namespace MobilePhonesOntology.Controllers
{
    public class FindController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(PhoneSimple parameters)
        {
            var model = new FindViewModel
            {
                Brand = parameters.Brand,
                Model = parameters.Model
            };

            IEnumerable<Triple> triples = null;
            if (string.IsNullOrEmpty(model.Brand) && string.IsNullOrEmpty(model.Model))
            {
                return View(model);
            }
            //only brand given
            else if (!string.IsNullOrEmpty(model.Brand) && string.IsNullOrEmpty(model.Model))
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                    t.Object.GetFromNode(NodeName.Brand).Equals(model.Brand, StringComparison.OrdinalIgnoreCase));
            }
            //only model given
            else if (string.IsNullOrEmpty(model.Brand) && !string.IsNullOrEmpty(model.Model))
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                     t.Subject.GetFromNode(NodeName.Model).Contains(model.Model, StringComparison.OrdinalIgnoreCase));
            }
            //both given
            else
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                    t.Subject.GetFromNode(NodeName.Model).Contains(model.Model, StringComparison.OrdinalIgnoreCase) &&
                    t.Object.GetFromNode(NodeName.Brand).Contains(model.Brand, StringComparison.OrdinalIgnoreCase));
            }

            if (!triples.Any())
            {
                ModelState.AddModelError("", $"Unable find {model.Brand} {model.Model}.");
                return View();
            }

            model.Phones = triples.Select(t => new PhoneSimpleWithUri
            {
                Brand = t.Object.GetFromNode(NodeName.Brand),
                Model = t.Subject.GetFromNode(NodeName.Model),
                Uri = t.Subject.ToString()
            });
            model.Succes = true;

            return View(model);
        }
    }
}