using Microsoft.AspNetCore.Mvc;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using SurveyApp.Interfaces.SurveyInterfaces;
using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISurveyPollsterRepository _surveyPollsterRepository;

        public SurveyController(
            ISurveyRepository surveyRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            ICategoryRepository categoryRepository,
            ISurveyPollsterRepository surveyPollsterRepository)
        {
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _categoryRepository = categoryRepository;
            _surveyPollsterRepository = surveyPollsterRepository;
        }
        public IActionResult Index()
        {
            GetSurveysModel model = new GetSurveysModel();
            List<SurveyCategory> categories = new List<SurveyCategory>();
            categories = _categoryRepository.GetirHepsi();
            model.Categories = categories;
            List<bool> selectedCategories = new List<bool>();
            
            for (int i = 0; i < categories.Count; i++)
            {
                selectedCategories.Add(false);
            }
            model.SelectedCategories = selectedCategories;
            model.Surveys = null;
            model.AllSurveys = _surveyRepository.GetirHepsi();

            return View(model);

        }
        [HttpPost]
        public IActionResult Index(GetSurveysModel model)
        {

            List<SurveyCategory> categories = new List<SurveyCategory>();
            categories = _categoryRepository.GetirHepsi();
            model.Categories = categories;
            if (categories.Count != 0)
            {
                List<Survey> surveys = new List<Survey>();
                for (int i = 0; i < model.SelectedCategories.Count; i++)
                {
                    if (model.SelectedCategories[i] == true)
                    {
                        List<Survey> tempSurveys = new List<Survey>();
                        tempSurveys = _surveyRepository.GetirCategoryIdile(categories[i].SurveyCategoryId);
                        for (int j = 0; j < tempSurveys.Count; j++)
                        {
                            surveys.Add(tempSurveys[j]);
                        }
                    }
                }
                model.Surveys = surveys;
            }

            model.AllSurveys = _surveyRepository.GetirHepsi();
            return View(model);
        }

        [HttpGet]
        public IActionResult AttendSurvey(int id)
        {
            AttendSurveyModel attendSurveyModel = new AttendSurveyModel();
            Survey survey = new Survey();
            List<SurveyQuestion> questions = new List<SurveyQuestion>();
            List<List<SurveyAnswer>> answers = new List<List<SurveyAnswer>>();
            SurveyPollster pollster = new SurveyPollster();

            survey = _surveyRepository.GetirIdile(id);
            questions = _questionRepository.GetirSurveyIdileHepsi(id);
            for (int i = 0; i < questions.Count; i++)
            {
                List<SurveyAnswer> nullableAnswers = new List<SurveyAnswer>();
                nullableAnswers = _answerRepository.GetirQuestionIdileHepsi(questions[i].SurveyQuestionId);

                List<SurveyAnswer> tempAnswers = new List<SurveyAnswer>();
                for (int j = 0; j < nullableAnswers.Count; j++)
                {
                    if (nullableAnswers[j].AnswerText != null)
                    {
                        tempAnswers.Add(nullableAnswers[j]);
                    }
                }
                answers.Add(tempAnswers);
            }
            attendSurveyModel.Questions = questions;
            attendSurveyModel.Survey = survey;
            attendSurveyModel.Answers = answers;


            if (getIpAdress() == "")
            {
                return RedirectToAction("CantAttend", "Home");
            }
            else
            {
                if (_surveyPollsterRepository.ControlIp(attendSurveyModel.Survey.SurveyId, getIpAdress()))
                {
                    attendSurveyModel.SurveyPollsterSurveyId = survey.SurveyId;
                    attendSurveyModel.Ip = getIpAdress();
                    return View(attendSurveyModel);
                }
                else
                {
                    return RedirectToAction("Attended", "Home");
                }
            }
        }
        [HttpPost]
        public IActionResult AttendSurvey(AttendSurveyModel model)
        {

            SurveyPollster pollster = new SurveyPollster();

            pollster.SurveyId = model.SurveyPollsterSurveyId;
            pollster.Ip = model.Ip;
            _surveyPollsterRepository.Ekle(pollster);

            for (int i = 0; i < model.Questions.Count; i++)
            {
                SurveyAnswer answer = new SurveyAnswer();
                answer = _answerRepository.GetirAnswer(model.Questions[i].SurveyQuestionId, model.SelectedValues[i]);
                answer.Choice++;

                _answerRepository.Guncelle(answer);
            }
            return RedirectToAction("Index", "Survey");
        }
        [HttpGet]
        public IActionResult SurveyResults(int id)
        {
            GetResultModel model = new GetResultModel();

            Survey survey = new Survey();
            List<SurveyQuestion> questions = new List<SurveyQuestion>();
            List<List<SurveyAnswer>> answers = new List<List<SurveyAnswer>>();

            survey = _surveyRepository.GetirIdile(id);
            model.Survey = survey;

            questions = _questionRepository.GetirSurveyIdileHepsi(id);
            model.Questions = questions;

            foreach (var item in questions)
            {
                List<SurveyAnswer> answersTemp = new List<SurveyAnswer>();
                answersTemp = _answerRepository.GetirQuestionIdileHepsi(item.SurveyQuestionId);
                answers.Add(answersTemp);
            }
            model.Answers = answers;

            return View(model);
        }
        public IActionResult CreateSurvey()
        {
            AddSurveyModel model = new AddSurveyModel();
            model.Categories = _categoryRepository.GetirHepsi();
            model.QuestionQuantity = 0;
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateSurvey(AddSurveyModel model)
        {
            if (model.QuestionQuantity > 0)
            {
                return View(model);
            }
            Survey survey = new Survey();
            survey.Name = model.SurveyName;
            survey.WillPublished = 0;
            survey.CreatedBy = model.CreatedBy;
            model.CreatedDate = DateTime.Now;
            survey.CreatedDate = model.CreatedDate;
            survey.Description = model.Description;
            survey.SurveyCategoryId = model.CategoryId;
            _surveyRepository.Ekle(survey);
            for (int i = 0; i < model.QuestionsText.Count; i++)
            {
                SurveyQuestion question = new SurveyQuestion();
                question.SurveyId = survey.SurveyId;
                question.QuestionText = model.QuestionsText[i];
                _questionRepository.Ekle(question);

                for (int j = 0; j < model.AnswerTexts[i].Count; j++)
                {
                    if (model.AnswerTexts[i][j] != null)
                    {
                        SurveyAnswer answer = new SurveyAnswer();
                        answer.QuestionId = question.SurveyQuestionId;
                        answer.ChoiceIndex = j;
                        answer.Choice = 0;
                        answer.AnswerText = model.AnswerTexts[i][j];
                        _answerRepository.Ekle(answer);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public string getIpAdress()
        {
            IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            List<IPAddress> list = new List<IPAddress>();
            string result = "";
            if (remoteIpAddress != null)
            {
                if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    list = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
            .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToList();
                    int size = list.Count;
                    result = list[size - 1].ToString();
                    return result;
                }
                return result = remoteIpAddress.ToString();
            }
            return result;
        }
    }
}
