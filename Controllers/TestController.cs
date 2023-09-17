using Microsoft.AspNetCore.Mvc;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using SurveyApp.Models;
using System;
using System.Collections.Generic;

namespace SurveyApp.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ITestAnswerRepository _testAnswerRepository;
        private readonly ITestPollsterRepository _testPollsterRepository;
        public TestController(ITestRepository testRepository,
            ITestQuestionRepository testQuestionRepository,
            ITestAnswerRepository testAnswerRepository,
            ITestPollsterRepository testPollsterRepository)
        {
            _testRepository = testRepository;
            _testQuestionRepository = testQuestionRepository;
            _testAnswerRepository = testAnswerRepository;
            _testPollsterRepository = testPollsterRepository;
        }

        public IActionResult Index()
        {
            List<Test> tests = new List<Test>();
            tests = _testRepository.GetirHepsi();
            return View(tests);
        }

        public IActionResult CreateTest()
        {
            AddTestModel model = new AddTestModel();
            model.QuestionQuantity = 0;

            return View(model);
        }
        [HttpPost]
        public IActionResult CreateTest(AddTestModel model)
        {
            if (model.QuestionQuantity > 0)
            {
                return View(model);
            }
            Test test = new Test();

            test.Name = model.TestName;
            test.Duration = model.Duration;
            test.CreatedDateTime = DateTime.Now;
            test.CreatedBy = model.CreatedBy;
            test.Mail = model.Mail;
            test.Description = model.Description;
            test.WillSend = 0;
            test.WillPublished = 0;
            _testRepository.Ekle(test);

            for (int i = 0; i < model.QuestionsText.Count; i++)
            {
                TestQuestion question = new TestQuestion();
                question.TestId = test.TestId;
                question.QuestionText = model.QuestionsText[i];
                question.CorrectAnswer = model.CorrectAnswers[i];
                _testQuestionRepository.Ekle(question);

                List<string> answersTemp = new List<string>();
                for (int j = 0; j < 5; j++)
                {
                    if (model.AnswersText[i][j]==""||model.AnswersText[i][j]==null){}
                    else
                    {
                        answersTemp.Add(model.AnswersText[i][j]);
                    }
                }
                for (int k = 0; k < answersTemp.Count; k++)
                {
                    TestAnswer answer = new TestAnswer();
                    answer.TestQuestionId = question.TestQuestionId;
                    answer.Choice = 0;
                    answer.ChoiceIndex = k;
                    answer.AnswerText = answersTemp[k];
                    _testAnswerRepository.Ekle(answer);
                }
            }
            return RedirectToAction("Index", "Test");
        }
        [HttpGet]
        public IActionResult GetPollsterDetail(int id)
        {
            GetTestPollsterDetailModel getTestPollsterDetail = new GetTestPollsterDetailModel();
            getTestPollsterDetail.TestId = id;
            return View(getTestPollsterDetail);
        }
        [HttpPost]
        public IActionResult GetPollsterDetail(GetTestPollsterDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool isAnyTestPollster = _testPollsterRepository.isAnyTestPollster(model.TestId, model.SchoolId);
            if (isAnyTestPollster)
            {
                if (_testPollsterRepository.GetirIdile(_testPollsterRepository.GetTestPollsterId(model.TestId, model.SchoolId)).Wrong == -1)
                {
                    return RedirectToAction("AttendTest", "Test", new { id = model.TestId, pid = _testPollsterRepository.GetTestPollsterId(model.TestId, model.SchoolId) });
                }
                else
                {
                    return RedirectToAction("AttendedTest", "Home");
                }

            }
            else
            {
                TestPollster testPollster = new TestPollster();
                testPollster.TestId = model.TestId;
                testPollster.Name = model.Name;
                testPollster.SchoolId = model.SchoolId;
                testPollster.Correct = -1;
                testPollster.Wrong = -1;
                _testPollsterRepository.Ekle(testPollster);
                return RedirectToAction("AttendTest", "Test", new { id = model.TestId, pid = testPollster.TestPollsterId });
            }
        }

        [HttpGet]
        public IActionResult AttendTest(int id, int pid)
        {
            AttendTestModel attendTestModel = new AttendTestModel();

            attendTestModel.Pid = pid;

            Test test = new Test();
            List<TestQuestion> questions = new List<TestQuestion>();
            List<List<TestAnswer>> answers = new List<List<TestAnswer>>();
            List<int> selectedValues = new List<int>();

            test = _testRepository.GetirIdile(id);
            questions = _testQuestionRepository.GetirTestIdileHepsi(id);
            for (int i = 0; i < questions.Count; i++)
            {
                selectedValues.Add(-1);

                List<TestAnswer> tempAnswers = new List<TestAnswer>();
                tempAnswers = _testAnswerRepository.GetirQuestionIdileHepsi(questions[i].TestQuestionId);
                answers.Add(tempAnswers);
            }
            attendTestModel.Test = test;
            attendTestModel.Questions = questions;
            attendTestModel.SelectedValues = selectedValues;
            attendTestModel.Answers = answers;
            for (int i = 0; i < attendTestModel.Answers.Count; i++)
            {
                for (int j = 0; j < attendTestModel.Answers[i].Count; j++)
                {
                    Console.WriteLine(attendTestModel.Answers[i][j].AnswerText); 
                }
            }
            return View(attendTestModel);
        }
        [HttpPost]
        public IActionResult AttendTest(AttendTestModel model)
        {
            DateTime openTime = model.Test.CreatedDateTime.AddMinutes(model.Test.Duration);

            if (CompareDate(openTime, DateTime.Now))
            {
                return RedirectToAction("Index", "Test");
            }
            else
            {
                int correct = 0;
                int wrong = 0;
                for (int i = 0; i < model.Questions.Count; i++)
                {
                    TestAnswer answer = new TestAnswer();
                    answer = _testAnswerRepository.GetirAnswer(model.Questions[i].TestQuestionId, model.SelectedValues[i]);
                    answer.Choice++;
                    _testAnswerRepository.Guncelle(answer);


                    if (model.SelectedValues[i] == model.Questions[i].CorrectAnswer)
                    {
                        correct++;
                    }
                    else
                    {
                        wrong++;
                    }
                }

                TestPollster testPollster = new TestPollster();
                testPollster = _testPollsterRepository.GetirIdile(model.Pid);
                testPollster.Correct = correct;
                testPollster.Wrong = wrong;
                _testPollsterRepository.Guncelle(testPollster);

                return RedirectToAction("Index", "Test");

            }
        }

        [HttpGet]
        public IActionResult TestResult(int id)
        {
            GetTestResultModel model = new GetTestResultModel();
            Test test = new Test();
            List<TestQuestion> questions = new List<TestQuestion>();
            List<List<TestAnswer>> answers = new List<List<TestAnswer>>();

            test = _testRepository.GetirIdile(id);
            questions = _testQuestionRepository.GetirTestIdileHepsi(test.TestId);
            for (int i = 0; i < questions.Count; i++)
            {
                List<TestAnswer> tempAnswers = new List<TestAnswer>();
                tempAnswers = _testAnswerRepository.GetirQuestionIdileHepsi(questions[i].TestQuestionId);
                answers.Add(tempAnswers);
            }
            model.Test = test;
            model.Questions = questions;
            model.Answers = answers;

            return View(model);

        }
        bool CompareDate(DateTime date1, DateTime date2)
        {
            if (date1.Year < date2.Year)
            {
                return true;
            }
            else if (date1.Year == date2.Year)
            {
                if (date1.Month < date2.Month)
                {
                    return true;
                }
                else if (date1.Month == date2.Month)
                {
                    if (date1.Day < date2.Day)
                    {
                        return true;
                    }
                    else if (date1.Day == date2.Day)
                    {
                        if (date1.Hour < date2.Hour)
                        {
                            return true;
                        }
                        else if (date1.Hour == date2.Hour)
                        {
                            if (date1.Minute < date2.Minute)
                            {
                                return true;
                            }
                            else if (date1.Minute == date2.Minute)
                            {
                                if (date1.Second < date2.Second)
                                {
                                    return true;
                                }
                                else if (date1.Second == date2.Second)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
