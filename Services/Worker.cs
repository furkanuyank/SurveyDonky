using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using SurveyApp.Entities;
using SurveyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITestRepository _testRepository;
        private readonly ITestPollsterRepository _testPollsterRepository;
        public Worker(ITestRepository testRepository, ITestPollsterRepository testPollsterRepository, ILogger<Worker> logger)
        {
            _logger = logger;
            _testRepository = testRepository;
            _testPollsterRepository = testPollsterRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Test> tests = new List<Test>();
                tests = _testRepository.GetirHepsi();

                foreach (var item in tests)
                {
                    if (item.WillPublished == 1)
                    {
                        if (item.WillSend == 1)
                        {

                            DateTime openTime = item.CreatedDateTime.AddMinutes(item.Duration);
                            int seconds = Convert.ToInt32(((openTime - DateTime.Now).TotalSeconds));
                            if (seconds < 1)
                            {
                                Console.WriteLine("mail atıldı");
                                //mail at
                                List<TestPollster> testPollsters = new List<TestPollster>();
                                testPollsters = _testPollsterRepository.GetirTestIdile(item.TestId);
                                foreach (var pollster in testPollsters)
                                {
                                    if (pollster.Correct == -1)
                                    {
                                        _testPollsterRepository.Sil(pollster);
                                    }
                                }
                                testPollsters = _testPollsterRepository.GetirTestIdile(item.TestId);
                                if (testPollsters != null)
                                {
                                    var message = new MimeMessage();
                                    message.From.Add(new MailboxAddress(item.Name, "surveydonky@gmail.com"));
                                    message.To.Add(new MailboxAddress(item.CreatedBy, item.Mail));
                                    message.Subject = item.Name + "adlı testin sonuçları";
                                    TextPart body = new TextPart("html");
                                    body.Text +=
                                        "<table style='border:1px solid black; border-collapse:collapse;'>" +
                                        "<tr>" +
                                        "<th style='border:1px solid black; border-collapse:collapse; padding: 10px;'></th>" +
                                        "<th style='border:1px solid black; border-collapse:collapse; padding:10px;'>Öğrenci No</th>" +
                                        "<th style='border:1px solid black; border-collapse:collapse; padding:10px;'>İsim</th>" +
                                        "<th style='border:1px solid black; border-collapse:collapse; padding:10px;'>Doğru</th>" +
                                        "<th style='border:1px solid black; border-collapse:collapse; padding:10px;'>Yanlış</th>" +
                                        "<th style='border:1px solid black; border-collapse:collapse; padding:10px;'>Puan</th>" +
                                        "</tr>";

                                    double correctOrt = 0;
                                    double wrongOrt = 0;
                                    double puanOrt = 0;

                                    for (int i = 0; i < testPollsters.Count; i++)
                                    {

                                        correctOrt += testPollsters[i].Correct;
                                        wrongOrt += testPollsters[i].Wrong;
                                        double puan = (testPollsters[i].Correct * 100) / (testPollsters[i].Correct + testPollsters[i].Wrong);
                                        puanOrt += puan;

                                        body.Text += "<tr>" +
                                            "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + (i + 1) + "</td>" +
                                            "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + testPollsters[i].SchoolId + "</td>" +
                                            "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + testPollsters[i].Name + "</td>" +
                                            "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + testPollsters[i].Correct + "</td>" +
                                            "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + testPollsters[i].Wrong + "</td>" +
                                            "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + puan + "</td>" +
                                            "</tr>";
                                    }
                                    body.Text += "<tr>" +
                                        "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'></td>" +
                                        "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'></td>" +
                                        "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>Ortalama</td>" +
                                        "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + (correctOrt / testPollsters.Count) + "</td>" +
                                        "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'>" + (wrongOrt / testPollsters.Count) + "</td>" +
                                        "<td style='border:1px solid black; border-collapse:collapse; padding:10px;'> " + (puanOrt / testPollsters.Count) + "</td>" +
                                        "</tr>" +
                                        "</table>";


                                    message.Body = body;
                                    using (var client = new SmtpClient())
                                    {
                                        client.Connect("smtp.gmail.com", 587, false);
                                        client.Authenticate("surveydonky@gmail.com", "surveydonky.17234");

                                        client.Send(message);
                                        client.Disconnect(true);
                                    }

                                    item.WillSend = 0;
                                    _testRepository.Guncelle(item);
                                }
                            }
                        }
                    }
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker Starting");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopping");
            return base.StopAsync(cancellationToken);
        }
    }
}
