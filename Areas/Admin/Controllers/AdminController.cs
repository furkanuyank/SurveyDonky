using Microsoft.AspNetCore.Mvc;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using SurveyApp.Interfaces.SurveyInterfaces;
using SurveyApp.Models;
using System;
using System.Collections.Generic;

namespace SurveyApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AdminController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ITestAnswerRepository _testAnswerRepository;
        private readonly ICategoryRepository _categoryRepository;


        public AdminController(
            ISurveyRepository surveyRepository,
            IQuestionRepository questionRepository,
            ITestRepository testRepository,
            IAnswerRepository answerRepository,
            ITestQuestionRepository testQuestionRepository,
            ITestAnswerRepository testAnswerRepository,
            ICategoryRepository categoryRepository
            )
        {
            _surveyRepository = surveyRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _testQuestionRepository = testQuestionRepository;
            _categoryRepository = categoryRepository;
            _testAnswerRepository = testAnswerRepository;
        }

        public IActionResult SurveyList()
        {
            if (Request.Headers["Referer"].ToString().Contains("Login/AdminLogin") || Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                List<Survey> surveys = new List<Survey>();
                surveys = _surveyRepository.GetirHepsi();

                return View(surveys);

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }


        }
        [HttpGet]
        public IActionResult SurveyDetail(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin/SurveyList"))
            {
                SurveyDetailModel surveyDetailModel = new SurveyDetailModel();
                Survey survey = new Survey();
                List<SurveyQuestion> questions = new List<SurveyQuestion>();
                List<List<SurveyAnswer>> answers = new List<List<SurveyAnswer>>();
                survey = _surveyRepository.GetirIdile(id);
                questions = _questionRepository.GetirSurveyIdileHepsi(id);

                for (int i = 0; i < questions.Count; i++)
                {
                    List<SurveyAnswer> tempAnswers = new List<SurveyAnswer>();
                    tempAnswers = _answerRepository.GetirQuestionIdileHepsi(questions[i].SurveyQuestionId);
                    answers.Add(tempAnswers);
                }
                surveyDetailModel.Survey = survey;
                surveyDetailModel.Questions = questions;
                surveyDetailModel.Answers = answers;

                return View(surveyDetailModel);

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        public IActionResult CategoryList()
        {

            if (Request.Headers["Referer"].ToString().Contains("Login/AdminLogin") || Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                List<SurveyCategory> categories = new List<SurveyCategory>();
                categories = _categoryRepository.GetirHepsi();
                return View(categories);

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        public IActionResult DeleteCategory(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin/CategoryList"))
            {
                SurveyCategory category = new SurveyCategory();
                category = _categoryRepository.GetirIdile(id);
                _categoryRepository.Sil(category);

                return RedirectToAction("CategoryList", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        public IActionResult AddCategory()
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin/CategoryList"))
            {
                SurveyCategory category = new SurveyCategory();
                return View(category);

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        [HttpPost]
        public IActionResult AddCategory(SurveyCategory category)
        {
            _categoryRepository.Ekle(category);
            return RedirectToAction("CategoryList", "Admin", new { area = "Admin" });
        }

        public IActionResult TestList()
        {
            if (Request.Headers["Referer"].ToString().Contains("Login/AdminLogin") || Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                List<Test> tests = new List<Test>();
                tests = _testRepository.GetirHepsi();

                return View(tests);

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        [HttpGet]
        public IActionResult TestDetail(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin/TestList"))
            {
                TestDetailModel testDetailModel = new TestDetailModel();
                Test test = new Test();
                List<TestQuestion> questions = new List<TestQuestion>();
                List<List<TestAnswer>> answers = new List<List<TestAnswer>>();
                test = _testRepository.GetirIdile(id);
                questions = _testQuestionRepository.GetirTestIdileHepsi(id);

                for (int i = 0; i < questions.Count; i++)
                {
                    List<TestAnswer> tempAnswers = new List<TestAnswer>();
                    tempAnswers = _testAnswerRepository.GetirQuestionIdileHepsi(questions[i].TestQuestionId);
                    answers.Add(tempAnswers);
                }
                testDetailModel.Test = test;
                testDetailModel.Questions = questions;
                testDetailModel.Answers = answers;

                return View(testDetailModel);

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }

        public IActionResult SilSurvey(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                Survey survey = new Survey();
                survey = _surveyRepository.GetirIdile(id);
                _surveyRepository.Sil(survey);

                return RedirectToAction("SurveyList", "Admin", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        public IActionResult SilTest(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                Test test = new Test();
                test = _testRepository.GetirIdile(id);
                _testRepository.Sil(test);

                return RedirectToAction("TestList", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }

        public IActionResult YayinlaSurvey(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                Survey survey = new Survey();
                survey = _surveyRepository.GetirIdile(id);
                survey.WillPublished = 1;
                _surveyRepository.Guncelle(survey);

                return RedirectToAction("SurveyList", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        public IActionResult YayinlaTest(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                Test test = new Test();
                test = _testRepository.GetirIdile(id);
                test.WillPublished = 1;
                test.WillSend = 1;
                test.CreatedDateTime = DateTime.Now;
                _testRepository.Guncelle(test);

                return RedirectToAction("TestList", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }

        public IActionResult KaldirSurvey(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                Survey survey = new Survey();
                survey = _surveyRepository.GetirIdile(id);
                survey.WillPublished = 0;
                _surveyRepository.Guncelle(survey);

                return RedirectToAction("SurveyList", "Admin", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
        public IActionResult KaldirTest(int id)
        {
            if (Request.Headers["Referer"].ToString().Contains("Admin/Admin"))
            {
                Test test = new Test();
                test = _testRepository.GetirIdile(id);
                test.WillPublished = 0;
                test.WillSend = 0;
                _testRepository.Guncelle(test);

                return RedirectToAction("TestList", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("CantAccess", "Login", new { area = "" });
            }

        }
    }
}
